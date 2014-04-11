using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	public class TextureCube : LayeredTexture
	{
		protected internal override TextureTarget Target { get { return TextureTarget.TextureCubeMap; } }
		protected internal override GetPName TargetBinding { get { return GetPName.TextureBindingCubeMap; } }

		public TextureCube() { }

		public override void Storage(int levels, Format format, Vector4i dimensions)
		{
			throw new NotImplementedException();
		}
	}
}
