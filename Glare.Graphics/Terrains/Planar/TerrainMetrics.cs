using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar
{
	public struct TerrainMetrics
	{
		/// <summary>
		/// Get the total number of blocks that have been drawn.
		/// These are the 16x16 chunks of vertices that are applied at different resolutions.
		/// </summary>
		public int BlocksDrawn { get; internal set; }

		/// <summary>
		/// Get the total number of triangles drawn.
		/// If a <see cref="EffectTechnique"/> has multiple passes, each pass is counted.
		/// </summary>
		public int TrianglesDrawn { get; internal set; }

		public string Summary
		{
			get { return string.Format("{0} block(s) drawn, {1} triangle(s) drawn", BlocksDrawn, TrianglesDrawn); }
		}

		public void Reset()
		{
			TrianglesDrawn = 0;
			BlocksDrawn = 0;
		}
	}
}
