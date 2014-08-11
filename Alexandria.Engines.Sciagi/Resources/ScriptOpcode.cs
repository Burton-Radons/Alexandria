using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi.Resources {
	/// <summary>
	/// An opcode in a <see cref="Script"/>.
	/// </summary>
	public enum ScriptOpcode {
		/// <summary>(accumulator = ~accumulator) Invert the bits of the accumulator.</summary>
		[ScriptOpcode("ii")]
		BinaryNot = 0x00,

		/// <summary>[value --] (accumulator += value) Pop a value and add it to the accumulator.</summary>
		[ScriptOpcode("iii")]
		Add = 0x02,

		/// <summary>[value --] (accumulator -= value) Pop a value and subtract it into the accumulator.</summary>
		[ScriptOpcode("iii")]
		Subtract = 0x04,

		/// <summary>[value --] (accumulator *= value) Pop a value and multiply it into the accumulator.</summary>
		[ScriptOpcode("iii")]
		Multiply = 0x06,

		/// <summary>[value --] (accumulator = (value == 0 ? 0 : accumulator / value)) Divide the accumulator by a popped value. If value is 0, accumulator is set to 0.</summary>
		[ScriptOpcode("iii")]
		Divide = 0x08,

		/// <summary>[value --] (accumulator = (value == 0 ? 0 : accumulator % value)) Modulo the accumulator by a popped value. If value is 0, accumulator is set to 0.</summary>
		[ScriptOpcode("iii")]
		Modulo = 0x0A,

		/// <summary>[value --] (accumulator = value &gt;&gt; accumulator) Shift the value right by the accumulator.</summary>
		[ScriptOpcode("iii")]
		ShiftRight = 0x0C,

		/// <summary>[value --] (accumulator = value &lt;&lt; accumulator) Shift the value left by the accumulator.</summary>
		[ScriptOpcode("iii")]
		ShiftLeft = 0x0E,

		/// <summary>[value --] (accumulator ^= value) Binary exclusive OR the accumulator with the value.</summary>
		[ScriptOpcode("iii")]
		ExclusiveOr = 0x10,

		/// <summary>[value --] (accumulator &amp;= value) Binary AND the accumulator with the value.</summary>
		[ScriptOpcode("iii")]
		And = 0x12,

		/// <summary>[value --] (accumulator |= value) Binary OR the accumulator with the value.</summary>
		[ScriptOpcode("iii")]
		Or = 0x14,

		/// <summary>(accumulator = -accumulator) Negate the accumulator.</summary>
		[ScriptOpcode("ii")]
		Negate = 0x16,

		/// <summary>(accumulator = accumulator == 0 ? 1 : 0) Boolean NOT the accumulator.</summary>
		[ScriptOpcode("ii")]
		BooleanNot = 0x18,

		/// <summary>[value --] (accumulator = (accumulator == value) ? 1 : 0) Test for equality.</summary>
		[ScriptOpcode("ibi")]
		TestEqual = 0x1A,

		/// <summary>[value --] (accumulator = (accumulator != value) ? 1 : 0) Test for inequality.</summary>
		[ScriptOpcode("ibi")]
		TestNotEqual = 0x1C,

		/// <summary>[value --] (accumulator = (value &gt; accumulator) ? 1 : 0) Test for greater than.</summary>
		[ScriptOpcode("ibi")]
		TestGreater = 0x1E,

		/// <summary>[value --] (accumulator = (value &gt;= accumulator) ? 1 : 0) Test for greater than or equal to.</summary>
		[ScriptOpcode("ibi")]
		TestGreaterOrEqual = 0x20,

		/// <summary>[value --] (accumulator = (value &lt; accumulator) ? 1 : 0) Test for less than.</summary>
		[ScriptOpcode("ibi")]
		TestLess = 0x22,

		/// <summary>[value --] (accumulator = (value &lt;= accumulator) ? 1 : 0) Test for less than or equal to.</summary>
		[ScriptOpcode("ibi")]
		TestLessOrEqual = 0x24,

		/// <summary>[value --] (accumulator = ((ushort)value &gt; (ushort)accumulator) ? 1 : 0) Test for unsigned greater than.</summary>
		[ScriptOpcode("ibi")]
		TestUnsignedGreater = 0x26,

		/// <summary>[value --] (accumulator = ((ushort)value &gt;= (ushort)accumulator) ? 1 : 0) Test for unsigned greater than or equal to.</summary>
		[ScriptOpcode("ibi")]
		TestUnsignedGreaterOrEqual = 0x28,

		/// <summary>[value --] (accumulator = (pop() &lt; accumulator) ? 1 : 0) Test for unsigned less than.</summary>
		[ScriptOpcode("ibi")]
		TestUnsignedLess = 0x2A,

		/// <summary>[value --] (accumulator = (pop() &lt;= accumulator) ? 1 : 0) Test for unsigned less than or equal to.</summary>
		[ScriptOpcode("ibi")]
		TestUnsignedLessOrEqual = 0x2C,

		/// <summary>{RelativeOffset offset} (if(accumulator != 0) pc += offset) Branch relative if <c>true</c>.</summary>
		[ScriptOpcode("b---r")]
		BranchRelativeIfTrue = 0x2E,

		/// <summary>{RelativeOffset offset} (if(accumulator != 0) pc += offset) Branch relative if <c>false</c>.</summary>
		[ScriptOpcode("b---r")]
		BranchRelativeIfFalse = 0x30,

		/// <summary>{RelativeOffset offset} (pc += offset) Jump relative.</summary>
		[ScriptOpcode("----r")]
		JumpRelative = 0x32,

		/// <summary>ldi {Integer value} (accumulator = value) Load data immediate.</summary>
		[ScriptOpcode("-i--i")]
		SetAccumulator = 0x34,

		/// <summary>push [-- accumulator] (push(accumulator)) Push the accumulator onto the stack.</summary>
		[ScriptOpcode("i--i-")]
		PushAccumulator = 0x36,

		/// <summary>pushi value {Integer value} [-- value] (push(value)) Push the value onto the stack.</summary>
		[ScriptOpcode("---ii")]
		PushImmediate = 0x38,

		/// <summary>toss [value --] (pop()) Toss the top value off the stack.</summary>
		[ScriptOpcode("--?--")]
		Toss = 0x3A,

		/// <summary>dup [value -- value value] (push(peek())) Push the top value on the stack.</summary>
		[ScriptOpcode("--??-")]
		Duplicate = 0x3C,

		/// <summary>link count {uint count} [-- [count]] (sp += count * 2) Make space for the given number of entries.</summary>
		[ScriptOpcode("----u")]
		Link = 0x3E,
	}

	/// <summary>
	/// An operand to a <see cref="ScriptInstruction"/>.
	/// </summary>
	public enum ScriptOperandType {
		/// <summary>None or invalid value.</summary>
		None,

		/// <summary>Unknown operand.</summary>
		Unknown,

		/// <summary>For operands, these are stored as <see cref="Int16"/> when <see cref="ScriptOpcode"/> is even, and <see cref="SByte"/> when <see cref="ScriptOpcode"/> is odd.</summary>
		Integer,

		/// <summary>For operands, these are stored as <see cref="UInt16"/> when <see cref="ScriptOpcode"/> is even, and <see cref="Byte"/> when <see cref="ScriptOpcode"/> is odd.</summary>
		UnsignedInteger,

		/// <summary>Boolean operand.</summary>
		Boolean,

		/// <summary>Stored as <see cref="Int16"/> when <see cref="ScriptOpcode"/> is even; <see cref="SByte"/> when <see cref="ScriptOpcode"/> is odd.</summary>
		RelativeOffset,
	}

	/// <summary>
	/// Describes a <see cref="ScriptOpcode"/>.
	/// </summary>
	public class ScriptOpcodeAttribute : Attribute {
		/// <summary>Get the accumulator input.</summary>
		public ScriptOperandType AccumulatorInput { get; set; }

		/// <summary>Get the accumulator output.</summary>
		public ScriptOperandType AccumulatorOutput { get; set; }

		/// <summary>Get the operand.</summary>
		public ScriptOperandType Operand { get; set; }

		/// <summary>Get the stack input.</summary>
		public ScriptOperandType StackInput { get; set; }

		/// <summary>Get the stack output effect.</summary>
		public ScriptOperandType StackOutput { get; set; }

		/// <summary>Get whether this is a branch.</summary>
		public bool IsBranch { get { return Operand == ScriptOperandType.RelativeOffset; } }

		/// <summary>
		/// Code is "[accumulator input][accumulator output][stack input][stack output][operand]".
		/// </summary>
		/// <param name="code"></param>
		public ScriptOpcodeAttribute(string code) {
			AccumulatorInput = GetCode(code, 0);
			AccumulatorOutput = GetCode(code, 1);
			StackInput = GetCode(code, 2);
			StackOutput = GetCode(code, 3);
			Operand = GetCode(code, 4);
		}

		static ScriptOperandType GetCode(string code, int index) {
			if (index >= code.Length)
				return ScriptOperandType.None;
			switch (code[index]) {
				case '-': return ScriptOperandType.None;
				case '?': return ScriptOperandType.Unknown;
				case 'i': return ScriptOperandType.Integer;
				case 'u': return ScriptOperandType.UnsignedInteger;
				case 'b': return ScriptOperandType.Boolean;
				case 'r': return ScriptOperandType.RelativeOffset;
				default: throw new NotImplementedException();
			}
		}
	}
}
