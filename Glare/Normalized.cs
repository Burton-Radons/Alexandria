 
 

using System;

namespace Glare {
				/// <summary>
			/// A value between <c>-1.0</c> and <c>1.0</c> that is stored in a single <see cref="SByte"/>.
			/// </summary>
			/// <remarks>
			/// All public constructors take floating-point values and none take coded values, because that would be confusing and error-prone. To construct a <see cref="NormalizedSByte"/> with a code, assign <see cref="Value"/> directly or use <see cref="CreateCoded"/>.
			/// </remarks>
			public struct NormalizedSByte : IComparable<NormalizedSByte>, IEquatable<NormalizedSByte>, IFormattable {
				/// <summary>Get or set the coded value.</summary>
				public SByte Value;

				NormalizedSByte(SByte value) { Value = value; }

				/// <summary>Construct a <see cref="NormalizedSByte"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedSByte(float value) : this(((SByte)(Math.Max(Math.Min((Double)value, -1.0), 1.0) * SByte.MaxValue))) { }

				/// <summary>Construct a <see cref="NormalizedSByte"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedSByte(double value) : this(((SByte)(Math.Max(Math.Min((Double)value, -1.0), 1.0) * SByte.MaxValue))) { }

				/// <summary>Get a zero value.</summary>
				public static readonly NormalizedSByte Zero = new NormalizedSByte(0);

				/// <summary>Get a one value.</summary>
				public static readonly NormalizedSByte One = new NormalizedSByte(1);

				/// <summary>Compare this value to a double.</summary>
				public int CompareTo(double other) { return ((Double)this).CompareTo(other); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedSByte other) { return Value.CompareTo(other.Value); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedByte other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt16 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt16 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt32 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt32 other) { return ((Double)this).CompareTo(other); }
				
				/// <summary>Create a <see cref="NormalizedSByte"/> from a coded value.</summary>
				public static NormalizedSByte CreateCoded(SByte Value) { return new NormalizedSByte(Value); }

				/// <summary>Check whether this equals a <see cref="Single"/>, <see cref="Double"/>, or any other normalized type.</summary>
				public override bool Equals(object obj) {
					if (obj is Single) return Equals((Single)obj);
					if (obj is Double) return Equals((Double)obj);
					if(obj is NormalizedSByte) return Equals((NormalizedSByte)obj);
					if(obj is NormalizedByte) return Equals((NormalizedByte)obj);
					if(obj is NormalizedInt16) return Equals((NormalizedInt16)obj);
					if(obj is NormalizedUInt16) return Equals((NormalizedUInt16)obj);
					if(obj is NormalizedInt32) return Equals((NormalizedInt32)obj);
					if(obj is NormalizedUInt32) return Equals((NormalizedUInt32)obj);
					return base.Equals(obj);
				}

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(double obj) { return (double)this == obj; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedSByte obj) { return Value == obj.Value; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedByte obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt16 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt16 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt32 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt32 obj) { return (double)this == (double)obj; }
				
