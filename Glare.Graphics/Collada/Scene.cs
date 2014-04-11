using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public class Scene : Element, IExtras {
		internal const string XmlName = "scene";

		ExtraCollection extras;
		VisualSceneInstance visualSceneInstance;

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName)]
		public ExtraCollection Extras {
			get { return extras; }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		[XmlElement(VisualSceneInstance.XmlName)]
		public VisualSceneInstance VisualSceneInstance {
			get { return visualSceneInstance; }
			set { SetElement(ref visualSceneInstance, value); }
		}

		public Scene() {
			Extras = new ExtraCollection();
		}
	}

	[Serializable]
	public class SceneCollection : ElementCollection<Scene> {
		public SceneCollection() { }
		public SceneCollection(int capacity) : base(capacity) { }
		public SceneCollection(IEnumerable<Scene> collection) : base(collection) { }
		public SceneCollection(params Scene[] list) : base(list) { }
	}
}
