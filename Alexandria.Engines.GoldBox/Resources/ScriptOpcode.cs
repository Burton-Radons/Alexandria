using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.GoldBox.Resources {
	public enum ScriptOpcode : byte {
		Invalid = 0xFE,
		Stop = 0x00,

		/// <summary>(a:x)</summary>
		Goto = 0x01,

		/// <summary>(a:x) Call the function (x - 0x7FFE) is the address.</summary>
		Call = 0x02,
		If = 0x03,
		Add = 0x04,
		Subtract = 0x05,
		U08 = 0x08,
		/// <summary>(b:x n:y) Set y to x</summary>
		Set = 0x09,
		U0A = 0x0A,
		/// <summary>(b:x b:y b:z) Add y of creature x to battle (z == x).</summary>
		AddCreatureToBattle = 0x0B,
		U0C = 0x0C,
		U0D = 0x0D,
		ShowSmallImage = 0x0E,

		/// <summary>(b:digitCount v:stringTarget) Ask for an integer value from the user and put the result in the target.</summary>
		AskIntegerByte = 0x0F,

		U10 = 0x10,
		/// <summary>(c:x) Add x to the printed string.</summary>
		PrintStringAdd = 0x11,
		/// <summary>(c:x) Print x.</summary>
		PrintString = 0x12,
		Return = 0x13,
		U14 = 0x14,
		U16 = 0x16,
		IfTest = 0x17,
		U18 = 0x18,
		U1B = 0x1B,
		U1C = 0x1C,
		PressEnterToContinue = 0x1D,
		U1F = 0x1F,
		ChangeMap = 0x20,

		/// <summary>(b:X b:Y) Either some kind of change map, or change what area view is shown (X == Y).</summary>
		SetAreaViewMap = 0x21,

		AskYesOrNo = 0x22,
		Break = 0x23,
		Battle = 0x24,

		/// <summary>(s:x array[bs]:y) Branch to a location based on something about x. (y - 0x7FFE) is the address.</summary>
		Switch = 0x25,

		Ask = 0x2B,

		AskYesOrNo_MatrixCubed = 0x2C,

		/// <summary>Show the area view map?</summary>
		ShowAreaViewMap = 0x2D,
		And = 0x2F,
		PrintNewLine = 0x33,
		U35 = 0x35,
		/// <summary>(b:x b:y) Add creature x to party with strength? multiplier y (y is 100 for no scaling)</summary>
		AddCreatureToParty = 0x36,
		U38 = 0x38,
		U3A = 0x3A,
		U3C = 0x3C,
		DrawAdventureScreen = 0x3D,

		/// <summary>(b:X) Some kind of if; used in demo (4) at the start.</summary>
		U42 = 0x42,
		U43 = 0x43,
		U4C = 0x4C,
		U60 = 0x60,
		U84 = 0x84,
		U85 = 0x85,
		U86 = 0x86,
		U87 = 0x87,
		U89 = 0x89,
		U8A = 0x8A,
		U8E = 0x8E,
		U90 = 0x90,
		U99 = 0x99,
		U9D = 0x9D,
		UA7 = 0xA7,
	}
}
