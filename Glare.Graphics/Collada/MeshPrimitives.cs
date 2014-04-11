using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	public abstract class MeshPrimitives : Element, IName {
		internal const string XmlMaterialAttribute = "material";

		SharedInputCollection inputs;

		[XmlElement("input")]
		public SharedInputCollection Inputs {
			get { return inputs; }
			set { SetCollection<SharedInput, SharedInputCollection>(ref inputs, value); }
		}

		/// <summary>Get or set the id of the material to use.</summary>
		[XmlAttribute(XmlMaterialAttribute, DataType = "NCName")]
		public string Material { get; set; }

		/// <summary>Get or set the name of the object, optional.</summary>
		[XmlAttribute(XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		public MeshPrimitives() {
			Inputs = new SharedInputCollection();
		}

		public MeshPrimitives(string material)
			: this() {
			Material = material;
		}

		protected static string[] IndicesToString<TList, TIndicesList>(TList list)
			where TList : IList<TIndicesList>
			where TIndicesList : IList<int> {
			if (list == null)
				return new string[0];

			string[] result = new string[list.Count];
			StringBuilder builder = new StringBuilder();

			for (int index = 0; index < result.Length; index++) {
				builder.Clear();
				if (list[index] != null)
					Int32ArrayElement.CollectionToString(builder, list[index]);
				result[index] = builder.ToString();
			}

			return result;
		}

		protected static void StringToIndices<TList, TIndicesList>(TList source, string[] value)
			where TList : IList<TIndicesList>
			where TIndicesList : IList<int>, new() {
			//source.Capacity = value.Length;

			for (int index = 0; index < value.Length; index++) {
				var list = new TIndicesList();
				Int32ArrayElement.StringToCollection(value[index], list);
				source.Add(list);
			}
		}
	}

	public abstract class MeshMultipleElementsPrimitives : MeshPrimitives {
		[XmlIgnore]
		public List<List<int>> Elements { get; set; }

		[XmlAttribute("count")]
		public int XmlCount {
			get { return Elements.Count; }
			set { Elements.Capacity = value; }
		}

		[XmlElement("p")]
		public string[] XmlElementsString {
			get { return IndicesToString<List<List<int>>, List<int>>(Elements); }
			set { StringToIndices<List<List<int>>, List<int>>(Elements, value); }
		}

		public MeshMultipleElementsPrimitives()
			: base() {
			Elements = new List<List<int>>();
		}

		public MeshMultipleElementsPrimitives(string material, params List<int>[] elements)
			: base(material) {
			Material = material;
			Elements.AddRange(elements);
		}
	}

	public abstract class MeshSingleElementsPrimitives : MeshPrimitives {
		[XmlIgnore]
		public List<int> Elements { get; set; }

		[XmlAttribute("count")]
		public int XmlCount {
			get { return Elements.Count; }
			set { Elements.Capacity = value; }
		}

		[XmlElement("p")]
		public string XmlElementsString {
			get { return Int32ArrayElement.CollectionToString(Elements); }
			set { Int32ArrayElement.StringToCollection(value, Elements); }
		}

		public MeshSingleElementsPrimitives()
			: base() {
			Elements = new List<int>();
		}

		public MeshSingleElementsPrimitives(string material, params int[] elements) : this(material, (IEnumerable<int>)elements) { }

		public MeshSingleElementsPrimitives(string material, IEnumerable<int> elements)
			: base(material) {
			Elements = new List<int>(elements);
		}
	}

	[Serializable]
	public class MeshPrimitivesCollection : ElementCollection<MeshPrimitives> {
		public MeshPrimitivesCollection() { }
		public MeshPrimitivesCollection(int capacity) : base(capacity) { }
		public MeshPrimitivesCollection(IEnumerable<MeshPrimitives> collection) : base(collection) { }
		public MeshPrimitivesCollection(params MeshPrimitives[] list) : base(list) { }
	}

	[Serializable]
	public class MeshPolygons : MeshMultipleElementsPrimitives {
		internal const string XmlName = "polygons";

		public MeshPolygons() : base() { }
		public MeshPolygons(string material, params List<int>[] elements) : base(material, elements) { }
	}

	[Serializable]
	public class MeshTriangles : MeshSingleElementsPrimitives {
		internal const string XmlName = "triangles";

		public MeshTriangles() : base() { }
		public MeshTriangles(string material, params int[] elements) : base(material, elements) { }
		public MeshTriangles(string material, IEnumerable<int> elements) : base(material, elements) { }
	}

	[Serializable]
	public class MeshTriangleStrips : MeshMultipleElementsPrimitives {
		internal const string XmlName = "tristrips";

		public MeshTriangleStrips() : base() { }
		public MeshTriangleStrips(string material, params List<int>[] elements) : base(material, elements) { }
	}
}
