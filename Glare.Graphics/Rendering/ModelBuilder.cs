using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Glare.Graphics.Internal;
using Glare.Internal;

using Scalar = System.Single;
using BVector2 = Glare.Vector2f;
using BVector3 = Glare.Vector3f;
using BVector4 = Glare.Vector4f;

namespace Glare.Graphics.Rendering {
	public partial class ModelBuilder {
		abstract class Channel {
			public abstract int Count { get; }
			public abstract ModelAttributeChannel Id { get; }

			public abstract void AddVertex();
			public abstract void CheckSize(ref int size);
			public abstract void Write(byte[] data, ref int offset, Model model, GraphicsBuffer buffer);
		}

		class Channel<T> : Channel where T : struct {
			public readonly ModelAttributeChannel id;
			public readonly int Index;
			public readonly T DefaultValue;

			public override ModelAttributeChannel Id { get { return id; } }

			static readonly int TypeSize = Marshal.SizeOf(typeof(T));

			T[] list;
			int count;

			public T this[int index] { get { return list[index]; } }

			public Channel(ModelAttributeChannel id, int index, T defaultValue, int count = 0) {
				this.id = id;
				this.Index = index;
				this.count = -count;
				this.DefaultValue = defaultValue;
			}

			public override int Count { get { return count; } }

			public override void AddVertex() {
				if (count <= 0) {
					count--;
					if (id == ModelAttributeChannel.Position)
						MakePositive();
				} else {
					if (count >= list.Length) {
						var copy = new T[(count + 1) * 3 / 2];
						list.CopyTo(copy, 0);
						list = copy;
					}
					list[count] = DefaultValue;
					count++;
				}
			}

			void Round(ref int size) { size = (size + 3) & ~3; }

			public override void CheckSize(ref int size) {
				if (count < 0)
					return;
				Round(ref size);
				size += TypeSize * count;
			}

			void MakePositive() {
				count = -count;
				list = new T[Math.Max(count, 16)];
				for (int index = 0; index < count; index++)
					list[index] = DefaultValue;
			}

			public void SetLast(T value) { SetLast(ref value); }

			public void SetLast(ref T value) {
				if (count < 0)
					MakePositive();
				list[count - 1] = value;
			}

			public override void Write(byte[] data, ref int offset, Model model, GraphicsBuffer buffer) {
				if (count <= 0)
					return;

				Round(ref offset);
				model.Attributes.Add(new ModelAttribute(buffer, offset, Format.From<T>(), 0, Id, Index));
				offset += list.CopyTo(0, count, data, offset);
			}
		}

		class ChannelList<T> : Channel where T : struct {
			readonly List<Channel<T>> channels = new List<Channel<T>>();
			int count = 0;

			public readonly T DefaultValue;
			readonly ModelAttributeChannel id;

			public override int Count { get { return count; } }

			public override ModelAttributeChannel Id { get { return id; } }

			public ChannelList(ModelAttributeChannel id, T defaultValue) { this.id = id; this.DefaultValue = defaultValue; }

			public override void AddVertex() {
				foreach (Channel<T> channel in channels)
					channel.AddVertex();
				count++;
			}

			public override void CheckSize(ref int size) {
				foreach (var channel in channels)
					channel.CheckSize(ref size);
			}

			public void SetLast(T value, int channel = 0) {
				for (int index = channels.Count; index <= channel; index++)
					channels.Add(new Channel<T>(Id, index, DefaultValue, count));
				channels[channel].SetLast(ref value);
			}

			public void SetLast(ref T value, int channel = 0) {
				for (int index = channels.Count; index <= channel; index++)
					channels.Add(new Channel<T>(Id, index, DefaultValue, count));
				channels[channel].SetLast(ref value);
			}

			public override void Write(byte[] data, ref int offset, Model model, GraphicsBuffer buffer) {
				foreach (var channel in channels)
					channel.Write(data, ref offset, model, buffer);
			}
		}

		struct MeshInfo {
			public readonly ModelBone Bone;
			public readonly List<ModelPart> Parts;

			public MeshInfo(ModelBone bone) {
				Bone = bone;
				Parts = new List<ModelPart>();
			}

			public void AddCentrePoints(ref SphereBuilder sphereBuilder, Channel<BVector3> positionChannel, int[] indices) {
				// Calculate the centre of the mesh.
				foreach (var part in Parts) {
					for (int index = part.Offset / 4, count = part.Count + index; index < count; index++) {
						int element = indices[index];
						if (element == -1)
							continue;
						Vector3f point = positionChannel[element];

						sphereBuilder.AddCentrePoint(ref point);
					}
				}
			}

