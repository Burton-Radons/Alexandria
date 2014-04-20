using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	public enum ResourceType : byte {
		/// <summary>In SCI1+ (type 0x80) these are 256-colour views (extension ".v56").</summary>
		View = 0x00,

		/// <summary>In SCI1+ (type 0x81) these are 256-colour background pictures (extension ".p56").</summary>
		Picture = 0x01,

		/// <summary>In SCI1+ (type 0x82) these have the extension ".scr".</summary>
		Script = 0x02,

		/// <summary>In SCI1+ (type 0x83) these are deprecated in favour of messages (extension ".tex").</summary>
		Text = 0x03,

		/// <summary>In SCI1+ (type 0x84) these are MIDI music (extension ".snd").</summary>
		Sound = 0x04,

		Memory = 0x05,

		/// <summary>In SCI1+ (type 0x86) these are not used (extension ".voc").</summary>
		Vocabulary = 0x06,

		/// <summary>In SCI1+ (type 0x87) these have the extension ".fon".</summary>
		Font = 0x07,

		/// <summary>In SCI1+ (type 0x88) these are deprecated in favour of <see cref="ViewSci1"/>-based cursors (extension ".cur").</summary>
		Cursor = 0x08,

		/// <summary>In SCI1+ (type 0x89) audio patches have the file extension ".pat".</summary>
		Patch = 0x09,

		/// <summary>In SCI1+ (type 0x8A) these have the file extension ".bit" and their purpose is unknown.</summary>
		Bitmap = 0x0A,

		/// <summary>In SCI1+ (type 0x8B) these are 256-colour palette files with file extension ".pal".</summary>
		Palette = 0x0B,

		/// <summary>In SCI1+ (type 0x8C) these are CD audio resources with file extension ".cda".</summary>
		CDAudio = 0x0C,

		/// <summary>In SCI1+ (type 0x8D) these have the file extension ".aud".</summary>
		Audio = 0x0D,

		/// <summary>In SCI1+ (type 0x8E) these have the file extension ".syn".</summary>
		Sync = 0x0E,

		/// <summary>In SCI1+ (type 0x8F) these have the file extension ".msg".</summary>
		Message = 0x0F,

		/// <summary>In SCI1+ (type 0x90) these have the file extension ".map".</summary>
		Map = 0x10,

		/// <summary>In SCI1+ (type 0x91) these have the file extension ".hep" and describe dynamic script data.</summary>
		Heap = 0x11,
		Audio36 = 0x12,
		Sync36 = 0x13,
		Unknown1 = 0x14,
		Unknown2 = 0x15,
		Robot = 0x16,
		Invalid = 0x17,

		/// <summary>A mask to isolate the <see cref="ResourceType"/> from the <see cref="Sci1Mask"/>.</summary>
		Mask = 0x7F,

		/// <summary>A mask to indicate that the <see cref="ResourceType"/> is for SCI1+.</summary>
		Sci1Mask = 0x80,

		End = 0xFF,
	}
}
