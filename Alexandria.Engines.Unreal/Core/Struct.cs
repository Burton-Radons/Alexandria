using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
    public class Struct : SourceObject {
		public override string ShortDescription {
			get {
				string text = "struct " + Export.Name + " { ";
				string lastType = "";

				foreach(var child in Children) {
					var property = child as Property;

					if(property != null) {
						var newType = property.TypeDescription;
						if(newType == lastType)
							text += ", " + child.Export.Name;
						else {
							if(lastType != "")
								text += "; ";
							text += "var " + child.ShortDescription;
						}
						lastType = newType;
					} else {
						if(lastType != "")
							text += "; ";
						text += child.ShortDescription;
						lastType = "";
					}

				}

				if(lastType != "")
					text += "; ";

				return text + "};";
			}
		}
    }
}
