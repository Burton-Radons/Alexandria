using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Alexandria.Platforms.Wii {
	/// <summary>
	/// A Nintendo Optical Disc format, which is a collection of <see cref="NintendoOpticalDiscVolume"/>s.
	/// </summary>
	public class NintendoOpticalDisc : ArchiveAsset {
		/// <summary>Magic number for a Wii game.</summary>
		public const int WiiMagic = 0x5D1C9EA3;

		/// <summary>Offset of the volume table for a Wii disc.</summary>
		public const int VolumeTableOffset = 0x40000;

		/// <summary>Whether audio streaming is used?</summary>
		public byte DiscAudioStreaming { get; private set; }

		/// <summary>The disc number.</summary>
		public byte DiscNumber { get; private set; }

		/// <summary>Get the disc version.</summary>
		public byte DiscVersion { get; private set; }

		/// <summary>Get the game this is for.</summary>
		public NintendoOpticalDiscGame DiscGame { get; private set; }

		/// <summary>Get the system identifier.</summary>
		public NintendoOpticalDiscId DiscId { get; private set; }

		/// <summary>Get the maker identifier.</summary>
		public NintendoOpticalDiscMaker DiscMaker { get; private set; }

		/// <summary>Get the region identifier.</summary>
		public NintendoOpticalDiscRegion DiscRegion { get; private set; }

		/// <summary>Get the stream buffer size.</summary>
		public byte DiscStreamBufferSize { get; private set; }

		/// <summary>Get the game system.</summary>
		public NintendoOpticalDiscSystem DiscSystem { get; private set; }

		/// <summary>Get the disc's title.</summary>
		public string DiscTitle { get; private set; }

		internal BinaryReader Reader { get; private set; }

		internal NintendoOpticalDisc(AssetLoader loader)
			: base(loader) {
			BinaryReader reader;

			int magic = loader.Reader.ReadInt32();
			loader.Reader.BaseStream.Seek(-4, SeekOrigin.Current);
			if (magic == DolphinCompressedDisc.Magic) {
				var disc = new DolphinCompressedDisc(loader.Reader);
				var stream = new DolphinCompressedDiscStream(disc);
				loader.Reader = new BigEndianBinaryReader(stream);
			} else
				loader.MakeBigEndian();

			reader = Reader = loader.Reader;

			DiscId = (NintendoOpticalDiscId)reader.ReadByte();
			DiscGame = (NintendoOpticalDiscGame)reader.ReadInt16();
			DiscRegion = (NintendoOpticalDiscRegion)reader.ReadByte();
			DiscMaker = (NintendoOpticalDiscMaker)reader.ReadInt16();
			DiscNumber = reader.ReadByte();
			DiscVersion = reader.ReadByte();
			DiscAudioStreaming = reader.ReadByte();
			DiscStreamBufferSize = reader.ReadByte();
			Unknowns.ReadBytes(reader, 14);
			int wiiMagic = reader.ReadInt32();
			int gameCubeMagic = reader.ReadInt32();

			if (wiiMagic == (int)NintendoOpticalDiscSystem.Wii)
				DiscSystem = NintendoOpticalDiscSystem.Wii;
			else if (gameCubeMagic == (int)NintendoOpticalDiscSystem.GameCube)
				DiscSystem = Wii.NintendoOpticalDiscSystem.GameCube;
			else
				throw new InvalidDataException();

			DiscTitle = reader.ReadStringz(64, Encoding.ASCII);

			if (DiscSystem == Wii.NintendoOpticalDiscSystem.GameCube) {
				NintendoOpticalDiscPartition.LoadFileTable(this, reader, DiscSystem);
			} else {
				reader.BaseStream.Position = VolumeTableOffset;
				for (int volumeIndex = 0; volumeIndex < 4; volumeIndex++) {
					int partitionCount = reader.ReadInt32();
					long partitionTableOffset = reader.ReadUInt32() * 4L;

					if (partitionCount > 0)
						new NintendoOpticalDiscVolume(this, volumeIndex, partitionCount, partitionTableOffset);
				}

				foreach (NintendoOpticalDiscVolume volume in Children)
					volume.LoadPartitions(loader);
				foreach (NintendoOpticalDiscVolume volume in Children)
					foreach (NintendoOpticalDiscPartition partition in volume.Children)
						partition.LoadHeaders(loader);
			}
		}
	}

	/// <summary>Identifies the system the disc is for.</summary>
	public enum NintendoOpticalDiscId {
		/// <summary>The Nintendo GameCube.</summary>
		GameCube = 'G',

		/// <summary>The Nintendo Wii.</summary>
		Wii = 'R',
	}

	public enum NintendoOpticalDiscGame {
		/// <summary>Metroid Prime Trilogy ("3M").</summary>
		MetroidPrimeTrilogy = ('3' << 8) + 'M',

		/// <summary>Tales of Symphonia: Dawn of the New World ("T4").</summary>
		TalesOfSymphoniaDawnOfTheNewWorld = ('T' << 8) + '4',

		/// <summary>The Legend of Zelda: The Wind Waker ("ZL").</summary>
		TheLegendOfZeldaTheWindWaker = ('Z' << 8) + 'L',
	}

	/// <summary>Specifies the region for a <see cref="NintendoOpticalDisc"/>.</summary>
	public enum NintendoOpticalDiscRegion {
		/// <summary>Region-free.</summary>
		RegionFree = 'A',

		/// <summary>Germany.</summary>
		Germany = 'D',

		/// <summary>NTSC in general.</summary>
		NTSC = 'E',

		/// <summary>France.</summary>
		France = 'F',

		/// <summary>Italy.</summary>
		Italy = 'I',

		/// <summary>Japan.</summary>
		Japan = 'J',

		/// <summary>Korea.</summary>
		Korea = 'K',

		/// <summary>Japanese import to PAL regions.</summary>
		PalImport1 = 'L',

		/// <summary>Japanese import to PAL regions.</summary>
		PalImport2 = 'M',

		/// <summary>Japanese import to USA and other NTSC regions.</summary>
		JapanNTSCImport = 'N',

		/// <summary>Used in the SDK.</summary>
		SDK = 'O',

		/// <summary>General PAL.</summary>
		PAL = 'P',

		/// <summary>Korea, with Japanese language.</summary>
		KoreaJapaneseLanguage = 'Q',

		/// <summary>Russia (PAL).</summary>
		Russia = 'R',

		/// <summary>Spanish speaking regions (all PAL?).</summary>
		SpanishLanguage = 'S',

		/// <summary>Korea, with English language.</summary>
		KoreaEnglishLanguage = 'T',

		/// <summary>Australia (PAL?).</summary>
		Australia = 'U',

		/// <summary>Taiwan (NTSC).</summary>
		Taiwan = 'W',

		/// <summary>Used for a few PAL games.</summary>
		PAL2 = 'X',

		/// <summary>Used for German and French PAL games.</summary>
		GermanyOrFrance = 'Y',

		/// <summary>Used for Prince of Persia: The Forgotten Sands (NTSC, Wii).</summary>
		PrinceOfPersiaForgottenSands = 'Z',
	}

	/// <summary>Manufacturer Id for a <see cref="NintendoOpticalDisc"/>.</summary>
	public enum NintendoOpticalDiscMaker {
		/// <summary>Nintendo ("01").</summary>
		Nintendo = ('0' << 8) + '1',

		/// <summary>Either Namco Bandai or Namco Tales Studio; unknown ("AF").</summary>
		NamcoBandai = ('A' << 8) + 'F',
	}

	/// <summary>The system contained within a <see cref="NintendoOpticalDisc"/>.</summary>
	public enum NintendoOpticalDiscSystem {
		/// <summary>GameCube.</summary>
		GameCube = unchecked((int)0xC2339F3D),

		/// <summary>Wii.</summary>
		Wii = 0x5D1C9EA3,
	}

	/// <summary>A volume in a <see cref="NintendoOpticalDisc"/>.</summary>
	public class NintendoOpticalDiscVolume : FolderAsset {
		readonly int PartitionCount;
		readonly long PartitionTableOffset;

		/// <summary>Get the zero-based index of the volume.</summary>
		public int Index { get; private set; }

		/// <summary>Get the containing <see cref="NintendoOpticalDisc"/>.</summary>
		public new NintendoOpticalDisc Parent { get { return (NintendoOpticalDisc)base.Parent; } }

		internal NintendoOpticalDiscVolume(NintendoOpticalDisc disc, int index, int partitionCount, long partitionTableOffset)
			: base(disc, "Volume " + (index + 1)) {
			PartitionCount = partitionCount;
			PartitionTableOffset = partitionTableOffset;
		}

		internal void LoadPartitions(AssetLoader loader) {
			BinaryReader reader = loader.Reader;

			loader.Position = PartitionTableOffset;
			for (int partitionIndex = 0; partitionIndex < PartitionCount; partitionIndex++)
				new NintendoOpticalDiscPartition(this, loader, partitionIndex);
		}
	}

	/// <summary>
	/// A partition within a <see cref="NintendoOpticalDiscVolume"/>, which is in turn within a <see cref="Disc"/>.
	/// </summary>
	public class NintendoOpticalDiscPartition : FolderAsset {
		readonly Aes Cypher = Aes.Create();
		internal long DataLength { get; private set; }
		long DataOffset;
		readonly long HeaderOffset;

		/// <summary>Get the containing <see cref="NintendoOpticalDisc"/>.</summary>
		public NintendoOpticalDisc Disc { get { return (NintendoOpticalDisc)Parent.Parent; } }

		/// <summary>Zero-based index of this partition in the volume.</summary>
		public int Index { get; private set; }

		/// <summary>Get the containing <see cref="NintendoOpticalDiscVolume"/>.</summary>
		public new NintendoOpticalDiscVolume Parent { get { return (NintendoOpticalDiscVolume)base.Parent; } }

		/// <summary>Get the type of this partition.</summary>
		public NintendoOpticalDiscPartitionType Type { get; private set; }

		internal NintendoOpticalDiscPartition(NintendoOpticalDiscVolume volume, AssetLoader loader, int index)
			: base(volume, volume.Name + ", Partition " + (index + 1)) {
			BinaryReader reader = loader.Reader;

			Cypher.Padding = PaddingMode.None;
			Cypher.Mode = CipherMode.CBC;

			Index = index;
			HeaderOffset = reader.ReadUInt32() * 4L;
			Type = (NintendoOpticalDiscPartitionType)reader.ReadInt32();
		}

		static readonly byte[] MasterKey = new byte[] { 0xEB, 0xE4, 0x2A, 0x22, 0x5E, 0x85, 0x93, 0xE4, 0x48, 0xD9, 0xC5, 0x45, 0x73, 0x81, 0xAA, 0xF7 };

		internal static Asset LoadFile(FolderAsset parent, BinaryReader reader, ref int index, NintendoOpticalDiscSystem system) {
			int nameOffset = reader.ReadInt32();
			bool isDirectory = (nameOffset & 0xFF000000) != 0;
			nameOffset &= 0xFFFFFF;

			if (isDirectory)
				return new NintendoOpticalDiscPartitionFolder(parent, ref index, nameOffset, reader, system);
			else
				return new NintendoOpticalDiscPartitionFile(parent, index++, nameOffset, reader, system);
		}

		internal static void LoadFileName(Asset file, BinaryReader reader, long baseNameOffset) {
			if (file is NintendoOpticalDiscPartitionFolder)
				((NintendoOpticalDiscPartitionFolder)file).ReadName(reader, baseNameOffset);
			else
				((NintendoOpticalDiscPartitionFile)file).ReadName(reader, baseNameOffset);
		}

		internal static void LoadFileTable(FolderAsset parent, BinaryReader reader, NintendoOpticalDiscSystem system) {
			reader.BaseStream.Position = 0x424;
			long fileTableOffset = LoadOffset(reader, system);

			reader.BaseStream.Position = fileTableOffset + 8;
			int count = reader.ReadInt32();
			for (int index = 1; index < count; )
				LoadFile(parent, reader, ref index, system);

			foreach (Asset asset in parent.Children)
				LoadFileName(asset, reader, fileTableOffset + count * 12);
		}

		internal void LoadHeaders(AssetLoader loader) {
			BinaryReader reader = loader.Reader;

			reader.BaseStream.Position = HeaderOffset;
			Unknowns.ReadBytes(reader, 447);
			byte[] encryptedTitleKey = reader.ReadBytes(16);
			Unknowns.ReadBytes(reader, 13);
			byte[] titleId = reader.ReadBytes(16);
			Unknowns.ReadBytes(reader, 204);
			DataOffset = reader.ReadUInt32() * 4L + HeaderOffset;
			DataLength = reader.ReadUInt32() * 4L;

			byte[] keyIv = new byte[16];
			titleId.CopyTo(0, 8, keyIv, 0);

			ICryptoTransform keyDecryptor = Cypher.CreateDecryptor(MasterKey, keyIv);
			Key = keyDecryptor.TransformFinalBlock(encryptedTitleKey, 0, encryptedTitleKey.Length);

			LoadFileTable(this, OpenReader(), NintendoOpticalDiscSystem.Wii);
			SortChildrenRecursively();
		}

		internal static long LoadOffset(BinaryReader reader, NintendoOpticalDiscSystem system) {
			switch (system) {
				case NintendoOpticalDiscSystem.Wii: return reader.ReadUInt32() * 4L;
				case NintendoOpticalDiscSystem.GameCube: return reader.ReadUInt32();
				default: throw new NotImplementedException();
			}
		}

		/// <summary>Total size in bytes of a cluster, including the IV and the hash. The actual data size is <see cref="ClusterDataSize"/>.</summary>
		public const int ClusterSize = 0x8000;

		/// <summary>Size in bytes of the data of a cluster, excluding the IV and the hash. The total cluster size is <see cref="ClusterSize"/>.</summary>
		public const int ClusterDataSize = 0x7C00;

		/// <summary>Size in bytes of a cluster's header.</summary>
		public const int ClusterHeaderSize = ClusterSize - ClusterDataSize;

		/// <summary>Offset of the cluster's data within its header.</summary>
		public const int ClusterDataOffset = 0x400;

		/// <summary>Offset of a cluster's IV within its header.</summary>
		public const int ClusterIVOffset = 0x3D0;

		readonly byte[] KeyIV = new byte[16], ClusterData = new byte[ClusterDataSize];
		byte[] Key;

		/// <summary>Open the disc partition as a stream.</summary>
		/// <returns></returns>
		public DiscPartitionStream Open() { return new DiscPartitionStream(this); }

		/// <summary>Get a big-endian reader for the stream.</summary>
		/// <returns></returns>
		public BigEndianBinaryReader OpenReader() { return new BigEndianBinaryReader(Open()); }

		internal byte[] ReadCluster(int index) {
			long offset = DataOffset + index * (long)ClusterSize;
			BinaryReader reader = Disc.Reader;
			int read;

			lock (reader) {
				reader.BaseStream.Position = offset + ClusterIVOffset;
				reader.Read(KeyIV, 0, KeyIV.Length);

				reader.BaseStream.Position = offset + ClusterDataOffset;
				read = reader.Read(ClusterData, 0, ClusterDataSize);
			}

			ICryptoTransform decryptor = Cypher.CreateDecryptor(Key, KeyIV);
			return decryptor.TransformFinalBlock(ClusterData, 0, read);
		}
	}

	/// <summary>A stream for decoding a <see cref="NintendoOpticalDiscPartition"/>.</summary>
	public class DiscPartitionStream : Stream {
		byte[] ClusterData;
		int ClusterIndex = -1;

		readonly NintendoOpticalDiscPartition Partition;
		long PositionField;

		/// <summary>Returns <c>true</c>.</summary>
		public override bool CanRead { get { return true; } }

		/// <summary>Returns <c>true</c>.</summary>
		public override bool CanSeek { get { return true; } }

		/// <summary>Returns <c>false</c>.</summary>
		public override bool CanWrite { get { return false; } }

		/// <summary>Get the length in bytes of the stream.</summary>
		public override long Length {
			get { return Partition.DataLength - (Partition.DataLength + NintendoOpticalDiscPartition.ClusterSize - 1) / NintendoOpticalDiscPartition.ClusterSize * NintendoOpticalDiscPartition.ClusterHeaderSize; }
		}

		/// <summary>Get or set the current position.</summary>
		public override long Position {
			get { return PositionField; }

			set {
				if (value < 0 || value > Length)
					throw new ArgumentOutOfRangeException("value");
				PositionField = value;
			}
		}

		/// <summary>Create a stream.</summary>
		/// <param name="partition"></param>
		public DiscPartitionStream(NintendoOpticalDiscPartition partition) {
			if (partition == null)
				throw new ArgumentNullException("partition");
			Partition = partition;
		}

		/// <summary>Has no effect.</summary>
		public override void Flush() { }

		/// <summary>Read data from the partition.</summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public override int Read(byte[] buffer, int offset, int count) {
			int read = 0;

			while (count > 0) {
				int positionCluster = checked((int)(PositionField / NintendoOpticalDiscPartition.ClusterDataSize));

				if (ClusterIndex != positionCluster) {
					ClusterData = Partition.ReadCluster(positionCluster);
					ClusterIndex = positionCluster;
				}

				int clusterOffset = checked((int)(PositionField - ClusterIndex * (long)NintendoOpticalDiscPartition.ClusterDataSize)); // Offset within the cluster.
				int toRead = Math.Min(ClusterData.Length - clusterOffset, count);

				ClusterData.CopyTo(clusterOffset, toRead, buffer, offset);
				offset += toRead;
				read += toRead;
				PositionField += toRead;
				count -= toRead;
			}

			return read;
		}

		/// <summary>Seek to a position within the partition.</summary>
		/// <param name="offset"></param>
		/// <param name="origin"></param>
		/// <returns></returns>
		public override long Seek(long offset, SeekOrigin origin) {
			switch (origin) {
				case SeekOrigin.Begin: Position = offset; break;
				case SeekOrigin.Current: Position += offset; break;
				case SeekOrigin.End: Position = Length + offset; break;
			}
			return Position;
		}

		/// <summary>Throws an exception.</summary>
		/// <param name="value"></param>
		public override void SetLength(long value) {
			throw new NotImplementedException();
		}

		/// <summary>Throws an exception.</summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		public override void Write(byte[] buffer, int offset, int count) {
			throw new NotImplementedException();
		}
	}

	/// <summary>A folder in a <see cref="NintendoOpticalDisc"/>.</summary>
	public class NintendoOpticalDiscPartitionFolder : FolderAsset {
		readonly int NameOffset;

		/// <summary>Get the zero-based index of the folder in the master list.</summary>
		public int Index { get; private set; }

		internal NintendoOpticalDiscPartitionFolder(FolderAsset parent, ref int index, int nameOffset, BinaryReader reader, NintendoOpticalDiscSystem system)
			: base(parent, "") {
			Index = index;
			NameOffset = nameOffset;
			int firstIndex = reader.ReadInt32();
			/*if (firstIndex != index + 1)
				throw new InvalidDataException();*/
			int endIndex = reader.ReadInt32();
			index++;
			while (index < endIndex)
				NintendoOpticalDiscPartition.LoadFile(this, reader, ref index, system);
		}

		internal void ReadName(BinaryReader reader, long baseNameOffset) {
			Name = reader.ReadStringzAt(baseNameOffset + NameOffset, Encoding.ASCII);

			foreach (Asset child in Children)
				NintendoOpticalDiscPartition.LoadFileName(child, reader, baseNameOffset);
		}
	}

	/// <summary>A file within a <see cref="NintendoOpticalDisc"/>.</summary>
	public class NintendoOpticalDiscPartitionFile : DataAsset {
		readonly long DataOffset;
		readonly int NameOffset;

		/// <summary>Get the zero-based index of the file within the master list.</summary>
		public int Index { get; private set; }

		/// <summary>Get the size in bytes of the file.</summary>
		public uint Size { get; private set; }

		internal NintendoOpticalDiscPartitionFile(FolderAsset parent, int index, int nameOffset, BinaryReader reader, NintendoOpticalDiscSystem system)
			: base(parent, "") {
			Index = index;
			NameOffset = nameOffset;
			DataOffset = NintendoOpticalDiscPartition.LoadOffset(reader, system);
			Size = reader.ReadUInt32();
		}

		/// <summary>Open a stream for th efile.</summary>
		/// <returns></returns>
		public override Stream Open() {
			return new SliceStream(OpenBase(), DataOffset, Size, false);
		}

		Stream OpenBase() {
			for (Asset asset = Parent; ; asset = asset.Parent) {
				if (asset is NintendoOpticalDiscPartition)
					return ((NintendoOpticalDiscPartition)asset).Open();
				if (asset is NintendoOpticalDisc)
					return ((NintendoOpticalDisc)asset).Reader.BaseStream;
			}
		}

		internal void ReadName(BinaryReader reader, long baseNameOffset) {
			Name = reader.ReadStringzAt(baseNameOffset + NameOffset, Encoding.ASCII);
		}
	}

	/// <summary>The type of a <see cref="NintendoOpticalDiscPartition"/>.</summary>
	public enum NintendoOpticalDiscPartitionType {
		/// <summary>The partition contains game data.</summary>
		GameData = 0,

		/// <summary>The partition contains system data.</summary>
		SystemUpgrade = 1,
	}

	/// <summary>Handles reading a <see cref="NintendoOpticalDisc"/>.</summary>
	public class NintendoOpticalDiscFormat : AssetFormat {
		internal NintendoOpticalDiscFormat(Platform platform)
			: base(platform, typeof(NintendoOpticalDisc), canLoad: true, extensions: new string[] { ".iso" }) {
		}

		/// <summary>Attempt to match the format.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			BinaryReader reader = loader.Reader;

			int magic = reader.ReadInt32();
			if (magic == DolphinCompressedDisc.Magic)
				return LoadMatchStrength.Strong;
			reader.BaseStream.Seek(-4, SeekOrigin.Current);

			for (int codeIndex = 0; codeIndex < 6; codeIndex++) {
				byte code = reader.ReadByte();
				if (code < 0x30 || code > 0x7A)
					return LoadMatchStrength.None;
			}

			reader.BaseStream.Seek(4, SeekOrigin.Current); // DiscNumber, DiscVersion, AudioStreaming, StreamBufferSize
			for (int i = 0; i < 14; i++)
				if (reader.ReadByte() != 0)
					return LoadMatchStrength.None;

			int wiiMagic = reader.ReadInt32BE();
			int gameCubeMagic = reader.ReadInt32BE();
			if (wiiMagic != (int)NintendoOpticalDiscSystem.Wii && gameCubeMagic != (int)NintendoOpticalDiscSystem.GameCube)
				return LoadMatchStrength.None;

			for (int i = 0; i < 64; i++) {
				byte value = reader.ReadByte();
				if (value != 0 && (value < 0x20 || value > 0x7A))
					return LoadMatchStrength.None;
			}

			return LoadMatchStrength.Medium;
		}

		/// <summary>Load a disc.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override Asset Load(AssetLoader loader) {
			return new NintendoOpticalDisc(loader);
		}
	}
}
