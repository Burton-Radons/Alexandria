 

using System;
using System.Collections.Generic;

namespace Glare.Framework {
	/// <summary>Provides static methods for constructing aggregate types.</summary>
	public static class Aggregate {
		/// <summary>Create an aggregate from the parameters.</summary>
		public static Aggregate<T1> Create<T1>(T1 item1) { return new Aggregate<T1>(item1); }

				/// <summary>Create an aggregate from a tuple.</summary>
		public static Aggregate<T1> Create<T1>(Tuple<T1> tuple) { return new Aggregate<T1>(tuple); }
		
		/// <summary>Create an aggregate from the parameters.</summary>
		public static Aggregate<T1, T2> Create<T1, T2>(T1 item1, T2 item2) { return new Aggregate<T1, T2>(item1, item2); }

				/// <summary>Create an aggregate from a tuple.</summary>
		public static Aggregate<T1, T2> Create<T1, T2>(Tuple<T1, T2> tuple) { return new Aggregate<T1, T2>(tuple); }
		
		/// <summary>Create an aggregate from the parameters.</summary>
		public static Aggregate<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) { return new Aggregate<T1, T2, T3>(item1, item2, item3); }

				/// <summary>Create an aggregate from a tuple.</summary>
		public static Aggregate<T1, T2, T3> Create<T1, T2, T3>(Tuple<T1, T2, T3> tuple) { return new Aggregate<T1, T2, T3>(tuple); }
		
