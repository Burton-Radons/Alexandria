using ICSharpCode.SharpZipLib.Zip.Compression;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation {
	public struct RecordReader : IDisposable {
		RecordField field;

		readonly RecordHeader record;
		BinaryReader compressedReader, reader;
		long endOffset;

		public BinaryReader Reader {
			get {
				CheckReader();
				return reader;
			}
		}

		public long EndOffset { get { CheckReader(); return endOffset; } }

		public RecordHeader Record { get { return record; } }

		public RecordField Field { get { return field; } }

		string FieldName { get { return "'" + field.Type + "' field in the '" + record.Type + "' record"; } }

		public RecordReader(RecordHeader record, BinaryReader reader) {
			this.record = record;
			this.field = new RecordField();

			if (record.IsCompressed) {
				this.reader = null;
				this.compressedReader = reader;
				this.endOffset = -1;
			} else {
				this.reader = reader;
				this.compressedReader = null;
				this.endOffset = record.EndOffset;
			}
		}

		void CheckEnd() {
			if (field.Type != null && Reader.BaseStream.Position != field.EndOffset)
				throw new InvalidOperationException("The " + FieldName + " has not been properly read.");
		}

		void CheckReader() {
			if (reader == null) {
				if (!record.IsCompressed)
					throw new InvalidOperationException();
				int compressedSize = checked((int)record.Size);
				int uncompressedSize = checked((int)compressedReader.ReadUInt32());

				byte[] compressedData = new byte[compressedSize];
				byte[] uncompressedData = new byte[uncompressedSize];

				int compressedRead = compressedReader.Read(compressedData, 0, compressedSize);
				Inflater inflater = new Inflater();
				inflater.SetInput(compressedData);
				int uncompressedRead = inflater.Inflate(uncompressedData, 0, uncompressedSize);
				reader = new BinaryReader(new MemoryStream(uncompressedData, false));
				endOffset = uncompressedSize;
			}
		}

		public void Dispose() {
			CheckEnd();
			if (record.IsCompressed)
				Reader.Close();
		}

		/// <summary>Attempt to read a <see cref="RecordField"/> header, storing it in <see cref="Field"/>, returning whether there is one or if the end has been reached.</summary>
		/// <returns>Whether there is a field or the end has been reached.</returns>
		public bool ReadField() {
			CheckEnd();
			if (Reader.BaseStream.Position >= endOffset)
				return false;
			field = new RecordField(Reader);
			return true;
		}

		public int ReadInt32Body() { RequireFieldSize(4); return ReadInt32(); }
		public long ReadInt64Body() { RequireFieldSize(8); return ReadInt64(); }

		public long ReadInt64OrInt32Body() {
			if (field.Size == 4)
				return ReadInt32();
			return ReadInt64Body();
		}

		public float ReadSingleBody() { RequireFieldSize(4); return ReadSingle(); }

		public string ReadStringBody() {
			RequireMinimumFieldSize(1);
			byte[] data = Reader.ReadBytes(field.Size);
			return Encoding.UTF8.GetString(data, 0, field.Size - 1);
		}

		public short ReadInt16() { return Reader.ReadInt16(); }
		public int ReadInt32() { return Reader.ReadInt32(); }
		public long ReadInt64() { return Reader.ReadInt64(); }
		public float ReadSingle() { return Reader.ReadSingle(); }
		public ushort ReadUInt16() { return Reader.ReadUInt16(); }
		public uint ReadUInt32() { return Reader.ReadUInt32(); }
		public ulong ReadUInt64() { return Reader.ReadUInt64(); }
		public FormId ReadFormId() { return new FormId(Reader); }

		public void RequireFieldSize(int bytes) {
			if (field.Size != bytes)
				throw new InvalidDataException("The " + FieldName + " must be " + bytes + " byte(s) long, not " + field.Size + ".");
		}

		public void RequireMinimumFieldSize(int bytes) {
			if (field.Size < bytes)
				throw new InvalidDataException("The " + FieldName + " must be at least " + bytes + " byte(s) long, not " + field.Size + ".");
		}

		/// <summary>Skip past any and all <see cref="RecordField"/>s to the end of the <see cref="Record"/>. This will nullify <see cref="Field"/>.</summary>
		public void SkipToEndOfRecord() {
			field = new RecordField();
			(reader ?? compressedReader).BaseStream.Position = record.EndOffset;
		}

		public Exception UnknownFieldException() { return new NotImplementedException("Unknown " + FieldName + "."); }

		public static implicit operator BinaryReader(RecordReader reader) { return reader.Reader; }
	}
}
