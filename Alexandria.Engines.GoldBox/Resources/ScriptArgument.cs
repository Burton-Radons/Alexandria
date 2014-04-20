using System;
using System.Collections.Generic;
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
}
