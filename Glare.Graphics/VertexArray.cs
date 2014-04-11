using Glare.Graphics.Internal;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	public class VertexArray : GraphicsResource
	{
		readonly Program program;

		public Program Program { get { return program; } }

		public VertexArray(Program program) : base(AllocateId()) {
			this.program = program;
		}

		public void Bind(ProgramAttribute attribute, GraphicsBuffer buffer, int offsetInBytes, Format format, int stride)
		{
			if (attribute == null)
				throw new ArgumentNullException("attribute");
			throw new NotImplementedException();
			//GLExt.VertexAttribFormat(attribute.Index, format.ComponentCount, format.VertexAttribPonterType, format.IsNormalized, offsetInBytes);
		}
			/*
	public readonly ModelAttributeChannel Channel;

		/// <summary>Get the buffer to bind this to.</summary>
		public readonly GraphicsBuffer Buffer;

		/// <summary>Get the zero-based index of the <see cref="Channel"/> to bind to.</summary>
		public readonly int Index;

		/// <summary>Get the name of the channel to bind to,</summary>
		public readonly string Name;

		/// <summary>Get the offset in bytes from the start of the <see cref="Buffer"/> to the attribute of the first vertex.</summary>
		public readonly int OffsetInBytes;

		/// <summary>Get the data format of the attribute. This must be a member of <see cref="VectorFormats"/>.</summary>
		public readonly Format Format;

		/// <summary>Get the number of bytes between each attribute value, or 0 to use the size of the <see cref="Format"/>.</summary>
		public readonly int Stride;*/

		static int AllocateId()
		{
			using (Context.Lock())
				return GL.GenVertexArray();
		}

		protected override void DisposeBase()
		{
			GL.DeleteVertexArray(Id);
		}

		internal VertexArrayLock Lock() { return new VertexArrayLock(this); }


	}

	struct VertexArrayLock : IDisposable
	{
		ContextLock context;
		VertexArray array;
		int was;

		internal VertexArrayLock(VertexArray array)
		{
			this.context = Context.Lock();
			this.array = array;
			GL.GetInteger(GetPName.VertexArrayBinding, out was);
			GL.BindVertexArray(array.Id);
		}

		public void Dispose()
		{
			try
			{
				Context.CheckError();
			}
			finally
			{
				GL.BindVertexArray(array.Id);
				context.Dispose();
			}
		}
	}
}
