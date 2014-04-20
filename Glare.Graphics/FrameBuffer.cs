using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	/// <summary>
	/// A configuration of <see cref="RenderBuffer"/> and <see cref="Texture"/> objects that can be rendered to.
	/// </summary>
	public class FrameBuffer : GraphicsResource {
		FrameBufferAttachmentCollection color;
		FrameBufferAttachment depth, stencil;

		/// <summary>Get the color attachment points.</summary>
		public FrameBufferAttachmentCollection Colors { get { return color; } }

		/// <summary>Get the depth attachment point.</summary>
		public FrameBufferAttachment Depth { get { return depth; } }

		/// <summary>Get the stencil attachment point.</summary>
		public FrameBufferAttachment Stencil { get { return stencil; } }

		public FrameBuffer()
			: base(AllocateId()) {
			FrameBufferAttachment[] color = new FrameBufferAttachment[Device.GetInt32(GetPName.MaxColorAttachments)];
			for (int index = 0; index < color.Length; index++)
				color[index] = new FrameBufferAttachment(this, FramebufferAttachment.ColorAttachment0 + index);
			this.color = new FrameBufferAttachmentCollection(color);
			this.depth = new FrameBufferAttachment(this, (FramebufferAttachment)FramebufferAttachment.DepthAttachment);
			this.stencil = new FrameBufferAttachment(this, (FramebufferAttachment)FramebufferAttachment.StencilAttachment);
		}

		static int AllocateId() {
			using (Context.Lock())
				return GL.GenFramebuffer();
		}

		protected override void DisposeBase() {
			GL.DeleteFramebuffer(Id);
		}

		internal int GLBind() {
			int current;
			GL.GetInteger(GetPName.DrawFramebufferBinding, out current);
			GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, Id);
			return current;
		}

		internal FrameBufferBinding Bind() { return new FrameBufferBinding(this); }

		internal void GLUnbind(int old) {
			GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, old);
		}
	}

	internal struct FrameBufferBinding : IDisposable {
		FrameBuffer buffer;
		int bound;

		public FrameBufferBinding(FrameBuffer buffer) {
			this.buffer = buffer;
			bound = buffer.GLBind();
		}

		public void Dispose() {
			try { Context.CheckError(); } finally { buffer.GLUnbind(bound); }
		}
	}
}
