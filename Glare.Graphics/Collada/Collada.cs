using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Declares the root of the document that contains some of the content in the COLLADA schema.</summary>
	/// <remarks>The COLLADA schema is XML based; therefore, it must have exactly one document root element or document entity to be a well-formed XML document. The COLLADA element serves that purpose.</remarks>
	[Serializable, XmlRoot("COLLADA", Namespace = "http://www.collada.org/2005/11/COLLADASchema")]
	[XmlInclude(typeof(Mesh))]
	public class Collada : Element, IAsset, IExtras {
		Asset asset;
		EffectCollection effects;
		ExtraCollection extras;
		GeometryCollection geometries;
		LibraryImages images;
		MaterialCollection materials;
		Scene scene;
		VisualSceneCollection visualScenes;

		const int AssetOrder = 0;
		const int ImagesOrder = AssetOrder + 1;
		const int EffectsOrder = ImagesOrder + 1;
		const int MaterialsOrder = EffectsOrder + 1;
		const int GeometriesOrder = MaterialsOrder + 1;
		const int VisualScenesOrder = GeometriesOrder + 1;
		const int SceneOrder = VisualScenesOrder + 1;
		const int ExtrasOrder = SceneOrder + 1;

		internal readonly Dictionary<string, Element> ElementsById = new Dictionary<string, Element>();

		/// <summary>Defines asset-management information regarding its parent element.</summary>
		[XmlElement(Asset.XmlName, Order = AssetOrder)]
		public Asset Asset {
			get { return asset; }
			set { SetElement(ref asset, value); }
		}

		/// <summary>Get or set the library of <see cref="Effect"/> objects.</summary>
		[XmlArray("library_effects", Order = EffectsOrder), XmlArrayItem(Effect.XmlName)]
		public EffectCollection Effects {
			get { return effects; }
			set { SetCollection<Effect, EffectCollection>(ref effects, value); }
		}

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName, Order = ExtrasOrder)]
		public ExtraCollection Extras {
			get { return extras; }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		/// <summary>Get or set the library of <see cref="Geometry"/> objects.</summary>
		[XmlArray("library_geometries", Order = GeometriesOrder), XmlArrayItem(Geometry.XmlName)]
		public GeometryCollection Geometries {
			get { return geometries; }
			set { SetCollection<Geometry, GeometryCollection>(ref geometries, value); }
		}

		/// <summary>Provides a library for the storage of <see cref="Image"/> assets. </summary>
		[XmlElement(LibraryImages.XmlName, Order = ImagesOrder)]
		public LibraryImages Images {
			get { return GetCollection(ref images); }
			set { SetElement(ref images, value); }
		}

		/// <summary>Get or set the library of <see cref="Material"/> objects.</summary>
		[XmlArray("library_materials", Order = MaterialsOrder), XmlArrayItem(Material.XmlName)]
		public MaterialCollection Materials {
			get { return materials; }
			set { SetCollection<Material, MaterialCollection>(ref materials, value); }
		}

		/// <summary>Get or set the <see cref="Scene"/> object.</summary>
		[XmlElement("scene", Order = SceneOrder)]
		public Scene Scene {
			get { return scene; }
			set { SetElement(ref scene, value); }
		}

		/// <summary>The COLLADA schema revision with which the instance document conforms. The only valid value is 1.5.0. Required.</summary>
		[XmlAttribute("version")]
		public string Version { get; set; }

		/// <summary>Get or set the library of <see cref="VisualScene"/> objects.</summary>
		[XmlArray("library_visual_scenes", Order = VisualScenesOrder), XmlArrayItem(VisualScene.XmlName)]
		public VisualSceneCollection VisualScenes {
			get { return visualScenes; }
			set { SetCollection<VisualScene, VisualSceneCollection>(ref visualScenes, value); }
		}

		public Collada() {
			Asset = new Glare.Graphics.Collada.Asset();
			Effects = new EffectCollection();
			Extras = new ExtraCollection();
			Geometries = new GeometryCollection();
			Materials = new MaterialCollection();
			Version = "1.4.1";
			VisualScenes = new VisualSceneCollection();
		}

		static XmlSerializer Serializer;

		/// <summary>Get a reusable <see cref="XmlSerializer"/> that can serializer or deserialize <see cref="Collada"/> documents.</summary>
		/// <returns></returns>
		public static XmlSerializer GetSerializer() { return Serializer ?? (Serializer = new XmlSerializer(typeof(Collada))); }

		public static void Test() {
			var document = new Collada() {
				Asset = new Asset() {
					Created = DateTime.Parse("2005-11-14T02:16:38Z"),
					Modified = DateTime.Parse("2005-11-15T11:36:38Z"),
					Revision = "1.0",
				},

				Effects = new EffectCollection() {
					new Effect("whitePhong") {
						Profiles = new ProfileCollection() {
							new ProfileCommon(
								new ProfileCommonTechnique("phong1",
									new PhongShader() {
										Emission = new Vector4d(1, 1, 1, 1),
										Ambient = new Vector4d(1, 1, 1, 1),
										Diffuse = new Vector4d(1, 1, 1, 1),
										Specular = new Vector4d(1, 1, 1, 1),
										Shininess = 20.0,
										Reflective = new Vector4d(1, 1, 1, 1),
										Reflectivity = 0.5,
										Transparent = new Vector4d(1, 1, 1, 1),
										Transparency = 1.0,
									})),
						},
					},
				},

				Materials = new MaterialCollection() {
					new Material("whiteMaterial",
						new InstanceEffect("#whitePhong")),
				},

				Geometries = new GeometryCollection() {
					new Geometry("box") {
						Name = "box", 

						Element = new Mesh() {
							Sources = new SourceCollection() {
								new Source("box-pos",
									new SingleArrayElement("box-Pos-array", 
										-0.5f, 0.5f, 0.5f,
										0.5f, 0.5f, 0.5f,
										-0.5f, -0.5f, 0.5f,
										0.5f, -0.5f, 0.5f,
										-0.5f, 0.5f, -0.5f,
										0.5f, 0.5f, -0.5f,
										-0.5f, -0.5f, -0.5f,
										0.5f, -0.5f, -0.5f),
									new SourceTechniqueCommon(
										new Accessor("#box-Pos-array", 8, 3, 0,
											new Parameter("X", ParameterType.Double),
											new Parameter("Y", ParameterType.Double),
											new Parameter("Z", ParameterType.Double)))),

								new Source("box-0-Normal",
									new SingleArrayElement("box-0-Normal-array", 
										1, 0, 0, 
										-1, 0, 0, 
										0, 1, 0, 
										0, -1, 0, 
										0, 0, 1, 
										0, 0, -1),
									new SourceTechniqueCommon(
										new Accessor("#box-0-Normal-array", 6, 3, 0,
											new Parameter("X", ParameterType.Double),
											new Parameter("Y", ParameterType.Double),
											new Parameter("Z", ParameterType.Double)))),
							},

							Vertices = new VertexCollection("box-Vtx",
								new Input(InputSemantic.Position, "#box-Pos")),

							Elements= new MeshPrimitivesCollection() {
								new MeshPolygons("WHITE",
									new List<int>() { 0, 4, 2, 4, 3, 4, 1, 4 },
									new List<int>() { 0, 2, 1, 2, 5, 2, 4, 2 },
									new List<int>() { 6, 3, 7, 3, 3, 3, 2, 3 },
									new List<int>() { 0, 1, 4, 1, 6, 1, 2, 1 },
									new List<int>() { 3, 0, 7, 0, 5, 0, 1, 0 },
									new List<int>() { 5, 5, 7, 5, 6, 5, 4, 5 })
							},
						} // Mesh
					} // Geometry
				}, // Geometries

				VisualScenes = new VisualSceneCollection() {
					new VisualScene("DefaultScene",
						new Node("Box") {
							Name = "Box",

							Transforms = new TransformCollection() {
								new TranslateTransform(0, 0, 0),
								new RotateTransform(0, 0, 1, Angle.Zero),
								new RotateTransform(0, 1, 0, Angle.Zero),
								new RotateTransform(1, 0, 0, Angle.Zero),
								new ScaleTransform(1, 1, 1)
							},

							GeometryInstances = new GeometryInstanceCollection() {
								new InstanceGeometry("#box",
									new BindMaterial(
										new BindMaterialTechniqueCommon(
											new InstanceMaterial("WHITE", "#whiteMaterial"))))
							}
						})
				}, // VisualScenes

				Scene = new Scene() {
					VisualSceneInstance = new VisualSceneInstance("#DefaultScene"),
				}, // Scene
			};

			TestSerialization(document);
		}

		static void TestSerialization(object value) {
			var xmlWriterSettings = new XmlWriterSettings() { Indent = true, IndentChars = "    " };

			var builder = new StringBuilder();
			var writer = XmlWriter.Create(builder, xmlWriterSettings);
			var serializer = new XmlSerializer(value.GetType());

			serializer.Serialize(writer, value);
			string text = builder.ToString();

			var reader = XmlReader.Create(new StringReader(text), new XmlReaderSettings() { });
			var value2 = serializer.Deserialize(reader);

			builder.Clear();
			writer = XmlWriter.Create(builder, xmlWriterSettings);

			serializer.Serialize(writer, value2);
			string text2 = builder.ToString();

			if (text != text2)
				throw new InvalidOperationException("Reserialization didn't produce the same object!");
		}
	}
}
