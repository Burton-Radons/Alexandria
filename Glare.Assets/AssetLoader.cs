#define DebugBreak

using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>An error that has been recorded while loading an <see cref="Asset"/>. This is collected in <see cref="AssetLoader"/>'s <see cref="AssetLoader.Errors"/> property.</summary>
	public class AssetLoadError {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly string message;

		/// <summary>Get the load info. The <see cref="AssetLoader.Reader"/>/<see cref="AssetLoader.Stream"/> may be closed.</summary>
		public AssetLoader Loader { get; private set; }

		/// <summary>Get a description of the problem. This may be localized.</summary>
		public virtual string Message { get { return message; } }

		/// <summary>Get the position the <see cref="AssetLoader.Stream"/> was at when this error was generated.</summary>
		public long Offset { get; private set; }

		public string OffsetHex { get { return Math.Abs(Offset) < 10 ? Offset.ToString() : string.Format("{1}{0}/{0:X}h", Math.Abs(Offset), Offset < 0 ? "-" : ""); } }

		public AssetLoadError(AssetLoader loader, long? offset, string message) {
			Loader = loader;
			Offset = offset.HasValue ? offset.Value : loader.Position;
			this.message = message;
		}

		public override string ToString() {
			string message = "";

			try {
				message = Message;
			} catch (Exception exception) {
				message = "Message exception: " + exception;
			}

			return string.Format("{0} (offset {1}/{1:X}h): {2}", Loader.Name, Offset, message);
		}

		#region Inner types

		public class InvalidData : AssetLoadError {
			public string Expected { get; private set; }
			public string Received { get; private set; }
			public override string Message { get { return string.Format(Properties.Resources.AssetLoadError_InvalidData, Expected, Received); } }

			public InvalidData(AssetLoader info, long offset, string expected, string received)
				: base(info, offset, null) {
				Expected = expected;
				Received = received;
			}
		}

		/// <summary>The file was not as big as it needed to be.</summary>
		public class UnexpectedEndOfFile : AssetLoadError {
			public override string Message { get { return Properties.Resources.AssetLoadError_UnexpectedEndOfFile; } }
			public UnexpectedEndOfFile(AssetLoader info) : base(info, null, null) { }
		}

		public class UnexpectedPosition : AssetLoadError {
			public long Expected { get; private set; }
			public long Received { get; private set; }
			public override string Message { get { return string.Format(Properties.Resources.AssetLoadError_UnexpectedPosition, Expected, Received); } }

			public UnexpectedPosition(AssetLoader loader, long expected)
				: base(loader, null, null) {
				Expected = expected;
				Received = loader.Position;
			}
		}

		#endregion Inner types
	}

	public class AssetLoader {
		/// <summary>Get whether the <see cref="Stream"/>'s <see cref="Stream.Position"/> is at least <see cref="End"/>, indicating that the entire stream has been read.</summary>
		public bool AtEnd { get { return Stream.Position >= End; } }

		public Asset Context { get; private set; }

		/// <summary>Get the end offset of the <see cref="Reader"/>, which is <see cref="Start"/> + <see cref="Length"/>.</summary>
		public long End { get { return Start + Length; } }

		/// <summary>Get the collection of errors accumulated while loading.</summary>
		public RichList<AssetLoadError> Errors { get; private set; }

		/// <summary>Get whether there were any <see cref="Errors"/> while loading.</summary>
		public bool HasErrors { get { return Errors.Count > 0; } }

		/// <summary>Get the length in bytes of the <see cref="Reader"/>.</summary>
		public long Length { get; private set; }

		public string Name { get; private set; }

		public FileManager FileManager { get; private set; }

		/// <summary>Get the <see cref="BinaryReader"/> to load from.</summary>
		public BinaryReader Reader { get; set; }

		/// <summary>Get or set the current position of the <see cref="Stream"/>.</summary>
		public long Position {
			get { return Stream.Position; }
			set { Stream.Position = value; }
		}

		public int ShortLength { get { return checked((int)Length); } }

		/// <summary>Get the starting offset in the <see cref="Stream"/> that this <see cref="AssetLoader"/> object was created with.</summary>
		public long Start { get; private set; }

		/// <summary>Get the <see cref="System.IO.Stream"/> from the <see cref="Reader"/>.</summary>
		public Stream Stream { get { return Reader.BaseStream; } }

		public AssetLoader(BinaryReader reader, string name, FileManager manager, Asset contextResource = null, long? length = null) {
			if (reader == null)
				throw new ArgumentNullException("reader");
			if (manager == null)
				throw new ArgumentNullException("manager");

			Start = reader.BaseStream.Position;
			Reader = reader;
			Name = name;
			FileManager = manager;
			Context = contextResource;
			Length = length.HasValue ? length.Value : reader.BaseStream.Length;
			Errors = new RichList<AssetLoadError>();
		}

		public void AddError(long? offset, string message) {
			Errors.Add(new AssetLoadError(this, offset, message));

#if DebugBreak
			if (Debugger.IsAttached)
				Debugger.Break();
#endif // DebugBreak
		}
		public void AddError(long? offset, string messageFormat, params object[] args) { AddError(offset, string.Format(messageFormat, args)); }

		bool ExpectCore(long expected, long received) {
			if (expected == received)
				return true;
			Errors.Add(new AssetLoadError.InvalidData(this, Position - 2, ValueToString(expected), ValueToString(received)));
			return false;
		}

		public bool Expect(byte expected) { return ExpectCore(expected, Reader.ReadByte()); }
		public bool Expect(ushort expected) { return ExpectCore(expected, Reader.ReadUInt16()); }
		public bool Expect(uint expected) { return ExpectCore(expected, Reader.ReadUInt32()); }
		public bool Expect(ulong expected) { return ExpectCore((long)expected, (long)Reader.ReadUInt64()); }

		public bool Expect(sbyte expected) { return ExpectCore(expected, Reader.ReadSByte()); }
		public bool Expect(short expected) { return ExpectCore(expected, Reader.ReadInt16()); }
		public bool Expect(int expected) { return ExpectCore(expected, Reader.ReadInt32()); }
		public bool Expect(long expected) { return ExpectCore(expected, Reader.ReadInt64()); }

		public bool ExpectMagic(string expected) {
			var received = Reader.ReadString(expected.Length, Encoding.ASCII);

			if (received == expected)
				return true;
			Errors.Add(new AssetLoadError.InvalidData(this, Position - received.Length, expected, received));
			return false;
		}

		public bool ExpectPosition(long expected) {
			if (Position == expected)
				return true;
			Errors.Add(new AssetLoadError.UnexpectedPosition(this, expected));
			Position = expected;
			return false;
		}

		public bool ExpectZeroes(int size, int count) {
			bool result = true;

			for (int index = 0; index < count; index++) {
				switch (size) {
					case 2: result = Expect((short)0) && result; break;
					case 4: result = Expect((int)0) && result; break;
					case 8: result = Expect((long)0) && result; break;

					default:
						for (int element = 0; element < size; element++)
							result = Expect((byte)0) && result;
						break;
				}
			}

			return result;
		}

		/// <summary>Switch the <see cref="Reader"/> for a <see cref="BigEndianBinaryReader"/>.</summary>
		public BinaryReader MakeBigEndian() {
			return Reader = new BigEndianBinaryReader(Stream);
		}

		/// <summary>Read a <see cref="Box3f"/> that is conveyed using absolute coordinates, with a triplet of min values and a triplet of max values. This checks that the min values are less than or equal to the max values, reporting errors if not.</summary>
		/// <returns></returns>
		public Box3f ReadCheckedAbsoluteBox3f() {
			var offset = Position;
			Vector3f min = Reader.ReadVector3f();
			Vector3f max = Reader.ReadVector3f();
			if (min.X > max.X || min.Y > max.Y || min.Z > max.Z)
				AddError(offset, "Absolute {0} has min values greater than max values (min = {1}, max = {2})", typeof(Box3f).Name, min.ToShortString(), max.ToShortString());
			return new Box3f(min, max);
		}

		public static Stream SystemFileOpener(string name, FileMode mode, FileAccess access, FileShare share) {
			return File.Open(name, mode, access, share);
		}

		public void Reset() { Stream.Position = Start; }

		static string ValueToString(long value) {
			if (value < 10)
				return value.ToString();
			return string.Format("{0}/{0:X}h", value);
		}
	}
}
