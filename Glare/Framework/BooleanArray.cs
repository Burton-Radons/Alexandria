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

		/// <summary>Get a unit value of 1.</summary>
		public const Unit UnitOne = 1;

		/// <summary>Get a unit value of 0.</summary>
		public const Unit UnitZero = 0;

		/// <summary>Get a unit value of all bits set.</summary>
		public const Unit UnitMax = ~UnitZero;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly int pCount;

		readonly int pUnitCount;

		internal readonly unsafe Unit* pUnits;

		public int Count { get { return pCount; } }

		/// <summary>Get the number of bit packed units in the array.</summary>
		public int UnitCount { get { return pUnitCount; } }

		public bool this[int index] {
			get {
				if (index < 0 || index >= pCount)
					throw new ArgumentOutOfRangeException("index");
				unsafe {
					return (pUnits[index >> UnitShift] & (UnitOne << (index & UnitMask))) != 0;
				}
			}

			set {
				if (index < 0 || index >= pCount)
					throw new ArgumentOutOfRangeException("index");

				int unitIndex = index >> UnitShift;
				Unit unitBit = UnitOne << (index & UnitMask);

				unsafe {
					if (value)
						pUnits[unitIndex] |= unitBit;
					else
						pUnits[unitIndex] &= ~unitBit;
				}
			}
		}

		public BooleanArray(int length) {
			pUnitCount = (length + BitsPerUnit - 1) / BitsPerUnit;
			unsafe {
				pUnits = (Unit*)Marshal.AllocHGlobal(pUnitCount * (BitsPerUnit / 8)).ToPointer();
			}
			//pUnits = new Unit[(length + BitsPerUnit - 1) / BitsPerUnit];
			pCount = length;
		}

		public BooleanArray(long length) : this(checked((int)length)) { }

		~BooleanArray() { unsafe { Marshal.FreeHGlobal(new IntPtr(pUnits)); } }

		/// <summary>Check the range of values that is inclusive to the end.</summary>
		/// <param name="start">The first index.</param>
		/// <param name="count">The number of elements.</param>
		void pCheckRange(int start, int count) {
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

		public int IndexOf(bool item) {
			return IndexOf(item, 0);
		}

		public int IndexOf(bool item, int startIndex) {
			if (startIndex < 0 || startIndex > Count)
				throw new ArgumentOutOfRangeException("startIndex");

			int blockCount = pUnitCount;
			int blockIndex = startIndex / BitsPerUnit;
			int bitIndex = startIndex & UnitMask;

			unsafe {
				for (; blockIndex < blockCount; bitIndex = 0, blockIndex++) {
					Unit block = pUnits[blockIndex];

					if (item ? block != 0 : block == 0)
						for (; bitIndex < BitsPerUnit; bitIndex++) {
							bool value = (block & (UnitOne << bitIndex)) != 0;
							if (value == item) {
								int index = bitIndex + blockIndex * BitsPerUnit;
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
		public void Set(int start, int count, bool value) {
			pCheckRange(start, count);

			int end = start + count;
			int endUnit = (end - 1) >> UnitShift;
			int unit = start >> UnitShift;
			Unit mask;

			// Get bits to the end of the range or to the end of the start unit.
			if (endUnit == unit)
				mask = GetMask(count, start & UnitMask);
			else
				mask = UnitMax << (start & UnitMask);

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
					else if (unit == endUnit)
						// Last unit, get the final bit mask.
						mask = GetMask(end & UnitMask);
					else // unit > endUnit
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

		public bool Contains(bool item) { return IndexOf(item) >= 0; }

		public void CopyTo(bool[] array, int arrayIndex) {
			for (int index = 0; index < Count; index++)
				array[index + arrayIndex] = this[index];
		}

		bool ICollection<bool>.IsReadOnly { get { return false; } }
		bool IList.IsReadOnly { get { return false; } }

		bool ICollection<bool>.Remove(bool item) { throw FixedSizeException(); }

		public Enumerator GetEnumerator() { return new Enumerator(this); }

		IEnumerator<bool> IEnumerable<bool>.GetEnumerator() { return GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

		int IList.Add(object value) { throw FixedSizeException(); }

		void IList.Clear() { throw FixedSizeException(); }

		bool IList.Contains(object value) { return Contains((bool)value); }

		int IList.IndexOf(object value) { return IndexOf((bool)value); }

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

		public struct Enumerator : IEnumerator<bool> {
			unsafe readonly Unit* Units;
			readonly int Count;
			int BlockIndex, BitIndex;
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

			public bool Current {
				get { return (BlockValue & (UnitOne << BitIndex)) != 0; }
			}

			void IDisposable.Dispose() { }

			object IEnumerator.Current { get { return Current; } }

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

			public void Reset() {
				BitIndex = BitsPerUnit;
				BlockIndex = -1;
			}
		}
	}
}