			public void AddRadiusPoints(ref SphereBuilder sphereBuilder, Channel<BVector3> positionChannel, int[] indices) {
				foreach (var part in Parts)
					for (int index = part.Offset / 4, count = part.Count + index; index < count; index++) {
						int element = indices[index];
						if (element == -1)
							continue;
						Vector3f point = positionChannel[element];

						sphereBuilder.AddRadiusPoint(ref point);
					}
			}
		}

		struct ChannelEnumerator : IEnumerator<Channel>, IEnumerable<Channel> {
			readonly ModelBuilder builder;
			int index;

			public ChannelEnumerator(ModelBuilder builder) {
				this.builder = builder;
				this.index = -1;
			}

			public Channel Current {
				get {
					switch (index) {
						case 0: return builder.positionChannel;
						case 1: return builder.normalChannel;
						case 2: return builder.colorChannel;
						case 3: return builder.texelChannels;
						case 4: return builder.boneIndicesChannels;
						case 5: return builder.boneWeightsChannels;
						default: throw new Exception();
					}
				}
			}

			public void Dispose() { }

			object System.Collections.IEnumerator.Current { get { return Current; } }

			public bool MoveNext() { return ++index <= 5; }

			public void Reset() { index = -1; }

			public ChannelEnumerator GetEnumerator() { return new ChannelEnumerator(builder); }

			IEnumerator<Channel> IEnumerable<Channel>.GetEnumerator() { return GetEnumerator(); }

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
		}

		readonly Channel<BVector3> positionChannel = new Channel<BVector3>(ModelAttributeChannel.Position, 0, Vector3f.Zero);
		readonly Channel<Vector4rgba> colorChannel = new Channel<Vector4rgba>(ModelAttributeChannel.Diffuse, 0, Vector4rgba.One);
		readonly Channel<BVector3> normalChannel = new Channel<BVector3>(ModelAttributeChannel.Normal, 0, Vector3f.Zero);
		readonly ChannelList<BVector2> texelChannels = new ChannelList<BVector2>(ModelAttributeChannel.Texel, Vector2f.Zero);
		readonly ChannelList<Vector4i> boneIndicesChannels = new ChannelList<Vector4i>(ModelAttributeChannel.BoneIndices, Vector4i.UnitX);
		readonly ChannelList<Vector4f> boneWeightsChannels = new ChannelList<Vector4f>(ModelAttributeChannel.BoneWeights, Vector4f.UnitX);

		readonly List<MeshInfo> meshes = new List<MeshInfo>();

		int[] indices = new int[16];
		int indexCount = 0;
		int firstIndex = 0;

		ChannelEnumerator Channels { get { return new ChannelEnumerator(this); } }

		public int IndexCount { get { return indexCount; } }
		public int VertexCount { get { return positionChannel.Count; } }

		public ModelBuilder() {
		}

		void AddVertex() {
			foreach (var channel in Channels)
				channel.AddVertex();
		}

		public int AddVertex(Vector3 position) { AddVertex(); positionChannel.SetLast((Vector3f)position.InMetres); return positionChannel.Count - 1; }
		public int AddVertex(Vector3f position) { AddVertex(); positionChannel.SetLast((BVector3)position); return positionChannel.Count - 1; }
		public int AddVertex(Vector3d position) { return AddVertex((BVector3)position); }

		public void SetColor(Vector4rgba value) { colorChannel.SetLast(ref value); }
		public void SetColor(Vector3f value) { SetColor(new Vector4rgba(value.X, value.Y, value.Z, 1)); }
		public void SetColor(Vector3d value) { SetColor((BVector3)value); }
		public void SetNormal(Vector3f value) { normalChannel.SetLast((BVector3)value); }
		public void SetNormal(Vector3d value) { SetNormal((BVector3)value); }
		public void SetNormal(double x, double y, double z) { normalChannel.SetLast(new BVector3((Scalar)x, (Scalar)y, (Scalar)z)); }
		public void SetTexel(Vector2f value, int channel = 0) { texelChannels.SetLast((BVector2)value, channel); }
		public void SetTexel(Vector2d value, int channel = 0) { SetTexel((BVector2)value, channel); }

		public void SetBoneIndices(Vector4i value, int channel = 0) { boneIndicesChannels.SetLast(value, channel); }
		public void SetBoneWeights(Vector4f value, int channel = 0) { boneWeightsChannels.SetLast(value, channel); }

		void CheckIndexList(int count) {
			if (indexCount + count > indices.Length) {
				var newList = new int[(indexCount + count + 1) * 3 / 2];
				indices.CopyTo(newList, 0);
				indices = newList;
			}
		}

		public void AddIndex(ushort index) { AddIndex(index == 0xFFFF ? -1 : index); }

