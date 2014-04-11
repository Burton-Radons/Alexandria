using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Plugins.General {
	public class LuaFunction {
		public ReadOnlyList<LuaInstruction> Instructions { get; private set; }

		public ReadOnlyList<object> Constants { get; private set; }

		public ReadOnlyList<LuaFunction> Closures { get; private set; }

		public bool HasVariableArguments { get; private set; }

		public int Index { get; private set; }

		public int LineDefined { get; private set; }

		public ReadOnlyList<int> LineInfo { get; private set; }

		public ReadOnlyList<LuaLocalVariable> LocalVariables { get; private set; }

		public int MaxStackSize { get; private set; }

		public LuaModule Module { get; private set; }

		public int UpValueCount { get; private set; }

		public int ParameterCount { get; private set; }

		public LuaFunction Parent { get; private set; }

		public string Source { get; private set; }

		public string SourceOrIndex { get { return Source ?? ("#" + Index); } }

		public ReadOnlyList<string> UpValues { get; private set; }

		public LuaFunction(BinaryReader reader, LuaModule module) {
			Module = module;

			Source = module.ReadString(reader);
			LineDefined = module.ReadInt(reader);
			UpValueCount = reader.ReadByte();
			ParameterCount = reader.ReadByte();
			HasVariableArguments = reader.ReadByte() != 0;
			MaxStackSize = reader.ReadByte();

			var lineInfoCount = module.ReadInt(reader);
			LineInfo = new ArrayBackedList<int>(reader.ReadArrayInt32(lineInfoCount));

			var localVariableCount = module.ReadInt(reader);
			LuaLocalVariable[] localVariables = new LuaLocalVariable[localVariableCount];
			for (int index = 0; index < localVariableCount; index++)
				localVariables[index] = new LuaLocalVariable(reader, this);
			LocalVariables = new ArrayBackedList<LuaLocalVariable>(localVariables);

			var upValueCount = module.ReadInt(reader);
			string[] upValues = new string[upValueCount];
			for (int index = 0; index < upValueCount; index++)
				upValues[index] = module.ReadString(reader);
			UpValues = new ArrayBackedList<string>(upValues);

			var constantCount = module.ReadInt(reader);
			object[] constants = new object[constantCount];
			for (int index = 0; index < constantCount; index++) {
				LuaType type = (LuaType)reader.ReadByte();

				switch (type) {
					case LuaType.Nil: break;
					case LuaType.Boolean: constants[index] = reader.ReadByte() != 0; break;
					case LuaType.Number: constants[index] = module.ReadNumber(reader); break;
					case LuaType.String: constants[index] = module.ReadString(reader); break;
					default: throw new NotSupportedException();
				}
			}
			Constants = new ArrayBackedList<object>(constants);

			var functionCount = module.ReadInt(reader);
			LuaFunction[] functions = new LuaFunction[functionCount];
			for (int index = 0; index < functionCount; index++)
				functions[index] = new LuaFunction(reader, index, this);
			Closures = new ArrayBackedList<LuaFunction>(functions);

			var codeCount = module.ReadInt(reader);
			LuaInstruction[] code = new LuaInstruction[codeCount];
			for (int index = 0; index < codeCount; index++)
				code[index] = new LuaInstruction(this, module.ReadInstructionCode(reader));
			Instructions = new ArrayBackedList<LuaInstruction>(code);
		}

		public LuaFunction(BinaryReader reader, int index, LuaFunction parent)
			: this(reader, parent.Module) {
			Index = index;
			Parent = parent;
		}
	}

}