		/// <summary>Create an aggregate from the parameters.</summary>
		public static Aggregate<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) { return new Aggregate<T1, T2, T3, T4>(item1, item2, item3, item4); }

				/// <summary>Create an aggregate from a tuple.</summary>
		public static Aggregate<T1, T2, T3, T4> Create<T1, T2, T3, T4>(Tuple<T1, T2, T3, T4> tuple) { return new Aggregate<T1, T2, T3, T4>(tuple); }
		
		/// <summary>Create an aggregate from the parameters.</summary>
		public static Aggregate<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) { return new Aggregate<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5); }

				/// <summary>Create an aggregate from a tuple.</summary>
		public static Aggregate<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(Tuple<T1, T2, T3, T4, T5> tuple) { return new Aggregate<T1, T2, T3, T4, T5>(tuple); }
		
		/// <summary>Create an aggregate from the parameters.</summary>
		public static Aggregate<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) { return new Aggregate<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6); }

				/// <summary>Create an aggregate from a tuple.</summary>
		public static Aggregate<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(Tuple<T1, T2, T3, T4, T5, T6> tuple) { return new Aggregate<T1, T2, T3, T4, T5, T6>(tuple); }
		
		/// <summary>Create an aggregate from the parameters.</summary>
		public static Aggregate<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7) { return new Aggregate<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7); }

		
		/// <summary>Create an aggregate from the parameters.</summary>
		public static Aggregate<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8) { return new Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(item1, item2, item3, item4, item5, item6, item7, item8); }

		
		/// <summary>Create an aggregate from the parameters.</summary>
		public static Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9) { return new Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item1, item2, item3, item4, item5, item6, item7, item8, item9); }

		
		
		/// <summary>Create an aggregate from the key value pair.</summary>
		public static Aggregate<T1, T2> Create<T1, T2>(KeyValuePair<T1, T2> value) { return new Aggregate<T1, T2>(value); }
	}

	/// <summary>A structure that contains 1 object. This is similar to <see cref="Tuple"/>, but this is a struct, not a class</summary>
	public partial struct Aggregate<T1> : IEquatable<Aggregate<T1>> {
		/// <summary>The field for this element of the aggregate.</summary>
		public T1 Item1;
		
		/// <summary>Construct the aggregate by assigning the elements to the values provided.</summary>
		public Aggregate(T1 item1 = default(T1)) {
			Item1 = item1; 
			}

				/// <summary>Copy the elements of a tuple.</summary>
		/// <param name="tuple"></param>
		public Aggregate(Tuple<T1> tuple) {
			if(tuple == null)
				throw new ArgumentNullException("tuple");
			Item1 = tuple.Item1;
			}
		
		/// <summary>Get whether the elements of the other aggregate match this one.</summary>
		public bool Equals(Aggregate<T1> other) {
			return EqualityComparer<T1>.Default.Equals(Item1, other.Item1);
		}

		/// <summary>If the object is an aggregate of the same type, then the elements will be compared and returned; otherwise false will be returned.</summary>
		public override bool Equals(Object obj) {
			if(obj is Aggregate<T1>)
				return Equals((Aggregate<T1>)obj);
			return base.Equals(obj);
		}

		/// <summary>Get a hash code from the elements of the aggregate.</summary>
		public override int GetHashCode() { return Item1.GetHashCode(); }

		/// <summary>Convert to a comma-separated string of each element surrounded with "&lt;" and "&gt;" braces.</summary>
		public override string ToString() { return "<" + Item1.ToString() + ">"; }

		/// <summary>Get whether the elements of the aggregates are equal.</summary>
		public static bool operator ==(Aggregate<T1> a, Aggregate<T1> b) { return a.Equals(b); }

		/// <summary>Get whether the elements of the aggregates are unequal.</summary>
		public static bool operator !=(Aggregate<T1>a, Aggregate<T1>b) { return !a.Equals(b); }
	}

	/// <summary>A structure that contains 2 objects. This is similar to <see cref="Tuple"/>, but this is a struct, not a class</summary>
	public partial struct Aggregate<T1, T2> : IEquatable<Aggregate<T1, T2>> {
		/// <summary>The field for this element of the aggregate.</summary>
		public T1 Item1;
		/// <summary>The field for this element of the aggregate.</summary>
		public T2 Item2;
		
		/// <summary>Construct the aggregate by assigning the elements to the values provided.</summary>
		public Aggregate(T1 item1 = default(T1), T2 item2 = default(T2)) {
			Item1 = item1; 
			Item2 = item2; 
			}

				/// <summary>Copy the elements of a tuple.</summary>
		/// <param name="tuple"></param>
		public Aggregate(Tuple<T1, T2> tuple) {
			if(tuple == null)
				throw new ArgumentNullException("tuple");
			Item1 = tuple.Item1;
			Item2 = tuple.Item2;
			}
		
		/// <summary>Get whether the elements of the other aggregate match this one.</summary>
		public bool Equals(Aggregate<T1, T2> other) {
			return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2);
		}

		/// <summary>If the object is an aggregate of the same type, then the elements will be compared and returned; otherwise false will be returned.</summary>
		public override bool Equals(Object obj) {
			if(obj is Aggregate<T1, T2>)
				return Equals((Aggregate<T1, T2>)obj);
			return base.Equals(obj);
		}

		/// <summary>Get a hash code from the elements of the aggregate.</summary>
		public override int GetHashCode() { return Item1.GetHashCode() ^ Item2.GetHashCode(); }

		/// <summary>Convert to a comma-separated string of each element surrounded with "&lt;" and "&gt;" braces.</summary>
		public override string ToString() { return "<" + Item1.ToString() + ", " + Item2.ToString() + ">"; }

		/// <summary>Get whether the elements of the aggregates are equal.</summary>
		public static bool operator ==(Aggregate<T1, T2> a, Aggregate<T1, T2> b) { return a.Equals(b); }

		/// <summary>Get whether the elements of the aggregates are unequal.</summary>
		public static bool operator !=(Aggregate<T1, T2>a, Aggregate<T1, T2>b) { return !a.Equals(b); }
	}

	/// <summary>A structure that contains 3 objects. This is similar to <see cref="Tuple"/>, but this is a struct, not a class</summary>
	public partial struct Aggregate<T1, T2, T3> : IEquatable<Aggregate<T1, T2, T3>> {
		/// <summary>The field for this element of the aggregate.</summary>
		public T1 Item1;
		/// <summary>The field for this element of the aggregate.</summary>
		public T2 Item2;
		/// <summary>The field for this element of the aggregate.</summary>
		public T3 Item3;
		
		/// <summary>Construct the aggregate by assigning the elements to the values provided.</summary>
		public Aggregate(T1 item1 = default(T1), T2 item2 = default(T2), T3 item3 = default(T3)) {
			Item1 = item1; 
			Item2 = item2; 
			Item3 = item3; 
			}

				/// <summary>Copy the elements of a tuple.</summary>
		/// <param name="tuple"></param>
		public Aggregate(Tuple<T1, T2, T3> tuple) {
			if(tuple == null)
				throw new ArgumentNullException("tuple");
			Item1 = tuple.Item1;
			Item2 = tuple.Item2;
			Item3 = tuple.Item3;
			}
		
		/// <summary>Get whether the elements of the other aggregate match this one.</summary>
		public bool Equals(Aggregate<T1, T2, T3> other) {
			return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3);
		}

		/// <summary>If the object is an aggregate of the same type, then the elements will be compared and returned; otherwise false will be returned.</summary>
		public override bool Equals(Object obj) {
			if(obj is Aggregate<T1, T2, T3>)
				return Equals((Aggregate<T1, T2, T3>)obj);
			return base.Equals(obj);
		}

		/// <summary>Get a hash code from the elements of the aggregate.</summary>
		public override int GetHashCode() { return Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode(); }

		/// <summary>Convert to a comma-separated string of each element surrounded with "&lt;" and "&gt;" braces.</summary>
		public override string ToString() { return "<" + Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString() + ">"; }

		/// <summary>Get whether the elements of the aggregates are equal.</summary>
		public static bool operator ==(Aggregate<T1, T2, T3> a, Aggregate<T1, T2, T3> b) { return a.Equals(b); }

		/// <summary>Get whether the elements of the aggregates are unequal.</summary>
		public static bool operator !=(Aggregate<T1, T2, T3>a, Aggregate<T1, T2, T3>b) { return !a.Equals(b); }
	}

	/// <summary>A structure that contains 4 objects. This is similar to <see cref="Tuple"/>, but this is a struct, not a class</summary>
	public partial struct Aggregate<T1, T2, T3, T4> : IEquatable<Aggregate<T1, T2, T3, T4>> {
		/// <summary>The field for this element of the aggregate.</summary>
		public T1 Item1;
		/// <summary>The field for this element of the aggregate.</summary>
		public T2 Item2;
		/// <summary>The field for this element of the aggregate.</summary>
		public T3 Item3;
		/// <summary>The field for this element of the aggregate.</summary>
		public T4 Item4;
		
		/// <summary>Construct the aggregate by assigning the elements to the values provided.</summary>
		public Aggregate(T1 item1 = default(T1), T2 item2 = default(T2), T3 item3 = default(T3), T4 item4 = default(T4)) {
			Item1 = item1; 
			Item2 = item2; 
			Item3 = item3; 
			Item4 = item4; 
			}

				/// <summary>Copy the elements of a tuple.</summary>
		/// <param name="tuple"></param>
		public Aggregate(Tuple<T1, T2, T3, T4> tuple) {
			if(tuple == null)
				throw new ArgumentNullException("tuple");
			Item1 = tuple.Item1;
			Item2 = tuple.Item2;
			Item3 = tuple.Item3;
			Item4 = tuple.Item4;
			}
		
		/// <summary>Get whether the elements of the other aggregate match this one.</summary>
		public bool Equals(Aggregate<T1, T2, T3, T4> other) {
			return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4);
		}

		/// <summary>If the object is an aggregate of the same type, then the elements will be compared and returned; otherwise false will be returned.</summary>
		public override bool Equals(Object obj) {
			if(obj is Aggregate<T1, T2, T3, T4>)
				return Equals((Aggregate<T1, T2, T3, T4>)obj);
			return base.Equals(obj);
		}

		/// <summary>Get a hash code from the elements of the aggregate.</summary>
		public override int GetHashCode() { return Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode() ^ Item4.GetHashCode(); }

		/// <summary>Convert to a comma-separated string of each element surrounded with "&lt;" and "&gt;" braces.</summary>
		public override string ToString() { return "<" + Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString() + ", " + Item4.ToString() + ">"; }

		/// <summary>Get whether the elements of the aggregates are equal.</summary>
		public static bool operator ==(Aggregate<T1, T2, T3, T4> a, Aggregate<T1, T2, T3, T4> b) { return a.Equals(b); }

		/// <summary>Get whether the elements of the aggregates are unequal.</summary>
		public static bool operator !=(Aggregate<T1, T2, T3, T4>a, Aggregate<T1, T2, T3, T4>b) { return !a.Equals(b); }
	}

	/// <summary>A structure that contains 5 objects. This is similar to <see cref="Tuple"/>, but this is a struct, not a class</summary>
	public partial struct Aggregate<T1, T2, T3, T4, T5> : IEquatable<Aggregate<T1, T2, T3, T4, T5>> {
		/// <summary>The field for this element of the aggregate.</summary>
		public T1 Item1;
		/// <summary>The field for this element of the aggregate.</summary>
		public T2 Item2;
		/// <summary>The field for this element of the aggregate.</summary>
		public T3 Item3;
		/// <summary>The field for this element of the aggregate.</summary>
		public T4 Item4;
		/// <summary>The field for this element of the aggregate.</summary>
		public T5 Item5;
		
		/// <summary>Construct the aggregate by assigning the elements to the values provided.</summary>
		public Aggregate(T1 item1 = default(T1), T2 item2 = default(T2), T3 item3 = default(T3), T4 item4 = default(T4), T5 item5 = default(T5)) {
			Item1 = item1; 
			Item2 = item2; 
			Item3 = item3; 
			Item4 = item4; 
			Item5 = item5; 
			}

				/// <summary>Copy the elements of a tuple.</summary>
		/// <param name="tuple"></param>
		public Aggregate(Tuple<T1, T2, T3, T4, T5> tuple) {
			if(tuple == null)
				throw new ArgumentNullException("tuple");
			Item1 = tuple.Item1;
			Item2 = tuple.Item2;
			Item3 = tuple.Item3;
			Item4 = tuple.Item4;
			Item5 = tuple.Item5;
			}
		
		/// <summary>Get whether the elements of the other aggregate match this one.</summary>
		public bool Equals(Aggregate<T1, T2, T3, T4, T5> other) {
			return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5);
		}

		/// <summary>If the object is an aggregate of the same type, then the elements will be compared and returned; otherwise false will be returned.</summary>
		public override bool Equals(Object obj) {
			if(obj is Aggregate<T1, T2, T3, T4, T5>)
				return Equals((Aggregate<T1, T2, T3, T4, T5>)obj);
			return base.Equals(obj);
		}

		/// <summary>Get a hash code from the elements of the aggregate.</summary>
		public override int GetHashCode() { return Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode() ^ Item4.GetHashCode() ^ Item5.GetHashCode(); }

		/// <summary>Convert to a comma-separated string of each element surrounded with "&lt;" and "&gt;" braces.</summary>
		public override string ToString() { return "<" + Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString() + ", " + Item4.ToString() + ", " + Item5.ToString() + ">"; }

		/// <summary>Get whether the elements of the aggregates are equal.</summary>
		public static bool operator ==(Aggregate<T1, T2, T3, T4, T5> a, Aggregate<T1, T2, T3, T4, T5> b) { return a.Equals(b); }

		/// <summary>Get whether the elements of the aggregates are unequal.</summary>
		public static bool operator !=(Aggregate<T1, T2, T3, T4, T5>a, Aggregate<T1, T2, T3, T4, T5>b) { return !a.Equals(b); }
	}

	/// <summary>A structure that contains 6 objects. This is similar to <see cref="Tuple"/>, but this is a struct, not a class</summary>
	public partial struct Aggregate<T1, T2, T3, T4, T5, T6> : IEquatable<Aggregate<T1, T2, T3, T4, T5, T6>> {
		/// <summary>The field for this element of the aggregate.</summary>
		public T1 Item1;
		/// <summary>The field for this element of the aggregate.</summary>
		public T2 Item2;
		/// <summary>The field for this element of the aggregate.</summary>
		public T3 Item3;
		/// <summary>The field for this element of the aggregate.</summary>
		public T4 Item4;
		/// <summary>The field for this element of the aggregate.</summary>
		public T5 Item5;
		/// <summary>The field for this element of the aggregate.</summary>
		public T6 Item6;
		
		/// <summary>Construct the aggregate by assigning the elements to the values provided.</summary>
		public Aggregate(T1 item1 = default(T1), T2 item2 = default(T2), T3 item3 = default(T3), T4 item4 = default(T4), T5 item5 = default(T5), T6 item6 = default(T6)) {
			Item1 = item1; 
			Item2 = item2; 
			Item3 = item3; 
			Item4 = item4; 
			Item5 = item5; 
			Item6 = item6; 
			}

				/// <summary>Copy the elements of a tuple.</summary>
		/// <param name="tuple"></param>
		public Aggregate(Tuple<T1, T2, T3, T4, T5, T6> tuple) {
			if(tuple == null)
				throw new ArgumentNullException("tuple");
			Item1 = tuple.Item1;
			Item2 = tuple.Item2;
			Item3 = tuple.Item3;
			Item4 = tuple.Item4;
			Item5 = tuple.Item5;
			Item6 = tuple.Item6;
			}
		
		/// <summary>Get whether the elements of the other aggregate match this one.</summary>
		public bool Equals(Aggregate<T1, T2, T3, T4, T5, T6> other) {
			return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6);
		}

		/// <summary>If the object is an aggregate of the same type, then the elements will be compared and returned; otherwise false will be returned.</summary>
		public override bool Equals(Object obj) {
			if(obj is Aggregate<T1, T2, T3, T4, T5, T6>)
				return Equals((Aggregate<T1, T2, T3, T4, T5, T6>)obj);
			return base.Equals(obj);
		}

		/// <summary>Get a hash code from the elements of the aggregate.</summary>
		public override int GetHashCode() { return Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode() ^ Item4.GetHashCode() ^ Item5.GetHashCode() ^ Item6.GetHashCode(); }

		/// <summary>Convert to a comma-separated string of each element surrounded with "&lt;" and "&gt;" braces.</summary>
		public override string ToString() { return "<" + Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString() + ", " + Item4.ToString() + ", " + Item5.ToString() + ", " + Item6.ToString() + ">"; }

		/// <summary>Get whether the elements of the aggregates are equal.</summary>
		public static bool operator ==(Aggregate<T1, T2, T3, T4, T5, T6> a, Aggregate<T1, T2, T3, T4, T5, T6> b) { return a.Equals(b); }

		/// <summary>Get whether the elements of the aggregates are unequal.</summary>
		public static bool operator !=(Aggregate<T1, T2, T3, T4, T5, T6>a, Aggregate<T1, T2, T3, T4, T5, T6>b) { return !a.Equals(b); }
	}

	/// <summary>A structure that contains 7 objects. This is similar to <see cref="Tuple"/>, but this is a struct, not a class</summary>
	public partial struct Aggregate<T1, T2, T3, T4, T5, T6, T7> : IEquatable<Aggregate<T1, T2, T3, T4, T5, T6, T7>> {
		/// <summary>The field for this element of the aggregate.</summary>
		public T1 Item1;
		/// <summary>The field for this element of the aggregate.</summary>
		public T2 Item2;
		/// <summary>The field for this element of the aggregate.</summary>
		public T3 Item3;
		/// <summary>The field for this element of the aggregate.</summary>
		public T4 Item4;
		/// <summary>The field for this element of the aggregate.</summary>
		public T5 Item5;
		/// <summary>The field for this element of the aggregate.</summary>
		public T6 Item6;
		/// <summary>The field for this element of the aggregate.</summary>
		public T7 Item7;
		
		/// <summary>Construct the aggregate by assigning the elements to the values provided.</summary>
		public Aggregate(T1 item1 = default(T1), T2 item2 = default(T2), T3 item3 = default(T3), T4 item4 = default(T4), T5 item5 = default(T5), T6 item6 = default(T6), T7 item7 = default(T7)) {
			Item1 = item1; 
			Item2 = item2; 
			Item3 = item3; 
			Item4 = item4; 
			Item5 = item5; 
			Item6 = item6; 
			Item7 = item7; 
			}

		
		/// <summary>Get whether the elements of the other aggregate match this one.</summary>
		public bool Equals(Aggregate<T1, T2, T3, T4, T5, T6, T7> other) {
			return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(Item7, other.Item7);
		}

		/// <summary>If the object is an aggregate of the same type, then the elements will be compared and returned; otherwise false will be returned.</summary>
		public override bool Equals(Object obj) {
			if(obj is Aggregate<T1, T2, T3, T4, T5, T6, T7>)
				return Equals((Aggregate<T1, T2, T3, T4, T5, T6, T7>)obj);
			return base.Equals(obj);
		}

		/// <summary>Get a hash code from the elements of the aggregate.</summary>
		public override int GetHashCode() { return Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode() ^ Item4.GetHashCode() ^ Item5.GetHashCode() ^ Item6.GetHashCode() ^ Item7.GetHashCode(); }

		/// <summary>Convert to a comma-separated string of each element surrounded with "&lt;" and "&gt;" braces.</summary>
		public override string ToString() { return "<" + Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString() + ", " + Item4.ToString() + ", " + Item5.ToString() + ", " + Item6.ToString() + ", " + Item7.ToString() + ">"; }

		/// <summary>Get whether the elements of the aggregates are equal.</summary>
		public static bool operator ==(Aggregate<T1, T2, T3, T4, T5, T6, T7> a, Aggregate<T1, T2, T3, T4, T5, T6, T7> b) { return a.Equals(b); }

		/// <summary>Get whether the elements of the aggregates are unequal.</summary>
		public static bool operator !=(Aggregate<T1, T2, T3, T4, T5, T6, T7>a, Aggregate<T1, T2, T3, T4, T5, T6, T7>b) { return !a.Equals(b); }
	}

	/// <summary>A structure that contains 8 objects. This is similar to <see cref="Tuple"/>, but this is a struct, not a class</summary>
	public partial struct Aggregate<T1, T2, T3, T4, T5, T6, T7, T8> : IEquatable<Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>> {
		/// <summary>The field for this element of the aggregate.</summary>
		public T1 Item1;
		/// <summary>The field for this element of the aggregate.</summary>
		public T2 Item2;
		/// <summary>The field for this element of the aggregate.</summary>
		public T3 Item3;
		/// <summary>The field for this element of the aggregate.</summary>
		public T4 Item4;
		/// <summary>The field for this element of the aggregate.</summary>
		public T5 Item5;
		/// <summary>The field for this element of the aggregate.</summary>
		public T6 Item6;
		/// <summary>The field for this element of the aggregate.</summary>
		public T7 Item7;
		/// <summary>The field for this element of the aggregate.</summary>
		public T8 Item8;
		
		/// <summary>Construct the aggregate by assigning the elements to the values provided.</summary>
		public Aggregate(T1 item1 = default(T1), T2 item2 = default(T2), T3 item3 = default(T3), T4 item4 = default(T4), T5 item5 = default(T5), T6 item6 = default(T6), T7 item7 = default(T7), T8 item8 = default(T8)) {
			Item1 = item1; 
			Item2 = item2; 
			Item3 = item3; 
			Item4 = item4; 
			Item5 = item5; 
			Item6 = item6; 
			Item7 = item7; 
			Item8 = item8; 
			}

		
		/// <summary>Get whether the elements of the other aggregate match this one.</summary>
		public bool Equals(Aggregate<T1, T2, T3, T4, T5, T6, T7, T8> other) {
			return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(Item7, other.Item7) && EqualityComparer<T8>.Default.Equals(Item8, other.Item8);
		}

		/// <summary>If the object is an aggregate of the same type, then the elements will be compared and returned; otherwise false will be returned.</summary>
		public override bool Equals(Object obj) {
			if(obj is Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>)
				return Equals((Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>)obj);
			return base.Equals(obj);
		}

		/// <summary>Get a hash code from the elements of the aggregate.</summary>
		public override int GetHashCode() { return Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode() ^ Item4.GetHashCode() ^ Item5.GetHashCode() ^ Item6.GetHashCode() ^ Item7.GetHashCode() ^ Item8.GetHashCode(); }

		/// <summary>Convert to a comma-separated string of each element surrounded with "&lt;" and "&gt;" braces.</summary>
		public override string ToString() { return "<" + Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString() + ", " + Item4.ToString() + ", " + Item5.ToString() + ", " + Item6.ToString() + ", " + Item7.ToString() + ", " + Item8.ToString() + ">"; }

		/// <summary>Get whether the elements of the aggregates are equal.</summary>
		public static bool operator ==(Aggregate<T1, T2, T3, T4, T5, T6, T7, T8> a, Aggregate<T1, T2, T3, T4, T5, T6, T7, T8> b) { return a.Equals(b); }

		/// <summary>Get whether the elements of the aggregates are unequal.</summary>
		public static bool operator !=(Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>a, Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>b) { return !a.Equals(b); }
	}

	/// <summary>A structure that contains 9 objects. This is similar to <see cref="Tuple"/>, but this is a struct, not a class</summary>
	public partial struct Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IEquatable<Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>> {
		/// <summary>The field for this element of the aggregate.</summary>
		public T1 Item1;
		/// <summary>The field for this element of the aggregate.</summary>
		public T2 Item2;
		/// <summary>The field for this element of the aggregate.</summary>
		public T3 Item3;
		/// <summary>The field for this element of the aggregate.</summary>
		public T4 Item4;
		/// <summary>The field for this element of the aggregate.</summary>
		public T5 Item5;
		/// <summary>The field for this element of the aggregate.</summary>
		public T6 Item6;
		/// <summary>The field for this element of the aggregate.</summary>
		public T7 Item7;
		/// <summary>The field for this element of the aggregate.</summary>
		public T8 Item8;
		/// <summary>The field for this element of the aggregate.</summary>
		public T9 Item9;
		
		/// <summary>Construct the aggregate by assigning the elements to the values provided.</summary>
		public Aggregate(T1 item1 = default(T1), T2 item2 = default(T2), T3 item3 = default(T3), T4 item4 = default(T4), T5 item5 = default(T5), T6 item6 = default(T6), T7 item7 = default(T7), T8 item8 = default(T8), T9 item9 = default(T9)) {
			Item1 = item1; 
			Item2 = item2; 
			Item3 = item3; 
			Item4 = item4; 
			Item5 = item5; 
			Item6 = item6; 
			Item7 = item7; 
			Item8 = item8; 
			Item9 = item9; 
			}

		
		/// <summary>Get whether the elements of the other aggregate match this one.</summary>
		public bool Equals(Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9> other) {
			return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(Item7, other.Item7) && EqualityComparer<T8>.Default.Equals(Item8, other.Item8) && EqualityComparer<T9>.Default.Equals(Item9, other.Item9);
		}

		/// <summary>If the object is an aggregate of the same type, then the elements will be compared and returned; otherwise false will be returned.</summary>
		public override bool Equals(Object obj) {
			if(obj is Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>)
				return Equals((Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>)obj);
			return base.Equals(obj);
		}

		/// <summary>Get a hash code from the elements of the aggregate.</summary>
		public override int GetHashCode() { return Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode() ^ Item4.GetHashCode() ^ Item5.GetHashCode() ^ Item6.GetHashCode() ^ Item7.GetHashCode() ^ Item8.GetHashCode() ^ Item9.GetHashCode(); }

		/// <summary>Convert to a comma-separated string of each element surrounded with "&lt;" and "&gt;" braces.</summary>
		public override string ToString() { return "<" + Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString() + ", " + Item4.ToString() + ", " + Item5.ToString() + ", " + Item6.ToString() + ", " + Item7.ToString() + ", " + Item8.ToString() + ", " + Item9.ToString() + ">"; }

		/// <summary>Get whether the elements of the aggregates are equal.</summary>
		public static bool operator ==(Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9> a, Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9> b) { return a.Equals(b); }

		/// <summary>Get whether the elements of the aggregates are unequal.</summary>
		public static bool operator !=(Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>a, Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>b) { return !a.Equals(b); }
	}

		partial struct Aggregate<T1, T2> {
		/// <summary>Initialise the aggregate based on the items from the key value pair.</summary>
		public Aggregate(KeyValuePair<T1, T2> value) { Item1 = value.Key; Item2 = value.Value; }
	}
}

