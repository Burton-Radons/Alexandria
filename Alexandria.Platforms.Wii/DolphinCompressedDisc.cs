using Glare.Internal;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Alexandria.Platforms.Wii {
	/// <summary>
	/// A compressed form of a <see cref="NintendoOpticalDisc"/> used by the Dolphin emulator.
	/// </summary>
	public class DolphinCompressedDisc {
		/// <summary>Magic number for the disc.</summary>
		public const int Magic = unchecked((int)0xB10BC001);

		readonly DolphinCompressedDiscSystem System;
		public long CompressedDataSize { get; private set; }
		public long UncompressedDataSize { get; private set; }
		public int BlockSize { get; private set; }
		internal readonly Block[] Blocks;
		internal readonly long BlockDataOffset;

		internal readonly BinaryReader Reader;

		internal struct Block {
			public long OffsetCode;

			public bool IsUncompressed { get { return ((ulong)OffsetCode & (1UL << 63)) != 0; } }
			public long Offset { get { return OffsetCode & (long)((~0UL) >> 1); } }
			public uint Adler32HashOfCompressedData;
		}

		internal DolphinCompressedDisc(BinaryReader reader) {
			Reader = reader;
			reader.Require(Magic);
			System = (DolphinCompressedDiscSystem)reader.ReadUInt32();
			CompressedDataSize = reader.ReadInt64();
			UncompressedDataSize = reader.ReadInt64();

			BlockSize = reader.ReadInt32();

			int blockCount = reader.ReadInt32();
			Blocks = new Block[blockCount];
			for (int index = 0; index < blockCount; index++)
				Blocks[index].OffsetCode = reader.ReadInt64();
			for (int index = 0; index < blockCount; index++)
				Blocks[index].Adler32HashOfCompressedData = reader.ReadUInt32();
			BlockDataOffset = reader.BaseStream.Position;
		}
	}

	/// <summary>A <see cref="Stream"/> for reading from a <see cref="DolphinCompressedDisc"/>.</summary>
	public class DolphinCompressedDiscStream : Stream {
		readonly DolphinCompressedDisc Disc;
		byte[] CurrentBlock;
		int CurrentBlockIndex = -1;
		long CurrentPosition;
		readonly int BlockSize;

		/// <summary>Returns <c>true</c>.</summary>
		public override bool CanRead { get { return true; } }

		/// <summary>Returns <c>true</c>.</summary>
		public override bool CanSeek { get { return true; } }

		/// <summary>Returns <c>false</c>.</summary>
		public override bool CanWrite { get { return false; } }

		public override long Length {
			get { return Disc.UncompressedDataSize; }
		}

		public override long Position {
			get { return CurrentPosition; }

			set {
				if (value < 0 || value > Length)
					throw new ArgumentOutOfRangeException("value");
				CurrentPosition = value;
			}
		}

		public DolphinCompressedDiscStream(DolphinCompressedDisc disc) {
			if (disc == null)
				throw new ArgumentNullException("disc");
			Disc = disc;
			BlockSize = disc.BlockSize;
			CurrentBlock = new byte[disc.BlockSize];
		}

		/// <summary>Has no effect.</summary>
		public override void Flush() { }

		public override int Read(byte[] buffer, int offset, int count) {
			int read = 0;

			while (count > 0) {
				int blockIndex = checked((int)(CurrentPosition / BlockSize));

				if (CurrentBlockIndex != blockIndex) {
					lock (Disc.Reader) {
						Stream stream = Disc.Reader.BaseStream;
						var block = Disc.Blocks[blockIndex];

						stream.Position = block.Offset + Disc.BlockDataOffset;
						if(block.IsUncompressed)
							stream.Read(CurrentBlock, 0, BlockSize);
						else
							new InflaterInputStream(stream) { IsStreamOwner = false }.Read(CurrentBlock, 0, BlockSize);

						CurrentBlockIndex = blockIndex;
					}
				}

				int clusterOffset = checked((int)(CurrentPosition - blockIndex * (long)BlockSize)); // Offset within the cluster.
				int toRead = Math.Min(BlockSize - clusterOffset, count);

				CurrentBlock.CopyTo(clusterOffset, toRead, buffer, offset);
				offset += toRead;
				read += toRead;
				CurrentPosition += toRead;
				count -= toRead;
			}

			return read;
		}

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

	public enum DolphinCompressedDiscSystem {
		GameCube = 0,
		Wii = 1,
	}
}
