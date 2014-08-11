#define DebugBreak

using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>A loader for an <see cref="Asset"/>.</summary>
	public class AssetLoader : NotifyingObject {
		double ProgressField = Double.NaN;

		/// <summary><see cref="PropertyInfo"/> for the <see cref="Progress"/> property.</summary>
		public static readonly PropertyInfo ProgressProperty = GetProperty<AssetLoader>("Progress");

		/// <summary>Get the asset manager.</summary>
		public AssetManager AssetManager { get; private set; }

		/// <summary>Get whether the <see cref="Stream"/>'s <see cref="System.IO.Stream.Position"/> is at least <see cref="End"/>, indicating that the entire stream has been read.</summary>
		public bool AtEnd { get { return Stream.Position >= End; } }

		/// <summary>Get the <see cref="Length"/> as an <see cref="Int32"/>, throwing an exception if it's too long.</summary>
		public int CheckedShortLength { get { return checked((int)Length); } }

		/// <summary>Get the context <see cref="Asset"/>.</summary>
		public Asset Context { get; private set; }

		/// <summary>Get the end offset of the <see cref="Reader"/>, which is <see cref="Start"/> + <see cref="Length"/>.</summary>
		public long End { get { return Start + Length; } }

		/// <summary>Get the collection of errors accumulated while loading.</summary>
		public Codex<AssetLoadError> Errors { get; private set; }

		/// <summary>Get whether there were any <see cref="Errors"/> while loading.</summary>
		public bool HasErrors { get { return Errors.Count > 0; } }

		/// <summary>Get the length in bytes of the <see cref="Reader"/>.</summary>
		public long Length { get; private set; }

		/// <summary>Get the file manager.</summary>
		public FileManager FileManager { get; private set; }

		/// <summary>Get an optional name describing the context.</summary>
		public string Name { get; private set; }

		/// <summary>Get or set the current position of the <see cref="Stream"/>.</summary>
		public long Position {
			get { return Stream.Position; }
			set { Stream.Position = value; }
		}

		/// <summary>Get (or set, if you're a <see cref="AssetLoader"/> in a <see cref="AssetFormat.Load"/> context) the percentage of how much of the <see cref="Asset"/> has been loaded, from 0 (starting) to 100 (complete). The default is <see cref="Double.NaN"/>, meaning that there is no valid value.</summary>
		public double Progress {
			get { return ProgressField; }
			set { SetProperty(ProgressProperty, ref ProgressField, ref value); }
		}
		
		/// <summary>Get the <see cref="BinaryReader"/> to load from.</summary>
		public BinaryReader Reader { get; set; }

		/// <summary>Get the number of bytes remaining from the <see cref="Reader"/>'s current position.</summary>
		public long Remaining { get { return End - Position; } }

		/// <summary>Get the starting offset in the <see cref="Stream"/> that this <see cref="AssetLoader"/> object was created with.</summary>
		public long Start { get; private set; }

		/// <summary>Get the <see cref="System.IO.Stream"/> from the <see cref="Reader"/>.</summary>
		public Stream Stream { get { return Reader.BaseStream; } }

		/// <summary>Initialise the <see cref="Asset"/> loader.</summary>
		/// <param name="assetManager"></param>
		/// <param name="reader"></param>
		/// <param name="name"></param>
		/// <param name="fileManager">The <see cref="FileManager"/> to use to load any attached files. If this is <c>null</c> (the default), then the system file manager (<see cref="Glare.Assets.FileManager.System"/>) is used.</param>
		/// <param name="contextResource"></param>
		/// <param name="length"></param>
		public AssetLoader(AssetManager assetManager, BinaryReader reader, string name, FileManager fileManager, Asset contextResource = null, long? length = null) {
			if (assetManager == null)
				throw new ArgumentNullException("assetManager");
			if (reader == null)
				throw new ArgumentNullException("reader");

			AssetManager = assetManager;
			Start = reader.BaseStream.Position;
			Reader = reader;
			Name = name;
			FileManager = fileManager ?? FileManager.System;
			Context = contextResource;
			Length = length.HasValue ? length.Value : reader.BaseStream.Length;
			Errors = new Codex<AssetLoadError>();
		}

		/// <summary>Add a generic load error.</summary>
		/// <param name="offset"></param>
		/// <param name="message"></param>
		public void AddError(long? offset, string message) {
			Errors.Add(new AssetLoadError(this, offset, message));

#if DebugBreak
			if (Debugger.IsAttached)
				Debugger.Break();
#endif // DebugBreak
		}

		/// <summary>Add a generic load error.</summary>
		/// <param name="offset"></param>
		/// <param name="messageFormat"></param>
		/// <param name="args"></param>
		public void AddError(long? offset, string messageFormat, params object[] args) { AddError(offset, string.Format(messageFormat, args)); }

		bool ExpectCore(long expected, long received) {
			if (expected == received)
				return true;
			Errors.Add(new AssetLoadError.InvalidData(this, Position - 2, ValueToString(expected), ValueToString(received)));
			return false;
		}

		/// <summary>Expect a <see cref="UInt16"/> value; if found, return <c>true</c>; otherwise report an error and return <c>false</c></summary>
		/// <param name="expected"></param>
		/// <returns></returns>
		public bool Expect(byte expected) { return ExpectCore(expected, Reader.ReadByte()); }

		/// <summary>Expect a <see cref="Byte"/> value; if found, return <c>true</c>; otherwise report an error and return <c>false</c></summary>
		/// <param name="expected"></param>
		/// <returns></returns>
		public bool Expect(ushort expected) { return ExpectCore(expected, Reader.ReadUInt16()); }

		/// <summary>Expect a <see cref="UInt32"/> value; if found, return <c>true</c>; otherwise report an error and return <c>false</c></summary>
		/// <param name="expected"></param>
		/// <returns></returns>
		public bool Expect(uint expected) { return ExpectCore(expected, Reader.ReadUInt32()); }

		/// <summary>Expect a <see cref="UInt64"/> value; if found, return <c>true</c>; otherwise report an error and return <c>false</c></summary>
		/// <param name="expected"></param>
		/// <returns></returns>
		public bool Expect(ulong expected) { return ExpectCore((long)expected, (long)Reader.ReadUInt64()); }

		/// <summary>Expect a <see cref="SByte"/> value; if found, return <c>true</c>; otherwise report an error and return <c>false</c></summary>
		/// <param name="expected"></param>
		/// <returns></returns>
		public bool Expect(sbyte expected) { return ExpectCore(expected, Reader.ReadSByte()); }

		/// <summary>Expect a <see cref="Int16"/> value; if found, return <c>true</c>; otherwise report an error and return <c>false</c></summary>
		/// <param name="expected"></param>
		/// <returns></returns>
		public bool Expect(short expected) { return ExpectCore(expected, Reader.ReadInt16()); }

		/// <summary>Expect a <see cref="Int32"/> value; if found, return <c>true</c>; otherwise report an error and return <c>false</c></summary>
		/// <param name="expected"></param>
		/// <returns></returns>
		public bool Expect(int expected) { return ExpectCore(expected, Reader.ReadInt32()); }

		/// <summary>Expect a <see cref="Int64"/> value; if found, return <c>true</c>; otherwise report an error and return <c>false</c></summary>
		/// <param name="expected"></param>
		/// <returns></returns>
		public bool Expect(long expected) { return ExpectCore(expected, Reader.ReadInt64()); }

		/// <summary>Expect a magic sequence of bytes. If found, return <c>true</c>; otherwise report an error and return <c>false</c>.</summary>
		/// <param name="expected"></param>
		/// <returns></returns>
		public bool ExpectMagic(string expected) {
			var received = Reader.ReadString(expected.Length, Encoding.ASCII);

			if (received == expected)
				return true;
			Errors.Add(new AssetLoadError.InvalidData(this, Position - received.Length, expected, received));
			return false;
		}

		/// <summary>Expect the input to be at a given position; if not so, report an error.</summary>
		/// <param name="expected"></param>
		/// <returns></returns>
		public bool ExpectPosition(long expected) {
			if (Position == expected)
				return true;
			Errors.Add(new AssetLoadError.UnexpectedPosition(this, expected));
			Position = expected;
			return false;
		}

		/// <summary>Expect a sequence of zeroes, reporting errors if they are not found.</summary>
		/// <param name="size"></param>
		/// <param name="count"></param>
		/// <returns></returns>
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

		/// <summary>Set <see cref="Progress"/> to the current reader <see cref="Position"/>, based on the <see cref="Start"/> offset and the <see cref="Length"/>.</summary>
		public void SetProgressToPosition() {
			Progress = (Position - Start) / (double)(Length - Start);
		}

		/// <summary>Switch to the use of a <see cref="MarkingStream"/>.</summary>
		/// <returns></returns>
		public MarkingStream StartMarking() {
			if (Reader.BaseStream is MarkingStream) {
				return (MarkingStream)Reader.BaseStream;
			}

			var markingStream = new MarkingStream(Reader.BaseStream);
			if (Reader is BigEndianBinaryReader)
				Reader = new BigEndianBinaryReader(markingStream);
			else
				Reader = new BinaryReader(markingStream);

			return markingStream;
		}

		/// <summary>Switch to the use of a <see cref="MarkingStream"/>.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public MarkingStream StartMarking(out BinaryReader reader) {
			MarkingStream stream = StartMarking();
			reader = Reader;
			return stream;
		}

		/// <summary>Reset the stream position to the original position when the asset loader was created.</summary>
		public void Reset() { Stream.Position = Start; }

		static string ValueToString(long value) {
			if (value < 10)
				return value.ToString();
			return string.Format("{0}/{0:X}h", value);
		}
	}
}
