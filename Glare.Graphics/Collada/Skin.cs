using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Contains vertex and primitive information sufficient to describe blend-weight skinning. </summary>
	/// <remarks>
	/// For character skinning, an animation engine drives the joints (skeleton) of a skinned character. A skin mesh describes the associations between the joints and the mesh vertices forming the skin topology. The joints influence the transformation of skin mesh vertices according to a controlling algorithm. 
	/// 
	/// A common skinning algorithm blends the influences of neighboring joints according to weighted values. 
	/// 
	/// The classical skinning algorithm transforms points of a geometry (for example vertices of a mesh) with matrices of nodes (sometimes called joints) and averages the result using scalar weights. The affected geometry is called the skin, the combination of a transform (node) and its corresponding weight is called an influence, and the set of influencing nodes (usually a hierarchy) is called a skeleton. “Skinning” involves two steps: 
	/// 
	/// •  Preprocessing, known as “binding the skeleton to the skin” 
	/// •  Running the skinning algorithm to modify the shape of the skin as the pose of the skeleton changes 
	/// 
	/// The results of the pre-processing, or “skinning information” consists of the following: 
	/// •  bind-shape: also called “default shape”. This is the shape of the skin when it was bound to the skeleton. This includes positions (required) for each corresponding <mesh>vertex and may optionally include additional vertex attributes. 
	/// •  influences: a variable-length lists of node + weight pairs for each <mesh>vertex. 
	/// •  bind-pose: the transforms of all influences at the time of binding. This per-node information is usually represented bya “bind-matrix”, which is the local-to-world matrix of a node at the time of binding. 
	/// 
	/// In the skinning algorithm, all transformations are done relative to the bind-pose. This relative transform is usually pre-computed for each node in the skeleton and is stored as a skinning matrix. 
	/// 
	/// To derive the new (“skinned”) position of a vertex, the skinning matrix of each influencing node transforms the bind-shape position of the vertex and the result is averaged using the blending weights. 
	/// 
	/// The easiest way to derive the skinning matrix is to multiply the current local-to-world matrix of a node by the inverse of the node’s bind-matrix. This effectively cancels out the bind-pose transform of each node and allows us to work in the common object space of the skin. 
	/// 
	/// The binding process usually involves: 
	/// •  Storing the current shape of the skin as the bind-shape 
	/// •  Computing and storing the bind-matrices 
	/// •  Generating default blending weights, usually with some fall-off function: the farther a joint is from a given vertex, the less it influences it. Also, if a weight is 0,the influence can be omitted. 
	/// 
	/// After that, the artist is allowed tohand-modify the weights, usually by “painting” them on the mesh. 
	/// </remarks>
	[Serializable]
	public class Skin : ControlElement, IExtras {
		internal const string XmlName = "skin";

		ExtraCollection extras;
		SourceCollection sources;

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName, Order = 1)]
		public ExtraCollection Extras {
			get { return GetCollection(ref extras); }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		/// <summary>A URI reference to the base mesh (a static mesh or a morphed mesh). This also provides the bind-shape of the skinned mesh. Required.</summary>
		[XmlAttribute("source", DataType = "anyURI")]
		public string Source { get; set; }

		/// <summary>Provides most of the data required for skinning the given base mesh.</summary>
		[XmlElement(Glare.Graphics.Collada.Source.XmlName, Order = 0)]
		public SourceCollection Sources {
			get { return GetCollection(ref sources); }
			set { SetCollection<Source, SourceCollection>(ref sources, value); }
		}
	}
}
