using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	/// <summary>
	/// A <see cref="FrameBuffer"/> attachment point.
	/// </summary>
	public class FrameBufferAttachment
	{
		readonly FrameBuffer frameBuffer;
		readonly FramebufferAttachment attachment;

		internal FrameBufferAttachment(FrameBuffer frameBuffer, FramebufferAttachment attachment)
		{
			this.frameBuffer = frameBuffer;
			this.attachment = attachment;
		}

		public void Attach(Texture1D texture, int level = 0)
		{
			using(frameBuffer.Bind())
				GL.FramebufferTexture1D(FramebufferTarget.DrawFramebuffer, attachment, TextureTarget.Texture1D, texture.Id, level);
		}

		public void Attach(Texture2D texture, int level = 0)
		{
			using(frameBuffer.Bind())
				GL.FramebufferTexture2D(FramebufferTarget.DrawFramebuffer, attachment, TextureTarget.Texture2D, texture.Id, level);
		}

		public void Attach(Texture3D texture, int layer, int level = 0) { AttachLayer(texture.Id, layer, level); }

		public void Attach(Texture2DArray texture, int layer, int level = 0) { AttachLayer(texture.Id, layer, level); }

		public void Attach(Texture texture, int layer, int level = 0)
		{
			switch (texture.Target)
			{
				case TextureTarget.Texture1D:
					if (layer != 0)
						throw new ArgumentOutOfRangeException("layer");
					Attach((Texture1D)texture, level);
					break;

				case TextureTarget.Texture2D:
					if (layer != 0)
						throw new ArgumentOutOfRangeException("layer");
					Attach((Texture2D)texture, level);
					break;

				case TextureTarget.Texture3D:
				case TextureTarget.Texture2DArray:
					AttachLayer(texture.Id, layer, level);
					break;

				default:
					throw new NotImplementedException();
			}
		}

		public void Attach(TextureSurface surface, int level = 0)
		{
			switch (surface.Texture.Target)
			{
				case TextureTarget.Texture1D: Attach((Texture1D)surface.Texture, level); break;
				case TextureTarget.Texture2D: Attach((Texture2D)surface.Texture, level); break;
				default: throw new NotImplementedException();
			}
		}

		public void Attach(TextureLevel level) { Attach(level.Surface, level.Level); }

		void AttachLayer(int id, int layer, int level)
		{
			using(frameBuffer.Bind())
				GL.FramebufferTextureLayer(FramebufferTarget.DrawFramebuffer, attachment, id, level, layer);
		}
	}

	public class FrameBufferAttachmentCollection : ReadOnlyCollection<FrameBufferAttachment>
	{
		internal FrameBufferAttachmentCollection(IList<FrameBufferAttachment> values) : base(values) { }
	}
}

