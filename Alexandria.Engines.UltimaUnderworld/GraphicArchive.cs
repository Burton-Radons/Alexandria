using Alexandria.Compression;
using Glare;
using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.UltimaUnderworld {
	public class GraphicArchive : FolderAsset {
		internal GraphicArchive(int paletteIndex, AssetLoader loader, int[] sizes = null)
			: base(loader) {
			State state = State.Get(this);
			PaletteAsset palette = state.GetPalette(paletteIndex);

			using (BinaryReader reader = loader.Reader) {
				byte forceSizeCode = reader.ReadByte();
				int forceSize = 0;

				if (forceSizeCode == 2)
					forceSize = reader.ReadByte();
				else if (forceSizeCode != 1)
					throw new InvalidDataException();

				int count = reader.ReadUInt16();
				int[] offsets = reader.ReadArrayInt32(count + 1);

				for (int index = 0; index < count; index++) {
					int length = offsets[index + 1] - offsets[index];

					reader.BaseStream.Position = offsets[index];

					int width = -1, height = -1;

					if (sizes != null && index * 2 < sizes.Length) {
						width = sizes[index * 2 + 0];
						height = sizes[index * 2 + 1];
					} else if (forceSize != 0)
						width = height = forceSize;

					new Graphic(this, loader, index, length, width, height, palette);
				}
			}
		}
	}

	public class Graphic : IndexedTextureAsset {
		/// <summary>Center position to subtract from the display location to get the position to display at.</summary>
		public Vector2i Center { get; private set; }

		internal Graphic(GraphicArchive archive, AssetLoader loader, int index, int length, int width, int height, PaletteAsset palette, int[] aux = null, int auxOffset = 0)
			: base(archive, loader) {
			BinaryReader reader = loader.Reader;

			Name = index.ToString();
			if (length == 0)
				return;

			if (width >= 0 && height >= 0) {
				int[] indices = reader.ReadBytesAsInt32(width * height);
				Setup(palette, width, height, indices);
			} else {
				byte format = reader.ReadByte();
				width = reader.ReadByte();
				height = reader.ReadByte();
				Load(reader, width, height, palette, format, aux, auxOffset);
			}

			Center = new Vector2i(width / 2 - 4, height - 4);
		}

		void Load(BinaryReader reader, int width, int height, PaletteAsset palette, byte format, int[] aux = null, int auxOffset = 0) {
			int[] indices = new int[width * height];
			ushort dataSize;

			switch (format) {
				case 4: // Uncompressed
					dataSize = reader.ReadUInt16();
					if (width * height != dataSize)
						throw new InvalidDataException();
					reader.ReadBytesAsInt32(indices, 0, width * height);
					break;

				case 6: // 5-bit RLE
					dataSize = reader.ReadUInt16();
					LoadRLE(indices, reader, width, height, 5, aux, auxOffset);
					break;

				case 8: // 4-bit RLE
					if (aux == null)
						throw new NotImplementedException("Need to load auxiliary palette.");
					dataSize = reader.ReadUInt16();
					LoadRLE(indices, reader, width, height, 4, aux, auxOffset);
					break;

				case 10: // 4-bit aux palette indices.
					throw new NotImplementedException("Need to load auxiliary palette.");

				default:
					throw new NotImplementedException("Unknown graphic format " + format + ".");
			}

			Setup(palette, width, height, indices);
		}

		struct RLELoader {
			int BitCount;
			int Bits;

			readonly int CodeSize;
			readonly int CodeMask;
			readonly BinaryReader Reader;
			readonly int[] Aux;
			readonly int AuxOffset;

			public RLELoader(BinaryReader reader, int codeSize, int[] aux, int auxOffset) {
				Reader = reader;
				CodeSize = codeSize;
				CodeMask = (1 << CodeSize) - 1;
				Bits = BitCount = 0;
				Aux = aux;
				AuxOffset = auxOffset;
			}

			public int ReadCode() {
				BitCount -= CodeSize;
				if (BitCount < 0) {
					Bits = (Bits << 8) | Reader.ReadByte();
					BitCount += 8;
				}

				return (Bits >> BitCount) & CodeMask;
			}

			/// <summary>Read a two-piece code.</summary>
			/// <remarks>There is an apparent error in the code by using a shift of 4 instead of <see cref="CodeSize"/>; this is an error in the data.</remarks>
			/// <returns></returns>
			public int ReadCode2() { return (ReadCode() << 4) + ReadCode(); }

			/// <summary>Read a three-piece code.</summary>
			/// <remarks>There is an apparent error in the code by using a shift of 4 instead of <see cref="CodeSize"/>; this is an error in the data.</remarks>
			/// <returns></returns>
			public int ReadCode3() { return (((ReadCode() << 4) + ReadCode()) << 4) + ReadCode(); }

			public int ReadCount() {
				int value = ReadCode();

				if (value == 0) {
					value = ReadCode2();
					if (value == 0)
						value = ReadCode3();
				}

				return value;
			}

			public int ReadAux() { return Aux[AuxOffset + ReadCode()]; }
		}

		void LoadRLE(int[] indices, BinaryReader reader, int width, int height, int codeSize, int[] aux = null, int auxOffset = 0) {
			RLELoader loader = new RLELoader(reader, codeSize, aux, auxOffset);
			bool state = true;

			for (int offset = 0, end = width * height; offset < end; state = !state) {
				int count = loader.ReadCount();

				if (state) {
					if (count == 2) {
						int repeatCount = loader.ReadCount();
						for (int repeatIndex = 0; repeatIndex < repeatCount; repeatIndex++) {
							int runCount = loader.ReadCount();
							int runValue = loader.ReadAux();

							for (int runIndex = 0; runIndex < runCount; runIndex++)
								indices[offset++] = runValue;
						}
					} else if (count > 1) {
						int value = loader.ReadAux();
						for (int index = 0; index < count; index++)
							indices[offset++] = value;
					}
				} else {
					for (int index = 0; index < count; index++)
						indices[offset++] = loader.ReadAux();
				}
			}
		}
	}

	public class GraphicArchiveFormat : AssetFormat {
		internal GraphicArchiveFormat(Engine engine)
			: base(engine, typeof(GraphicArchive), canLoad: true, extension: ".gr") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			BinaryReader reader = loader.Reader;
			long length = loader.Length;

			if (length < 5)
				return LoadMatchStrength.None;

			byte forceSizeCode = reader.ReadByte();
			int forceSize = 0;

			if (forceSizeCode == 2)
				forceSize = reader.ReadByte();
			else if (forceSizeCode != 1)
				return LoadMatchStrength.None;

			int count = reader.ReadUInt16();
			long first = loader.Position + (count + 1) * 4;
			if (length < first || count < 2)
				return LoadMatchStrength.None;

			long last = 0;

			for (int index = 0; index <= count; index++) {
				int offset = reader.ReadInt32();

				if (index == 0) {
					if (offset != first)
						return LoadMatchStrength.None;
				} else if (offset <= last)
					return LoadMatchStrength.None;

				last = offset;
			}

			if (last != length)
				return LoadMatchStrength.None;

			// This is a pretty weak header match, so don't overdo it.
			return LoadMatchStrength.Medium;
		}

		static readonly Dictionary<string, int> Palettes = new Dictionary<string, int>() {
			{ "animo.gr", 0 },
			{ "armorf.gr", 0 },
			{ "armorm.gr", 0 },
			{ "bodies.gr", 0 },
			{ "chains.gr", 0 },
			{ "charhead.gr", 0 },
			{ "chrbtns.gr", 0 },
			{ "compass.gr", 0 },
			{ "converse.gr", 0 },
			{ "doors.gr", 0 },
			{ "dragons.gr", 0 },
			{ "eyes.gr", 0 },
			{ "flasks.gr", 0 },
			{ "genhead.gr", 0 },
			{ "heads.gr", 0 },
			{ "lfti.gr", 0 },
			{ "opbtn.gr", 2 },
			{ "optb.gr", 0 },
			{ "optbtns.gr", 0 },
			{ "panels.gr", 0 },
			{ "weapons.gr", 0 },
		};
		//		Graphic.LoadWeaponOffsets (weaponGraphics, this, "DATA/WEAPONS.DAT");

		public override Asset Load(AssetLoader loader) {
			string name = Path.GetFileName(loader.Name).ToLowerInvariant();
			int palette;

			if (!Palettes.TryGetValue(name, out palette))
				palette = 0;
			return new GraphicArchive(palette, loader);
		}
	}
}
