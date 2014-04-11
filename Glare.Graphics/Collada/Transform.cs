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
	[Serializable]
	public abstract class Transform : ScopedIdElement {
		[XmlText]
		public abstract string XmlValueString { get; set; }
	}

	[Serializable]
	public class TransformCollection : ElementCollection<Transform> {
		public TransformCollection() { }
		public TransformCollection(int capacity) : base(capacity) { }
		public TransformCollection(IEnumerable<Transform> collection) : base(collection) { }
		public TransformCollection(params Transform[] list) : base(list) { }
	}

	[Serializable]
	public class RotateTransform : Transform {
		internal const string XmlName = "rotate";

		/// <summary>Get or set the rotation angle.</summary>
		[XmlIgnore]
		public Angle Angle { get; set; }

		/// <summary>Get or set the axis on which to rotate.</summary>
		[XmlIgnore]
		public Vector3d Axis { get; set; }

		[XmlText]
		public override string XmlValueString {
			get { return string.Format("{0} {1} {2} {3}", Axis.X, Axis.Y, Axis.Z, Angle.InDegrees); }

			set {
				List<Double> list = new List<Double>();
				DoubleArrayElement.StringToCollection(value, list);

				if (list.Count != 4)
					throw new Exception();
				Axis = new Vector3d(list[0], list[1], list[2]);
				Angle = Angle.Degrees(list[3]);
			}
		}

		public RotateTransform() { }

		public RotateTransform(Vector3d axis, Angle angle) : this() { Axis = axis; Angle = angle; }

		public RotateTransform(double axisX, double axisY, double axisZ, Angle angle) : this(new Vector3d(axisX, axisY, axisZ), angle) { }
	}

	[Serializable]
	public class ScaleTransform : Transform {
		internal const string XmlName = "scale";

		[XmlIgnore]
		public Vector3d Scale { get; set; }

		[XmlText]
		public override string XmlValueString {
			get { return string.Format("{0} {1} {2}", Scale.X, Scale.Y, Scale.Z); }

			set {
				List<Double> list = new List<Double>();
				DoubleArrayElement.StringToCollection(value, list);

				if (list.Count != 3)
					throw new Exception();
				Scale = new Vector3d(list[0], list[1], list[2]);
			}
		}

		public ScaleTransform() { }

		public ScaleTransform(Vector3d scale) : this() { Scale = scale; }

		public ScaleTransform(double x, double y, double z) : this(new Vector3d(x, y, z)) { }
	}

	[Serializable]
	public class TranslateTransform : Transform {
		internal const string XmlName = "translate"; 

		[XmlIgnore]
		public Vector3d Translation { get; set; }

		[XmlText]
		public override string XmlValueString {
			get { return string.Format("{0} {1} {2}", Translation.X, Translation.Y, Translation.Z); }

			set {
				List<Double> list = new List<Double>();
				DoubleArrayElement.StringToCollection(value, list);

				if (list.Count != 3)
					throw new Exception();
				Translation = new Vector3d(list[0], list[1], list[2]);
			}
		}

		public TranslateTransform() { }

		public TranslateTransform(Vector3d translation) : this() { Translation = translation; }

		public TranslateTransform(double x, double y, double z) : this(new Vector3d(x, y, z)) { }
	}
}
