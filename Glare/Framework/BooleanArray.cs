using Glare.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Unit = System.UInt64;

namespace Glare.Framework {
	/// <summary>
	/// A packed list of <see cref="Boolean"/> values stored as a list of unsigned integers, both to save space and to allow for efficient operations.
	/// </summary>
	public class BooleanArray : IList<bool>, IList {
		/// <summary>Get a bit shift for turning an index into a unit index.</summary>
		public const int UnitShift = 6;

		/// <summary>Get a mask for isolating the bit index in a unit.</summary>
		const int UnitMask = (BitsPerUnit) - 1;

		/// <summary>Get the number of bits in a unit value.</summary>
		public const int BitsPerUnit = 1 << UnitShift;

		/// <summary>Get the number of bytes in a unit value.</summary>
		public const int BytesPerUnit = (BitsPerUnit + 7) / 8;

		/// <summary>Get a unit value of 1.</summary>
		public const Unit UnitOne = 1;

		/// <summary>Get a unit value of 0.</summary>
		public const Unit UnitZero = 0;

		/// <summary>Get a unit value of all bits set.</summary>
		public const Unit UnitMax = ~UnitZero;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly long pCount;

		readonly long pUnitCount;

		internal readonly unsafe Unit* pUnits;

		/// <summary>Get the number of bits in the array.</summary>
		public long Count { get { return pCount; } }

		int ICollection<bool>.Count { get { return checked((int)pCount); } }
		int ICollection.Count { get { return checked((int)pCount); } }
		

		/// <summary>Get the number of bit packed units in the array.</summary>
		public long UnitCount { get { return pUnitCount; } }

		bool IList<bool>.this[int index] { get { return this[index]; } set { this[index] = value; } }

		/// <summary>Get or set a boolean value in the array.</summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public bool this[long index] {
			get {
				if (index < 0 || index >= pCount)
					throw new ArgumentOutOfRangeException("index");
				unsafe {
					return (pUnits[index >> UnitShift] & (UnitOne << (int)(index & UnitMask))) != 0;
				}
			}

			set {
				if (index < 0 || index >= pCount)
					throw new ArgumentOutOfRangeException("index");

				int unitIndex = checked((int)(index >> UnitShift));
				Unit unitBit = UnitOne << (int)(index & UnitMask);

				unsafe {
					if (value)
						pUnits[unitIndex] |= unitBit;
					else
						pUnits[unitIndex] &= ~unitBit;
				}
			}
		}

		/// <summary>Initialise the boolean array.</summary>
		/// <param name="length"></param>
		public BooleanArray(long length) {
			pUnitCount = (length + BitsPerUnit - 1) / BitsPerUnit;
			unsafe {
				pUnits = (Unit*)Marshal.AllocHGlobal(new IntPtr(pUnitCount * BytesPerUnit)).ToPointer();
			}
			//pUnits = new Unit[(length + BitsPerUnit - 1) / BitsPerUnit];
			pCount = length;
		}

		/// <summary>Frees the memory allocated for the array.</summary>
		~BooleanArray() { unsafe { Marshal.FreeHGlobal(new IntPtr(pUnits)); } }

		/// <summary>Check the range of values that is inclusive to the end.</summary>
		/// <param name="start">The first index.</param>
		/// <param name="count">The number of elements.</param>
		void pCheckRange(long start, long count) {
			if (start < 0 || start > pCount)
				throw new ArgumentOutOfRangeException("start");
			if (count < 0 || start + count > pCount)
				throw new ArgumentOutOfRangeException("count");
		}

		static Unit GetBitCount(int bits) { return UnitOne << bits; }
		static Unit GetMask(int bits) { return (UnitOne << bits) - 1; }
		static Unit GetMask(int bits, int shift) { return ((UnitOne << bits) - 1) << shift; }

