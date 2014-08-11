using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	public class ProgramAttribute : ProgramObject {
		readonly Format format;
		readonly int count;

		GraphicsBuffer bindBuffer;
		int bindOffsetInBytes;
		Format bindFormat;
		int bindStride;

		/// <summary>Get the number of elements of type <see cref="Format"/> that are in the attribute.</summary>
		public int Count { get { return count; } }

		/// <summary>Get the format of the attribute.</summary>
		public Format Format { get { return format; } }

		internal ProgramAttribute(Program program, string name, int index, int count, ActiveAttribType type)
			: base(program, name, index) {
			this.format = Format.From(type);
			this.count = count;
			bindFormat = Formats.Vector4nb;
		}

		public void Bind(GraphicsBuffer buffer, int offsetInBytes, Format format, int stride) {
			VertexAttribPointerType? type = format.VertexAttribPointerType;

			if (buffer == null)
				throw new ArgumentNullException("buffer");
			if (!type.HasValue)
				throw new Exception("This is not a valid " + typeof(Format).Name + " to Bind to a " + GetType().Name + ".");
			if (stride < 0)
				throw new ArgumentOutOfRangeException("stride");

			bindBuffer = buffer;
			bindOffsetInBytes = offsetInBytes;
			bindFormat = format;
			bindStride = stride;
			if (object.ReferenceEquals(Program, Device.Program))
				DoBind();
		}

		public void BindArray<T>(Format format, params T[] vertices) where T : struct {
			var buffer = GraphicsBuffer.Create(vertices);
			Bind(buffer, 0, format, 0);
		}

		public void BindArray(params double[] vertices) { BindArray(Formats.Vector1d, vertices); }
		public void BindArray(params Vector2d[] vertices) { BindArray(Formats.Vector2d, vertices); }
		public void BindArray(params Vector3d[] vertices) { BindArray(Formats.Vector3d, vertices); }
		public void BindArray(params Vector4d[] vertices) { BindArray(Formats.Vector4d, vertices); }

		public void BindArray(params float[] vertices) { BindArray(Formats.Vector1f, vertices); }
		public void BindArray(params Vector2f[] vertices) { BindArray(Formats.Vector2f, vertices); }
		public void BindArray(params Vector3f[] vertices) { BindArray(Formats.Vector3f, vertices); }
		public void BindArray(params Vector4f[] vertices) { BindArray(Formats.Vector4f, vertices); }

		public void BindArray(params int[] vertices) { BindArray(Formats.Vector1i, vertices); }
		public void BindArray(params Vector2i[] vertices) { BindArray(Formats.Vector2i, vertices); }
		public void BindArray(params Vector3i[] vertices) { BindArray(Formats.Vector3i, vertices); }
		public void BindArray(params Vector4i[] vertices) { BindArray(Formats.Vector4i, vertices); }


		void ClearBind() {
			bindBuffer = null;
			bindOffsetInBytes = 0;
			bindStride = 0;
			bindFormat = Formats.Vector4nb;
		}

		internal void DoBind() {
			if (bindBuffer == null) {
				GL.DisableVertexAttribArray(index);
				Context.CheckError();
			} else {
				GL.EnableVertexAttribArray(index);
				Context.CheckError();
				GL.BindBuffer(BufferTarget.ArrayBuffer, bindBuffer.Id);
				Context.CheckError();
				GL.VertexAttribPointer(index, bindFormat.Channels.RowCount, bindFormat.VertexAttribPointerType.Value, bindFormat.IsNormalized, bindStride, bindOffsetInBytes);
				Context.CheckError();
			}
		}
	}
}
