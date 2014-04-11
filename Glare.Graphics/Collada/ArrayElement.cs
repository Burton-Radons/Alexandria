using Glare.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	public abstract class ArrayElement : IdElement, IName {
		[XmlIgnore]
		public abstract GraphicsBuffer Buffer { get; }

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }
	}

	/// <summary>A data array element that is part of a <see cref="Source"/>.</summary>
	public abstract class ArrayElement<TValue, TStorage, TArrayValues> : ArrayElement
		where TArrayValues : IArrayValues<TValue, TStorage>, new() {
		internal const string XmlCountName = "count";

		[XmlIgnore]
		public TArrayValues Values { get; set; }

		[XmlText]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string XmlStringValues {
			get {
				if (Values == null)
					return "";

				StringBuilder builder = new StringBuilder();
				ValuesToString(builder);
				return builder.ToString();
			}

			set {
				if (value == null)
					return;
				StringToValues(value);
			}
		}

		[XmlAttribute("count")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int XmlCount {
			get { return Values.Count; }
			set { Values.Capacity = value; }
		}

		public ArrayElement() {
			Values = new TArrayValues();
		}

		public ArrayElement(string id, IEnumerable<TValue> values) : this() {
			Id = id;
			Values.AddRange(values);
		}

		public ArrayElement(string id, params TValue[] values) : this(id, (IEnumerable<TValue>)values) { }

		protected abstract void ValuesToString(StringBuilder builder);
		protected abstract void StringToValues(string value);
	}

	public interface IArrayValues<TValue, TStorage> : IList<TValue> {
		IList<TStorage> Storage { get; }

		GraphicsBuffer Buffer { get; }

		int Capacity { get; set; }

		void AddRange(IEnumerable<TValue> collection);

		void Flush();
	}

	[Serializable]
	public class BooleanArrayElement : ArrayElement<Boolean, byte, BooleanArrayValues> {
		internal const string XmlName = "bool_array";

		[XmlIgnore]
		public override GraphicsBuffer Buffer { get { return Values.Buffer; } }

		protected override void ValuesToString(StringBuilder builder) { CollectionToString(builder, Values); }
		protected override void StringToValues(string value) { StringToCollection(value, Values); }

		public static void CollectionToString(StringBuilder builder, IEnumerable<bool> items) {
			foreach (bool item in items) {
				if (builder.Length > 0)
					builder.Append(" ");
				builder.Append(item);
			}
		}

		public static void StringToCollection(string value, ICollection<bool> items) {
			for (int offset = 0; ; ) {
				for (; offset < value.Length && XmlConvert.IsWhitespaceChar(value[offset]); offset++) ;
				if (offset >= value.Length)
					break;

				int next = offset + 1;
				for (; next < value.Length && !XmlConvert.IsWhitespaceChar(value[next]); next++) ;

				string item = value.Substring(offset, next - offset);
				items.Add(bool.Parse(item));

				offset = next + 1;
			}
		}
	}

	public class BooleanArrayValues : ArrayBackedList<Boolean>, IArrayValues<Boolean, byte> {
		readonly DynamicBuffer<byte> storage = new DynamicBuffer<byte>();

		public IList<byte> Storage { get { return storage; } }

		public GraphicsBuffer Buffer { get { return storage.Buffer; } }

		public override void Add(bool item) {
			base.Add(item);
			storage.Add((byte)(item ? 1 : 0));
		}

		public override void Clear() {
			base.ClearFast();
			storage.ClearFast();
		}

		public override void ClearFast() {
			base.ClearFast();
			storage.ClearFast();
		}

		public void Flush() { storage.Flush(); }

		public override void Insert(int index, bool item) {
			base.Insert(index, item);
			storage.Insert(index, (byte)(item ? 1 : 0));
		}

		public override void RemoveAt(int index) {
			base.RemoveAt(index);
			storage.RemoveAt(index);
		}
	}

	/// <summary>Declares the storage for a homogenous array of floating-point values.</summary>
	[Serializable]
	public class DoubleArrayElement : ArrayElement<Double, Single, DoubleArrayValues> {
		internal const string XmlName = "float_array";

		[XmlIgnore]
		public override GraphicsBuffer Buffer { get { return Values.Buffer; } }

		const int DefaultDigits = 6;
		const int DefaultMagnitude = 38;

		int digits = DefaultDigits, magnitude = DefaultMagnitude;

		/// <summary>The number of significant decimal digits ofthe floating-point values that can be contained in the array. The minimum value is 1; the maximum is 17. The default is 6. Optional.</summary>
		[XmlAttribute("digits")]
		[DefaultValue(DefaultDigits)]
		public int Digits {
			get { return digits; }
			set { digits = value; }
		}


		/// <summary>The largest exponent of the floating-pointvalues that can be contained in the array. The maximum value is 308; the minimum is -324. The default is 38. Optional.</summary>
		[XmlAttribute("magnitude")]
		[DefaultValue(DefaultMagnitude)]
		public int Magnitude {
			get { return magnitude; }
			set { magnitude = value; }
		}

		public DoubleArrayElement() { }
		public DoubleArrayElement(string id, IEnumerable<Double> values) : base(id, values) { }
		public DoubleArrayElement(string id, params Double[] values) : this(id, (IEnumerable<Double>)values) { }

		protected override void ValuesToString(StringBuilder builder) { CollectionToString(builder, Values); }
		protected override void StringToValues(string value) { StringToCollection(value, Values); }

		public static void CollectionToString(StringBuilder builder, IEnumerable<Double> items) {
			foreach (Double item in items) {
				if (builder.Length > 0)
					builder.Append(" ");
				builder.Append(item);
			}
		}

		public static void StringToCollection(string value, ICollection<Double> items) {
			for (int offset = 0; ; ) {
				for (; offset < value.Length && XmlConvert.IsWhitespaceChar(value[offset]); offset++) ;
				if (offset >= value.Length)
					break;

				int next = offset + 1;
				for (; next < value.Length && !XmlConvert.IsWhitespaceChar(value[next]); next++) ;

				string item = value.Substring(offset, next - offset);
				items.Add(Double.Parse(item));

				offset = next + 1;
			}
		}
	}

	public class DoubleArrayValues : ArrayBackedList<Double>, IArrayValues<Double, Single> {
		readonly DynamicBuffer<Single> storage = new DynamicBuffer<Single>();

		public IList<Single> Storage { get { return storage; } }

		public GraphicsBuffer Buffer { get { return storage.Buffer; } }

		public DoubleArrayValues() : base() { }
		public DoubleArrayValues(int capacity) : base(capacity) { }
		public DoubleArrayValues(params double[] collection) : base(collection) { }
		public DoubleArrayValues(IEnumerable<double> collection) : base(collection) { }

		public override void Add(Double item) {
			base.Add(item);
			storage.Add((Single)item);
		}

		public override void Clear() {
			base.ClearFast();
			storage.ClearFast();
		}

		public override void ClearFast() {
			base.ClearFast();
			storage.ClearFast();
		}

		public void Flush() { storage.Flush(); }

		public override void Insert(int index, Double item) {
			base.Insert(index, item);
			storage.Insert(index, (Single)item);
		}

		public override void RemoveAt(int index) {
			base.RemoveAt(index);
			storage.RemoveAt(index);
		}
	}

	/// <summary>Contains an array of XML IDREF values.</summary>
	[Serializable]
	public class IdArrayElement : StringArrayElement<IdArrayValues> {
		internal const string XmlName = "IDREF_array";
	}

	public class IdArrayValues : StringArrayValues {
		public IdArrayValues() : base() { }
		public IdArrayValues(int capacity) : base(capacity) { }
		public IdArrayValues(params String[] collection) : base(collection) { }
		public IdArrayValues(IEnumerable<String> collection) : base(collection) { }

		protected override int Translate(string value) {
			throw new NotImplementedException();
		}
	}

	[Serializable]
	public class Int32ArrayElement : ArrayElement<Int32, Int32, Int32ArrayValues> {
		internal const string XmlName = "int_array";

		/// <summary>The largest integer value that can be contained in the array. The default is 2147483647. Optional.</summary>
		[XmlAttribute("maxInclusive")]
		[DefaultValue(int.MaxValue)]
		public int MaxInclusive { get; set; }

		/// <summary>The smallest integer value that can be contained in the array. The default is -2147483648. Optional.</summary>
		[XmlAttribute("minInclusive")]
		[DefaultValue(int.MinValue)]
		public int MinInclusive { get; set; }

		[XmlIgnore]
		public override GraphicsBuffer Buffer { get { return Values.Buffer; } }

		public Int32ArrayElement() {
			MinInclusive = int.MinValue;
			MaxInclusive = int.MaxValue;
		}

		protected override void ValuesToString(StringBuilder builder) { CollectionToString(builder, Values); }
		protected override void StringToValues(string value) { StringToCollection(value, Values); }

		public static string CollectionToString(IEnumerable<int> items) {
			StringBuilder builder = new StringBuilder();
			CollectionToString(builder, items);
			return builder.ToString();
		}

		public static void CollectionToString(StringBuilder builder, IEnumerable<int> items) {
			foreach (int item in items) {
				if (builder.Length > 0)
					builder.Append(" ");
				builder.Append(item);
			}
		}

		public static void StringToCollection(string value, ICollection<int> items) {
			for (int offset = 0; ; ) {
				for (; offset < value.Length && XmlConvert.IsWhitespaceChar(value[offset]); offset++) ;
				if (offset >= value.Length)
					break;

				int next = offset + 1;
				for (; next < value.Length && !XmlConvert.IsWhitespaceChar(value[next]); next++) ;

				string item = value.Substring(offset, next - offset);
				items.Add(int.Parse(item));

				offset = next + 1;
			}
		}
	}

	public class Int32ArrayValues : DynamicBuffer<int>, IArrayValues<int, int> {
		public IList<int> Storage { get { return this; } }

		public Int32ArrayValues() { }
		public Int32ArrayValues(int capacity) : base(capacity) { }
		public Int32ArrayValues(params int[] collection) : base(collection) { }
		public Int32ArrayValues(IEnumerable<int> collection) : base(collection) { }
	}

	/// <summary>Stores a homogenous array of symbolic name values as a sequence of XML xs:Name values.</summary>
	[Serializable]
	public class NameArrayElement : StringArrayElement<NameArrayValues> {
		internal const string XmlName = "Name_array";
	}

	public class NameArrayValues : StringArrayValues {
		public NameArrayValues() : base() { }
		public NameArrayValues(int capacity) : base(capacity) { }
		public NameArrayValues(params String[] collection) : base(collection) { }
		public NameArrayValues(IEnumerable<String> collection) : base(collection) { }

		protected override int Translate(string value) {
			throw new NotImplementedException();
		}
	}

	/// <summary>Declares the storage for a homogenous array of scoped-identifier reference values.</summary>
	[Serializable]
	public class ScopedIdArrayElement : StringArrayElement<ScopedIdArrayValues> {
		internal const string XmlName = "SIDREF_array";
	}

	public class ScopedIdArrayValues : StringArrayValues {
		public ScopedIdArrayValues() : base() { }
		public ScopedIdArrayValues(int capacity) : base(capacity) { }
		public ScopedIdArrayValues(params String[] collection) : base(collection) { }
		public ScopedIdArrayValues(IEnumerable<String> collection) : base(collection) { }

		protected override int Translate(string value) {
			throw new NotImplementedException();
		}
	}

	/// <summary>Declares the storage for a homogenous array of floating-point values.</summary>
	[Serializable]
	public class SingleArrayElement : ArrayElement<Single, Single, SingleArrayValues> {
		internal const string XmlName = "float_array";

		[XmlIgnore]
		public override GraphicsBuffer Buffer { get { return Values.Buffer; } }

		const int DefaultDigits = 6;
		const int DefaultMagnitude = 38;

		int digits = DefaultDigits, magnitude = DefaultMagnitude;

		/// <summary>The number of significant decimal digits ofthe floating-point values that can be contained in the array. The minimum value is 1; the maximum is 17. The default is 6. Optional.</summary>
		[XmlAttribute("digits")]
		[DefaultValue(DefaultDigits)]
		public int Digits {
			get { return digits; }
			set { digits = value; }
		}


		/// <summary>The largest exponent of the floating-pointvalues that can be contained in the array. The maximum value is 308; the minimum is -324. The default is 38. Optional.</summary>
		[XmlAttribute("magnitude")]
		[DefaultValue(DefaultMagnitude)]
		public int Magnitude {
			get { return magnitude; }
			set { magnitude = value; }
		}

		public SingleArrayElement() { }
		public SingleArrayElement(string id, IEnumerable<Single> values) : base(id, values) { }
		public SingleArrayElement(string id, params Single[] values) : this(id, (IEnumerable<Single>)values) { }

		protected override void ValuesToString(StringBuilder builder) { CollectionToString(builder, Values); }
		protected override void StringToValues(string value) { StringToCollection(value, Values); }

		public static void CollectionToString(StringBuilder builder, IEnumerable<Single> items) {
			foreach (Single item in items) {
				if (builder.Length > 0)
					builder.Append(" ");
				builder.Append(item);
			}
		}

		public static void StringToCollection(string value, ICollection<Single> items) {
			for (int offset = 0; ; ) {
				for (; offset < value.Length && XmlConvert.IsWhitespaceChar(value[offset]); offset++) ;
				if (offset >= value.Length)
					break;

				int next = offset + 1;
				for (; next < value.Length && !XmlConvert.IsWhitespaceChar(value[next]); next++) ;

				string item = value.Substring(offset, next - offset);
				items.Add(Single.Parse(item));

				offset = next + 1;
			}
		}
	}

	public class SingleArrayValues : DynamicBuffer<Single>, IArrayValues<Single, Single> {
		public IList<float> Storage { get { return this; } }

		public SingleArrayValues() { }
		public SingleArrayValues(int capacity) : base(capacity) { }
		public SingleArrayValues(params Single[] collection) : base(collection) { }
		public SingleArrayValues(IEnumerable<Single> collection) : base(collection) { }
	}

	public abstract class StringArrayElement<TArrayValues> : ArrayElement<String, Int32, TArrayValues>
		where TArrayValues : StringArrayValues, new() {
		[XmlIgnore]
		public override GraphicsBuffer Buffer { get { return Values.Buffer; } }

		protected override void ValuesToString(StringBuilder builder) { CollectionToString(builder, Values); }
		protected override void StringToValues(string value) { StringToCollection(value, Values); }

		public static void CollectionToString(StringBuilder builder, IEnumerable<string> items) {
			foreach (string item in items) {
				if (builder.Length > 0)
					builder.Append(" ");
				builder.Append(XmlConvert.EncodeName(item));
			}
		}

		public static void StringToCollection(string value, ICollection<string> items) {
			for (int offset = 0; ; ) {
				for (; offset < value.Length && XmlConvert.IsWhitespaceChar(value[offset]); offset++) ;
				if (offset >= value.Length)
					break;

				int next = offset + 1;
				for (; next < value.Length && !XmlConvert.IsWhitespaceChar(value[next]); next++) ;

				string item = value.Substring(offset, next - offset);
				items.Add(XmlConvert.DecodeName(item));

				offset = next + 1;
			}
		}
	}

	public abstract class StringArrayValues : ArrayBackedList<String>, IArrayValues<String, Int32> {
		readonly DynamicBuffer<Int32> storage = new DynamicBuffer<Int32>();

		public IList<Int32> Storage { get { return storage; } }

		public GraphicsBuffer Buffer { get { return storage.Buffer; } }

		public StringArrayValues() : base() { }
		public StringArrayValues(int capacity) : base(capacity) { }
		public StringArrayValues(params String[] collection) : base(collection) { }
		public StringArrayValues(IEnumerable<String> collection) : base(collection) { }

		public override void Add(String item) {
			base.Add(item);
			storage.Add(Translate(item));
		}

		public override void Clear() {
			base.ClearFast();
			storage.ClearFast();
		}

		public override void ClearFast() {
			base.ClearFast();
			storage.ClearFast();
		}

		public void Flush() { storage.Flush(); }

		public override void Insert(int index, String item) {
			base.Insert(index, item);
			storage.Insert(index, Translate(item));
		}

		public override void RemoveAt(int index) {
			base.RemoveAt(index);
			storage.RemoveAt(index);
		}

		protected abstract int Translate(string value);
	}
}
