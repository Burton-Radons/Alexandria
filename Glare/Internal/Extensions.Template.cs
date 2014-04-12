using System;
using System.Collections.Generic;
using System.IO;

namespace Glare.Internal {
	public static partial class Extensions {
	}

	public static partial class ExtensionMethods {
		#region BinaryReader

		#endregion BinaryReader

					/// <summary>Read a <c>Byte</c> value, and return whether it's equal to the expected value.</summary>
			public static bool ReadMatch(this BinaryReader reader, Byte expected) {
				Byte value = reader.ReadByte();
				return expected == value;
			}

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Byte> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadByte(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Byte> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Byte</c> values.</summary>
			public static Byte[] ReadArrayByte(this BinaryReader reader, int count) { Byte[] array = new Byte[count]; reader.ReadArray(array, 0, count); return array; }

			/// <summary>Read a <c>Byte</c> value, and if it doesn't match the expected value, throw an <see cref="InvalidDataException"/>.</summary>
			public static void ReadExpected(this BinaryReader reader, Byte expected) {
				Byte value = reader.ReadByte();
				if(value != expected)
					throw CreateExpectationException(expected, value);
			}

			public static void Require(this BinaryReader reader, Byte value) {
				Byte read = reader.ReadByte();
				if (read != value)
					throw new Exception("Expected Byte " + value + " but received " + read + ".");
			}

			/// <summary>Seek to the position and read a <see cref="Byte"/>.</summary>
			public static Byte ReadByteAt(this BinaryReader reader, long offset) {
				reader.BaseStream.Position = offset;
				return reader.ReadByte();
			}

								/// <summary>Read a <c>SByte</c> value, and return whether it's equal to the expected value.</summary>
			public static bool ReadMatch(this BinaryReader reader, SByte expected) {
				SByte value = reader.ReadSByte();
				return expected == value;
			}

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<SByte> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadSByte(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<SByte> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>SByte</c> values.</summary>
			public static SByte[] ReadArraySByte(this BinaryReader reader, int count) { SByte[] array = new SByte[count]; reader.ReadArray(array, 0, count); return array; }

			/// <summary>Read a <c>SByte</c> value, and if it doesn't match the expected value, throw an <see cref="InvalidDataException"/>.</summary>
			public static void ReadExpected(this BinaryReader reader, SByte expected) {
				SByte value = reader.ReadSByte();
				if(value != expected)
					throw CreateExpectationException(expected, value);
			}

			public static void Require(this BinaryReader reader, SByte value) {
				SByte read = reader.ReadSByte();
				if (read != value)
					throw new Exception("Expected SByte " + value + " but received " + read + ".");
			}

			/// <summary>Seek to the position and read a <see cref="SByte"/>.</summary>
			public static SByte ReadSByteAt(this BinaryReader reader, long offset) {
				reader.BaseStream.Position = offset;
				return reader.ReadSByte();
			}

								/// <summary>Read a <c>Int16</c> value, and return whether it's equal to the expected value.</summary>
			public static bool ReadMatch(this BinaryReader reader, Int16 expected) {
				Int16 value = reader.ReadInt16();
				return expected == value;
			}

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Int16> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadInt16(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Int16> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Int16</c> values.</summary>
			public static Int16[] ReadArrayInt16(this BinaryReader reader, int count) { Int16[] array = new Int16[count]; reader.ReadArray(array, 0, count); return array; }

			/// <summary>Read a <c>Int16</c> value, and if it doesn't match the expected value, throw an <see cref="InvalidDataException"/>.</summary>
			public static void ReadExpected(this BinaryReader reader, Int16 expected) {
				Int16 value = reader.ReadInt16();
				if(value != expected)
					throw CreateExpectationException(expected, value);
			}

			public static void Require(this BinaryReader reader, Int16 value) {
				Int16 read = reader.ReadInt16();
				if (read != value)
					throw new Exception("Expected Int16 " + value + " but received " + read + ".");
			}

			/// <summary>Seek to the position and read a <see cref="Int16"/>.</summary>
			public static Int16 ReadInt16At(this BinaryReader reader, long offset) {
				reader.BaseStream.Position = offset;
				return reader.ReadInt16();
			}

							/// <summary>Read a <c>Int16</c> value in big-endian byte order.</summary>
				public static Int16 ReadInt16BE(this BinaryReader reader) {
					return reader.ReadInt16().ReverseBytes();
				}

				public static Int16 ReadInt16(this BinaryReader reader, ByteOrder byteOrder) {
					switch (byteOrder) {
						case ByteOrder.LittleEndian: return reader.ReadInt16();
						case ByteOrder.BigEndian: return reader.ReadInt16BE();
						default: throw new ArgumentException("byteOrder");
					}
				}

				/// <summary>Seek to the position and read a <see cref="Int16"/>.</summary>
				public static Int16 ReadInt16At(this BinaryReader reader, long offset, ByteOrder byteOrder) {
					reader.BaseStream.Position = offset;
					return reader.ReadInt16(byteOrder);
				}
				
				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<Int16> values, int start, int count, ByteOrder byteOrder) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadInt16(byteOrder); }

				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<Int16> values, ByteOrder byteOrder) { reader.ReadArray(values, 0, values.Count, byteOrder); }

