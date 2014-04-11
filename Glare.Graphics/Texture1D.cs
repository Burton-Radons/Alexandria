using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	public class Texture1D : FlatTexture
	{
		protected override TextureSurface BaseSurface
		{
			get { throw new NotImplementedException(); }
		}

		protected internal override TextureTarget Target { get { return TextureTarget.Texture1D; } }
		protected internal override GetPName TargetBinding { get { return GetPName.TextureBinding1DArray; } }

		public Texture1D() : base() { }

		public override void Storage(int levels, Format format, Vector4i dimensions)
		{
			throw new NotImplementedException();
		}
	}
}