		public void AddIndex(int index) { CheckIndexList(1); indices[indexCount++] = index; }
		public void AddIndices(int a, int b) { CheckIndexList(2); indices[indexCount + 0] = a; indices[indexCount + 1] = b; indexCount += 2; }
		public void AddIndices(int a, int b, int c) { CheckIndexList(3); indices[indexCount + 0] = a; indices[indexCount + 1] = b; indices[indexCount + 2] = c; indexCount += 3; }
		public void AddIndices(int a, int b, int c, int d) { CheckIndexList(4); indices[indexCount + 0] = a; indices[indexCount + 1] = b; indices[indexCount + 2] = c; indices[indexCount + 3] = d; indexCount += 4; }
		public void AddIndices(int a, int b, int c, int d, int e) { CheckIndexList(5); indices[indexCount + 0] = a; indices[indexCount + 1] = b; indices[indexCount + 2] = c; indices[indexCount + 3] = d; indices[indexCount + 4] = e; indexCount += 5; }
		public void AddIndices(int a, int b, int c, int d, int e, int f) { CheckIndexList(6); indices[indexCount + 0] = a; indices[indexCount + 1] = b; indices[indexCount + 2] = c; indices[indexCount + 3] = d; indices[indexCount + 4] = e; indices[indexCount + 5] = f; indexCount += 6; }
		public void AddIndices(params int[] indices) { AddIndices((IList<int>)indices); }

		public void AddIndices(IList<int> indices) {
			CheckIndexList(indices.Count);
			indices.CopyTo(this.indices, indexCount);
			indexCount += indices.Count;
		}

		struct SphereBuilder {
			public Vector3d Centre;
			public double RadiusSquared;
			public int CentreTotal;

			public double Radius { get { return Math.Sqrt(RadiusSquared); } }

			public Sphere3d Sphere { get { return new Sphere3d(ref Centre, Radius); } }

			public void AddCentrePoint(ref BVector3 point) {
				Centre += point;
				CentreTotal++;
			}

			public void CalculateCentre() {
				Centre /= CentreTotal;
			}

			public void AddRadiusPoint(ref BVector3 point) {
				RadiusSquared = Math.Max(RadiusSquared, ((Vector3d)point).DistanceSquared(Centre));
			}
		}

		public Model Finish() {
			GraphicsBuffer buffer = new GraphicsBuffer();
			SphereBuilder modelBounds = new SphereBuilder();

			foreach (MeshInfo meshInfo in meshes)
				meshInfo.AddCentrePoints(ref modelBounds, positionChannel, indices);
			modelBounds.CalculateCentre();
			foreach (MeshInfo meshInfo in meshes)
				meshInfo.AddRadiusPoints(ref modelBounds, positionChannel, indices);

			Model model = new Model(buffer, ElementType.UInt32, modelBounds.Sphere);

			// Put the bones in the model.
			foreach (var mesh in meshes)
				if (mesh.Bone != null && mesh.Bone.model == null)
					model.Bones.Add(mesh.Bone);

			// Create the meshes and put them in the model.
			foreach (MeshInfo meshInfo in meshes) {
				SphereBuilder meshSphere = new SphereBuilder();

				meshInfo.AddCentrePoints(ref meshSphere, positionChannel, indices);
				meshSphere.CalculateCentre();
				meshInfo.AddRadiusPoints(ref meshSphere, positionChannel, indices);

				ModelMesh mesh = new ModelMesh(meshSphere.Sphere, meshInfo.Bone, meshInfo.Parts);
				model.Meshes.Add(mesh);
			}

			// Calculate the total size of the buffer.
			int bufferSize = indexCount * 4;
			foreach (var channel in Channels)
				channel.CheckSize(ref bufferSize);

			// Write the data into the buffer.
			byte[] bufferData = new byte[bufferSize];
			int bufferOffset = indexCount * 4;
			indices.CopyTo(0, indexCount, bufferData, 0);
			foreach (var channel in Channels)
				channel.Write(bufferData, ref bufferOffset, model, buffer);
			buffer.Data(bufferData);

			return model;
		}

		/// <summary>Finish the current mesh, if any, and start a new one.</summary>
		/// <param name="bone">The bone to use, or <c>null</c> to use identity.</param>
		/// <returns></returns>
		public ModelBuilder FinishMesh(ModelBone bone = null) {
			meshes.Add(new MeshInfo(bone));
			firstIndex = indexCount;
			return this;
		}

		/// <summary>Finish a part of the current mesh of the model.</summary>
		/// <param name="primitive"></param>
		/// <param name="material"></param>
		/// <returns></returns>
		public ModelPart FinishPart(Primitive primitive, ModelMaterial material) {
			int myFirstIndex = firstIndex;
			if (meshes.Count == 0)
				FinishMesh(null);
			ModelPart part = new ModelPart(primitive, myFirstIndex * 4, indexCount - myFirstIndex, material);
			firstIndex = indexCount;
			meshes[meshes.Count - 1].Parts.Add(part);
			return part;
		}
	}
}
