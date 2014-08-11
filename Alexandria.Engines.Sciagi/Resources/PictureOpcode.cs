using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi.Resources {
	/// <summary>Identifies most of the <see cref="PictureInstruction"/>s. If this is <see cref="ExtendedOpcode"/>, then the <see cref="PictureExtendedOpcode"/> identifies the actual instruction.</summary>
	public enum PictureOpcode {
		/// <summary>(byte ditherPaletteIndex) Set colors, then enable visual.</summary>
		SetColor = 0xF0,

		/// <summary>Stop drawing to the visual layer.</summary>
		DisableVisual = 0xF1,

		/// <summary>(byte priority) Set the priority value to darw.</summary>
		SetPriority = 0xF2,

		/// <summary>Stop drawing to the priority map.</summary>
		DisablePriority = 0xF3,

		/// <summary>Draw patterns.</summary>
		DrawRelativePatterns = 0xF4,

		/// <summary>Draw a line strip.</summary>
		DrawRelativeMediumLines = 0xF5,

		/// <summary>Draw a line strip.</summary>
		DrawRelativeLongLines = 0xF6,

		/// <summary>Draw a line strip.</summary>
		DrawRelativeShortLines = 0xF7,

		/// <summary>Draw floodfill.</summary>
		DrawFloodfill = 0xF8,

		/// <summary>Set the pattern flags.</summary>
		SetPatternFlags = 0xF9,

		/// <summary>Draw patterns.</summary>
		DrawAbsolutePatterns = 0xFA,

		/// <summary>Set the control code and enable the control map.</summary>
		SetControl = 0xFB,

		/// <summary>Disable the control map.</summary>
		DisableControl = 0xFC,

		/// <summary>Draw patterns.</summary>
		DrawRelativeMediumPatterns = 0xFD,

		/// <summary>Use an extended opcode, specified with <see cref="PictureExtendedOpcode"/>.</summary>
		ExtendedOpcode = 0xFE,

		/// <summary>Terminate drawing.</summary>
		Quit = 0xFF,
	}

	/// <summary>When a <see cref="PictureInstruction"/>'s <see cref="PictureInstruction.Opcode"/> is <see cref="PictureOpcode.ExtendedOpcode"/>, this specifies the actual opcode.</summary>
	public enum PictureExtendedOpcode {
		/// <summary>Set a dither palette entry.</summary>
		SetPaletteEntry = 0x00,

		/// <summary>Set a dither palette.</summary>
		SetPalette = 0x01,

		/// <summary>Unknown. Stored as (byte[41] Unknown).</summary>
		Mono0 = 0x02,

		/// <summary>Unknown. Stored as (byte Unknown).</summary>
		Mono1 = 0x03,

		/// <summary>Unknown. Stored as (byte Unknown).</summary>
		Mono2 = 0x04,

		/// <summary>Unknown. Stored as (byte Unknown).</summary>
		Mono3 = 0x05,

		/// <summary>Unknown.</summary>
		Mono4 = 0x06,

		/// <summary>Not implemented.</summary>
		EmbeddedView = 0x07,

		/// <summary>Not implemented.</summary>
		SetPriorityTable = 0x08,

		/// <summary>Used by this library to synthesize instructions to draw <see cref="PictureCel"/>s.</summary>
		DrawCel = 512,
	}
}
