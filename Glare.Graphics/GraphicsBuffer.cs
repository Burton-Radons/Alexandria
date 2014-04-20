using Glare.Framework;
using Glare.Graphics.Internal;
using Glare.Internal;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	/// <summary>
	/// A buffer object. This is named <see cref="GraphicsBuffer"/> and not Buffer due to the collision with System.Buffer.
	/// </summary>
	public class GraphicsBuffer : BaseBuffer {
		protected internal override BufferTarget Target { get { return BufferTarget.CopyReadBuffer; } }

		/// <summary>Create a new, empty <see cref="GraphicsBuffer"/>.</summary>
		public GraphicsBuffer() : base() { }

		/// <summary>Create a new <see cref="GraphicsBuffer"/> initialised to a specific size using <see cref="Storage"/>.</summary>
		/// <param name="context">The <see cref="Context"/> this uses. This must not be <c>null</c>.</param>
		/// <param name="size">The size in bytes of the <see cref="GraphicsBuffer"/>'s data.</param>
		/// <param name="usage">How the <see cref="GraphicsBuffer"/> is to be used.</param>
		public GraphicsBuffer(long size, BufferUsage usage = BufferUsage.StaticDraw) : base(size, usage) { }

		/// <summary>Create a <see cref="GraphicsBuffer"/> with the initial data.</summary>
		/// <typeparam name="T">The type of the elements. The type must contain structures only.</typeparam>
		/// <param name="platform">The <see cref="Context"/> to use.</param>
		/// <param name="data">The data to copy to the GL before returning, or <c>null</c> to keep it uninitialised.</param>
		/// <param name="first">The index of the first element to copy from <paramref name="data"/>.</param>
		/// <param name="count">The number of elements of <paramref name="data"/> to copy, and the length in <typeparamref name="T"/> type elements of the <see cref="GraphicsBuffer"/>.</param>
		/// <param name="usage">A hint for how the <see cref="GraphicsBuffer"/> will be used.</param>
		/// <returns>The new buffer object.</returns>
		public static GraphicsBuffer Create<T>(T[] data, int first, int count, BufferUsage usage = BufferUsage.StaticDraw) where T : struct {
			var buffer = new GraphicsBuffer();
			buffer.Data(data, first, count, usage);
			return buffer;
		}

		public static GraphicsBuffer Create<T>(T[] data, BufferUsage usage = BufferUsage.StaticDraw) where T : struct {
			if (data == null)
				throw new ArgumentNullException("data");
			return Create(data, 0, data.Length, usage);
		}

		public static GraphicsBuffer Create<T>(BufferUsage usage, params T[] data) where T : struct { return Create(data, usage); }
		public static GraphicsBuffer Create<T>(params T[] data) where T : struct { return Create(BufferUsage.StaticDraw, data); }

		public static GraphicsBuffer Create<T>(int count, BufferUsage usage = BufferUsage.StaticDraw) where T : struct {
			return new GraphicsBuffer(count * Marshal.SizeOf(typeof(T)), usage);
		}
	}

	public class DynamicBuffer<T> : RichList<T> where T : struct {
		static readonly int size = Marshal.SizeOf(typeof(T));
		GraphicsBuffer buffer;
		BufferUsage usage = BufferUsage.DynamicDraw;
		int dirtyStart = -1;
		int dirtyEnd;

		/// <summary>Update the <see cref="GraphicsBuffer"/> with any changes to the collection, and then return it.</summary>
		public GraphicsBuffer Buffer {
			get {
				Flush();
				return buffer;
			}
		}

		public DynamicBuffer() {
			buffer = new GraphicsBuffer();
		}

		public DynamicBuffer(int capacity)
			: base(capacity) {
			buffer = new GraphicsBuffer();
		}

		public DynamicBuffer(params T[] collection) : base(collection) { }

		public DynamicBuffer(IEnumerable<T> collection) : base(collection) { }

		public override void Add(T item) {
			base.Add(item);
		}

		void AddDirty(int start, int end) {
			if (dirtyStart < 0) {
				dirtyStart = start;
				dirtyEnd = end;
			} else {
				dirtyStart = Math.Min(dirtyStart, start);
				dirtyEnd = Math.Max(dirtyEnd, end);
			}
		}

		public override void Clear() {
			base.Clear();
			dirtyStart = dirtyEnd = -1;
		}

		public override void ClearFast() {
			base.ClearFast();
			dirtyStart = dirtyEnd = -1;
		}

		/// <summary>
		/// Update the <see cref="Buffer"/> with any changes. Note that this is also done by accessing the <see cref="Buffer"/> property.
		/// </summary>
		public void Flush() {
			if (dirtyStart < 0)
				return;

			if (Count * size > buffer.Size) {
				buffer.Storage(size * 3 / 2, usage);
				buffer.Write(0, Array, 0, Count);
			} else
				buffer.Write(dirtyStart * size, Array, dirtyStart, dirtyEnd - dirtyStart);

			dirtyStart = dirtyEnd = -1;
		}

		public override void Insert(int index, T item) {
			base.Insert(index, item);
		}

		public override void RemoveAt(int index) {
			base.RemoveAt(index);
		}
	}
}
