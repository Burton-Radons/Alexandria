using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare;
using Glare.Internal;

namespace Alexandria.Engines.Sciagi.Resources {
	public class Script : ResourceData {
		public byte[] Data { get; private set; }

		/// <summary>Return a reader at the strings block or <c>null</c> if there is none.</summary>
		public ScriptReader StringsBlock { get { return CreateReader(ScriptBlockType.Strings); } }

		public Script(BinaryReader reader, Resource resource)
			: base(resource) {
				Data = reader.BaseStream.ReadAllBytes();
		}

		public ScriptReader CreateReader() { return new ScriptReader(this); }

		/// <summary>Return a reader at the first block of this type or <c>null</c> if there is none.</summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public ScriptReader CreateReader(ScriptBlockType type) {
			var reader = CreateReader();
			return reader.ToFirstBlock(type) ? reader : null;
		}
	}

	public enum ScriptBlockType : ushort {
		End = 0,
		Object = 1,
		Code = 2,
		Synonyms = 3,
		Said = 4,
		Strings = 5,
		Class = 6,
		Exports = 7,
		Relocations = 8,
		Preload = 9,
		Locals = 10,
	}

	public class ScriptReader {
		public Script Script { get; private set; }

		public BinaryReader Reader { get; private set; }

		public ScriptBlockType BlockType { get; private set; }

		public int BlockSize { get; private set; }

		public long BlockEnd { get; private set; }

		public bool Ended { get { return BlockType == ScriptBlockType.End; } }

		public ScriptReader(Script script)
			: this(script, script.Data) {
		}

		public ScriptReader(Script script, byte[] data, int offset = 0) {
			var stream = new MemoryStream(data, false);
			stream.Position = offset;

			Script = script;
			Reader = new BinaryReader(stream);
			if (script.EngineVersion == EngineVersion.SCI0Early)
				stream.Seek(2, SeekOrigin.Current);
			BlockEnd = stream.Position;
			BlockType = ScriptBlockType.Code;
			NextBlock();
		}

		public ushort ReadUInt16() { return Reader.ReadUInt16(); }

		/// <summary>Move the offset to the next block and read its header.</summary>
		/// <returns></returns>
		public ScriptBlockType NextBlock() {
			if (BlockType == ScriptBlockType.End)
				return BlockType;
			Reader.BaseStream.Position = BlockEnd;
			BlockType = (ScriptBlockType)ReadUInt16();
			if (BlockType == ScriptBlockType.End) {
				BlockSize = 0;
				BlockEnd = Reader.BaseStream.Position;
			} else {
				BlockSize = ReadUInt16();
				if (BlockSize < 4)
					throw new Exception("Invalid block size in script resource.");
				BlockSize -= 4;
				BlockEnd = Reader.BaseStream.Position + BlockSize;
			}

			return BlockType;
		}

		public void Skip(int count = 1) {
			if (count < 0 || Reader.BaseStream.Position + count > BlockEnd)
				throw new ArgumentOutOfRangeException("count");
			Reader.BaseStream.Seek(count, SeekOrigin.Current);
		}

		/// <summary>Fast forward to the first block with the given type, returning true if found or false if not found.</summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool ToFirstBlock(ScriptBlockType type) {
			for (; !Ended; NextBlock())
				if (BlockType == type)
					return true;
			return false;
		}

		/// <summary>Find the exports block, returning its offset (of the first entry, not the counter) and the number of elements in it.</summary>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public bool FindExports(out int offset, out int count) {
			offset = count = 0;
			if (!ToFirstBlock(ScriptBlockType.Exports))
				return false;
			count = ReadUInt16();
			offset = checked((int)Reader.BaseStream.Position);
			return true;
		}
	}
}