				/// <summary>Get a hash code based on the coded value.</summary>
				public override int GetHashCode() { return Value.GetHashCode(); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4nsb".</summary>
				public override string ToString() { return ToString(null, null); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4nsb".</summary>
				public string ToString(string format, IFormatProvider provider) { return ((double)this).ToString(format, provider) + "nsb"; }

				/// <summary>Implicity cast from <see cref="NormalizedSByte"/> to <see cref="Float16"/></summary>
				public static implicit operator Float16(NormalizedSByte Value) { return (Float16)(SByte)(Math.Max((Value.Value / (Double)SByte.MaxValue), 1.0)); }

				/// <summary>Implicitly cast from <see cref="NormalizedSByte"/> to <see cref="Single"/></summary>
				public static implicit operator float(NormalizedSByte Value) { return (float)(SByte)(Math.Max((Value.Value / (Double)SByte.MaxValue), 1.0)); }

				/// <summary>Implicitly cast from <see cref="NormalizedSByte"/> to <see cref="Double"/></summary>
				public static implicit operator double(NormalizedSByte Value) { return (SByte)(Math.Max((Value.Value / (Double)SByte.MaxValue), 1.0)); }

				/// <summary>Explicitly cast from <see cref="NormalizedSByte"/> to <see cref="Int32"/></summary>
				public static explicit operator int(NormalizedSByte Value) { return (int)(float)Value; }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedSByte"/>.</summary>
				public static explicit operator NormalizedSByte(Float16 Value) { return new NormalizedSByte((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedSByte"/>.</summary>
				public static explicit operator NormalizedSByte(float Value) { return new NormalizedSByte(Value); }

				/// <summary>Explicitly cast from <see cref="Double"/> to <see cref="NormalizedSByte"/>.</summary>
				public static explicit operator NormalizedSByte(double Value) { return new NormalizedSByte(Value); }

				/// <summary>Explicitly cast from <see cref="Int32"/> to <see cref="NormalizedSByte"/>.</summary>
				public static explicit operator NormalizedSByte(int Value) { return new NormalizedSByte((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Int64"/> to <see cref="NormalizedSByte"/>.</summary>
				public static explicit operator NormalizedSByte(long Value) { return new NormalizedSByte((Double)Value); }

				/// <summary>Explicitly cast from NormalizedSByte to NormalizedByte.</summary>
				public static explicit operator NormalizedByte(NormalizedSByte value) { return new NormalizedByte((double)value); }

				/// <summary>Explicitly cast from NormalizedSByte to NormalizedInt16.</summary>
				public static explicit operator NormalizedInt16(NormalizedSByte value) { return new NormalizedInt16((double)value); }

				/// <summary>Explicitly cast from NormalizedSByte to NormalizedUInt16.</summary>
				public static explicit operator NormalizedUInt16(NormalizedSByte value) { return new NormalizedUInt16((double)value); }

				/// <summary>Explicitly cast from NormalizedSByte to NormalizedInt32.</summary>
				public static explicit operator NormalizedInt32(NormalizedSByte value) { return new NormalizedInt32((double)value); }

				/// <summary>Explicitly cast from NormalizedSByte to NormalizedUInt32.</summary>
				public static explicit operator NormalizedUInt32(NormalizedSByte value) { return new NormalizedUInt32((double)value); }

				
				/// <summary>Get whether these values are equal.</summary>
				public static bool operator ==(NormalizedSByte a, NormalizedSByte b) { return a.Value == b.Value; }

				/// <summary>Get whether these values are unequal.</summary>
				public static bool operator !=(NormalizedSByte a, NormalizedSByte b) { return a.Value != b.Value; }

				/// <summary>Get whether <paramref name="a"/> is greater than <paramref name="b"/>.</summary>
				public static bool operator >(NormalizedSByte a, NormalizedSByte b) { return a.CompareTo(b) > 0; }

				/// <summary>Get whether <paramref name="a"/> is greater than or equal to <paramref name="b"/>.</summary>
				public static bool operator >=(NormalizedSByte a, NormalizedSByte b) { return a.CompareTo(b) >= 0; }

				/// <summary>Get whether <paramref name="a"/> is less than <paramref name="b"/>.</summary>
				public static bool operator <(NormalizedSByte a, NormalizedSByte b) { return a.CompareTo(b) < 0; }

				/// <summary>Get whether <paramref name="a"/> is less than or equal to <paramref name="b"/>.</summary>
				public static bool operator <=(NormalizedSByte a, NormalizedSByte b) { return a.CompareTo(b) <= 0; }
			}
					/// <summary>
			/// A value between <c>0.0</c> and <c>1.0</c> that is stored in a single <see cref="Byte"/>.
			/// </summary>
			/// <remarks>
			/// All public constructors take floating-point values and none take coded values, because that would be confusing and error-prone. To construct a <see cref="NormalizedByte"/> with a code, assign <see cref="Value"/> directly or use <see cref="CreateCoded"/>.
			/// </remarks>
			public struct NormalizedByte : IComparable<NormalizedByte>, IEquatable<NormalizedByte>, IFormattable {
				/// <summary>Get or set the coded value.</summary>
				public Byte Value;

				NormalizedByte(Byte value) { Value = value; }

				/// <summary>Construct a <see cref="NormalizedByte"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedByte(float value) : this(((Byte)(Math.Max(Math.Min((Double)value, 0.0), 1.0) * Byte.MaxValue))) { }

				/// <summary>Construct a <see cref="NormalizedByte"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedByte(double value) : this(((Byte)(Math.Max(Math.Min((Double)value, 0.0), 1.0) * Byte.MaxValue))) { }

				/// <summary>Get a zero value.</summary>
				public static readonly NormalizedByte Zero = new NormalizedByte(0);

				/// <summary>Get a one value.</summary>
				public static readonly NormalizedByte One = new NormalizedByte(1);

				/// <summary>Compare this value to a double.</summary>
				public int CompareTo(double other) { return ((Double)this).CompareTo(other); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedByte other) { return Value.CompareTo(other.Value); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedSByte other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt16 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt16 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt32 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt32 other) { return ((Double)this).CompareTo(other); }
				
				/// <summary>Create a <see cref="NormalizedByte"/> from a coded value.</summary>
				public static NormalizedByte CreateCoded(Byte Value) { return new NormalizedByte(Value); }

				/// <summary>Check whether this equals a <see cref="Single"/>, <see cref="Double"/>, or any other normalized type.</summary>
				public override bool Equals(object obj) {
					if (obj is Single) return Equals((Single)obj);
					if (obj is Double) return Equals((Double)obj);
					if(obj is NormalizedSByte) return Equals((NormalizedSByte)obj);
					if(obj is NormalizedByte) return Equals((NormalizedByte)obj);
					if(obj is NormalizedInt16) return Equals((NormalizedInt16)obj);
					if(obj is NormalizedUInt16) return Equals((NormalizedUInt16)obj);
					if(obj is NormalizedInt32) return Equals((NormalizedInt32)obj);
					if(obj is NormalizedUInt32) return Equals((NormalizedUInt32)obj);
					return base.Equals(obj);
				}

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(double obj) { return (double)this == obj; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedByte obj) { return Value == obj.Value; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedSByte obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt16 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt16 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt32 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt32 obj) { return (double)this == (double)obj; }
				
				/// <summary>Get a hash code based on the coded value.</summary>
				public override int GetHashCode() { return Value.GetHashCode(); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4nb".</summary>
				public override string ToString() { return ToString(null, null); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4nb".</summary>
				public string ToString(string format, IFormatProvider provider) { return ((double)this).ToString(format, provider) + "nb"; }

				/// <summary>Implicity cast from <see cref="NormalizedByte"/> to <see cref="Float16"/></summary>
				public static implicit operator Float16(NormalizedByte Value) { return (Float16)(Byte)((Value.Value / (Double)Byte.MaxValue)); }

				/// <summary>Implicitly cast from <see cref="NormalizedByte"/> to <see cref="Single"/></summary>
				public static implicit operator float(NormalizedByte Value) { return (float)(Byte)((Value.Value / (Double)Byte.MaxValue)); }

				/// <summary>Implicitly cast from <see cref="NormalizedByte"/> to <see cref="Double"/></summary>
				public static implicit operator double(NormalizedByte Value) { return (Byte)((Value.Value / (Double)Byte.MaxValue)); }

				/// <summary>Explicitly cast from <see cref="NormalizedByte"/> to <see cref="Int32"/></summary>
				public static explicit operator int(NormalizedByte Value) { return (int)(float)Value; }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedByte"/>.</summary>
				public static explicit operator NormalizedByte(Float16 Value) { return new NormalizedByte((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedByte"/>.</summary>
				public static explicit operator NormalizedByte(float Value) { return new NormalizedByte(Value); }

				/// <summary>Explicitly cast from <see cref="Double"/> to <see cref="NormalizedByte"/>.</summary>
				public static explicit operator NormalizedByte(double Value) { return new NormalizedByte(Value); }

				/// <summary>Explicitly cast from <see cref="Int32"/> to <see cref="NormalizedByte"/>.</summary>
				public static explicit operator NormalizedByte(int Value) { return new NormalizedByte((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Int64"/> to <see cref="NormalizedByte"/>.</summary>
				public static explicit operator NormalizedByte(long Value) { return new NormalizedByte((Double)Value); }

				/// <summary>Explicitly cast from NormalizedByte to NormalizedSByte.</summary>
				public static explicit operator NormalizedSByte(NormalizedByte value) { return new NormalizedSByte((double)value); }

				/// <summary>Explicitly cast from NormalizedByte to NormalizedInt16.</summary>
				public static explicit operator NormalizedInt16(NormalizedByte value) { return new NormalizedInt16((double)value); }

				/// <summary>Explicitly cast from NormalizedByte to NormalizedUInt16.</summary>
				public static explicit operator NormalizedUInt16(NormalizedByte value) { return new NormalizedUInt16((double)value); }

				/// <summary>Explicitly cast from NormalizedByte to NormalizedInt32.</summary>
				public static explicit operator NormalizedInt32(NormalizedByte value) { return new NormalizedInt32((double)value); }

				/// <summary>Explicitly cast from NormalizedByte to NormalizedUInt32.</summary>
				public static explicit operator NormalizedUInt32(NormalizedByte value) { return new NormalizedUInt32((double)value); }

				
				/// <summary>Get whether these values are equal.</summary>
				public static bool operator ==(NormalizedByte a, NormalizedByte b) { return a.Value == b.Value; }

				/// <summary>Get whether these values are unequal.</summary>
				public static bool operator !=(NormalizedByte a, NormalizedByte b) { return a.Value != b.Value; }

				/// <summary>Get whether <paramref name="a"/> is greater than <paramref name="b"/>.</summary>
				public static bool operator >(NormalizedByte a, NormalizedByte b) { return a.CompareTo(b) > 0; }

				/// <summary>Get whether <paramref name="a"/> is greater than or equal to <paramref name="b"/>.</summary>
				public static bool operator >=(NormalizedByte a, NormalizedByte b) { return a.CompareTo(b) >= 0; }

				/// <summary>Get whether <paramref name="a"/> is less than <paramref name="b"/>.</summary>
				public static bool operator <(NormalizedByte a, NormalizedByte b) { return a.CompareTo(b) < 0; }

				/// <summary>Get whether <paramref name="a"/> is less than or equal to <paramref name="b"/>.</summary>
				public static bool operator <=(NormalizedByte a, NormalizedByte b) { return a.CompareTo(b) <= 0; }
			}
					/// <summary>
			/// A value between <c>-1.0</c> and <c>1.0</c> that is stored in a single <see cref="Int16"/>.
			/// </summary>
			/// <remarks>
			/// All public constructors take floating-point values and none take coded values, because that would be confusing and error-prone. To construct a <see cref="NormalizedInt16"/> with a code, assign <see cref="Value"/> directly or use <see cref="CreateCoded"/>.
			/// </remarks>
			public struct NormalizedInt16 : IComparable<NormalizedInt16>, IEquatable<NormalizedInt16>, IFormattable {
				/// <summary>Get or set the coded value.</summary>
				public Int16 Value;

				NormalizedInt16(Int16 value) { Value = value; }

				/// <summary>Construct a <see cref="NormalizedInt16"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedInt16(float value) : this(((Int16)(Math.Max(Math.Min((Double)value, -1.0), 1.0) * Int16.MaxValue))) { }

				/// <summary>Construct a <see cref="NormalizedInt16"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedInt16(double value) : this(((Int16)(Math.Max(Math.Min((Double)value, -1.0), 1.0) * Int16.MaxValue))) { }

				/// <summary>Get a zero value.</summary>
				public static readonly NormalizedInt16 Zero = new NormalizedInt16(0);

				/// <summary>Get a one value.</summary>
				public static readonly NormalizedInt16 One = new NormalizedInt16(1);

				/// <summary>Compare this value to a double.</summary>
				public int CompareTo(double other) { return ((Double)this).CompareTo(other); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt16 other) { return Value.CompareTo(other.Value); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedSByte other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedByte other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt16 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt32 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt32 other) { return ((Double)this).CompareTo(other); }
				
				/// <summary>Create a <see cref="NormalizedInt16"/> from a coded value.</summary>
				public static NormalizedInt16 CreateCoded(Int16 Value) { return new NormalizedInt16(Value); }

				/// <summary>Check whether this equals a <see cref="Single"/>, <see cref="Double"/>, or any other normalized type.</summary>
				public override bool Equals(object obj) {
					if (obj is Single) return Equals((Single)obj);
					if (obj is Double) return Equals((Double)obj);
					if(obj is NormalizedSByte) return Equals((NormalizedSByte)obj);
					if(obj is NormalizedByte) return Equals((NormalizedByte)obj);
					if(obj is NormalizedInt16) return Equals((NormalizedInt16)obj);
					if(obj is NormalizedUInt16) return Equals((NormalizedUInt16)obj);
					if(obj is NormalizedInt32) return Equals((NormalizedInt32)obj);
					if(obj is NormalizedUInt32) return Equals((NormalizedUInt32)obj);
					return base.Equals(obj);
				}

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(double obj) { return (double)this == obj; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt16 obj) { return Value == obj.Value; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedSByte obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedByte obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt16 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt32 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt32 obj) { return (double)this == (double)obj; }
				
				/// <summary>Get a hash code based on the coded value.</summary>
				public override int GetHashCode() { return Value.GetHashCode(); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4ns".</summary>
				public override string ToString() { return ToString(null, null); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4ns".</summary>
				public string ToString(string format, IFormatProvider provider) { return ((double)this).ToString(format, provider) + "ns"; }

				/// <summary>Implicity cast from <see cref="NormalizedInt16"/> to <see cref="Float16"/></summary>
				public static implicit operator Float16(NormalizedInt16 Value) { return (Float16)(Int16)(Math.Max((Value.Value / (Double)Int16.MaxValue), 1.0)); }

				/// <summary>Implicitly cast from <see cref="NormalizedInt16"/> to <see cref="Single"/></summary>
				public static implicit operator float(NormalizedInt16 Value) { return (float)(Int16)(Math.Max((Value.Value / (Double)Int16.MaxValue), 1.0)); }

				/// <summary>Implicitly cast from <see cref="NormalizedInt16"/> to <see cref="Double"/></summary>
				public static implicit operator double(NormalizedInt16 Value) { return (Int16)(Math.Max((Value.Value / (Double)Int16.MaxValue), 1.0)); }

				/// <summary>Explicitly cast from <see cref="NormalizedInt16"/> to <see cref="Int32"/></summary>
				public static explicit operator int(NormalizedInt16 Value) { return (int)(float)Value; }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedInt16"/>.</summary>
				public static explicit operator NormalizedInt16(Float16 Value) { return new NormalizedInt16((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedInt16"/>.</summary>
				public static explicit operator NormalizedInt16(float Value) { return new NormalizedInt16(Value); }

				/// <summary>Explicitly cast from <see cref="Double"/> to <see cref="NormalizedInt16"/>.</summary>
				public static explicit operator NormalizedInt16(double Value) { return new NormalizedInt16(Value); }

				/// <summary>Explicitly cast from <see cref="Int32"/> to <see cref="NormalizedInt16"/>.</summary>
				public static explicit operator NormalizedInt16(int Value) { return new NormalizedInt16((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Int64"/> to <see cref="NormalizedInt16"/>.</summary>
				public static explicit operator NormalizedInt16(long Value) { return new NormalizedInt16((Double)Value); }

				/// <summary>Explicitly cast from NormalizedInt16 to NormalizedSByte.</summary>
				public static explicit operator NormalizedSByte(NormalizedInt16 value) { return new NormalizedSByte((double)value); }

				/// <summary>Explicitly cast from NormalizedInt16 to NormalizedByte.</summary>
				public static explicit operator NormalizedByte(NormalizedInt16 value) { return new NormalizedByte((double)value); }

				/// <summary>Explicitly cast from NormalizedInt16 to NormalizedUInt16.</summary>
				public static explicit operator NormalizedUInt16(NormalizedInt16 value) { return new NormalizedUInt16((double)value); }

				/// <summary>Explicitly cast from NormalizedInt16 to NormalizedInt32.</summary>
				public static explicit operator NormalizedInt32(NormalizedInt16 value) { return new NormalizedInt32((double)value); }

				/// <summary>Explicitly cast from NormalizedInt16 to NormalizedUInt32.</summary>
				public static explicit operator NormalizedUInt32(NormalizedInt16 value) { return new NormalizedUInt32((double)value); }

				
				/// <summary>Get whether these values are equal.</summary>
				public static bool operator ==(NormalizedInt16 a, NormalizedInt16 b) { return a.Value == b.Value; }

				/// <summary>Get whether these values are unequal.</summary>
				public static bool operator !=(NormalizedInt16 a, NormalizedInt16 b) { return a.Value != b.Value; }

				/// <summary>Get whether <paramref name="a"/> is greater than <paramref name="b"/>.</summary>
				public static bool operator >(NormalizedInt16 a, NormalizedInt16 b) { return a.CompareTo(b) > 0; }

				/// <summary>Get whether <paramref name="a"/> is greater than or equal to <paramref name="b"/>.</summary>
				public static bool operator >=(NormalizedInt16 a, NormalizedInt16 b) { return a.CompareTo(b) >= 0; }

				/// <summary>Get whether <paramref name="a"/> is less than <paramref name="b"/>.</summary>
				public static bool operator <(NormalizedInt16 a, NormalizedInt16 b) { return a.CompareTo(b) < 0; }

				/// <summary>Get whether <paramref name="a"/> is less than or equal to <paramref name="b"/>.</summary>
				public static bool operator <=(NormalizedInt16 a, NormalizedInt16 b) { return a.CompareTo(b) <= 0; }
			}
					/// <summary>
			/// A value between <c>-1.0</c> and <c>1.0</c> that is stored in a single <see cref="UInt16"/>.
			/// </summary>
			/// <remarks>
			/// All public constructors take floating-point values and none take coded values, because that would be confusing and error-prone. To construct a <see cref="NormalizedUInt16"/> with a code, assign <see cref="Value"/> directly or use <see cref="CreateCoded"/>.
			/// </remarks>
			public struct NormalizedUInt16 : IComparable<NormalizedUInt16>, IEquatable<NormalizedUInt16>, IFormattable {
				/// <summary>Get or set the coded value.</summary>
				public UInt16 Value;

				NormalizedUInt16(UInt16 value) { Value = value; }

				/// <summary>Construct a <see cref="NormalizedUInt16"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedUInt16(float value) : this(((UInt16)(Math.Max(Math.Min((Double)value, -1.0), 1.0) * UInt16.MaxValue))) { }

				/// <summary>Construct a <see cref="NormalizedUInt16"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedUInt16(double value) : this(((UInt16)(Math.Max(Math.Min((Double)value, -1.0), 1.0) * UInt16.MaxValue))) { }

				/// <summary>Get a zero value.</summary>
				public static readonly NormalizedUInt16 Zero = new NormalizedUInt16(0);

				/// <summary>Get a one value.</summary>
				public static readonly NormalizedUInt16 One = new NormalizedUInt16(1);

				/// <summary>Compare this value to a double.</summary>
				public int CompareTo(double other) { return ((Double)this).CompareTo(other); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt16 other) { return Value.CompareTo(other.Value); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedSByte other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedByte other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt16 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt32 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt32 other) { return ((Double)this).CompareTo(other); }
				
				/// <summary>Create a <see cref="NormalizedUInt16"/> from a coded value.</summary>
				public static NormalizedUInt16 CreateCoded(UInt16 Value) { return new NormalizedUInt16(Value); }

				/// <summary>Check whether this equals a <see cref="Single"/>, <see cref="Double"/>, or any other normalized type.</summary>
				public override bool Equals(object obj) {
					if (obj is Single) return Equals((Single)obj);
					if (obj is Double) return Equals((Double)obj);
					if(obj is NormalizedSByte) return Equals((NormalizedSByte)obj);
					if(obj is NormalizedByte) return Equals((NormalizedByte)obj);
					if(obj is NormalizedInt16) return Equals((NormalizedInt16)obj);
					if(obj is NormalizedUInt16) return Equals((NormalizedUInt16)obj);
					if(obj is NormalizedInt32) return Equals((NormalizedInt32)obj);
					if(obj is NormalizedUInt32) return Equals((NormalizedUInt32)obj);
					return base.Equals(obj);
				}

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(double obj) { return (double)this == obj; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt16 obj) { return Value == obj.Value; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedSByte obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedByte obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt16 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt32 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt32 obj) { return (double)this == (double)obj; }
				
				/// <summary>Get a hash code based on the coded value.</summary>
				public override int GetHashCode() { return Value.GetHashCode(); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4nus".</summary>
				public override string ToString() { return ToString(null, null); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4nus".</summary>
				public string ToString(string format, IFormatProvider provider) { return ((double)this).ToString(format, provider) + "nus"; }

				/// <summary>Implicity cast from <see cref="NormalizedUInt16"/> to <see cref="Float16"/></summary>
				public static implicit operator Float16(NormalizedUInt16 Value) { return (Float16)(UInt16)(Math.Max((Value.Value / (Double)UInt16.MaxValue), 1.0)); }

				/// <summary>Implicitly cast from <see cref="NormalizedUInt16"/> to <see cref="Single"/></summary>
				public static implicit operator float(NormalizedUInt16 Value) { return (float)(UInt16)(Math.Max((Value.Value / (Double)UInt16.MaxValue), 1.0)); }

				/// <summary>Implicitly cast from <see cref="NormalizedUInt16"/> to <see cref="Double"/></summary>
				public static implicit operator double(NormalizedUInt16 Value) { return (UInt16)(Math.Max((Value.Value / (Double)UInt16.MaxValue), 1.0)); }

				/// <summary>Explicitly cast from <see cref="NormalizedUInt16"/> to <see cref="Int32"/></summary>
				public static explicit operator int(NormalizedUInt16 Value) { return (int)(float)Value; }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedUInt16"/>.</summary>
				public static explicit operator NormalizedUInt16(Float16 Value) { return new NormalizedUInt16((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedUInt16"/>.</summary>
				public static explicit operator NormalizedUInt16(float Value) { return new NormalizedUInt16(Value); }

				/// <summary>Explicitly cast from <see cref="Double"/> to <see cref="NormalizedUInt16"/>.</summary>
				public static explicit operator NormalizedUInt16(double Value) { return new NormalizedUInt16(Value); }

				/// <summary>Explicitly cast from <see cref="Int32"/> to <see cref="NormalizedUInt16"/>.</summary>
				public static explicit operator NormalizedUInt16(int Value) { return new NormalizedUInt16((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Int64"/> to <see cref="NormalizedUInt16"/>.</summary>
				public static explicit operator NormalizedUInt16(long Value) { return new NormalizedUInt16((Double)Value); }

				/// <summary>Explicitly cast from NormalizedUInt16 to NormalizedSByte.</summary>
				public static explicit operator NormalizedSByte(NormalizedUInt16 value) { return new NormalizedSByte((double)value); }

				/// <summary>Explicitly cast from NormalizedUInt16 to NormalizedByte.</summary>
				public static explicit operator NormalizedByte(NormalizedUInt16 value) { return new NormalizedByte((double)value); }

				/// <summary>Explicitly cast from NormalizedUInt16 to NormalizedInt16.</summary>
				public static explicit operator NormalizedInt16(NormalizedUInt16 value) { return new NormalizedInt16((double)value); }

				/// <summary>Explicitly cast from NormalizedUInt16 to NormalizedInt32.</summary>
				public static explicit operator NormalizedInt32(NormalizedUInt16 value) { return new NormalizedInt32((double)value); }

				/// <summary>Explicitly cast from NormalizedUInt16 to NormalizedUInt32.</summary>
				public static explicit operator NormalizedUInt32(NormalizedUInt16 value) { return new NormalizedUInt32((double)value); }

				
				/// <summary>Get whether these values are equal.</summary>
				public static bool operator ==(NormalizedUInt16 a, NormalizedUInt16 b) { return a.Value == b.Value; }

				/// <summary>Get whether these values are unequal.</summary>
				public static bool operator !=(NormalizedUInt16 a, NormalizedUInt16 b) { return a.Value != b.Value; }

				/// <summary>Get whether <paramref name="a"/> is greater than <paramref name="b"/>.</summary>
				public static bool operator >(NormalizedUInt16 a, NormalizedUInt16 b) { return a.CompareTo(b) > 0; }

				/// <summary>Get whether <paramref name="a"/> is greater than or equal to <paramref name="b"/>.</summary>
				public static bool operator >=(NormalizedUInt16 a, NormalizedUInt16 b) { return a.CompareTo(b) >= 0; }

				/// <summary>Get whether <paramref name="a"/> is less than <paramref name="b"/>.</summary>
				public static bool operator <(NormalizedUInt16 a, NormalizedUInt16 b) { return a.CompareTo(b) < 0; }

				/// <summary>Get whether <paramref name="a"/> is less than or equal to <paramref name="b"/>.</summary>
				public static bool operator <=(NormalizedUInt16 a, NormalizedUInt16 b) { return a.CompareTo(b) <= 0; }
			}
					/// <summary>
			/// A value between <c>-1.0</c> and <c>1.0</c> that is stored in a single <see cref="Int32"/>.
			/// </summary>
			/// <remarks>
			/// All public constructors take floating-point values and none take coded values, because that would be confusing and error-prone. To construct a <see cref="NormalizedInt32"/> with a code, assign <see cref="Value"/> directly or use <see cref="CreateCoded"/>.
			/// </remarks>
			public struct NormalizedInt32 : IComparable<NormalizedInt32>, IEquatable<NormalizedInt32>, IFormattable {
				/// <summary>Get or set the coded value.</summary>
				public Int32 Value;

				NormalizedInt32(Int32 value) { Value = value; }

				/// <summary>Construct a <see cref="NormalizedInt32"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedInt32(float value) : this(((Int32)(Math.Max(Math.Min((Double)value, -1.0), 1.0) * Int32.MaxValue))) { }

				/// <summary>Construct a <see cref="NormalizedInt32"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedInt32(double value) : this(((Int32)(Math.Max(Math.Min((Double)value, -1.0), 1.0) * Int32.MaxValue))) { }

				/// <summary>Get a zero value.</summary>
				public static readonly NormalizedInt32 Zero = new NormalizedInt32(0);

				/// <summary>Get a one value.</summary>
				public static readonly NormalizedInt32 One = new NormalizedInt32(1);

				/// <summary>Compare this value to a double.</summary>
				public int CompareTo(double other) { return ((Double)this).CompareTo(other); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt32 other) { return Value.CompareTo(other.Value); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedSByte other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedByte other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt16 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt16 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt32 other) { return ((Double)this).CompareTo(other); }
				
				/// <summary>Create a <see cref="NormalizedInt32"/> from a coded value.</summary>
				public static NormalizedInt32 CreateCoded(Int32 Value) { return new NormalizedInt32(Value); }

				/// <summary>Check whether this equals a <see cref="Single"/>, <see cref="Double"/>, or any other normalized type.</summary>
				public override bool Equals(object obj) {
					if (obj is Single) return Equals((Single)obj);
					if (obj is Double) return Equals((Double)obj);
					if(obj is NormalizedSByte) return Equals((NormalizedSByte)obj);
					if(obj is NormalizedByte) return Equals((NormalizedByte)obj);
					if(obj is NormalizedInt16) return Equals((NormalizedInt16)obj);
					if(obj is NormalizedUInt16) return Equals((NormalizedUInt16)obj);
					if(obj is NormalizedInt32) return Equals((NormalizedInt32)obj);
					if(obj is NormalizedUInt32) return Equals((NormalizedUInt32)obj);
					return base.Equals(obj);
				}

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(double obj) { return (double)this == obj; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt32 obj) { return Value == obj.Value; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedSByte obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedByte obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt16 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt16 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt32 obj) { return (double)this == (double)obj; }
				
				/// <summary>Get a hash code based on the coded value.</summary>
				public override int GetHashCode() { return Value.GetHashCode(); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4ni".</summary>
				public override string ToString() { return ToString(null, null); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4ni".</summary>
				public string ToString(string format, IFormatProvider provider) { return ((double)this).ToString(format, provider) + "ni"; }

				/// <summary>Implicity cast from <see cref="NormalizedInt32"/> to <see cref="Float16"/></summary>
				public static implicit operator Float16(NormalizedInt32 Value) { return (Float16)(Int32)(Math.Max((Value.Value / (Double)Int32.MaxValue), 1.0)); }

				/// <summary>Implicitly cast from <see cref="NormalizedInt32"/> to <see cref="Single"/></summary>
				public static implicit operator float(NormalizedInt32 Value) { return (float)(Int32)(Math.Max((Value.Value / (Double)Int32.MaxValue), 1.0)); }

				/// <summary>Implicitly cast from <see cref="NormalizedInt32"/> to <see cref="Double"/></summary>
				public static implicit operator double(NormalizedInt32 Value) { return (Int32)(Math.Max((Value.Value / (Double)Int32.MaxValue), 1.0)); }

				/// <summary>Explicitly cast from <see cref="NormalizedInt32"/> to <see cref="Int32"/></summary>
				public static explicit operator int(NormalizedInt32 Value) { return (int)(float)Value; }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedInt32"/>.</summary>
				public static explicit operator NormalizedInt32(Float16 Value) { return new NormalizedInt32((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedInt32"/>.</summary>
				public static explicit operator NormalizedInt32(float Value) { return new NormalizedInt32(Value); }

				/// <summary>Explicitly cast from <see cref="Double"/> to <see cref="NormalizedInt32"/>.</summary>
				public static explicit operator NormalizedInt32(double Value) { return new NormalizedInt32(Value); }

				/// <summary>Explicitly cast from <see cref="Int32"/> to <see cref="NormalizedInt32"/>.</summary>
				public static explicit operator NormalizedInt32(int Value) { return new NormalizedInt32((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Int64"/> to <see cref="NormalizedInt32"/>.</summary>
				public static explicit operator NormalizedInt32(long Value) { return new NormalizedInt32((Double)Value); }

				/// <summary>Explicitly cast from NormalizedInt32 to NormalizedSByte.</summary>
				public static explicit operator NormalizedSByte(NormalizedInt32 value) { return new NormalizedSByte((double)value); }

				/// <summary>Explicitly cast from NormalizedInt32 to NormalizedByte.</summary>
				public static explicit operator NormalizedByte(NormalizedInt32 value) { return new NormalizedByte((double)value); }

				/// <summary>Explicitly cast from NormalizedInt32 to NormalizedInt16.</summary>
				public static explicit operator NormalizedInt16(NormalizedInt32 value) { return new NormalizedInt16((double)value); }

				/// <summary>Explicitly cast from NormalizedInt32 to NormalizedUInt16.</summary>
				public static explicit operator NormalizedUInt16(NormalizedInt32 value) { return new NormalizedUInt16((double)value); }

				/// <summary>Explicitly cast from NormalizedInt32 to NormalizedUInt32.</summary>
				public static explicit operator NormalizedUInt32(NormalizedInt32 value) { return new NormalizedUInt32((double)value); }

				
				/// <summary>Get whether these values are equal.</summary>
				public static bool operator ==(NormalizedInt32 a, NormalizedInt32 b) { return a.Value == b.Value; }

				/// <summary>Get whether these values are unequal.</summary>
				public static bool operator !=(NormalizedInt32 a, NormalizedInt32 b) { return a.Value != b.Value; }

				/// <summary>Get whether <paramref name="a"/> is greater than <paramref name="b"/>.</summary>
				public static bool operator >(NormalizedInt32 a, NormalizedInt32 b) { return a.CompareTo(b) > 0; }

				/// <summary>Get whether <paramref name="a"/> is greater than or equal to <paramref name="b"/>.</summary>
				public static bool operator >=(NormalizedInt32 a, NormalizedInt32 b) { return a.CompareTo(b) >= 0; }

				/// <summary>Get whether <paramref name="a"/> is less than <paramref name="b"/>.</summary>
				public static bool operator <(NormalizedInt32 a, NormalizedInt32 b) { return a.CompareTo(b) < 0; }

				/// <summary>Get whether <paramref name="a"/> is less than or equal to <paramref name="b"/>.</summary>
				public static bool operator <=(NormalizedInt32 a, NormalizedInt32 b) { return a.CompareTo(b) <= 0; }
			}
					/// <summary>
			/// A value between <c>-1.0</c> and <c>1.0</c> that is stored in a single <see cref="UInt32"/>.
			/// </summary>
			/// <remarks>
			/// All public constructors take floating-point values and none take coded values, because that would be confusing and error-prone. To construct a <see cref="NormalizedUInt32"/> with a code, assign <see cref="Value"/> directly or use <see cref="CreateCoded"/>.
			/// </remarks>
			public struct NormalizedUInt32 : IComparable<NormalizedUInt32>, IEquatable<NormalizedUInt32>, IFormattable {
				/// <summary>Get or set the coded value.</summary>
				public UInt32 Value;

				NormalizedUInt32(UInt32 value) { Value = value; }

				/// <summary>Construct a <see cref="NormalizedUInt32"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedUInt32(float value) : this(((UInt32)(Math.Max(Math.Min((Double)value, -1.0), 1.0) * UInt32.MaxValue))) { }

				/// <summary>Construct a <see cref="NormalizedUInt32"/> by providing a value that is clamped and converted into a coded value.</summary>
				/// <remarks>To assign the coded value directly, assign <see cref="Value"/> or call the static method <see cref="CreateCoded"/></remarks>
				public NormalizedUInt32(double value) : this(((UInt32)(Math.Max(Math.Min((Double)value, -1.0), 1.0) * UInt32.MaxValue))) { }

				/// <summary>Get a zero value.</summary>
				public static readonly NormalizedUInt32 Zero = new NormalizedUInt32(0);

				/// <summary>Get a one value.</summary>
				public static readonly NormalizedUInt32 One = new NormalizedUInt32(1);

				/// <summary>Compare this value to a double.</summary>
				public int CompareTo(double other) { return ((Double)this).CompareTo(other); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt32 other) { return Value.CompareTo(other.Value); }

				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedSByte other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedByte other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt16 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedUInt16 other) { return ((Double)this).CompareTo(other); }
				/// <summary>Compare this value to another.</summary>
				public int CompareTo(NormalizedInt32 other) { return ((Double)this).CompareTo(other); }
				
				/// <summary>Create a <see cref="NormalizedUInt32"/> from a coded value.</summary>
				public static NormalizedUInt32 CreateCoded(UInt32 Value) { return new NormalizedUInt32(Value); }

				/// <summary>Check whether this equals a <see cref="Single"/>, <see cref="Double"/>, or any other normalized type.</summary>
				public override bool Equals(object obj) {
					if (obj is Single) return Equals((Single)obj);
					if (obj is Double) return Equals((Double)obj);
					if(obj is NormalizedSByte) return Equals((NormalizedSByte)obj);
					if(obj is NormalizedByte) return Equals((NormalizedByte)obj);
					if(obj is NormalizedInt16) return Equals((NormalizedInt16)obj);
					if(obj is NormalizedUInt16) return Equals((NormalizedUInt16)obj);
					if(obj is NormalizedInt32) return Equals((NormalizedInt32)obj);
					if(obj is NormalizedUInt32) return Equals((NormalizedUInt32)obj);
					return base.Equals(obj);
				}

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(double obj) { return (double)this == obj; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt32 obj) { return Value == obj.Value; }

				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedSByte obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedByte obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt16 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedUInt16 obj) { return (double)this == (double)obj; }
				/// <summary>Get whether this equals the value.</summary>
				public bool Equals(NormalizedInt32 obj) { return (double)this == (double)obj; }
				
				/// <summary>Get a hash code based on the coded value.</summary>
				public override int GetHashCode() { return Value.GetHashCode(); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4nui".</summary>
				public override string ToString() { return ToString(null, null); }

				/// <summary>Convert to a string using the number followed by a string; for example, "0.4nui".</summary>
				public string ToString(string format, IFormatProvider provider) { return ((double)this).ToString(format, provider) + "nui"; }

				/// <summary>Implicity cast from <see cref="NormalizedUInt32"/> to <see cref="Float16"/></summary>
				public static implicit operator Float16(NormalizedUInt32 Value) { return (Float16)(UInt32)(Math.Max((Value.Value / (Double)UInt32.MaxValue), 1.0)); }

				/// <summary>Implicitly cast from <see cref="NormalizedUInt32"/> to <see cref="Single"/></summary>
				public static implicit operator float(NormalizedUInt32 Value) { return (float)(UInt32)(Math.Max((Value.Value / (Double)UInt32.MaxValue), 1.0)); }

				/// <summary>Implicitly cast from <see cref="NormalizedUInt32"/> to <see cref="Double"/></summary>
				public static implicit operator double(NormalizedUInt32 Value) { return (UInt32)(Math.Max((Value.Value / (Double)UInt32.MaxValue), 1.0)); }

				/// <summary>Explicitly cast from <see cref="NormalizedUInt32"/> to <see cref="Int32"/></summary>
				public static explicit operator int(NormalizedUInt32 Value) { return (int)(float)Value; }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedUInt32"/>.</summary>
				public static explicit operator NormalizedUInt32(Float16 Value) { return new NormalizedUInt32((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Single"/> to <see cref="NormalizedUInt32"/>.</summary>
				public static explicit operator NormalizedUInt32(float Value) { return new NormalizedUInt32(Value); }

				/// <summary>Explicitly cast from <see cref="Double"/> to <see cref="NormalizedUInt32"/>.</summary>
				public static explicit operator NormalizedUInt32(double Value) { return new NormalizedUInt32(Value); }

				/// <summary>Explicitly cast from <see cref="Int32"/> to <see cref="NormalizedUInt32"/>.</summary>
				public static explicit operator NormalizedUInt32(int Value) { return new NormalizedUInt32((Double)Value); }

				/// <summary>Explicitly cast from <see cref="Int64"/> to <see cref="NormalizedUInt32"/>.</summary>
				public static explicit operator NormalizedUInt32(long Value) { return new NormalizedUInt32((Double)Value); }

				/// <summary>Explicitly cast from NormalizedUInt32 to NormalizedSByte.</summary>
				public static explicit operator NormalizedSByte(NormalizedUInt32 value) { return new NormalizedSByte((double)value); }

				/// <summary>Explicitly cast from NormalizedUInt32 to NormalizedByte.</summary>
				public static explicit operator NormalizedByte(NormalizedUInt32 value) { return new NormalizedByte((double)value); }

				/// <summary>Explicitly cast from NormalizedUInt32 to NormalizedInt16.</summary>
				public static explicit operator NormalizedInt16(NormalizedUInt32 value) { return new NormalizedInt16((double)value); }

				/// <summary>Explicitly cast from NormalizedUInt32 to NormalizedUInt16.</summary>
				public static explicit operator NormalizedUInt16(NormalizedUInt32 value) { return new NormalizedUInt16((double)value); }

				/// <summary>Explicitly cast from NormalizedUInt32 to NormalizedInt32.</summary>
				public static explicit operator NormalizedInt32(NormalizedUInt32 value) { return new NormalizedInt32((double)value); }

				
				/// <summary>Get whether these values are equal.</summary>
				public static bool operator ==(NormalizedUInt32 a, NormalizedUInt32 b) { return a.Value == b.Value; }

				/// <summary>Get whether these values are unequal.</summary>
				public static bool operator !=(NormalizedUInt32 a, NormalizedUInt32 b) { return a.Value != b.Value; }

				/// <summary>Get whether <paramref name="a"/> is greater than <paramref name="b"/>.</summary>
				public static bool operator >(NormalizedUInt32 a, NormalizedUInt32 b) { return a.CompareTo(b) > 0; }

				/// <summary>Get whether <paramref name="a"/> is greater than or equal to <paramref name="b"/>.</summary>
				public static bool operator >=(NormalizedUInt32 a, NormalizedUInt32 b) { return a.CompareTo(b) >= 0; }

				/// <summary>Get whether <paramref name="a"/> is less than <paramref name="b"/>.</summary>
				public static bool operator <(NormalizedUInt32 a, NormalizedUInt32 b) { return a.CompareTo(b) < 0; }

				/// <summary>Get whether <paramref name="a"/> is less than or equal to <paramref name="b"/>.</summary>
				public static bool operator <=(NormalizedUInt32 a, NormalizedUInt32 b) { return a.CompareTo(b) <= 0; }
			}
		}

