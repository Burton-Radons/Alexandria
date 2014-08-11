using Alexandria.Plugins.General.Controls;
using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Plugins.General {
	public class LuaModule : Asset {
		internal const string Magic = "\x1BLua";

		const int MinimumVersion = 0x50;
		const int MaximumVersion = 0x50;

		public ByteOrder Endianness { get; private set; }

		public int InstructionSize { get; private set; }

		public int InstructionOpSize { get; private set; }

		public int InstructionASize { get; private set; }

		public int InstructionBSize { get; private set; }

		public int InstructionCSize { get; private set; }

		public int IntSize { get; private set; }

		public LuaFunction Main { get; private set; }

		public int NumberSize { get; private set; }

		public int SizeTSize { get; private set; }

		/// <summary>
		/// Version code where the major version is in the top 4 bits and the minor version is in the bottom 4 bits, such as 0x52 for version 5.2.
		/// </summary>
		public byte VersionCode { get; private set; }

		public int VersionMajor { get { return VersionCode >> 4; } }
		public int VersionMinor { get { return VersionCode & 15; } }
		public string VersionString { get { return VersionMajor + "." + VersionMinor; } }
		
		public LuaModule(AssetManager manager, BinaryReader reader, string name)
			: base(manager, name) {
			using (reader) {
				reader.RequireMagic(Magic);
				VersionCode = reader.ReadByte();
				Endianness = reader.ReadByte() != 0 ? ByteOrder.LittleEndian : ByteOrder.BigEndian;
				IntSize = reader.ReadByte();
				SizeTSize = reader.ReadByte();
				InstructionSize = reader.ReadByte();
				InstructionOpSize = reader.ReadByte();
				InstructionASize = reader.ReadByte();
				InstructionBSize = reader.ReadByte();
				InstructionCSize = reader.ReadByte();
				NumberSize = reader.ReadByte();

				if (IntSize != 4 || SizeTSize != 4 || InstructionSize != 4 || InstructionOpSize != 6 || InstructionASize != 8 || InstructionBSize != 9 || InstructionCSize != 9 || NumberSize != 8)
					throw new InvalidDataException();

				double constant = ReadNumber(reader);
				if (constant != 3.14159265358979323846e7)
					throw new InvalidDataException();

				Main = new LuaFunction(reader, this);
			}
		}

		public override System.Windows.Forms.Control Browse(Action<double> progressUpdateCallback = null) {
			return new LuaModuleBrowser(this);
		}

		int ReadInt(BinaryReader reader, int bytes) {
			switch (bytes) {
				case 4: return reader.ReadInt32(Endianness);
				default: throw new NotImplementedException();
			}
		}

		internal uint ReadInstructionCode(BinaryReader reader) { return (uint)ReadInt(reader, InstructionSize); }
		internal int ReadInt(BinaryReader reader) { return ReadInt(reader, IntSize); }

		internal double ReadNumber(BinaryReader reader) {
			return reader.ReadDouble(Endianness);
		}

		internal int ReadSizeT(BinaryReader reader) { return ReadInt(reader, SizeTSize); }

		internal string ReadString(BinaryReader reader) {
			int size = ReadSizeT(reader);

			if (size == 0)
				return null;
			var value = reader.ReadString(size - 1, Encoding.GetEncoding("shift-jis"));
			reader.RequireZeroes(1);
			return value;
		}
	}


	public enum LuaOpcodeArgument {
		LiteralA,
		RegisterA,

		BooleanB,
		LiteralB,
		RegisterB,
		RegisterConstantB,
		UpValueB,

		LiteralC,
		RegisterC,
		RegisterConstantC,

		BranchSignedBx,
		ConstantBx,
		LiteralBx,
	}


	public class LuaLocalVariable {
		public LuaFunction Function { get; private set; }

		public LuaModule Module { get { return Function.Module; } }

		public string Name { get; private set; }

		public int ProgramCounterStart { get; private set; }

		public int ProgramCounterEnd { get; private set; }

		public LuaLocalVariable(BinaryReader reader, LuaFunction function) {
			Function = function;
			Name = Module.ReadString(reader);
			ProgramCounterStart = Module.ReadInt(reader);
			ProgramCounterEnd = Module.ReadInt(reader);
		}
	}

	public enum LuaType : byte {
		None = 255,
		Nil = 0,
		Boolean = 1,
		LightUserData = 2,
		Number = 3,
		String = 4,
		Table = 5,
		Function = 6,
		UserData = 7,
		Thread = 8,
	}

	class LuaFormat : AssetFormat {
		public LuaFormat(Plugin plugin) : base(plugin, typeof(LuaModule), canLoad: true) { }

		public override LoadMatchStrength LoadMatch(AssetLoader context) {
			if (!context.Reader.MatchMagic(LuaModule.Magic))
				return LoadMatchStrength.None;
			return LoadMatchStrength.Medium;
		}

		public override Asset Load(AssetLoader context) {
			return new LuaModule(Manager, context.Reader, context.Name);
		}
	}
}
