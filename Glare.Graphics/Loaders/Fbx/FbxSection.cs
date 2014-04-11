using Glare;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	public struct FbxSection {
		readonly BinaryReader Reader;

		/// <summary>Get whether this block contains sub-blocks.</summary>
		public bool HasSubBlocks { get { return ValueDataEnd != End; } }

		public bool IsValid { get { return End != 0; } }

		/// <summary>Name of the <see cref="FbxSection"/> type.</summary>
		public readonly string Name;

		/// <summary>File offset of the next <see cref="FbxSection"/>.</summary>
		public readonly uint End;

		/// <summary>File offset of the start of the <see cref="FbxSection"/>.</summary>
		public uint Start { get { return ValueDataStart - 12u - 1u - (uint)Name.Length; } }

		public uint ChildrenEnd { get { return End; } }

		public uint ChildrenStart { get { return ValueDataEnd; } }

		/// <summary>Number of values in the block.</summary>
		public readonly int ValueCount;

		/// <summary>Length in bytes of the value data.</summary>
		public readonly uint ValueDataLength;

		/// <summary>Get the file offset of the end of the value data.</summary>
		public uint ValueDataEnd { get { return ValueDataStart + ValueDataLength; } }

		/// <summary>Get the file offset of the start of the value data.</summary>
		public readonly uint ValueDataStart;

		public FbxSection(BinaryReader reader, string requiredName = null) {
			Reader = reader;
			End = reader.ReadUInt32();
			ValueCount = reader.ReadInt32();
			ValueDataLength = reader.ReadUInt32();

			byte nameLength = reader.ReadByte();
			Name = reader.ReadString(nameLength, Encoding.ASCII);

			ValueDataStart = checked((uint)reader.BaseStream.Position);

			if(requiredName != null)
				RequireName(requiredName);
		}

		public void RequireName(string requiredName) {
			if(Name != requiredName)
				throw new InvalidDataException("Expected '" + requiredName + "' section but received '" + Name + "'.");
		}

		public FbxSection ReadChild(string requiredName = null) {
			long position = Reader.BaseStream.Position;
			if (position < ChildrenStart || position >= ChildrenEnd)
				throw new InvalidOperationException("The reader stream is not within this section's sub-section data.");
			return new FbxSection(Reader);
		}

		public bool ReadChild(out FbxSection child, string requiredName = null) {
			child = ReadChild(requiredName);
			return child.IsValid;
		}

		public FbxValue ReadValue() {
			long position = Reader.BaseStream.Position;
			if (position < ValueDataStart || position >= ValueDataEnd)
				throw new InvalidOperationException("The reader stream is not within this block's value data.");
			return new FbxValue(Reader);
		}

		public double ReadDoubleValue() { return ReadValue().AsDouble; }
		public double[] ReadDoubleArrayValue() { return ReadValue().AsDoubleArray; }
		public int ReadInt32Value() { return ReadValue().AsInt32; }
		public int[] ReadInt32ArrayValue() { return ReadValue().AsInt32Array; }
		public long ReadInt64Value() { return ReadValue().AsInt64; }
		public string ReadStringValue() { return ReadValue().AsString; }

		public void RequireEnd() {
			if (Reader.BaseStream.Position != End)
				throw new InvalidOperationException("The reader stream did not fully consume this section.");
		}

		public DateTime ReadTimeStamp() {
			FbxSection section;
			int version = -1, year = -1, month = -1, day = -1, hour = -1, minute = -1, second = -1, millisecond = -1;

			while(ReadChild(out section)) {
				switch(section.Name) {
					case "Version":
						version = section.ReadInt32Value();
						if(version != 1000)
							throw new InvalidDataException("Invalid time stamp version " + version);
						break;

					case "Year": ReadTimestampSet(ref year, ref section); break;
					case "Month": ReadTimestampSet(ref month, ref section); break;
					case "Day": ReadTimestampSet(ref day, ref section); break;
					case "Hour": ReadTimestampSet(ref hour, ref section); break;
					case "Minute": ReadTimestampSet(ref minute, ref section); break;
					case "Second": ReadTimestampSet(ref second, ref section); break;
					case "Millisecond": ReadTimestampSet(ref millisecond, ref section); break;
					default: throw new InvalidDataException();
				}

				section.RequireEnd();
			}

			if(version < 0 || year < 0 || month < 0 || day < 0 || hour < 0 || minute < 0 || second < 0 || millisecond < 0)
				throw new InvalidDataException("Incomplete time stamp data.");
			return new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc);
		}

		static void ReadTimestampSet(ref int value, ref FbxSection section) {
			if(value != -1)
				throw new InvalidOperationException("Value " + section.Name + " has already been provided.");
			value = section.ReadInt32Value();
		}

		public void SeekToEnd() {
			Reader.BaseStream.Position = End;
		}
	}
}