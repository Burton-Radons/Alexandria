using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.GoldBox.Resources {
	/// <summary>An opcode in a script.</summary>
	public enum ScriptOpcode : byte {
		/// <summary></summary>
		Invalid = 0xFE,

		/// <summary></summary>
		Stop = 0x00,

		/// <summary>(a:x)</summary>
		Goto = 0x01,

		/// <summary>(a:x) Call the function (x - 0x7FFE) is the address.</summary>
		Call = 0x02,

		/// <summary></summary>
		If = 0x03,

		/// <summary></summary>
		Add = 0x04,

		/// <summary></summary>
		Subtract = 0x05,

		/// <summary></summary>
		U08 = 0x08,
		/// <summary>(b:x n:y) Set y to x</summary>
		Set = 0x09,

		/// <summary></summary>
		U0A = 0x0A,
		/// <summary>(b:x b:y b:z) Add y of creature x to battle (z == x).</summary>
		AddCreatureToBattle = 0x0B,

		/// <summary></summary>
		U0C = 0x0C,

		/// <summary></summary>
		U0D = 0x0D,

		/// <summary></summary>
		ShowSmallImage = 0x0E,

		/// <summary>(b:digitCount v:stringTarget) Ask for an integer value from the user and put the result in the target.</summary>
		AskIntegerByte = 0x0F,

		/// <summary></summary>
		U10 = 0x10,

		/// <summary>(c:x) Add x to the printed string.</summary>
		PrintStringAdd = 0x11,

		/// <summary>(c:x) Print x.</summary>
		PrintString = 0x12,

		/// <summary></summary>
		Return = 0x13,

		/// <summary></summary>
		U14 = 0x14,

		/// <summary></summary>
		U16 = 0x16,

		/// <summary></summary>
		IfTest = 0x17,

		/// <summary></summary>
		U18 = 0x18,

		/// <summary></summary>
		U1B = 0x1B,

		/// <summary></summary>
		U1C = 0x1C,

		/// <summary></summary>
		PressEnterToContinue = 0x1D,

		/// <summary></summary>
		U1F = 0x1F,

		/// <summary></summary>
		ChangeMap = 0x20,

		/// <summary>(b:X b:Y) Either some kind of change map, or change what area view is shown (X == Y).</summary>
		SetAreaViewMap = 0x21,

		/// <summary></summary>
		AskYesOrNo = 0x22,

		/// <summary></summary>
		Break = 0x23,

		/// <summary></summary>
		Battle = 0x24,

		/// <summary>(s:x array[bs]:y) Branch to a location based on something about x. (y - 0x7FFE) is the address.</summary>
		Switch = 0x25,

		/// <summary></summary>
		Ask = 0x2B,

		/// <summary></summary>
		AskYesOrNo_MatrixCubed = 0x2C,

		/// <summary>Show the area view map?</summary>
		ShowAreaViewMap = 0x2D,

		/// <summary></summary>
		And = 0x2F,

		/// <summary></summary>
		PrintNewLine = 0x33,

		/// <summary></summary>
		U35 = 0x35,

		/// <summary>(b:x b:y) Add creature x to party with strength? multiplier y (y is 100 for no scaling)</summary>
		AddCreatureToParty = 0x36,

		/// <summary></summary>
		U38 = 0x38,

		/// <summary></summary>
		U3A = 0x3A,

		/// <summary></summary>
		U3C = 0x3C,

		/// <summary></summary>
		DrawAdventureScreen = 0x3D,

		/// <summary>(b:X) Some kind of if; used in demo (4) at the start.</summary>
		U42 = 0x42,

		/// <summary></summary>
		U43 = 0x43,

		/// <summary></summary>
		U4C = 0x4C,

		/// <summary></summary>
		U60 = 0x60,

		/// <summary></summary>
		U84 = 0x84,

		/// <summary></summary>
		U85 = 0x85,

		/// <summary></summary>
		U86 = 0x86,

		/// <summary></summary>
		U87 = 0x87,

		/// <summary></summary>
		U89 = 0x89,

		/// <summary></summary>
		U8A = 0x8A,

		/// <summary></summary>
		U8E = 0x8E,

		/// <summary></summary>
		U90 = 0x90,

		/// <summary></summary>
		U99 = 0x99,

		/// <summary></summary>
		U9D = 0x9D,

		/// <summary></summary>
		UA7 = 0xA7,
	}
}
