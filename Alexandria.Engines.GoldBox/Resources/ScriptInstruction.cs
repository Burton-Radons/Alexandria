using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.GoldBox.Resources {
	/// <summary>Characters used in <see cref="ScriptInstruction"/> argument lists. Some are simple values, some are compound.</summary>
	public enum ScriptArgument : byte {
		/// <summary>Invalid value/unspecified.</summary>
		None = 0,

		/// <summary>An address value, stored as <see cref="ScriptOperandType.Short"/>. The value is <c>0x7FFE</c> (<see cref="Script.AddressOffset"/> too high. For example, the address <c>0x8014</c> is the first code byte of a script file. This can be confused with a <see cref="Variable"/> or a <see cref="ScriptOpcode.Goto"/>.</summary>
		Address = (Byte)'a',

		/// <summary>An array of values - a compound argument. In an argument list this is written as "p[count][type]", where count is the simple <see cref="ScriptArgument"/> of the number of elements, and type is the <see cref="ScriptArgument"/> of each element of the array. In the data these are stored as the count argument, followed by count number of elements.</summary>
		Array = (Byte)'p',

		/// <summary>A comparison stored as a literal byte value.</summary>
		Comparison = (Byte)'m',

		/// <summary>A literal byte value. This is stored as a single <see cref="Byte"/>, not as a <see cref="ScriptOperandType.Value"/>.</summary>
		Literal = (Byte)'l',

		/// <summary>An opcode value in a <see cref="Byte"/>. For opcodes <see cref="0x00"/> and <see cref="0x01"/> (<see cref="ScriptOpcode.Return"/> and <see cref="ScriptOpcode.Goto"/> respectively) there can be confusion about whether it's an opcode or a <see cref="Value"/> or <see cref="Address"/>/<see cref="Variable"/> value.</summary>
		Opcode = (Byte)'o',

		/// <summary>An optional compound argument. In an argument list this is written as "?[type]". If the type is found, it is added as an argument; otherwise it's skipped. This is hopefully just a temporary hack as we learn more about the script, as it seems impossible for it to be intentional, as <see cref="ScriptOpcode.Stop"/> and <see cref="ScriptOpcode.Goto"/> create confusion with <see cref="ScriptOperandType.Value"/> and <see cref="ScriptOperandType.Short"/>.</summary>
		Optional = (Byte)'?',

		/// <summary>A string value, either a literal or a variable index. This is stored as <see cref="ScriptOperandType.String"/> or <see cref="ScriptOperandType.StringVariable"/>.</summary>
		String = (Byte)'c',

		/// <summary>Some kind of unknown variable, stored as a <see cref="ScriptOperandType.UnknownVariable"/>.</summary>
		UnknownVariable = (Byte)'u',

		/// <summary>A byte value. This is stored as <see cref="ScriptOperandType.Value"/>. This may be signed. This can be confused with a <see cref="ScriptOpcode.Stop"/>.</summary>
		Value = (Byte)'b',

		/// <summary>A <see cref="Value"/> or a <see cref="Variable"/> or a <see cref="UnknownVariable"/>, depending upon the code <see cref="System.Byte"/>. This can be confused with <see cref="ScriptOpcode.Stop"/>, <see cref="ScriptOpcode.Goto"/>, <see cref="ScriptArgument.Address"/>, or even <see cref="ScriptArgument.Value"/> or <see cref="ScriptArgument.Variable"/> if there is no actual flexibility.</summary>
		ValueOrVariable = (Byte)'n',

		/// <summary>A variable index. This is stored as <see cref="ScriptOperandType.Short"/>. It could be confused with <see cref="ScriptOpcode.Goto"/>, <see cref="ScriptArgument.Address"/>, or even <see cref="ScriptArgument.ValueOrVariable"/>.</summary>
		Variable = (Byte)'v',
	}

	public class ScriptInstruction {
		/// <summary>
		/// See <see cref="ScriptArgument"/> for code values.
		/// </summary>
		internal static readonly Dictionary<ScriptOpcode, string> KnownOpcodes = new Dictionary<ScriptOpcode, string>() {
			{ ScriptOpcode.Stop, "" }, // 0x00 - Or not
			{ ScriptOpcode.Goto, "a" }, // 0x01
			{ ScriptOpcode.Call, "a" }, // 0x02
			{ ScriptOpcode.If, "nnm" }, // 0x03
			{ ScriptOpcode.Add, "?n?n?v" }, // 0x04
			{ ScriptOpcode.Subtract, "?b?v?v" }, // 0x05
			{ ScriptOpcode.U08, "bv" }, // 0x08
			{ ScriptOpcode.Set, "nv" }, // 0x09
			{ ScriptOpcode.U0A, "v" }, // 0x0A
			{ ScriptOpcode.AddCreatureToBattle, "nbn" }, // 0x0B
			{ ScriptOpcode.U0C, "bbbb" }, // 0x0C
			{ ScriptOpcode.U0D, "" }, // 0x0D
			{ ScriptOpcode.ShowSmallImage, "b" },  // 0x0E
			{ ScriptOpcode.U10, "" }, // 0x10
			{ ScriptOpcode.PrintStringAdd, "c" }, // 0x11
			{ ScriptOpcode.PrintString, "c" }, // 0x12
			{ ScriptOpcode.Return, "" }, // 0x13
			{ ScriptOpcode.U14, "vbvb" }, // 0x14
			{ ScriptOpcode.U16, "?v" }, // 0x16
			{ ScriptOpcode.IfTest, "" }, // 0x17
			{ ScriptOpcode.U18, "?v?b" }, // 0x18 - was s
			{ ScriptOpcode.U1B, "v" }, // 0x1B
			{ ScriptOpcode.U1C, "" }, // 0x1C
			{ ScriptOpcode.PressEnterToContinue, "" }, // 0x1D
			{ ScriptOpcode.ChangeMap, "b" }, // 0x20
			{ ScriptOpcode.SetAreaViewMap, "bb" }, // 0x21
			{ ScriptOpcode.AskYesOrNo, "" }, // 0x22
			{ ScriptOpcode.Break, "" }, // 0x23 - Return or break
			{ ScriptOpcode.Switch, "vpba" }, // 0x25
			{ ScriptOpcode.U2C, "b" }, // 0x2C
			{ ScriptOpcode.Ask, "vpbc" }, // 0x2B
			{ ScriptOpcode.ShowAreaViewMap, "v" }, // 0x2D
			{ ScriptOpcode.And, "nnv" }, // 0x2F
			{ ScriptOpcode.PrintNewLine, "" }, // 0x33
			{ ScriptOpcode.AddCreatureToParty, "bb" }, // 0x36
			{ ScriptOpcode.U38, "b" }, // 0x38
			{ ScriptOpcode.U3A, "" }, // 0x3A
			{ ScriptOpcode.U3C, "bn" }, // 0x3C
			{ ScriptOpcode.DrawAdventureScreen, "" }, // 0x3D
			{ ScriptOpcode.U42, "bbbb" }, // 0x42
			{ ScriptOpcode.U4C, "" }, // 0x4C
			{ ScriptOpcode.U60, "" }, // 0x60
			{ ScriptOpcode.U84, "" }, // 0x84
			{ ScriptOpcode.U85, "" }, // 0x85
			{ ScriptOpcode.U86, "" }, // 0x86
			{ ScriptOpcode.U87, "" }, // 0x87
			{ ScriptOpcode.U89, "" }, // 0x89
			{ ScriptOpcode.U8A, "" }, // 0x8A
			{ ScriptOpcode.U90, "" }, // 0x90
			{ ScriptOpcode.U99, "" }, // 0x99
			{ ScriptOpcode.U9D, "" }, // 0x9D
			{ ScriptOpcode.UA7, "" }, // 0xA7
		};

		/// <summary>Contains formatting strings for <see cref="ScriptOpcode"/>s.</summary>
		static readonly Dictionary<ScriptOpcode, string> OpcodeFormats = new Dictionary<ScriptOpcode, string>() {
			{ ScriptOpcode.Add, "{2} = {1} + {0}" },
			{ ScriptOpcode.And, "{2} = {1} & {0}" },
			{ ScriptOpcode.If, "If {0} {2} {1}:" },
			{ ScriptOpcode.IfTest, "If true:" },
			{ ScriptOpcode.Set, "{1} = {0}" },
			{ ScriptOpcode.Subtract, "{2} = {1} - {0}" },
		};

		/// <summary>Contains the <see cref="ScriptOpcode"/> values that skip an instruction if false.</summary>
		static readonly HashSet<ScriptOpcode> TestOpcodes = new HashSet<ScriptOpcode>() {
			ScriptOpcode.If,
			ScriptOpcode.IfTest,
		};

		/// <summary>Contains the <see cref="ScriptOpcode"/> values that force a branch.</summary>
		static readonly HashSet<ScriptOpcode> BranchOpcodes = new HashSet<ScriptOpcode>() {
			ScriptOpcode.Stop,
			ScriptOpcode.Switch,
			ScriptOpcode.Goto,
			ScriptOpcode.Return,
			ScriptOpcode.Break,
		};

		readonly protected ArrayBackedList<ScriptOperand> OperandsMutable = new ArrayBackedList<ScriptOperand>();
		internal readonly ArrayBackedList<ScriptOperand> TargetedByTokensMutable = new ArrayBackedList<ScriptOperand>();
		internal readonly ArrayBackedList<ScriptInstruction> TargetedByInstructionsMutable = new ArrayBackedList<ScriptInstruction>();

		/// <summary>Get the address of the <see cref="ScriptInstruction"/>. In the file, this is <see cref="Script.AddressOffset"/> (<c>0x7FFE</c>) too large.</summary>
		public long Address { get; protected set; }

		/// <summary>Get whether this <see cref="ScriptInstruction"/> forces a branch, such as <see cref="ScriptOpcode.Goto"/>.</summary>
		public bool IsBranch { get { return BranchOpcodes.Contains(Opcode); } }

		public bool IsOpcodeDefined { get { return typeof(ScriptOpcode).IsEnumDefined(Opcode); } }

		/// <summary>Get whether this <see cref="ScriptInstruction"/> is the target of another branching <see cref="ScriptInstruction"/>. <see cref="TargetedByTokens"/> and <see cref="TargetedByInstructions"/> list these.</summary>
		public bool IsTarget { get { return TargetedByTokensMutable.Count > 0; } }

		/// <summary>Get whether this <see cref="ScriptInstruction"/> contains a test, where if the test fails the next instruction is skipped.</summary>
		public bool IsTest { get { return TestOpcodes.Contains(Opcode); } }

		public ScriptOpcode Opcode { get; protected set; }

		public ReadOnlyList<ScriptOperand> Operands { get { return OperandsMutable; } }

		public Script Script { get; protected set; }

		public ReadOnlyList<ScriptInstruction> TargetedByInstructions { get { return TargetedByInstructionsMutable; } }

		public ReadOnlyList<ScriptOperand> TargetedByTokens { get { return TargetedByTokensMutable; } }

		protected ScriptInstruction(Script script, long offset, ScriptOpcode opcode) {
			Script = script;
			Address = offset + Script.AddressOffset;
			Opcode = opcode;
		}

		public ScriptInstruction(Script script, long offset, BinaryReader reader, ScriptOpcode opcode)
			: this(script, offset, opcode) {
			string operands;

			if (!KnownOpcodes.TryGetValue(opcode, out operands))
				return;

			for (int index = 0; index < operands.Length; index++)
				if (!ReadOperand(reader, operands, ref index, (ScriptArgument)operands[index]))
					break;
		}

		ScriptOperand GetOperand(int index) { return index < Operands.Count ? OperandsMutable[index] : ScriptOperand.Empty; }

		string GetOperandString(int index) { return " " + GetOperand(index); }

		int? GetIntegerOperand(int index) { ScriptOperand operand = GetOperand(index); return operand.IsValid && operand.IsValue ? (int?)operand.AsValue : null; }

		static bool IsCompoundOperandCode(ScriptArgument value) {
			switch (value) {
				case ScriptArgument.Address:
				case ScriptArgument.Literal:
				case ScriptArgument.Opcode:
				case ScriptArgument.String:
				case ScriptArgument.Value:
				case ScriptArgument.ValueOrVariable:
				case ScriptArgument.Variable:
				case ScriptArgument.Comparison:
					return false;
				case ScriptArgument.Array:
				case ScriptArgument.Optional:
					return false;
				case ScriptArgument.None:
					throw new ArgumentException("value");
				default:
					throw new NotImplementedException();
			}
		}

		internal protected virtual void Link() {
			foreach (ScriptOperand operand in Operands)
				operand.Link();
		}

		public static ScriptInstruction Read(Script script, BinaryReader reader) {
			long offset = reader.BaseStream.Position;
			ScriptOperand opcodeToken = new ScriptOperand(reader, ScriptArgument.Opcode);

			if (!opcodeToken.IsOpcode)
				return new Invalid(script, offset, opcodeToken);

			ScriptOpcode opcode = opcodeToken.GetOpcode();
			switch (opcode) {
				case ScriptOpcode.Call: return new Call(script, offset, reader);
				case ScriptOpcode.Switch: return new Switch(script, offset, reader);
				case ScriptOpcode.If: return new If(script, offset, reader);
				default: return new ScriptInstruction(script, offset, reader, opcode);
			}
		}

		protected bool ReadOperand(BinaryReader reader, string operands, ref int operandIndex, ScriptArgument operandCode) {
			long offset = reader.BaseStream.Position;
			ScriptOperand token = new ScriptOperand(reader, operandCode);
			token.Instruction = this;

			int outputIndex = OperandsMutable.Count;
			OperandsMutable.Add(token);
			switch (operandCode) {
				case ScriptArgument.Address:
				case ScriptArgument.Value:
				case ScriptArgument.ValueOrVariable:
				case ScriptArgument.String:
				case ScriptArgument.Variable:
				case ScriptArgument.Literal:
				case ScriptArgument.UnknownVariable:
				case ScriptArgument.Comparison:
					token.IsValid = token.MatchTypeCode(operandCode);
					break;

				case ScriptArgument.Array:
					ScriptArgument arrayCountType = (ScriptArgument)operands[++operandIndex];
					ScriptArgument arrayType = (ScriptArgument)operands[++operandIndex];

					if (IsCompoundOperandCode(arrayCountType) || IsCompoundOperandCode(arrayType))
						throw new InvalidOperationException();

					token.IsValid = token.MatchTypeCode(arrayCountType);
					if (!token.IsValid) {
						OperandsMutable[outputIndex] = token;
						return false;
					}

					for (int index = 0; index < token.AsValue; index++)
						if (!ReadOperand(reader, operands, ref operandIndex, arrayType))
							return false;
					break;

				case ScriptArgument.Optional:
					operandCode = (ScriptArgument)operands[++operandIndex];
					if (!token.MatchTypeCode(operandCode)) {
						reader.BaseStream.Position = offset;
						OperandsMutable.RemoveAt(outputIndex);
						return true;
					}
					break;

				default:
					throw new InvalidOperationException("Invalid operand type code '" + operandCode + "'.");
			}

			OperandsMutable[outputIndex] = token;
			return true;
		}

		protected bool ReadOperand(BinaryReader reader, ScriptArgument operandCode) {
			int index = -1;
			return ReadOperand(reader, null, ref index, operandCode);
		}


		public virtual void ToRichText(RichTextBuilder builder) {
			string format;

			if (OpcodeFormats.TryGetValue(Opcode, out format)) {
				for (int index = 0; index < format.Length; index++) {
					if (format[index] == '{') {
						if (format[index + 1] == '{') {
							index++;
							builder.Append('{');
						} else {
							int end;

							for (end = ++index; format[end] != '}'; end++) ;
							int operandIndex = int.Parse(format.Substring(index, end - index));
							ScriptOperand operand = GetOperand(operandIndex);
							operand.ToRichText(builder);
							index = end;
						}
					} else
						builder.Append(format[index]);
				}
			} else {
				if (IsOpcodeDefined)
					builder.Append(Opcode.ToString());
				else {
					builder.ForegroundColor = Color.Red;
					builder.AppendFormat("{0:X2}", (int)Opcode);
					builder.ForegroundColor = Color.Black;
				}

				foreach (ScriptOperand operand in Operands) {
					builder.Append(" ");
					operand.ToRichText(builder);
				}
			}
		}

		protected string ToStringOpcode() { return typeof(ScriptOpcode).IsEnumDefined(Opcode) ? Opcode.ToString() : string.Format("{0:X2}!!!!!", (int)Opcode); }
		protected string ToBaseString() { return ToStringOpcode(); }

		public override string ToString() {
			string format;
			if (OpcodeFormats.TryGetValue(Opcode, out format)) {
				try {
					object[] array = new object[Operands.Count];
					for (int index = 0; index < array.Length; index++)
						array[index] = Operands[index];
					return string.Format(format, (object[])array);
				} catch (Exception) {
				}
			}

			string text = ToBaseString();

			foreach (ScriptOperand operand in Operands)
				text += " " + operand.ToString();
			return text;
		}

		public class Call : ScriptInstruction {
			public Call(Script script, long offset, BinaryReader reader)
				: base(script, offset, reader, ScriptOpcode.Call) {
			}
		}

		public class If : ScriptInstruction {
			public byte? TestType { get { return GetOperand(2).TryValue; } }

			public If(Script script, long offset, BinaryReader reader)
				: base(script, offset, reader, ScriptOpcode.If) {
				/*int operandIndex = -1;
				ReadOperand(reader, null, ref operandIndex, 'n');
				ReadOperand(reader, null, ref operandIndex, 'n');
				TestType = reader.ReadByte();*/
			}

			string GetTestName() {
				switch (TestType) {
					case 0x16: return "==";
					case 0x17: return "!=";
					//case 0x17: return ">";
					//case 0x1B: return ">=";
					default: return string.Format("(?{0:X2}h)", TestType);
				}
			}

			public override string ToString() {
				string text = ToBaseString();

				text += string.Format("If {0} {1} {2} then:", Operands[0], GetTestName(), Operands[1]);
				return text;
			}
		}

		public class Switch : ScriptInstruction {
			public Switch(Script script, long offset, BinaryReader reader)
				: base(script, offset, reader, ScriptOpcode.Switch) {
			}
		}

		public class Invalid : ScriptInstruction {
			public ScriptOperand InvalidToken { get; private set; }

			public Invalid(Script script, long offset, ScriptOperand invalidToken)
				: base(script, offset, ScriptOpcode.Invalid) {
				InvalidToken = invalidToken;
				invalidToken.Instruction = this;
			}

			protected internal override void Link() {
				base.Link();
				InvalidToken.Link();
			}

			public override string ToString() {
				return string.Format("{0}!!!!!!!!!!!!!", InvalidToken);
			}
		}
	}

	public enum ScriptComparison {
		Equal = 0x16,
		NotEqual = 0x17,
	}
}
