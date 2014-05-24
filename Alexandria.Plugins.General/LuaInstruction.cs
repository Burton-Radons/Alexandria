using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Plugins.General {
	public struct LuaInstruction {
		readonly LuaFunction function;

		uint Code;

		const int MaxStack = 250;

		const int OpcodeSize = 6;
		const int ASize = 8;
		const int BSize = 9;
		const int CSize = 9;
		const int BxSize = BSize + CSize;

		const int OpcodeShift = 0;
		const int CShift = OpcodeShift + OpcodeSize;
		const int BShift = CShift + CSize;
		const int BxShift = CShift;
		const int AShift = BShift + BSize;

		const int AMax = (1 << ASize) - 1;
		const int BMax = (1 << BSize) - 1;
		const int BxMax = (1 << BxSize) - 1;
		const int CMax = (1 << CSize) - 1;
		const int SignedBxMax = BxMax >> 1;

		public Codex<LuaOpcodeArgument> Arguments { get { return OpcodeArguments[Opcode]; } }

		public LuaFunction Function { get { return function; } }

		public LuaModule Module { get { return function.Module; } }

		public LuaOpcode Opcode { get { return (LuaOpcode)GetBits(OpcodeSize, OpcodeShift); } }
		public int A { get { return GetBits(ASize, AShift); } }
		public int B { get { return GetBits(BSize, BShift); } }
		public int C { get { return GetBits(CSize, CShift); } }
		public int Bx { get { return GetBits(BxSize, BxShift); } }
		public int SignedBx { get { return Bx - SignedBxMax; } }

		public string ThenAction {
			get {
				switch (Opcode) {
					case LuaOpcode.LoadBoolean: return C != 0 ? "Skip" : null;
					case LuaOpcode.Equal:
					case LuaOpcode.LessThan:
					case LuaOpcode.LessThanOrEqualTo:
					case LuaOpcode.Test: return "Then";
					case LuaOpcode.TForLoop: return "if(R(" + GetArgumentRegister(A + 2) + ") == null)";
					default: return null;
				}
			}
		}

		public LuaInstruction(LuaFunction function, uint code) {
			this.function = function;
			Code = code;
		}

		int GetBits(int count, int shift) { return (int)((Code >> shift) & ((1 << count) - 1)); }

		string GetArgument(LuaOpcodeArgument argument) {
			switch (argument) {
				case LuaOpcodeArgument.BooleanB: return GetArgumentBoolean(B);

				case LuaOpcodeArgument.BranchSignedBx: return GetArgumentBranch(SignedBx);

				case LuaOpcodeArgument.ConstantBx: return GetArgumentConstant(Bx);

				case LuaOpcodeArgument.LiteralA: return GetArgumentLiteral(A);
				case LuaOpcodeArgument.LiteralB: return GetArgumentLiteral(B);
				case LuaOpcodeArgument.LiteralBx: return GetArgumentLiteral(Bx);
				case LuaOpcodeArgument.LiteralC: return GetArgumentLiteral(C);

				case LuaOpcodeArgument.RegisterA: return GetArgumentRegister(A);
				case LuaOpcodeArgument.RegisterB: return GetArgumentRegister(B);
				case LuaOpcodeArgument.RegisterC: return GetArgumentRegister(C);

				case LuaOpcodeArgument.RegisterConstantB: return GetArgumentRegisterConstant(B);
				case LuaOpcodeArgument.RegisterConstantC: return GetArgumentRegisterConstant(C);

				case LuaOpcodeArgument.UpValueB: return GetArgumentUpValue(B);

				default: throw new NotImplementedException();
			}
		}

		string GetArgumentBoolean(int value) { return (value != 0).ToString(); }
		string GetArgumentBranch(int value) { return value.ToString(); }

		string GetArgumentConstant(int value, bool quotes = false) {
			if (value >= Function.Constants.Count)
				return "K(Invalid index " + value + ")";
			object constant = Function.Constants[value];

			if (constant == null)
				return "nil";

			if (constant is string) {
				foreach (char ch in (string)constant) {
					if (!char.IsLetterOrDigit(ch) && ch != '_')
						quotes = true;
				}

				if ((string)constant == "nil")
					quotes = true;
				return quotes ? "\"" + constant + "\"" : constant.ToString();
			}

			if (constant is double)
				return constant.ToString();

			return "K(" + constant + ")";
		}

		string GetArgumentLiteral(int value) { return GetArgumentConstant(value, true); }
		string GetArgumentRegister(int value) { return "R" + value; }
		string GetArgumentRegisterConstant(int value) { return value < Function.MaxStackSize ? GetArgumentRegister(value) : GetArgumentConstant(value - MaxStack); }
		string GetArgumentUpValue(int value) { return "UpValue(" + (value < Function.UpValues.Count ? Function.UpValues[value] : "Invalid index " + value) + ")"; }
		string GetArgumentGlobal(int value) { return GetArgumentConstant(value); }

		string GetArgumentRegisterRange(int from, int to) {
			if (from != to)
				return "R[" + from + " .. " + to + "]";
			return GetArgumentRegister(from);
		}

		public override string ToString() {
			string text = Opcode.ToString();

			switch (Opcode) {
				case LuaOpcode.Call:
					text = (A - 1 == A + C - 2) ? "" : string.Format("{0} = ", GetArgumentRegisterRange(A, A + C - 2));
					return text + string.Format("{0}({1})", GetArgumentRegister(A), GetArgumentRegisterRange(A + 1, A + B - 1));

				case LuaOpcode.Closure:
					LuaFunction closure = Function.Closures[Bx];
					text = string.Format("{0} = closure({1}", GetArgumentRegister(A), closure.SourceOrIndex);
					if (closure.UpValueCount != 0)
						text += string.Format(", {0}", GetArgumentRegisterRange(A, A + closure.UpValueCount - 1));
					return text + ")";

				case LuaOpcode.GetGlobal:
					return string.Format("{0} = {1}", GetArgumentRegister(A), GetArgumentGlobal(Bx));

				case LuaOpcode.LoadBoolean: // R(A) = (bool)B; if(C) PC++
					return string.Format("{0} = {1}{2}", GetArgumentRegister(A), GetArgumentBoolean(B), (C != 0) ? "; PC++" : "");

				case LuaOpcode.LoadConstant: // R(A) = Constant(Bx)
					return string.Format("{0} = {1}", GetArgumentRegister(A), GetArgumentLiteral(Bx));

				case LuaOpcode.Move: // R(A) = R(B)
					return string.Format("{0} = {1}", GetArgumentRegister(A), GetArgumentRegister(B));

				case LuaOpcode.Return: // return R(A to A+B-2); if (B == 0) then return up to 'top'
					if (A - 1 == A + B - 2)
						return "return";
					return string.Format("return {0}", B == 0 ? GetArgumentRegister(A) + " to top" : GetArgumentRegisterRange(A, A + B - 2));

				case LuaOpcode.Self: return string.Format("{0} = {1}; {2} = {3}.{4}", GetArgumentRegister(A + 1), GetArgumentRegister(B), GetArgumentRegister(A), GetArgumentRegister(B), GetArgumentRegisterConstant
					(C));

				case LuaOpcode.SetGlobal: // Global[Constant[Bx]] = R(A)
					return string.Format("{0} = {1}", GetArgumentGlobal(Bx), GetArgumentRegister(A));

				default:
					foreach (LuaOpcodeArgument argument in Arguments)
						text += " " + GetArgument(argument);
					break;
			}

			return text;
		}

		static readonly Codex<LuaOpcodeArgument>
			Arguments_RegisterA = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA }),
			Arguments_RegisterA_BranchSignedBx = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.BranchSignedBx }),
			Arguments_RegisterA_RegisterB = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.RegisterB }),
			Arguments_RegisterA_RegisterB_RegisterC = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.RegisterB, LuaOpcodeArgument.RegisterC }),
			Arguments_RegisterA_RegisterB_RegisterConstantC = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.RegisterB, LuaOpcodeArgument.RegisterConstantC }),
			Arguments_RegisterA_RegisterC = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.RegisterC }),
			Arguments_RegisterA_ConstantBx = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.ConstantBx }),
			Arguments_RegisterA_LiteralBx = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.LiteralBx }),
			Arguments_RegisterA_BooleanB = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.BooleanB }),
			Arguments_RegisterA_UpValueB = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.UpValueB }),
			Arguments_RegisterA_RegisterConstantB_RegisterConstantC = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.RegisterConstantB, LuaOpcodeArgument.RegisterConstantC }),
			Arguments_RegisterA_LiteralB_LiteralC = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.RegisterA, LuaOpcodeArgument.LiteralB, LuaOpcodeArgument.LiteralC }),
			Arguments_BranchSignedBx = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.BranchSignedBx }),
			Arguments_LiteralA_RegisterConstantB_RegisterConstantC = new Codex<LuaOpcodeArgument>(new LuaOpcodeArgument[] { LuaOpcodeArgument.LiteralA, LuaOpcodeArgument.RegisterConstantB, LuaOpcodeArgument.RegisterConstantC });

		static readonly Dictionary<LuaOpcode, Codex<LuaOpcodeArgument>> OpcodeArguments = new Dictionary<LuaOpcode, Codex<LuaOpcodeArgument>>() {
			{ LuaOpcode.Move, Arguments_RegisterA_RegisterB },
			{ LuaOpcode.LoadConstant, Arguments_RegisterA_ConstantBx },
			{ LuaOpcode.LoadBoolean, Arguments_RegisterA_BooleanB },
			{ LuaOpcode.LoadNil, Arguments_RegisterA_RegisterB },
			{ LuaOpcode.GetUpValue,  Arguments_RegisterA_UpValueB },
			{ LuaOpcode.GetGlobal, Arguments_RegisterA_ConstantBx },
			{ LuaOpcode.GetTable, Arguments_RegisterA_RegisterB_RegisterConstantC },
			{ LuaOpcode.SetGlobal, Arguments_RegisterA_ConstantBx },
			{ LuaOpcode.SetUpValue, Arguments_RegisterA_UpValueB },
			{ LuaOpcode.SetTable, Arguments_RegisterA_RegisterConstantB_RegisterConstantC },
			{ LuaOpcode.NewTable, Arguments_RegisterA_LiteralB_LiteralC },
			{ LuaOpcode.Self, Arguments_RegisterA_RegisterB_RegisterConstantC },
			{ LuaOpcode.Add, Arguments_RegisterA_RegisterConstantB_RegisterConstantC },
			{ LuaOpcode.Subtract, Arguments_RegisterA_RegisterConstantB_RegisterConstantC },
			{ LuaOpcode.Multiply, Arguments_RegisterA_RegisterConstantB_RegisterConstantC },
			{ LuaOpcode.Divide, Arguments_RegisterA_RegisterConstantB_RegisterConstantC },
			{ LuaOpcode.Power, Arguments_RegisterA_RegisterConstantB_RegisterConstantC },
			{ LuaOpcode.Negate, Arguments_RegisterA_RegisterB },
			{ LuaOpcode.Not, Arguments_RegisterA_RegisterB },
			{ LuaOpcode.Concatenate, Arguments_RegisterA_RegisterB_RegisterC },
			{ LuaOpcode.Jump, Arguments_BranchSignedBx },
			{ LuaOpcode.Equal, Arguments_LiteralA_RegisterConstantB_RegisterConstantC },
			{ LuaOpcode.LessThan, Arguments_LiteralA_RegisterConstantB_RegisterConstantC },
			{ LuaOpcode.LessThanOrEqualTo, Arguments_LiteralA_RegisterConstantB_RegisterConstantC },
			{ LuaOpcode.Test, Arguments_RegisterA_RegisterB_RegisterConstantC },
			{ LuaOpcode.Call, Arguments_RegisterA_RegisterB_RegisterC },
			{ LuaOpcode.TailCall, Arguments_RegisterA_RegisterB },
			{ LuaOpcode.Return, Arguments_RegisterA_RegisterB },
			{ LuaOpcode.ForLoop, Arguments_RegisterA_BranchSignedBx },
			{ LuaOpcode.TForLoop, Arguments_RegisterA_RegisterC },
			{ LuaOpcode.TForPreparation, Arguments_RegisterA_BranchSignedBx },
			{ LuaOpcode.SetList, Arguments_RegisterA_LiteralBx },
			{ LuaOpcode.SetList0, Arguments_RegisterA_LiteralBx },
			{ LuaOpcode.Close, Arguments_RegisterA },
			{ LuaOpcode.Closure, Arguments_RegisterA_LiteralBx },
		};
	}

}
