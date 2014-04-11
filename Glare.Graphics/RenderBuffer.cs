using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	public class RenderBuffer : GraphicsResource {
		public RenderBuffer()
			: base(AllocateId()) {
		}

		static int AllocateId() {
			using (Context.Lock())
				return GL.GenRenderbuffer();
		}

		protected override void DisposeBase() {
			GL.DeleteRenderbuffer(Id);
		}
	}
}
