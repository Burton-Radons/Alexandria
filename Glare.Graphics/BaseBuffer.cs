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
	public abstract class BaseBuffer : GraphicsResource {
		/// <summary>Get the size in bytes of the buffer.</summary>
		public long Size { get { return GetInt64(BufferParameterName.BufferSize); } }

		protected internal abstract BufferTarget Target { get; }

		/// <summary>Create a new, empty <see cref="GraphicsBuffer"/>.</summary>
		public BaseBuffer() : base(CreateId()) { }

		/// <summary>Create a new <see cref="GraphicsBuffer"/> initialised to a specific size using <see cref="Storage"/>.</summary>
		/// <param name="context">The <see cref="Context"/> this uses. This must not be <c>null</c>.</param>
		/// <param name="size">The size in bytes of the <see cref="GraphicsBuffer"/>'s data.</param>
		/// <param name="usage">How the <see cref="GraphicsBuffer"/> is to be used.</param>
		public BaseBuffer(long size, BufferUsage usage = BufferUsage.StaticDraw)
			: this() {
			Storage(size, usage);
		}

		public void CopyTo(long sourceOffsetInBytes, BaseBuffer target, long targetOffsetInBytes, long sizeInBytes) {
			if (target == null)
				throw new ArgumentNullException("target");

			long size = Size, targetSize = target.Size;

			if (sourceOffsetInBytes < 0 || sourceOffsetInBytes > size)
				throw new ArgumentOutOfRangeException("sourceOffsetInBytes");
			if (targetOffsetInBytes < 0 || targetOffsetInBytes > targetSize)
				throw new ArgumentOutOfRangeException("targetOffsetInBytes");
			if (sizeInBytes < 0 || sourceOffsetInBytes + sizeInBytes > size || targetOffsetInBytes + sizeInBytes > targetSize)
				throw new ArgumentOutOfRangeException("sizeInBytes");

			using (Lock()) {
				GL.BindBuffer(BufferTarget.CopyWriteBuffer, target.Id);
				GL.CopyBufferSubData(Target, BufferTarget.CopyWriteBuffer, new IntPtr(sourceOffsetInBytes), new IntPtr(targetOffsetInBytes), new IntPtr(sizeInBytes));
				GL.BindBuffer(BufferTarget.CopyWriteBuffer, 0);
			}
		}

		public void CopyTo<T>(long sourceOffset, GraphicsBuffer target, long targetOffset, long count) {
			int size = SizeOf<T>();
			CopyTo(checked(size * sourceOffset), target, checked(targetOffset * size), checked(size * count));
		}

		static int CreateId() {
			using (Context.Lock())
				return GL.GenBuffer();
		}

		protected override void DisposeBase() {
			if (Context.Shared.MakeCurrent())
				GL.DeleteBuffer(Id);
		}

		internal long GetInt32(BufferParameterName pname) { int result; using (Lock()) GL.GetBufferParameter(Target, pname, out result); return result; }
		internal long GetInt64(BufferParameterName pname) { long result; using (Lock()) GL.GetBufferParameter(Target, pname, out result); return result; }

		internal BufferLock Lock() { return new BufferLock(this); }

		/// <summary>Specify the data for the buffer, completely overwriting anything that was already present.</summary>
		/// <typeparam name="T">The type of the elements. The type must contain structures only.</typeparam>
		/// <param name="data">The data to copy to the GL before returning, or <c>null</c> to keep it uninitialised.</param>
		/// <param name="first">The index of the first element to copy from <paramref name="data"/>.</param>
		/// <param name="count">The number of elements of <paramref name="data"/> to copy, and the length in <typeparamref name="T"/> type elements of the <see cref="GraphicsBuffer"/>.</param>
		/// <param name="usage">A hint for how the <see cref="GraphicsBuffer"/> will be used.</param>
		public void Data<T>(T[] data, int first, int count, BufferUsage usage = BufferUsage.StaticDraw) where T : struct {
			if (data == null) {
				if (first != 0)
					throw new ArgumentOutOfRangeException("first");
			} else {
				if (first < 0 || first > data.Length)
					throw new ArgumentOutOfRangeException("first");
				if (first + count > data.Length)
					throw new ArgumentOutOfRangeException("length");
			}

			var size = new IntPtr(count * Marshal.SizeOf(typeof(T)));

			if (data != null) {
				GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);

				try {
					IntPtr pointer = Marshal.UnsafeAddrOfPinnedArrayElement(data, first);
					Data(pointer, size, usage);
				} finally {
					handle.Free();
				}
			} else
				Data(IntPtr.Zero, size, usage);
		}

		/// <summary>Specify the data for the buffer, completely overwriting anything that was already present.</summary>
		/// <typeparam name="T">The type of the elements. The type must contain structures only.</typeparam>
		/// <param name="data">The data to copy to the GL before returning.</param>
		/// <param name="usage">A hint for how the <see cref="GraphicsBuffer"/> will be used.</param>
		public void Data<T>(T[] data, BufferUsage usage = BufferUsage.StaticDraw) where T : struct {
			if (data == null)
				throw new ArgumentNullException("data");
			Data(data, 0, data.Length, usage);
		}

		public void Data(IntPtr data, IntPtr size, BufferUsage usage = BufferUsage.StaticDraw) {
			using (Lock())
				GL.BufferData(Target, size, data, (BufferUsageHint)usage);
		}

		public void Data<T>(ref T data, IntPtr size, BufferUsage usage = BufferUsage.StaticDraw) where T : struct {
			using (Lock())
				GL.BufferData(Target, size, ref data, (BufferUsageHint)usage);
		}

		public void Data<T>(T[,] data, IntPtr size, BufferUsage usage) where T : struct {
			using (Lock())
				GL.BufferData(Target, size, data, (BufferUsageHint)usage);
		}

		public void Data<T>(T[,] data, BufferUsage usage) where T : struct {
			if (data == null)
				throw new ArgumentNullException("data");
			Data(data, new IntPtr(checked(Marshal.SizeOf(typeof(T)) * data.GetLongLength(0) * data.GetLongLength(1))), usage);
		}

		public void Data<T>(T[, ,] data, IntPtr size, BufferUsage usage) where T : struct {
			using (Lock())
				GL.BufferData(Target, size, data, (BufferUsageHint)usage);
		}

		public void Data<T>(T[, ,] data, BufferUsage usage) where T : struct {
			if (data == null)
				throw new ArgumentNullException("data");
			Data(data, new IntPtr(checked(Marshal.SizeOf(typeof(T)) * data.GetLongLength(0) * data.GetLongLength(1) * data.GetLongLength(2))), usage);
		}

		public BufferMap<T> Map<T>(int first, int count, BufferMapAccess access) where T : struct {
			long thisCount = Size / Marshal.SizeOf(typeof(T));

			if (first < 0 || first > thisCount)
				throw new ArgumentOutOfRangeException("first");
			if (count < 0 || first + count > thisCount)
				throw new ArgumentOutOfRangeException("count");
			return new BufferMap<T>(this, first, count, (BufferAccessMask)access);
		}

		public void Read<T>(long offsetInBytes, T[] data, long first, long count) {
			if (data == null)
				throw new ArgumentNullException("data");
			if (first < 0 || first > data.LongLength)
				throw new ArgumentOutOfRangeException("first");
			if (count < 0 || first + count > data.LongLength)
				throw new ArgumentOutOfRangeException("count");

			var size = SizeOf<T>();
			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);

			try {
				IntPtr pointer = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
				Read(offsetInBytes, new IntPtr(checked(pointer.ToInt64() + first * size)), checked(count * size));
			} finally {
				handle.Free();
			}
		}

		/// <summary>Read a section of the <see cref="GraphicsBuffer"/>. Use <see cref="ReadAt{T}(long,long)"/> to make <paramref name="offsetInBytes"/> into an index of <typeparamref name="T"/> instead.</summary>
		/// <typeparam name="T">The type of the object to read; it must be a struct.</typeparam>
		/// <param name="offsetInBytes">The offset in bytes from the start of the <see cref="GraphicsBuffer"/> to start writing at.</param>
		/// <param name="data">The data to read from the <see cref="GraphicsBuffer"/>.</param>
		public void Read<T>(long offsetInBytes, T[] data) where T : struct {
			if (data == null)
				throw new ArgumentNullException("data");
			Read(offsetInBytes, data, 0, data.Length);
		}

		public void Read(long offsetInBytes, IntPtr data, long size) {
			using (Lock())
				GL.GetBufferSubData(Target, new IntPtr(offsetInBytes), new IntPtr(size), data);
		}

		public void Read<T>(long offsetInBytes, ref T data, long sizeInBytes) where T : struct {
			using (Lock())
				GL.BufferSubData(Target, new IntPtr(offsetInBytes), new IntPtr(sizeInBytes), ref data);
		}

		public void Read<T>(long offsetInBytes, T[, ,] data, long count) where T : struct {
			using (Lock())
				GL.BufferSubData(Target, new IntPtr(offsetInBytes), new IntPtr(SizeOf<T>(count)), data);
		}

		public void Read<T>(long offsetInBytes, T[, ,] data) where T : struct {
			Read(offsetInBytes, data, checked(data.GetLongLength(0) * data.GetLongLength(1) * data.GetLongLength(2)));
		}

		public void Read<T>(long offsetInBytes, T[,] data, long count) where T : struct {
			using (Lock())
				GL.BufferSubData(Target, new IntPtr(offsetInBytes), new IntPtr(SizeOf<T>(count)), data);
		}

		public void Read<T>(long offsetInBytes, T[,] data) where T : struct {
			Read(offsetInBytes, data, checked(data.GetLongLength(0) * data.GetLongLength(1)));
		}

		public T[] Read<T>(long offsetInBytes, long count) where T : struct {
			T[] data = new T[count];
			Read(offsetInBytes, data, 0, count);
			return data;
		}

		/// <summary>Read the entire <see cref="GraphicsBuffer"/> as an array.</summary>
		/// <typeparam name="T">The type of the elements to read.</typeparam>
		/// <returns>The array data.</returns>
		public T[] Read<T>() where T : struct { return Read<T>(0, Size / SizeOf<T>()); }

		public void ReadAt<T>(long index, T[] data, long first, long count) where T : struct { Read<T>(SizeOf<T>(index), data, first, count); }
		public void ReadAt<T>(long index, T[] data) where T : struct { Read<T>(SizeOf<T>(index), data); }
		public void ReadAt<T>(long index, T[, ,] data, long count) where T : struct { Read<T>(SizeOf<T>(index), data, count); }
		public void ReadAt<T>(long index, T[, ,] data) where T : struct { Read<T>(SizeOf<T>(index), data); }
		public void ReadAt<T>(long index, T[,] data, long count) where T : struct { Read<T>(SizeOf<T>(index), data, count); }
		public void ReadAt<T>(long index, T[,] data) where T : struct { Read<T>(SizeOf<T>(index), data); }
		public T[] ReadAt<T>(long index, long count) where T : struct { return Read<T>(SizeOf<T>(index), count); }

		static int SizeOf<T>() { return Marshal.SizeOf(typeof(T)); }
		static long SizeOf<T>(long count) { return checked(count * SizeOf<T>()); }

		/// <summary>Initialise the buffer to a given size without providing any initial data.</summary>
		/// <param name="size">The size in bytes for the <see cref="GraphicsBuffer"/>.</param>
		/// <param name="usage"></param>
		public void Storage(long size, BufferUsage usage = BufferUsage.StaticDraw) {
			using (Lock())
				GL.BufferData(Target, new IntPtr(size), IntPtr.Zero, (BufferUsageHint)usage);
		}

		/// <summary>Initialise the buffer to a given size without providing any initial data.</summary>
		/// <typeparam name="T">The type of the elements of the <see cref="GraphicsBuffer"/>.</typeparam>
		/// <param name="count">The number of elements in the <see cref="GraphicsBuffer"/>.</param>
		/// <param name="usage">A hint for how the <see cref="GraphicsBuffer"/> will be used.</param>
		public void Storage<T>(long count, BufferUsage usage = BufferUsage.StaticDraw) where T : struct {
			Storage(SizeOf<T>(count), usage);
		}

		/// <summary>Write to a section of the <see cref="GraphicsBuffer"/> that has already been initialised with <see cref="Data"/> or <see cref="Storage"/>. Use <see cref="WriteAt&lt;T&gt;(long,T[],long,long)"/> to make <paramref name="offsetInBytes"/> into an index of <typeparamref name="T"/> instead.</summary>
		/// <typeparam name="T">The type of the object to write; it must be a struct.</typeparam>
		/// <param name="offsetInBytes">The offset in bytes from the start of the <see cref="GraphicsBuffer"/> to start writing at.</param>
		/// <param name="data">The data to write to the <see cref="GraphicsBuffer"/>.</param>
		/// <param name="first">The index of the first value in <paramref name="data"/> to write to the <see cref="GraphicsBuffer"/>.</param>
		/// <param name="count">The number of elements of <paramref name="data"/> to write to the <see cref="GraphicsBuffer"/>.</param>
		public void Write<T>(long offsetInBytes, T[] data, long first, long count) where T : struct {
			if (data == null)
				throw new ArgumentNullException("data");
			if (first < 0 || first > data.LongLength)
				throw new ArgumentOutOfRangeException("first");
			if (count < 0 || first + count > data.LongLength)
				throw new ArgumentOutOfRangeException("count");

			var size = SizeOf<T>();
			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);

			try {
				IntPtr pointer = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
				Write(offsetInBytes, new IntPtr(checked(pointer.ToInt64() + first * size)), checked(count * size));
			} finally {
				handle.Free();
			}
		}

		/// <summary>Write to a section of the <see cref="GraphicsBuffer"/> that has already been initialised with <see cref="Data"/> or <see cref="Storage"/>. Use <see cref="WriteAt&lt;T&gt;(long, T[])"/> to make <paramref name="offsetInBytes"/> into an index of <typeparamref name="T"/> instead.</summary>
		/// <typeparam name="T">The type of the object to write; it must be a struct.</typeparam>
		/// <param name="offsetInBytes">The offset in bytes from the start of the <see cref="GraphicsBuffer"/> to start writing at.</param>
		/// <param name="data">The data to write to the <see cref="GraphicsBuffer"/>.</param>
		public void Write<T>(long offsetInBytes, T[] data) where T : struct {
			if (data == null)
				throw new ArgumentNullException("data");
			Write(offsetInBytes, data, 0, data.Length);
		}

		/// <summary>Write to a section of the <see cref="GraphicsBuffer"/> that has already been initialised with <see cref="Data"/> or <see cref="Storage"/>.</summary>
		/// <param name="offsetInBytes"></param>
		/// <param name="data"></param>
		/// <param name="size"></param>
		public void Write(long offsetInBytes, IntPtr data, long size) {
			using (Lock())
				GL.BufferSubData(Target, new IntPtr(offsetInBytes), new IntPtr(size), data);
		}

		public void Write<T>(long offsetInBytes, ref T data, long sizeInBytes) where T : struct {
			using (Lock())
				GL.BufferSubData(Target, new IntPtr(offsetInBytes), new IntPtr(sizeInBytes), ref data);
		}

		public void Write<T>(long offsetInBytes, T[, ,] data, long count) where T : struct {
			using (Lock())
				GL.BufferSubData(Target, new IntPtr(offsetInBytes), new IntPtr(SizeOf<T>(count)), data);
		}

		public void Write<T>(long offsetInBytes, T[, ,] data) where T : struct {
			Write(offsetInBytes, data, checked(data.GetLongLength(0) * data.GetLongLength(1) * data.GetLongLength(2)));
		}

		public void Write<T>(long offsetInBytes, T[,] data, long count) where T : struct {
			using (Lock())
				GL.BufferSubData(Target, new IntPtr(offsetInBytes), new IntPtr(SizeOf<T>(count)), data);
		}

		public void Write<T>(long offsetInBytes, T[,] data) where T : struct {
			Write(offsetInBytes, data, checked(data.GetLongLength(0) * data.GetLongLength(1)));
		}

		public void WriteAt<T>(long index, T[] data, long first, long count) where T : struct { Write(SizeOf<T>(index), data, first, count); }
		public void WriteAt<T>(long index, T[] data) where T : struct { Write(SizeOf<T>(index), data); }
		public void WriteAt<T>(long index, T[, ,] data, long count) where T : struct { Write(SizeOf<T>(index), data, count); }
		public void WriteAt<T>(long index, T[, ,] data) where T : struct { Write(SizeOf<T>(index), data); }
		public void WriteAt<T>(long index, T[,] data, long count) where T : struct { Write(SizeOf<T>(index), data, count); }
		public void WriteAt<T>(long index, T[,] data) where T : struct { Write(SizeOf<T>(index), data); }
	}

	[Flags]
	public enum BufferMapAccess {
		/// <summary>The <see cref="GraphicsBuffer"/> data can be read from without error.</summary>
		Read = BufferAccessMask.MapReadBit,

		/// <summary>The <see cref="GraphicsBuffer"/> data can be written to without error.</summary>
		Write = BufferAccessMask.MapWriteBit,

		/// <summary>A combination of <see cref="Read"/> and <see cref="Write"/>.</summary>
		ReadWrite = Read | Write,

		/// <summary>The previous contents of the <see cref="GraphicsBuffer"/> may be discarded, with the assumption that the user will write to the data while mapped. This may not be used in combination with <see cref="Read"/>. This automatically includes <see cref="Write"/> permissions.</summary>
		InvalidateBuffer = BufferAccessMask.MapInvalidateBufferBit | Write,

		/// <summary>The previous contents of the range may be discarded, with the assumption that the user will write to the data while mapped.  This may not be used in combination with <see cref="Read"/>. This automatically includes <see cref="Write"/> permissions.</summary>
		InvalidateRange = BufferAccessMask.MapInvalidateRangeBit | Write,

		/// <summary>One or more discrete subranges of the mapping may be modified. <see cref="BufferMap{T}.Flush"/> must be called to explicitly flush modifications. This automatically includes <see cref="Write"/> permissions.</summary>
		FlushExplicit = BufferAccessMask.MapFlushExplicitBit | Write,

		/// <summary>The GL should not attempt to synchronize pending operations on the buffer before mapping it. No error is generated if pending operations which source or modify the buffer overlap the mapped region, but the result of such previous and any subsequent operations is undefined.</summary>
		Unsynchronized = BufferAccessMask.MapUnsynchronizedBit,
	}

	/// <summary>
	/// A mapped <see cref="GraphicsBuffer"/> object.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public struct BufferMap<T> : IDisposable, IList<T> where T : struct {
		static readonly int Size = Marshal.SizeOf(typeof(T));
		static readonly EqualityComparer<T> EqualityComparer = EqualityComparer<T>.Default;

		readonly BaseBuffer buffer;
		readonly IntPtr pointer;
		readonly int count;
		readonly IntPtr offset;

		public int Count { get { return count; } }

		public T this[int index] {
			get { return (T)Marshal.PtrToStructure(PointerAt(index), typeof(T)); }
			set { Marshal.StructureToPtr(value, PointerAt(index), false); }
		}

		internal BufferMap(BaseBuffer buffer, int first, int count, BufferAccessMask access) {
			this.buffer = buffer;
			this.count = count;
			this.offset = new IntPtr(first * Size);

			if (buffer.GetInt32(BufferParameterName.BufferMapped) != 0)
				throw new Exception("The buffer is already mapped.");
			using (buffer.Lock())
				this.pointer = GL.MapBufferRange(buffer.Target, offset, new IntPtr(count * Size), access);
		}

		Exception FixedSizeException() { return new InvalidOperationException("This is a fixed-size buffer map."); }

		void ICollection<T>.Add(T item) { throw FixedSizeException(); }
		void ICollection<T>.Clear() { throw FixedSizeException(); }
		bool ICollection<T>.IsReadOnly { get { return false; } }
		bool ICollection<T>.Remove(T item) { throw FixedSizeException(); }

		void IList<T>.Insert(int index, T item) { throw FixedSizeException(); }
		void IList<T>.RemoveAt(int index) { throw FixedSizeException(); }

		public void Dispose() {
			using (buffer.Lock())
				GL.UnmapBuffer(buffer.Target);
		}

		public int IndexOf(T item) {
			for (int index = 0; index < count; index++)
				if (EqualityComparer.Equals(item, this[index]))
					return index;
			return -1;
		}

		public bool Contains(T item) { return IndexOf(item) >= 0; }

		public void CopyTo(T[] array, int arrayIndex) {
			for (int index = 0; index < count; index++)
				array[index + arrayIndex] = this[index];
		}

		public void Flush(int first, int count) {
			if (first < 0 || first > this.count)
				throw new ArgumentOutOfRangeException("first");
			if (count < 0 || first + count > this.count)
				throw new ArgumentOutOfRangeException("count");

			using (buffer.Lock())
				GL.FlushMappedBufferRange(buffer.Target, offset + first * Size, new IntPtr(count * Size));
		}

		public Enumerator GetEnumerator() { return new Enumerator(this); }

		IEnumerator<T> IEnumerable<T>.GetEnumerator() { return GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

		IntPtr PointerAt(int index) {
			if (index < 0 || index >= count)
				throw new ArgumentOutOfRangeException("index");
			return pointer + index * Size;
		}

		public struct Enumerator : IEnumerator<T> {
			readonly BufferMap<T> map;
			int index;

			internal Enumerator(BufferMap<T> map) {
				this.map = map;
				this.index = -1;
			}

			public T Current { get { return map[index - 1]; } }

			public void Dispose() { }

			object System.Collections.IEnumerator.Current { get { return Current; } }

			public bool MoveNext() { return ++index < map.Count; }

			public void Reset() { index = -1; }
		}
	}

	internal struct BufferLock : IDisposable {
		ContextLock sharedLock;

		public BufferLock(BaseBuffer buffer, BufferTarget? target = null) {
			sharedLock = Context.Lock();
			GL.BindBuffer(target.GetValueOrDefault(buffer.Target), buffer.Id);
		}

		public void Dispose() {
			sharedLock.Dispose();
		}
	}
}