		/// <summary>Get a bit-packed unit.</summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Unit GetUnit(int index) {
			if (index < 0 || index >= pUnitCount)
				throw new ArgumentOutOfRangeException("index");
			unsafe { return pUnits[index]; }
		}

		int IList<bool>.IndexOf(bool item) { return checked((int)IndexOf(item)); }

		/// <summary>Get the first index of the array with the given value, or -1 if not found.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public long IndexOf(bool item) {
			return IndexOf(item, 0);
		}

		/// <summary>Get the first index of the array with the given value, or -1 if not found.</summary>
		/// <param name="item"></param>
		/// <param name="startIndex"></param>
		/// <returns></returns>
		public long IndexOf(bool item, long startIndex) {
			if (startIndex < 0 || startIndex > Count)
				throw new ArgumentOutOfRangeException("startIndex");

			long blockCount = pUnitCount;
			long blockIndex = checked((int)startIndex / BitsPerUnit);
			int bitIndex = (int)(startIndex & UnitMask);

			unsafe {
				for (; blockIndex < blockCount; bitIndex = 0, blockIndex++) {
					Unit block = pUnits[blockIndex];

					if (item ? block != 0 : block == 0)
						for (; bitIndex < BitsPerUnit; bitIndex++) {
							bool value = (block & (UnitOne << bitIndex)) != 0;
							if (value == item) {
								long index = bitIndex + blockIndex * BitsPerUnit;
								return index < Count ? index : -1;
							}
						}
				}
			}

			return -1;
		}

		/// <summary>Set a range of elements to the same value.</summary>
		/// <param name="start">The index of the first element to set.</param>
		/// <param name="count">The number of elements to set to the value.</param>
		/// <param name="value">The value to assign to the elements.</param>
		/// <remarks>The <see cref="BooleanArray"/> will efficiently assign values.</remarks>
		public void Set(long start, long count, bool value) {
			pCheckRange(start, count);

			long end = start + count;
			long endUnit = (end - 1) >> UnitShift;
			long unit = start >> UnitShift;
			Unit mask;

			// Get bits to the end of the range or to the end of the start unit.
			if (endUnit == unit)
				mask = GetMask((int)count, (int)(start & UnitMask));
			else
				mask = UnitMax << (int)(start & UnitMask);

			unsafe {
				while (true) {
					if (value)
						pUnits[unit] |= mask;
					else
						pUnits[unit] ^= ~mask;

					unit++;
					if (unit < endUnit)
						// At least BitsPerUnit more to go...
						mask = UnitMax;
					else if (unit == endUnit) {
						// Last unit, get the final bit mask.
						int bitCount = (int)(end & UnitMask);
						mask = bitCount == 0 ? UnitMax : GetMask(bitCount);
					} else // unit > endUnit
						break;
				}
			}
		}

		#region Interfaces

		Exception FixedSizeException() { return new NotSupportedException("This " + GetType().Name + " has fixed dimensions."); }

		void IList<bool>.Insert(int index, bool item) { throw FixedSizeException(); }
		void IList<bool>.RemoveAt(int index) { throw FixedSizeException(); }

		void ICollection<bool>.Add(bool item) { throw FixedSizeException(); }

		void ICollection<bool>.Clear() { throw FixedSizeException(); }

		/// <summary>Get whether the array contains the item.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(bool item) { return IndexOf(item) >= 0; }

		/// <summary>Copy into another array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(bool[] array, int arrayIndex) {
			for (int index = 0; index < Count; index++)
				array[index + arrayIndex] = this[index];
		}

		bool ICollection<bool>.IsReadOnly { get { return false; } }
		bool IList.IsReadOnly { get { return false; } }

		bool ICollection<bool>.Remove(bool item) { throw FixedSizeException(); }

		/// <summary>Get an enumerator over the elements of this array.</summary>
		/// <returns></returns>
		public Enumerator GetEnumerator() { return new Enumerator(this); }

		IEnumerator<bool> IEnumerable<bool>.GetEnumerator() { return GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

		int IList.Add(object value) { throw FixedSizeException(); }

		void IList.Clear() { throw FixedSizeException(); }

		bool IList.Contains(object value) { return Contains((bool)value); }

		int IList.IndexOf(object value) { return checked((int)IndexOf((bool)value)); }

		void IList.Insert(int index, object value) { throw FixedSizeException(); }

		bool IList.IsFixedSize { get { return true; } }

		void IList.Remove(object value) { throw FixedSizeException(); }

		void IList.RemoveAt(int index) { throw FixedSizeException(); }

		object IList.this[int index] {
			get { return this[index]; }
			set { this[index] = (bool)value; }
		}

		void ICollection.CopyTo(Array array, int index) {
			for (int bitIndex = 0; bitIndex < Count; bitIndex++)
				array.SetValue(this[bitIndex], bitIndex + index);
		}

		bool ICollection.IsSynchronized { get { return false; } }

		object ICollection.SyncRoot { get { return this; } }

		#endregion Interfaces

		/// <summary>
		/// An enumerator for a boolean array.
		/// </summary>
		public struct Enumerator : IEnumerator<bool> {
			unsafe readonly Unit* Units;
			readonly long Count;
			long BlockIndex;
			int BitIndex;
			Unit BlockValue;

			internal Enumerator(BooleanArray array) {
				unsafe {
					Units = array.pUnits;
				}

				BlockIndex = -1;
				BitIndex = BitsPerUnit;
				Count = array.Count;
				BlockValue = 0;
			}

			/// <summary>Get the current value.</summary>
			public bool Current {
				get { return (BlockValue & (UnitOne << BitIndex)) != 0; }
			}

			void IDisposable.Dispose() { }

			object IEnumerator.Current { get { return Current; } }

			/// <summary>Move to the next value in the array, returning whether the end has been reached.</summary>
			/// <returns></returns>
			public bool MoveNext() {
				BitIndex++;
				if (BitIndex + BlockIndex * BitsPerUnit >= Count)
					return false;

				if (BitIndex >= BitsPerUnit) {
					unsafe {
						BlockValue = Units[BlockIndex++];
					}

					BitIndex = 0;
				}

				return true;
			}

			/// <summary>Reset the enumerator parameters.</summary>
			public void Reset() {
				BitIndex = BitsPerUnit;
				BlockIndex = -1;
			}
		}
	}
}
