using Glare.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Glare.Assets {
	public enum LoverDirection {
		Loader,
		Saver,
	}

	/// <summary>
	/// A combined loader/saver that makes many operations much simpler. It can also read/write in different byte orders, and it can mark what has been read and what it's been read for, for debugging.
	/// </summary>
	public partial class Lover : IDisposable {
		enum ByteOrderDirectionCombination {
			LittleEndian = 0,
			BigEndian = 1,

			Loader = 0,
			Saver = 2,

			LittleEndianLoader = LittleEndian | Loader,
			BigEndianLoader = BigEndian | Loader,

			LittleEndianSaver = LittleEndian | Saver,
			BigEndianSaver = BigEndian | Saver,
		}

		const ByteOrderDirectionCombination LittleEndianLoader = ByteOrderDirectionCombination.LittleEndianLoader;
		const ByteOrderDirectionCombination BigEndianLoader = ByteOrderDirectionCombination.BigEndianLoader;

		const ByteOrderDirectionCombination LittleEndianSaver = ByteOrderDirectionCombination.LittleEndianSaver;
		const ByteOrderDirectionCombination BigEndianSaver = ByteOrderDirectionCombination.BigEndianSaver;

		struct ByteOrderDirectionCombinator {
			readonly ByteOrderDirectionCombination LittleEndian;
			readonly ByteOrderDirectionCombination BigEndian;

			public ByteOrderDirectionCombinator(ByteOrderDirectionCombination littleEndian, ByteOrderDirectionCombination bigEndian) {
				LittleEndian = littleEndian;
				BigEndian = bigEndian;
			}

			public ByteOrderDirectionCombination this[ByteOrder byteOrder] {
				get {
					if (byteOrder == ByteOrder.LittleEndian)
						return LittleEndian;
					if (byteOrder == ByteOrder.BigEndian)
						return BigEndian;
					throw new ArgumentException();
				}
			}
		}

		static readonly ByteOrderDirectionCombinator
			LoaderCombinator = new ByteOrderDirectionCombinator(ByteOrderDirectionCombination.LittleEndianLoader, ByteOrderDirectionCombination.BigEndianLoader),
			SaverCombinator = new ByteOrderDirectionCombinator(ByteOrderDirectionCombination.LittleEndianSaver, ByteOrderDirectionCombination.BigEndianSaver);

		readonly ByteOrderDirectionCombinator Combinator;
		ByteOrderDirectionCombination Combination;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly LoverDirection direction;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Encoding encoding;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Stream stream;

		bool isMarking;
		ByteOrder defaultByteOrder;

		readonly Codex<LoverSection> SectionsMutable = new Codex<LoverSection>();

		readonly byte[] Temporary = new byte[64];

		/// <summary>Get or set the default byte order to use.</summary>
		public ByteOrder DefaultByteOrder { 
			get { return defaultByteOrder; }

			set {
				Combination = Combinator[value];
				defaultByteOrder = value;
			}
		}

		/// <summary>Get the direction of operations, whether this is a loader or a saver.</summary>
		public LoverDirection Direction { get { return direction; } }

		/// <summary>Get or set the default string encoding to use.</summary>
		public Encoding Encoding {
			get { return encoding; }
			set {
				if (value == null)
					throw new ArgumentNullException("value");
				encoding = value;
			}
		}

		/// <summary>Get whether this is loading.</summary>
		public bool IsLoading { get { return direction == LoverDirection.Loader; } }

		/// <summary>Get whether this is saving.</summary>
		public bool IsSaving { get { return direction == LoverDirection.Saver; } }

		/// <summary>Get or set whether to not close the <see cref="Stream"/> on <see cref="Dispose"/>.</summary>
		public bool KeepOpen { get; set; }

		/// <summary>Get the length in bytes of the stream.</summary>
		public long Length { get { return stream.Length; } }

		/// <summary>Get or set the current stream position.</summary>
		public long Position {
			get { return stream.Position; }
			set { stream.Position = value; }
		}

		/// <summary>Get the sections that have been defined.</summary>
		public ReadOnlyCodex<LoverSection> Sections { get { return SectionsMutable; } }

		/// <summary>Get the backing <see cref="Stream"/>.</summary>
		public Stream Stream { get { return stream; } }

		Lover(Stream stream, LoverDirection direction, ByteOrder defaultByteOrder, Encoding encoding, bool keepOpen = false) {
			this.direction = direction;
			Combinator = IsLoading ? LoaderCombinator : SaverCombinator;
			this.stream = stream;
			DefaultByteOrder = defaultByteOrder;
			Encoding = encoding;
			KeepOpen = keepOpen;

			if (IsLoading)
				SectionsMutable = new Codex<LoverSection>();
		}

		/// <summary>
		/// Dispose of the stream.
		/// </summary>
		public void Dispose() {
			if (!KeepOpen && stream != null)
				stream.Close();
		}

		void ReadTemporary(int count) {
			int read = stream.Read(Temporary, 0, count);
			if (read != count)
				throw new EndOfStreamException("End of stream encountered.");
		}

		void WriteTemporary(int count) {
			stream.Write(Temporary, 0, count);
		}

		void RequireLoading() { if (!IsLoading) throw NotLoaderException(); }
		void RequireSaving() { if (!IsSaving) throw NotSaverException(); }

		Exception NotSaverException() { return new NotSupportedException("This is not a saver."); }
		Exception NotLoaderException() { return new NotSupportedException("This is not a loader."); }


	}

	/// <summary>A marked section of a stream.</summary>
	public class LoverSection {
		internal Codex<LoverSection> ChildrenMutable = new Codex<LoverSection>();

		/// <summary>Get the sub-sections of this section.</summary>
		public ReadOnlyCodex<LoverSection> Children { get { return ChildrenMutable; } }

		/// <summary>Get the context object of this section, or <c>null</c> for none.</summary>
		public object Context { get; internal set; }

		/// <summary>Get the end position of the section. This is -1 until the section is closed during loading.</summary>
		public long EndPosition { get; internal set; }

		/// <summary>Get the length in bytes of the section.</summary>
		public long Length { get { return EndPosition - StartPosition; } }

		/// <summary>Get the <see cref="Lover"/> this is a section for.</summary>
		public Lover Lover { get; private set; }

		/// <summary>Get a descriptive name of this section.</summary>
		public string Name { get; private set; }

		/// <summary>Get the section that this is contained in, or <c>null</c> for none.</summary>
		public LoverSection Parent { get; internal set; }

		/// <summary>Get the starting position of the section.</summary>
		public long StartPosition { get; private set; }

		internal LoverSection(Lover lover, LoverSection parent, string name, object context) {
			Parent = parent;
			Lover = lover;
			StartPosition = lover.Position;
			EndPosition = -1;
			Name = name;
			Context = context;
		}
	}
}
