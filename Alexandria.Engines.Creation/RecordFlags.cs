using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation {
	[Flags]
	public enum RecordFlags : uint {
		/// <summary>Master (ESM) file; TES4 only.</summary>
		Master = 0x1,

		Deleted = 0x20,

		/// <summary>Constant value. For REFR, seems to be hidden from local map - needs confirmation, related to shields.</summary>
		Constant = 0x40,

		/// <summary>TES4 record only. This makes Skyrim load the .STRINGS, .DLSTRINGS, and .ILSTRINGS files associated with the mod. If the flag is not set, lstrings are treated as zstrings. Also used for "is perch" and for PHZD to turn off fire.</summary>
		Localized = 0x80,

		/// <summary>For REFR: Inaccessible.</summary>
		MustUpdateAnims = 0x100,

		RefrHiddenFromLocalMap = 0x200,

		AchrStartsDead = 0x200,

		RefrMotionBlurCastsShadows = 0x200,

		QuestItem = 0x400,
		PersistentReference = 0x400,
		LscrDisplaysInMainMenu = 0x400,

		InitiallyDisabled = 0x800,

		Ignored = 0x1000,

		VisibleWhenDistant = 0x8000,

		ActiRandomAnimationStart = 0x8000,

		/// <summary>(ACTI) Dangerous / Off limits (interior cell)</summary>
		ActiDangerous = 0x20000,

		Compressed = 0x40000,

		CantWait = 0x80000,

		ActiIgnoreObjectInteraction = 0x100000,

		IsMarker = 0x800000,

		/// <summary>(ACTI) Obstacle / (REFR) No AI Acquire</summary>
		ActiObstacle = 0x2000000,

		RefrNoAiAcquire = 0x2000000,

		NavMeshGenFilter = 0x04000000,

		NavMeshGenBoundingBox = 0x08000000,

		FurnMustExitToTalk = 0x10000000,

		RefrReflectedByAutoWater = 0x10000000,

		FurnIdlmChildCanUse = 0x20000000,

		RefrDontHavokSettle = 0x20000000,

		NavMeshGenGround = 0x40000000,

		RefrNoRespawn = 0x40000000,

		RefrMultiBound = 0x80000000,
	}

}