				/// <summary>Read an array of <c>Int16</c> values.</summary>
				public static Int16[] ReadArrayInt16(this BinaryReader reader, int count, ByteOrder byteOrder) { Int16[] array = new Int16[count]; reader.ReadArray(array, 0, count, byteOrder); return array; }
								/// <summary>Read a <c>Int32</c> value, and return whether it's equal to the expected value.</summary>
			public static bool ReadMatch(this BinaryReader reader, Int32 expected) {
				Int32 value = reader.ReadInt32();
				return expected == value;
			}

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Int32> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadInt32(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Int32> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Int32</c> values.</summary>
			public static Int32[] ReadArrayInt32(this BinaryReader reader, int count) { Int32[] array = new Int32[count]; reader.ReadArray(array, 0, count); return array; }

			/// <summary>Read a <c>Int32</c> value, and if it doesn't match the expected value, throw an <see cref="InvalidDataException"/>.</summary>
			public static void ReadExpected(this BinaryReader reader, Int32 expected) {
				Int32 value = reader.ReadInt32();
				if(value != expected)
					throw CreateExpectationException(expected, value);
			}

			public static void Require(this BinaryReader reader, Int32 value) {
				Int32 read = reader.ReadInt32();
				if (read != value)
					throw new Exception("Expected Int32 " + value + " but received " + read + ".");
			}

			/// <summary>Seek to the position and read a <see cref="Int32"/>.</summary>
			public static Int32 ReadInt32At(this BinaryReader reader, long offset) {
				reader.BaseStream.Position = offset;
				return reader.ReadInt32();
			}

							/// <summary>Read a <c>Int32</c> value in big-endian byte order.</summary>
				public static Int32 ReadInt32BE(this BinaryReader reader) {
					return reader.ReadInt32().ReverseBytes();
				}

				public static Int32 ReadInt32(this BinaryReader reader, ByteOrder byteOrder) {
					switch (byteOrder) {
						case ByteOrder.LittleEndian: return reader.ReadInt32();
						case ByteOrder.BigEndian: return reader.ReadInt32BE();
						default: throw new ArgumentException("byteOrder");
					}
				}

				/// <summary>Seek to the position and read a <see cref="Int32"/>.</summary>
				public static Int32 ReadInt32At(this BinaryReader reader, long offset, ByteOrder byteOrder) {
					reader.BaseStream.Position = offset;
					return reader.ReadInt32(byteOrder);
				}
				
				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<Int32> values, int start, int count, ByteOrder byteOrder) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadInt32(byteOrder); }

				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<Int32> values, ByteOrder byteOrder) { reader.ReadArray(values, 0, values.Count, byteOrder); }

				/// <summary>Read an array of <c>Int32</c> values.</summary>
				public static Int32[] ReadArrayInt32(this BinaryReader reader, int count, ByteOrder byteOrder) { Int32[] array = new Int32[count]; reader.ReadArray(array, 0, count, byteOrder); return array; }
								/// <summary>Read a <c>Int64</c> value, and return whether it's equal to the expected value.</summary>
			public static bool ReadMatch(this BinaryReader reader, Int64 expected) {
				Int64 value = reader.ReadInt64();
				return expected == value;
			}

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Int64> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadInt64(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Int64> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Int64</c> values.</summary>
			public static Int64[] ReadArrayInt64(this BinaryReader reader, int count) { Int64[] array = new Int64[count]; reader.ReadArray(array, 0, count); return array; }

			/// <summary>Read a <c>Int64</c> value, and if it doesn't match the expected value, throw an <see cref="InvalidDataException"/>.</summary>
			public static void ReadExpected(this BinaryReader reader, Int64 expected) {
				Int64 value = reader.ReadInt64();
				if(value != expected)
					throw CreateExpectationException(expected, value);
			}

			public static void Require(this BinaryReader reader, Int64 value) {
				Int64 read = reader.ReadInt64();
				if (read != value)
					throw new Exception("Expected Int64 " + value + " but received " + read + ".");
			}

			/// <summary>Seek to the position and read a <see cref="Int64"/>.</summary>
			public static Int64 ReadInt64At(this BinaryReader reader, long offset) {
				reader.BaseStream.Position = offset;
				return reader.ReadInt64();
			}

							/// <summary>Read a <c>Int64</c> value in big-endian byte order.</summary>
				public static Int64 ReadInt64BE(this BinaryReader reader) {
					return reader.ReadInt64().ReverseBytes();
				}

				public static Int64 ReadInt64(this BinaryReader reader, ByteOrder byteOrder) {
					switch (byteOrder) {
						case ByteOrder.LittleEndian: return reader.ReadInt64();
						case ByteOrder.BigEndian: return reader.ReadInt64BE();
						default: throw new ArgumentException("byteOrder");
					}
				}

				/// <summary>Seek to the position and read a <see cref="Int64"/>.</summary>
				public static Int64 ReadInt64At(this BinaryReader reader, long offset, ByteOrder byteOrder) {
					reader.BaseStream.Position = offset;
					return reader.ReadInt64(byteOrder);
				}
				
				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<Int64> values, int start, int count, ByteOrder byteOrder) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadInt64(byteOrder); }

				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<Int64> values, ByteOrder byteOrder) { reader.ReadArray(values, 0, values.Count, byteOrder); }

				/// <summary>Read an array of <c>Int64</c> values.</summary>
				public static Int64[] ReadArrayInt64(this BinaryReader reader, int count, ByteOrder byteOrder) { Int64[] array = new Int64[count]; reader.ReadArray(array, 0, count, byteOrder); return array; }
								/// <summary>Read a <c>UInt16</c> value, and return whether it's equal to the expected value.</summary>
			public static bool ReadMatch(this BinaryReader reader, UInt16 expected) {
				UInt16 value = reader.ReadUInt16();
				return expected == value;
			}

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<UInt16> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadUInt16(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<UInt16> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>UInt16</c> values.</summary>
			public static UInt16[] ReadArrayUInt16(this BinaryReader reader, int count) { UInt16[] array = new UInt16[count]; reader.ReadArray(array, 0, count); return array; }

			/// <summary>Read a <c>UInt16</c> value, and if it doesn't match the expected value, throw an <see cref="InvalidDataException"/>.</summary>
			public static void ReadExpected(this BinaryReader reader, UInt16 expected) {
				UInt16 value = reader.ReadUInt16();
				if(value != expected)
					throw CreateExpectationException(expected, value);
			}

			public static void Require(this BinaryReader reader, UInt16 value) {
				UInt16 read = reader.ReadUInt16();
				if (read != value)
					throw new Exception("Expected UInt16 " + value + " but received " + read + ".");
			}

			/// <summary>Seek to the position and read a <see cref="UInt16"/>.</summary>
			public static UInt16 ReadUInt16At(this BinaryReader reader, long offset) {
				reader.BaseStream.Position = offset;
				return reader.ReadUInt16();
			}

							/// <summary>Read a <c>UInt16</c> value in big-endian byte order.</summary>
				public static UInt16 ReadUInt16BE(this BinaryReader reader) {
					return reader.ReadUInt16().ReverseBytes();
				}

				public static UInt16 ReadUInt16(this BinaryReader reader, ByteOrder byteOrder) {
					switch (byteOrder) {
						case ByteOrder.LittleEndian: return reader.ReadUInt16();
						case ByteOrder.BigEndian: return reader.ReadUInt16BE();
						default: throw new ArgumentException("byteOrder");
					}
				}

				/// <summary>Seek to the position and read a <see cref="UInt16"/>.</summary>
				public static UInt16 ReadUInt16At(this BinaryReader reader, long offset, ByteOrder byteOrder) {
					reader.BaseStream.Position = offset;
					return reader.ReadUInt16(byteOrder);
				}
				
				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<UInt16> values, int start, int count, ByteOrder byteOrder) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadUInt16(byteOrder); }

				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<UInt16> values, ByteOrder byteOrder) { reader.ReadArray(values, 0, values.Count, byteOrder); }

				/// <summary>Read an array of <c>UInt16</c> values.</summary>
				public static UInt16[] ReadArrayUInt16(this BinaryReader reader, int count, ByteOrder byteOrder) { UInt16[] array = new UInt16[count]; reader.ReadArray(array, 0, count, byteOrder); return array; }
								/// <summary>Read a <c>UInt32</c> value, and return whether it's equal to the expected value.</summary>
			public static bool ReadMatch(this BinaryReader reader, UInt32 expected) {
				UInt32 value = reader.ReadUInt32();
				return expected == value;
			}

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<UInt32> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadUInt32(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<UInt32> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>UInt32</c> values.</summary>
			public static UInt32[] ReadArrayUInt32(this BinaryReader reader, int count) { UInt32[] array = new UInt32[count]; reader.ReadArray(array, 0, count); return array; }

			/// <summary>Read a <c>UInt32</c> value, and if it doesn't match the expected value, throw an <see cref="InvalidDataException"/>.</summary>
			public static void ReadExpected(this BinaryReader reader, UInt32 expected) {
				UInt32 value = reader.ReadUInt32();
				if(value != expected)
					throw CreateExpectationException(expected, value);
			}

			public static void Require(this BinaryReader reader, UInt32 value) {
				UInt32 read = reader.ReadUInt32();
				if (read != value)
					throw new Exception("Expected UInt32 " + value + " but received " + read + ".");
			}

			/// <summary>Seek to the position and read a <see cref="UInt32"/>.</summary>
			public static UInt32 ReadUInt32At(this BinaryReader reader, long offset) {
				reader.BaseStream.Position = offset;
				return reader.ReadUInt32();
			}

							/// <summary>Read a <c>UInt32</c> value in big-endian byte order.</summary>
				public static UInt32 ReadUInt32BE(this BinaryReader reader) {
					return reader.ReadUInt32().ReverseBytes();
				}

				public static UInt32 ReadUInt32(this BinaryReader reader, ByteOrder byteOrder) {
					switch (byteOrder) {
						case ByteOrder.LittleEndian: return reader.ReadUInt32();
						case ByteOrder.BigEndian: return reader.ReadUInt32BE();
						default: throw new ArgumentException("byteOrder");
					}
				}

				/// <summary>Seek to the position and read a <see cref="UInt32"/>.</summary>
				public static UInt32 ReadUInt32At(this BinaryReader reader, long offset, ByteOrder byteOrder) {
					reader.BaseStream.Position = offset;
					return reader.ReadUInt32(byteOrder);
				}
				
				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<UInt32> values, int start, int count, ByteOrder byteOrder) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadUInt32(byteOrder); }

				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<UInt32> values, ByteOrder byteOrder) { reader.ReadArray(values, 0, values.Count, byteOrder); }

				/// <summary>Read an array of <c>UInt32</c> values.</summary>
				public static UInt32[] ReadArrayUInt32(this BinaryReader reader, int count, ByteOrder byteOrder) { UInt32[] array = new UInt32[count]; reader.ReadArray(array, 0, count, byteOrder); return array; }
								/// <summary>Read a <c>UInt64</c> value, and return whether it's equal to the expected value.</summary>
			public static bool ReadMatch(this BinaryReader reader, UInt64 expected) {
				UInt64 value = reader.ReadUInt64();
				return expected == value;
			}

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<UInt64> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadUInt64(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<UInt64> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>UInt64</c> values.</summary>
			public static UInt64[] ReadArrayUInt64(this BinaryReader reader, int count) { UInt64[] array = new UInt64[count]; reader.ReadArray(array, 0, count); return array; }

			/// <summary>Read a <c>UInt64</c> value, and if it doesn't match the expected value, throw an <see cref="InvalidDataException"/>.</summary>
			public static void ReadExpected(this BinaryReader reader, UInt64 expected) {
				UInt64 value = reader.ReadUInt64();
				if(value != expected)
					throw CreateExpectationException(expected, value);
			}

			public static void Require(this BinaryReader reader, UInt64 value) {
				UInt64 read = reader.ReadUInt64();
				if (read != value)
					throw new Exception("Expected UInt64 " + value + " but received " + read + ".");
			}

			/// <summary>Seek to the position and read a <see cref="UInt64"/>.</summary>
			public static UInt64 ReadUInt64At(this BinaryReader reader, long offset) {
				reader.BaseStream.Position = offset;
				return reader.ReadUInt64();
			}

							/// <summary>Read a <c>UInt64</c> value in big-endian byte order.</summary>
				public static UInt64 ReadUInt64BE(this BinaryReader reader) {
					return reader.ReadUInt64().ReverseBytes();
				}

				public static UInt64 ReadUInt64(this BinaryReader reader, ByteOrder byteOrder) {
					switch (byteOrder) {
						case ByteOrder.LittleEndian: return reader.ReadUInt64();
						case ByteOrder.BigEndian: return reader.ReadUInt64BE();
						default: throw new ArgumentException("byteOrder");
					}
				}

				/// <summary>Seek to the position and read a <see cref="UInt64"/>.</summary>
				public static UInt64 ReadUInt64At(this BinaryReader reader, long offset, ByteOrder byteOrder) {
					reader.BaseStream.Position = offset;
					return reader.ReadUInt64(byteOrder);
				}
				
				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<UInt64> values, int start, int count, ByteOrder byteOrder) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadUInt64(byteOrder); }

				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<UInt64> values, ByteOrder byteOrder) { reader.ReadArray(values, 0, values.Count, byteOrder); }

				/// <summary>Read an array of <c>UInt64</c> values.</summary>
				public static UInt64[] ReadArrayUInt64(this BinaryReader reader, int count, ByteOrder byteOrder) { UInt64[] array = new UInt64[count]; reader.ReadArray(array, 0, count, byteOrder); return array; }
								/// <summary>Read a <c>Single</c> value, and return whether it's equal to the expected value.</summary>
			public static bool ReadMatch(this BinaryReader reader, Single expected) {
				Single value = reader.ReadSingle();
				return expected == value;
			}

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Single> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadSingle(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Single> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Single</c> values.</summary>
			public static Single[] ReadArraySingle(this BinaryReader reader, int count) { Single[] array = new Single[count]; reader.ReadArray(array, 0, count); return array; }

			/// <summary>Read a <c>Single</c> value, and if it doesn't match the expected value, throw an <see cref="InvalidDataException"/>.</summary>
			public static void ReadExpected(this BinaryReader reader, Single expected) {
				Single value = reader.ReadSingle();
				if(value != expected)
					throw CreateExpectationException(expected, value);
			}

			public static void Require(this BinaryReader reader, Single value) {
				Single read = reader.ReadSingle();
				if (read != value)
					throw new Exception("Expected Single " + value + " but received " + read + ".");
			}

			/// <summary>Seek to the position and read a <see cref="Single"/>.</summary>
			public static Single ReadSingleAt(this BinaryReader reader, long offset) {
				reader.BaseStream.Position = offset;
				return reader.ReadSingle();
			}

							/// <summary>Read a <c>Single</c> value in big-endian byte order.</summary>
				public static Single ReadSingleBE(this BinaryReader reader) {
					return reader.ReadSingle().ReverseBytes();
				}

				public static Single ReadSingle(this BinaryReader reader, ByteOrder byteOrder) {
					switch (byteOrder) {
						case ByteOrder.LittleEndian: return reader.ReadSingle();
						case ByteOrder.BigEndian: return reader.ReadSingleBE();
						default: throw new ArgumentException("byteOrder");
					}
				}

				/// <summary>Seek to the position and read a <see cref="Single"/>.</summary>
				public static Single ReadSingleAt(this BinaryReader reader, long offset, ByteOrder byteOrder) {
					reader.BaseStream.Position = offset;
					return reader.ReadSingle(byteOrder);
				}
				
				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<Single> values, int start, int count, ByteOrder byteOrder) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadSingle(byteOrder); }

				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<Single> values, ByteOrder byteOrder) { reader.ReadArray(values, 0, values.Count, byteOrder); }

				/// <summary>Read an array of <c>Single</c> values.</summary>
				public static Single[] ReadArraySingle(this BinaryReader reader, int count, ByteOrder byteOrder) { Single[] array = new Single[count]; reader.ReadArray(array, 0, count, byteOrder); return array; }
								/// <summary>Read a <c>Double</c> value, and return whether it's equal to the expected value.</summary>
			public static bool ReadMatch(this BinaryReader reader, Double expected) {
				Double value = reader.ReadDouble();
				return expected == value;
			}

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Double> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadDouble(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Double> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Double</c> values.</summary>
			public static Double[] ReadArrayDouble(this BinaryReader reader, int count) { Double[] array = new Double[count]; reader.ReadArray(array, 0, count); return array; }

			/// <summary>Read a <c>Double</c> value, and if it doesn't match the expected value, throw an <see cref="InvalidDataException"/>.</summary>
			public static void ReadExpected(this BinaryReader reader, Double expected) {
				Double value = reader.ReadDouble();
				if(value != expected)
					throw CreateExpectationException(expected, value);
			}

			public static void Require(this BinaryReader reader, Double value) {
				Double read = reader.ReadDouble();
				if (read != value)
					throw new Exception("Expected Double " + value + " but received " + read + ".");
			}

			/// <summary>Seek to the position and read a <see cref="Double"/>.</summary>
			public static Double ReadDoubleAt(this BinaryReader reader, long offset) {
				reader.BaseStream.Position = offset;
				return reader.ReadDouble();
			}

							/// <summary>Read a <c>Double</c> value in big-endian byte order.</summary>
				public static Double ReadDoubleBE(this BinaryReader reader) {
					return reader.ReadDouble().ReverseBytes();
				}

				public static Double ReadDouble(this BinaryReader reader, ByteOrder byteOrder) {
					switch (byteOrder) {
						case ByteOrder.LittleEndian: return reader.ReadDouble();
						case ByteOrder.BigEndian: return reader.ReadDoubleBE();
						default: throw new ArgumentException("byteOrder");
					}
				}

				/// <summary>Seek to the position and read a <see cref="Double"/>.</summary>
				public static Double ReadDoubleAt(this BinaryReader reader, long offset, ByteOrder byteOrder) {
					reader.BaseStream.Position = offset;
					return reader.ReadDouble(byteOrder);
				}
				
				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<Double> values, int start, int count, ByteOrder byteOrder) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadDouble(byteOrder); }

				/// <summary>Read a part of a list.</summary>
				public static void ReadArray(this BinaryReader reader, IList<Double> values, ByteOrder byteOrder) { reader.ReadArray(values, 0, values.Count, byteOrder); }

				/// <summary>Read an array of <c>Double</c> values.</summary>
				public static Double[] ReadArrayDouble(this BinaryReader reader, int count, ByteOrder byteOrder) { Double[] array = new Double[count]; reader.ReadArray(array, 0, count, byteOrder); return array; }
					
					public static Byte Squared(this Byte value) { return (Byte)(value * value); }
					public static SByte Squared(this SByte value) { return (SByte)(value * value); }
					public static Int16 Squared(this Int16 value) { return (Int16)(value * value); }
					public static UInt16 Squared(this UInt16 value) { return (UInt16)(value * value); }
					public static Int32 Squared(this Int32 value) { return (Int32)(value * value); }
					public static UInt32 Squared(this UInt32 value) { return (UInt32)(value * value); }
					public static Int64 Squared(this Int64 value) { return (Int64)(value * value); }
					public static UInt64 Squared(this UInt64 value) { return (UInt64)(value * value); }
					public static Float16 Squared(this Float16 value) { return (Float16)(value * value); }
					public static Single Squared(this Single value) { return (Single)(value * value); }
					public static Double Squared(this Double value) { return (Double)(value * value); }
					public static NormalizedByte Squared(this NormalizedByte value) { return (NormalizedByte)(value * value); }
					public static NormalizedSByte Squared(this NormalizedSByte value) { return (NormalizedSByte)(value * value); }
		
		static InvalidDataException CreateExpectationException<T>(T expected, T value)  {
			return new InvalidDataException("Expected a " + expected + " " + typeof(T).Name + " value, but received " + value + ".");
		}


	}
}







