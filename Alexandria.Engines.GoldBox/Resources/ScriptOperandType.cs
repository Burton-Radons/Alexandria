using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.GoldBox.Resources {
	public enum ScriptOperandType {
		None = -1,

		/// <summary>A <see cref="System.Byte"/> value (<see cref="System.Byte"/> type, <see cref="System.Byte"/> value); Type is <c>0x00</c>. Can be confused with <see cref="ScriptOpcode.Stop"/>. This is a <see cref="ScriptArgument.Value"/> or <see cref="ScriptArgument.ValueOrVariable"/> value.</summary>
		Value = 0x00,

		/// <summary>A <see cref="System.UInt16"/> value (<see cref="System.Byte"/> type, <see cref="System.UInt16"/> value); type is <c>0x01</c>. Can be confused with <see cref="ScriptOpcode.Goto"/>. This is the same coding as a <see cref="Address"/>.</summary>
		Variable = 0x01,

		/// <summary>An unknown variable type, stored as (<see cref="System.Byte"/> type, <see cref="System.UInt16"/> value), where type is <c>0x03</c> and value is the variable index.</summary>
		UnknownVariable = 0x03,

		/// <summary>A literal string value. This is stored as (<see cref="System.Byte"/> type, <see cref="System.Byte"/> length, <see cref="System.Byte"/>[length] data), where type is <c>0x80</c> and data are for the coded string. You can read a coded string with <see cref="Engine.ReadCodedString"/>. This is a <see cref="ScriptArgument.String"/>.</summary>
		String = 0x80,

		/// <summary>A string variable index. This is stored as (<see cref="System.Byte"/> type, <see cref="System.UInt16"/> variable), where type is <c>0x81</c> and variable is the variable index. This is a <see cref="ScriptArgument.String"/>.</summary>
		StringVariable = 0x81,

		/// <summary>Stored as a literal <see cref="System.Byte"/>. This may be confused with a <see cref="Value"/> or a <see cref="Variable"/>.</summary>
		Opcode = -2,

		Address = -3,

		/// <summary><see cref="ScriptComparison"/> value stored as a single literal <see cref="Byte"/>.</summary>
		Comparison = -4,
	}
}
