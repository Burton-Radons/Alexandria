using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Rendering {
	public class Model : NamedObject {
		readonly List<ModelAttribute> attributes = new List<ModelAttribute>();
		readonly ModelBoneCollection bones;
		readonly Sphere3d bounds;
		readonly GraphicsBuffer elementBuffer;
		readonly ElementType elementType;
		readonly ModelMeshCollection meshes;

		/// <summary>Get the collection of attributes used by the model.</summary>
		public List<ModelAttribute> Attributes { get { return attributes; } }

		public ModelBoneCollection Bones { get { return bones; } }

		public Sphere3d Bounds { get { return bounds; } }

		/// <summary>Get the <see cref="GraphicsBuffer"/> to use for element indices, or 0 to not use element indices.</summary>
		public GraphicsBuffer ElementBuffer { get { return elementBuffer; } }

		/// <summary>Get the type of an element in the <see cref="ElementBuffer"/> array.</summary>
		public ElementType ElementType { get { return elementType; } }

		/// <summary>Get the dynamic collection of meshes attached to the <see cref="Model"/>.</summary>
		public ModelMeshCollection Meshes { get { return meshes; } }

		public Model(GraphicsBuffer elementBuffer, ElementType elementType, Sphere3d bounds) {
			if (elementBuffer == null)
				throw new ArgumentNullException("elementBuffer");
			this.elementBuffer = elementBuffer;
			this.elementType = elementType;
			this.bounds = bounds;
			bones = new ModelBoneCollection(this);
			meshes = new ModelMeshCollection(this);
		}

		public delegate void SetWorldMatrixCallback(ref Matrix4d world);

		public void BindAttributes(IChannelAttributeSource channelAttributes) {
			foreach (ModelAttribute modelAttribute in attributes) {
				ProgramAttribute attribute = channelAttributes.TryGetChannelAttribute(modelAttribute);
				if (attribute != null)
					attribute.Bind(modelAttribute.Buffer, modelAttribute.OffsetInBytes, modelAttribute.Format, modelAttribute.Stride);
			}
		}

		public void BindAttributes(Program program, string namePrefix, string nameSuffix) {
			var programAttributes = program.Attributes;
			StringBuilder builder = new StringBuilder();
			bool hasNamePrefix = !string.IsNullOrEmpty(namePrefix), hasNameSuffix = !string.IsNullOrEmpty(nameSuffix);

			foreach (ModelAttribute modelAttribute in attributes) {
				ProgramAttribute attribute;
				string name;

				if (!hasNamePrefix && !hasNameSuffix && modelAttribute.Index == 0)
					attribute = programAttributes.TryGetValue(name = modelAttribute.Channel.ToString());
				else {
					if (hasNamePrefix)
						builder.Append(namePrefix);
					builder.Append(modelAttribute.Channel.ToString());
					if (modelAttribute.Index > 0)
						builder.Append(modelAttribute.Index);
					if (hasNameSuffix)
						builder.Append(nameSuffix);
					attribute = programAttributes.TryGetValue(name = builder.ToString());
					builder.Clear();
				}

				if (attribute != null)
					attribute.Bind(modelAttribute.Buffer, modelAttribute.OffsetInBytes, modelAttribute.Format, modelAttribute.Stride);
				else if (modelAttribute.Channel == ModelAttributeChannel.Position && modelAttribute.Index == 0)
					throw new Exception("The program does not contain a " + name + " attribute for the vertex position, which is required.");
			}
		}

		public void Draw(Program program, ref Matrix4d world, SetWorldMatrixCallback setWorldMatrix, IModelMaterialBinder materialBinder) {
			ModelMaterial material = null;

			foreach (ModelMesh mesh in meshes) {
				if (setWorldMatrix != null) {
					Matrix4d meshWorld;

					if (mesh.bone != null) {
						mesh.bone.GetWorldTransform(out meshWorld);
						world.Multiply(ref meshWorld, out meshWorld);
						setWorldMatrix.Invoke(ref meshWorld);
					}
				}

				foreach (ModelPart part in mesh.parts) {
					if (part.Material != null && part.Material != material && materialBinder != null) {
						material = part.Material;
						materialBinder.BindMaterial(part.Material);
					}

					program.Draw(part.Primitive, part.Count, elementBuffer, elementType, part.Offset);
				}
			}
		}

		public void Draw(Program program, IChannelAttributeSource channelAttributes, ref Matrix4d world, SetWorldMatrixCallback setWorldMatrix, IModelMaterialBinder materialBinder) {
			if (channelAttributes != null)
				BindAttributes(channelAttributes);
			Draw(program, ref world, setWorldMatrix, materialBinder);
		}

		public void Draw(Program program, string attributeNamePrefix, string attributeNameSuffix, ref Matrix4d world, SetWorldMatrixCallback setWorldMatrix = null) {
			BindAttributes(program, attributeNamePrefix, attributeNameSuffix);
			Draw(program, ref world, setWorldMatrix, program as IModelMaterialBinder);
		}

		public void Draw(Program program, string attributeNamePrefix, string attributeNameSuffix, Matrix4d world, SetWorldMatrixCallback setWorldMatrix = null) { Draw(program, attributeNamePrefix, attributeNameSuffix, ref world, setWorldMatrix); }
		public void Draw(Program program, Matrix4d world, SetWorldMatrixCallback setWorldMatrix = null) { Draw(program, null, null, ref world, setWorldMatrix); }
		public void Draw(Program program, ref Matrix4d world, SetWorldMatrixCallback setWorldMatrix = null) { Draw(program, null, null, ref world, setWorldMatrix); }
		public void Draw(Program program) { Draw(program, Matrix4d.Identity, null); }
	}

	public abstract class ModelObject : NamedObject {
		internal Model model;

		/// <summary>Get the <see cref="Glare.Graphics.Model"/> that this is a part of, or <c>null</c> if it's not attached.</summary>
		public Model Model { get { return model; } }
	}
}
