using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	public class TextureCubeArray : LayeredTexture
	{
		protected internal override TextureTarget Target { get { return TextureTarget.TextureCubeMapArray; } }
		protected internal override GetPName TargetBinding { get { throw new Exception(); } }

		public TextureCubeArray() : base() { }

		public override void Storage(int levels, Format format, Vector4i dimensions)
		{
			throw new NotImplementedException();
		}
	}
}
