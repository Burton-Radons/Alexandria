using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains {
	public abstract class Terrain {
		readonly FrameBuffer frameBuffer;

		public FrameBuffer FrameBuffer { get { return frameBuffer; } }

		public Terrain() {
			this.frameBuffer = new FrameBuffer();
		}

		/// <summary>
		/// Update any data structures that have become out-of-date by modifications to the
		/// terrain. If the terrain is static, this is pointless (but harmless). This may
		/// cause the render target to change, which will clear the main buffer when reset.
		/// So don't call this in the middle of rendering.
		/// </summary>
		public virtual void Update() {
		}
	}
}
