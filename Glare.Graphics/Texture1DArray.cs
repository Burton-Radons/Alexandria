using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	public class Texture1DArray : FlatTexture
	{
		protected override TextureSurface BaseSurface
		{
			get { throw new NotImplementedException(); }
		}

		protected internal override TextureTarget Target { get { return TextureTarget.Texture1DArray; } }
		protected internal override GetPName TargetBinding { get { return GetPName.TextureBinding2D; } }

		public Texture1DArray() : base() { }

		public override void Storage(int levels, Format format, Vector4i dimensions)
		{
			throw new NotImplementedException();
		}
	}
}
