using Glare;
using Glare.Assets;
using Glare.Assets.Controls;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Sciagi.Resources {
	/// <summary>
	/// A 256-colour drawing that is part of a <see cref="Picture"/>.
	/// </summary>
	public class PictureCel : IndexedTextureAsset {
		/// <summary>Get the displacement? of the cel.</summary>
		public Vector2i Displacement { get; private set; }

		/// <summary>
		/// Get the zero-based index of this frame in the picture.
		/// </summary>
		public int FrameIndex { get; private set; }

		/// <summary>Get the mask index.</summary>
		public int MaskIndex { get; private set; }

		/// <summary>Get the location in the frame to draw this cel at.</summary>
		public Vector2i Offset { get; private set; }

		/// <summary>Get the <see cref="Picture"/> this is part of.</summary>
		public new Picture Parent { get { return (Picture)base.Parent; } }

		/// <summary>Get the priority to draw the cel at.</summary>
		public int Priority { get; private set; }

		internal PictureCel(Picture picture, int celIndex, AssetLoader loader)
			: base(loader) {
			Name = "Cel " + celIndex;
			BinaryReader reader = loader.Reader;

			reader.BaseStream.Position = 0x0E + celIndex * 0x2A;
			Vector2i dimensions = new Vector2i(reader.ReadUInt16(), reader.ReadUInt16());
			Displacement = new Vector2i(reader.ReadUInt16(), reader.ReadUInt16());
			MaskIndex = reader.ReadByte();
			loader.ExpectZeroes(1, 3);
			Unknowns.ReadInt32s(reader, 2, "Data offsets?");
			loader.ExpectZeroes(4, 1);
			int dataOffset = reader.ReadInt32();
			loader.ExpectZeroes(4, 2);
			Priority = reader.ReadUInt16();
			Offset = new Vector2i(reader.ReadUInt16(), reader.ReadUInt16()) * 2;
			if (Offset.Y > 0)
				Offset = new Vector2i(Offset.X, Offset.Y * 6 / 5);

			reader.BaseStream.Position = dataOffset;
			int[] indices = reader.ReadBytesAsInt32(dimensions.Product);

			Setup(picture.Palette.PaletteAsset, dimensions.X, dimensions.Y, indices);
		}
	}
}
