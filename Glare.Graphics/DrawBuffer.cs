using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	public class DrawBuffer
	{
		readonly Context platform;
		readonly int index;

		BlendState blend = BlendStates.Opaque;

		/// <summary>Get or set the blending state for this <see cref="DrawBuffer"/>. The default is <see cref="BlendStates.Opaque"/>.</summary>
		public BlendState Blend
		{
			get { return blend; }

			set
			{
				using (Device.Lock())
				{
					GL.BlendEquationSeparate(index, (BlendEquationMode)blend.EquationRgb, (BlendEquationMode)blend.EquationAlpha);
					Device.CheckError();
					GL.BlendFuncSeparate(index, (BlendingFactorSrc)blend.SourceRgb, (BlendingFactorDest)blend.DestinationRgb, (BlendingFactorSrc)blend.SourceAlpha, (BlendingFactorDest)blend.DestinationAlpha);
					Device.CheckError();
					GL.Enable(EnableCap.Blend);
				}
			}
		}

		internal DrawBuffer(Context platform, int index)
		{
			this.platform = platform;
			this.index = index;
		}
	}

	public class DrawBufferCollection : ReadOnlyCollection<DrawBuffer>
	{
		internal DrawBufferCollection(IList<DrawBuffer> list) : base(list) { }
	}
}
