
using System;
using Glare.Framework;

namespace Glare.Assets {
	partial class Lover {
		#region Explicit loading/saving

					
			/// <summary>Attempt to load a <see cref="SByte"/>.</summary>
			/// <param name="value">Receives the <see cref="SByte"/> value.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public void Load(out SByte value) {
									RequireLoading();
					value = (SByte)Stream.ReadByte();
							}

			/// <summary>Attempt to load a <see cref="SByte"/>.</summary>
			/// 
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			/// 
			public SByte LoadSByte() {
				SByte result;
				Load(out result);
				return result;
			}

			/// <summary>Attempt to save a <see cref="SByte"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// 
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			/// 
			public void Save(SByte value) {
									RequireSaving();
					Stream.WriteByte((byte)value);
							}

								
			/// <summary>Attempt to load a <see cref="Byte"/>.</summary>
			/// <param name="value">Receives the <see cref="Byte"/> value.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public void Load(out Byte value) {
									RequireLoading();
					value = (Byte)Stream.ReadByte();
							}

			/// <summary>Attempt to load a <see cref="Byte"/>.</summary>
			/// 
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			/// 
			public Byte LoadByte() {
				Byte result;
				Load(out result);
				return result;
			}

			/// <summary>Attempt to save a <see cref="Byte"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// 
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			/// 
			public void Save(Byte value) {
									RequireSaving();
					Stream.WriteByte((byte)value);
							}

											/// <summary>Attempt to load a <see cref="Int16"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Int16 LoadInt16() { Int16 result; Load(out result, Combination); return result; }

			/// <summary>Attempt to save a <see cref="Int16"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			public void Save(Int16 value) { Save(ref value, Combination); }

			/// <summary>Attempt to load a <see cref="Int16"/> using little-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Int16 LoadInt16LE() { Int16 result; Load(out result, LittleEndianLoader); return result; }

			/// <summary>Attempt to load a <see cref="Int16"/> using big-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Int16 LoadInt16BE() { Int16 result; Load(out result, BigEndianLoader); return result; }
			
			/// <summary>Attempt to load a <see cref="Int16"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">Receives the <see cref="Int16"/> value.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public void Load(out Int16 value) {
									Load(out value, Combination); 
							}

			/// <summary>Attempt to load a <see cref="Int16"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public Int16 LoadInt16(ByteOrder byteOrder) {
				Int16 result;
				Load(out result, LoaderCombinator[byteOrder]);
				return result;
			}

			/// <summary>Attempt to save a <see cref="Int16"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public void Save(Int16 value, ByteOrder byteOrder) {
									Save(ref value, SaverCombinator[byteOrder]);
							}

							void Load(out Int16 value, ByteOrderDirectionCombination combo) {
					switch(combo) {
						case LittleEndianLoader:
							ReadTemporary(2);
							value = (Int16)((Temporary[1] << 0) | (Temporary[0] << 8));
							break;

						case BigEndianLoader:
							ReadTemporary(2);
							value = (Int16)((Temporary[0] << 0) | (Temporary[1] << 8));
							break;

						default:
							throw NotLoaderException();
					}
				}

				void Save(ref Int16 value, ByteOrderDirectionCombination combo) {
					throw new NotImplementedException();
				}
											/// <summary>Attempt to load a <see cref="UInt16"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public UInt16 LoadUInt16() { UInt16 result; Load(out result, Combination); return result; }

			/// <summary>Attempt to save a <see cref="UInt16"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			public void Save(UInt16 value) { Save(ref value, Combination); }

			/// <summary>Attempt to load a <see cref="UInt16"/> using little-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public UInt16 LoadUInt16LE() { UInt16 result; Load(out result, LittleEndianLoader); return result; }

			/// <summary>Attempt to load a <see cref="UInt16"/> using big-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public UInt16 LoadUInt16BE() { UInt16 result; Load(out result, BigEndianLoader); return result; }
			
			/// <summary>Attempt to load a <see cref="UInt16"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">Receives the <see cref="UInt16"/> value.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public void Load(out UInt16 value) {
									Load(out value, Combination); 
							}

			/// <summary>Attempt to load a <see cref="UInt16"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public UInt16 LoadUInt16(ByteOrder byteOrder) {
				UInt16 result;
				Load(out result, LoaderCombinator[byteOrder]);
				return result;
			}

			/// <summary>Attempt to save a <see cref="UInt16"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public void Save(UInt16 value, ByteOrder byteOrder) {
									Save(ref value, SaverCombinator[byteOrder]);
							}

							void Load(out UInt16 value, ByteOrderDirectionCombination combo) {
					switch(combo) {
						case LittleEndianLoader:
							ReadTemporary(2);
							value = (UInt16)((Temporary[1] << 0) | (Temporary[0] << 8));
							break;

						case BigEndianLoader:
							ReadTemporary(2);
							value = (UInt16)((Temporary[0] << 0) | (Temporary[1] << 8));
							break;

						default:
							throw NotLoaderException();
					}
				}

				void Save(ref UInt16 value, ByteOrderDirectionCombination combo) {
					throw new NotImplementedException();
				}
											/// <summary>Attempt to load a <see cref="Int32"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Int32 LoadInt32() { Int32 result; Load(out result, Combination); return result; }

			/// <summary>Attempt to save a <see cref="Int32"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			public void Save(Int32 value) { Save(ref value, Combination); }

			/// <summary>Attempt to load a <see cref="Int32"/> using little-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Int32 LoadInt32LE() { Int32 result; Load(out result, LittleEndianLoader); return result; }

			/// <summary>Attempt to load a <see cref="Int32"/> using big-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Int32 LoadInt32BE() { Int32 result; Load(out result, BigEndianLoader); return result; }
			
			/// <summary>Attempt to load a <see cref="Int32"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">Receives the <see cref="Int32"/> value.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public void Load(out Int32 value) {
									Load(out value, Combination); 
							}

			/// <summary>Attempt to load a <see cref="Int32"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public Int32 LoadInt32(ByteOrder byteOrder) {
				Int32 result;
				Load(out result, LoaderCombinator[byteOrder]);
				return result;
			}

			/// <summary>Attempt to save a <see cref="Int32"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public void Save(Int32 value, ByteOrder byteOrder) {
									Save(ref value, SaverCombinator[byteOrder]);
							}

							void Load(out Int32 value, ByteOrderDirectionCombination combo) {
					switch(combo) {
						case LittleEndianLoader:
							ReadTemporary(4);
							value = (Int32)((Temporary[3] << 0) | (Temporary[2] << 8) | (Temporary[1] << 16) | (Temporary[0] << 24));
							break;

						case BigEndianLoader:
							ReadTemporary(4);
							value = (Int32)((Temporary[0] << 0) | (Temporary[1] << 8) | (Temporary[2] << 16) | (Temporary[3] << 24));
							break;

						default:
							throw NotLoaderException();
					}
				}

				void Save(ref Int32 value, ByteOrderDirectionCombination combo) {
					throw new NotImplementedException();
				}
											/// <summary>Attempt to load a <see cref="UInt32"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public UInt32 LoadUInt32() { UInt32 result; Load(out result, Combination); return result; }

			/// <summary>Attempt to save a <see cref="UInt32"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			public void Save(UInt32 value) { Save(ref value, Combination); }

			/// <summary>Attempt to load a <see cref="UInt32"/> using little-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public UInt32 LoadUInt32LE() { UInt32 result; Load(out result, LittleEndianLoader); return result; }

			/// <summary>Attempt to load a <see cref="UInt32"/> using big-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public UInt32 LoadUInt32BE() { UInt32 result; Load(out result, BigEndianLoader); return result; }
			
			/// <summary>Attempt to load a <see cref="UInt32"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">Receives the <see cref="UInt32"/> value.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public void Load(out UInt32 value) {
									Load(out value, Combination); 
							}

			/// <summary>Attempt to load a <see cref="UInt32"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public UInt32 LoadUInt32(ByteOrder byteOrder) {
				UInt32 result;
				Load(out result, LoaderCombinator[byteOrder]);
				return result;
			}

			/// <summary>Attempt to save a <see cref="UInt32"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public void Save(UInt32 value, ByteOrder byteOrder) {
									Save(ref value, SaverCombinator[byteOrder]);
							}

							void Load(out UInt32 value, ByteOrderDirectionCombination combo) {
					switch(combo) {
						case LittleEndianLoader:
							ReadTemporary(4);
							value = (UInt32)((Temporary[3] << 0) | (Temporary[2] << 8) | (Temporary[1] << 16) | (Temporary[0] << 24));
							break;

						case BigEndianLoader:
							ReadTemporary(4);
							value = (UInt32)((Temporary[0] << 0) | (Temporary[1] << 8) | (Temporary[2] << 16) | (Temporary[3] << 24));
							break;

						default:
							throw NotLoaderException();
					}
				}

				void Save(ref UInt32 value, ByteOrderDirectionCombination combo) {
					throw new NotImplementedException();
				}
											/// <summary>Attempt to load a <see cref="Int64"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Int64 LoadInt64() { Int64 result; Load(out result, Combination); return result; }

			/// <summary>Attempt to save a <see cref="Int64"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			public void Save(Int64 value) { Save(ref value, Combination); }

			/// <summary>Attempt to load a <see cref="Int64"/> using little-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Int64 LoadInt64LE() { Int64 result; Load(out result, LittleEndianLoader); return result; }

			/// <summary>Attempt to load a <see cref="Int64"/> using big-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Int64 LoadInt64BE() { Int64 result; Load(out result, BigEndianLoader); return result; }
			
			/// <summary>Attempt to load a <see cref="Int64"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">Receives the <see cref="Int64"/> value.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public void Load(out Int64 value) {
									Load(out value, Combination); 
							}

			/// <summary>Attempt to load a <see cref="Int64"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public Int64 LoadInt64(ByteOrder byteOrder) {
				Int64 result;
				Load(out result, LoaderCombinator[byteOrder]);
				return result;
			}

			/// <summary>Attempt to save a <see cref="Int64"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public void Save(Int64 value, ByteOrder byteOrder) {
									Save(ref value, SaverCombinator[byteOrder]);
							}

							void Load(out Int64 value, ByteOrderDirectionCombination combo) {
					switch(combo) {
						case LittleEndianLoader:
							ReadTemporary(8);
							value = (Int64)(((ulong)Temporary[7] << 0) | ((ulong)Temporary[6] << 8) | ((ulong)Temporary[5] << 16) | ((ulong)Temporary[4] << 24) | ((ulong)Temporary[3] << 32) | ((ulong)Temporary[2] << 40) | ((ulong)Temporary[1] << 48) | ((ulong)Temporary[0] << 56));
							break;

						case BigEndianLoader:
							ReadTemporary(8);
							value = (Int64)(((ulong)Temporary[0] << 0) | ((ulong)Temporary[1] << 8) | ((ulong)Temporary[2] << 16) | ((ulong)Temporary[3] << 24) | ((ulong)Temporary[4] << 32) | ((ulong)Temporary[5] << 40) | ((ulong)Temporary[6] << 48) | ((ulong)Temporary[7] << 56));
							break;

						default:
							throw NotLoaderException();
					}
				}

				void Save(ref Int64 value, ByteOrderDirectionCombination combo) {
					throw new NotImplementedException();
				}
											/// <summary>Attempt to load a <see cref="UInt64"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public UInt64 LoadUInt64() { UInt64 result; Load(out result, Combination); return result; }

			/// <summary>Attempt to save a <see cref="UInt64"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			public void Save(UInt64 value) { Save(ref value, Combination); }

			/// <summary>Attempt to load a <see cref="UInt64"/> using little-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public UInt64 LoadUInt64LE() { UInt64 result; Load(out result, LittleEndianLoader); return result; }

			/// <summary>Attempt to load a <see cref="UInt64"/> using big-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public UInt64 LoadUInt64BE() { UInt64 result; Load(out result, BigEndianLoader); return result; }
			
			/// <summary>Attempt to load a <see cref="UInt64"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">Receives the <see cref="UInt64"/> value.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public void Load(out UInt64 value) {
									Load(out value, Combination); 
							}

			/// <summary>Attempt to load a <see cref="UInt64"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public UInt64 LoadUInt64(ByteOrder byteOrder) {
				UInt64 result;
				Load(out result, LoaderCombinator[byteOrder]);
				return result;
			}

			/// <summary>Attempt to save a <see cref="UInt64"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public void Save(UInt64 value, ByteOrder byteOrder) {
									Save(ref value, SaverCombinator[byteOrder]);
							}

							void Load(out UInt64 value, ByteOrderDirectionCombination combo) {
					switch(combo) {
						case LittleEndianLoader:
							ReadTemporary(8);
							value = (UInt64)(((ulong)Temporary[7] << 0) | ((ulong)Temporary[6] << 8) | ((ulong)Temporary[5] << 16) | ((ulong)Temporary[4] << 24) | ((ulong)Temporary[3] << 32) | ((ulong)Temporary[2] << 40) | ((ulong)Temporary[1] << 48) | ((ulong)Temporary[0] << 56));
							break;

						case BigEndianLoader:
							ReadTemporary(8);
							value = (UInt64)(((ulong)Temporary[0] << 0) | ((ulong)Temporary[1] << 8) | ((ulong)Temporary[2] << 16) | ((ulong)Temporary[3] << 24) | ((ulong)Temporary[4] << 32) | ((ulong)Temporary[5] << 40) | ((ulong)Temporary[6] << 48) | ((ulong)Temporary[7] << 56));
							break;

						default:
							throw NotLoaderException();
					}
				}

				void Save(ref UInt64 value, ByteOrderDirectionCombination combo) {
					throw new NotImplementedException();
				}
											/// <summary>Attempt to load a <see cref="Single"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Single LoadSingle() { Single result; Load(out result, Combination); return result; }

			/// <summary>Attempt to save a <see cref="Single"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			public void Save(Single value) { Save(ref value, Combination); }

			/// <summary>Attempt to load a <see cref="Single"/> using little-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Single LoadSingleLE() { Single result; Load(out result, LittleEndianLoader); return result; }

			/// <summary>Attempt to load a <see cref="Single"/> using big-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Single LoadSingleBE() { Single result; Load(out result, BigEndianLoader); return result; }
			
			/// <summary>Attempt to load a <see cref="Single"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">Receives the <see cref="Single"/> value.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public void Load(out Single value) {
									Load(out value, Combination); 
							}

			/// <summary>Attempt to load a <see cref="Single"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public Single LoadSingle(ByteOrder byteOrder) {
				Single result;
				Load(out result, LoaderCombinator[byteOrder]);
				return result;
			}

			/// <summary>Attempt to save a <see cref="Single"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public void Save(Single value, ByteOrder byteOrder) {
									Save(ref value, SaverCombinator[byteOrder]);
							}

							void Load(out Single value, ByteOrderDirectionCombination combo) {
					switch(combo) {
						case LittleEndianLoader:
							ReadTemporary(4);
							value = (Single)((Temporary[3] << 0) | (Temporary[2] << 8) | (Temporary[1] << 16) | (Temporary[0] << 24));
							break;

						case BigEndianLoader:
							ReadTemporary(4);
							value = (Single)((Temporary[0] << 0) | (Temporary[1] << 8) | (Temporary[2] << 16) | (Temporary[3] << 24));
							break;

						default:
							throw NotLoaderException();
					}
				}

				void Save(ref Single value, ByteOrderDirectionCombination combo) {
					throw new NotImplementedException();
				}
											/// <summary>Attempt to load a <see cref="Double"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Double LoadDouble() { Double result; Load(out result, Combination); return result; }

			/// <summary>Attempt to save a <see cref="Double"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			public void Save(Double value) { Save(ref value, Combination); }

			/// <summary>Attempt to load a <see cref="Double"/> using little-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Double LoadDoubleLE() { Double result; Load(out result, LittleEndianLoader); return result; }

			/// <summary>Attempt to load a <see cref="Double"/> using big-endian byte order.</summary>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public Double LoadDoubleBE() { Double result; Load(out result, BigEndianLoader); return result; }
			
			/// <summary>Attempt to load a <see cref="Double"/> using the <see cref="DefaultByteOrder"/>.</summary>
			/// <param name="value">Receives the <see cref="Double"/> value.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			public void Load(out Double value) {
									Load(out value, Combination); 
							}

			/// <summary>Attempt to load a <see cref="Double"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsLoading"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public Double LoadDouble(ByteOrder byteOrder) {
				Double result;
				Load(out result, LoaderCombinator[byteOrder]);
				return result;
			}

			/// <summary>Attempt to save a <see cref="Double"/> using the given <see cref="ByteOrder"/>.</summary>
			/// <param name="value">The value to save.</param>
			/// <param name="byteOrder">The byte order to use.</param>
			/// <exception cref="NotSupportedException">This is not a <see cref="IsSaving"/> <see cref="Lover"/>.</exception>
			/// <exception cref="ArgumentException"><paramref name="byteOrder"/> is an unknown or invalid value.</exception>
			public void Save(Double value, ByteOrder byteOrder) {
									Save(ref value, SaverCombinator[byteOrder]);
							}

							void Load(out Double value, ByteOrderDirectionCombination combo) {
					switch(combo) {
						case LittleEndianLoader:
							ReadTemporary(8);
							value = (Double)(((ulong)Temporary[7] << 0) | ((ulong)Temporary[6] << 8) | ((ulong)Temporary[5] << 16) | ((ulong)Temporary[4] << 24) | ((ulong)Temporary[3] << 32) | ((ulong)Temporary[2] << 40) | ((ulong)Temporary[1] << 48) | ((ulong)Temporary[0] << 56));
							break;

						case BigEndianLoader:
							ReadTemporary(8);
							value = (Double)(((ulong)Temporary[0] << 0) | ((ulong)Temporary[1] << 8) | ((ulong)Temporary[2] << 16) | ((ulong)Temporary[3] << 24) | ((ulong)Temporary[4] << 32) | ((ulong)Temporary[5] << 40) | ((ulong)Temporary[6] << 48) | ((ulong)Temporary[7] << 56));
							break;

						default:
							throw NotLoaderException();
					}
				}

				void Save(ref Double value, ByteOrderDirectionCombination combo) {
					throw new NotImplementedException();
				}
					
		#endregion Explicit loading/saving
	}
}

