using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi.Resources {
	public enum PictureOpcode {
		/// <summary>(byte ditherPaletteIndex) Set colors, then enable visual.</summary>
		SetColor = 0xF0,

		/// <summary>Stop drawing to the visual layer.</summary>
		DisableVisual = 0xF1,

		/// <summary>(byte priority) Set the priority value to darw.</summary>
		SetPriority = 0xF2,

		/// <summary>Stop drawing to the priority map.</summary>
		DisablePriority = 0xF3,

		DrawRelativePatterns = 0xF4,
		DrawRelativeMediumLines = 0xF5,
		DrawRelativeLongLines = 0xF6,
		DrawRelativeShortLines = 0xF7,
		DrawFloodfill = 0xF8,
		SetPatternFlags = 0xF9,
		DrawAbsolutePatterns = 0xFA,
		SetControl = 0xFB,
		DisableControl = 0xFC,
		DrawRelativeMediumPatterns = 0xFD,
		ExtendedOpcode = 0xFE,

		/// <summary>Terminate drawing.</summary>
		Quit = 0xFF,
	}

	public enum PictureExtendedOpcode {
		SetPaletteEntry = 0x00,
		SetPalette = 0x01,

		/// <summary>Unknown. Stored as (byte[41] Unknown).</summary>
		Mono0 = 0x02,

		/// <summary>Unknown. Stored as (byte Unknown).</summary>
		Mono1 = 0x03,

		/// <summary>Unknown. Stored as (byte Unknown).</summary>
		Mono2 = 0x04,

		/// <summary>Unknown. Stored as (byte Unknown).</summary>
		Mono3 = 0x05,

		Mono4 = 0x06,
		EmbeddedView = 0x07,
		SetPriorityTable = 0x08,
	}
}
