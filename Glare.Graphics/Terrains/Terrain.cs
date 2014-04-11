using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains
{
	public abstract class Terrain
	{
		readonly FrameBuffer frameBuffer;

		public FrameBuffer FrameBuffer { get { return frameBuffer; } }

		public Terrain()
		{
			this.frameBuffer = new FrameBuffer();
		}
	}
}
