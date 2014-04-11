using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	public class Texture3D : LayeredTexture
	{
		protected internal override TextureTarget Target { get { return TextureTarget.Texture3D; } }
		protected internal override GetPName TargetBinding { get { return GetPName.TextureBinding3D; } }

		public Texture3D() : base() { }

		public override void Storage(int levels, Format format, Vector4i dimensions)
		{
			throw new NotImplementedException();
		}
	}
}
