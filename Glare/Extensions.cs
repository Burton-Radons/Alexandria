using System;
using System.Collections.Generic;
using System.IO;

namespace Glare {
	public static partial class ExtensionMethods {
		#region BinaryReader

					/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2f> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2f(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2f> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2f</c> values.</summary>
			public static Vector2f[] ReadArrayVector2f(this BinaryReader reader, int count) { Vector2f[] array = new Vector2f[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector2f ReadVector2f(this BinaryReader reader ) {
					Vector2f result;
										result.X = reader.ReadSingle();
										result.Y = reader.ReadSingle();
					return result;
				}
							public static void ReadVector2f(this BinaryReader reader , out Vector2f result) {
					
										result.X = reader.ReadSingle();
										result.Y = reader.ReadSingle();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2d> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2d(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2d> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2d</c> values.</summary>
			public static Vector2d[] ReadArrayVector2d(this BinaryReader reader, int count) { Vector2d[] array = new Vector2d[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector2d ReadVector2d(this BinaryReader reader ) {
					Vector2d result;
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
					return result;
				}
							public static void ReadVector2d(this BinaryReader reader , out Vector2d result) {
					
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2i> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2i(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2i> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2i</c> values.</summary>
			public static Vector2i[] ReadArrayVector2i(this BinaryReader reader, int count) { Vector2i[] array = new Vector2i[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector2i ReadVector2i(this BinaryReader reader ) {
					Vector2i result;
										result.X = reader.ReadInt32();
										result.Y = reader.ReadInt32();
					return result;
				}
							public static void ReadVector2i(this BinaryReader reader , out Vector2i result) {
					
										result.X = reader.ReadInt32();
										result.Y = reader.ReadInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2ui> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2ui(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2ui> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2ui</c> values.</summary>
			public static Vector2ui[] ReadArrayVector2ui(this BinaryReader reader, int count) { Vector2ui[] array = new Vector2ui[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector2ui ReadVector2ui(this BinaryReader reader ) {
					Vector2ui result;
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
					return result;
				}
							public static void ReadVector2ui(this BinaryReader reader , out Vector2ui result) {
					
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3</c> values.</summary>
			public static Vector3[] ReadArrayVector3(this BinaryReader reader, int count) { Vector3[] array = new Vector3[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector3 ReadVector3(this BinaryReader reader ) {
					Vector3 result;
										result.X = reader.ReadLength();
										result.Y = reader.ReadLength();
										result.Z = reader.ReadLength();
					return result;
				}
							public static void ReadVector3(this BinaryReader reader , out Vector3 result) {
					
										result.X = reader.ReadLength();
										result.Y = reader.ReadLength();
										result.Z = reader.ReadLength();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3f> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3f(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3f> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3f</c> values.</summary>
			public static Vector3f[] ReadArrayVector3f(this BinaryReader reader, int count) { Vector3f[] array = new Vector3f[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector3f ReadVector3f(this BinaryReader reader ) {
					Vector3f result;
										result.X = reader.ReadSingle();
										result.Y = reader.ReadSingle();
										result.Z = reader.ReadSingle();
					return result;
				}
							public static void ReadVector3f(this BinaryReader reader , out Vector3f result) {
					
										result.X = reader.ReadSingle();
										result.Y = reader.ReadSingle();
										result.Z = reader.ReadSingle();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3d> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3d(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3d> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3d</c> values.</summary>
			public static Vector3d[] ReadArrayVector3d(this BinaryReader reader, int count) { Vector3d[] array = new Vector3d[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector3d ReadVector3d(this BinaryReader reader ) {
					Vector3d result;
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
										result.Z = reader.ReadDouble();
					return result;
				}
							public static void ReadVector3d(this BinaryReader reader , out Vector3d result) {
					
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
										result.Z = reader.ReadDouble();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3i> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3i(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3i> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3i</c> values.</summary>
			public static Vector3i[] ReadArrayVector3i(this BinaryReader reader, int count) { Vector3i[] array = new Vector3i[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector3i ReadVector3i(this BinaryReader reader ) {
					Vector3i result;
										result.X = reader.ReadInt32();
										result.Y = reader.ReadInt32();
										result.Z = reader.ReadInt32();
					return result;
				}
							public static void ReadVector3i(this BinaryReader reader , out Vector3i result) {
					
										result.X = reader.ReadInt32();
										result.Y = reader.ReadInt32();
										result.Z = reader.ReadInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3ui> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3ui(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3ui> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3ui</c> values.</summary>
			public static Vector3ui[] ReadArrayVector3ui(this BinaryReader reader, int count) { Vector3ui[] array = new Vector3ui[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector3ui ReadVector3ui(this BinaryReader reader ) {
					Vector3ui result;
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
										result.Z = reader.ReadUInt32();
					return result;
				}
							public static void ReadVector3ui(this BinaryReader reader , out Vector3ui result) {
					
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
										result.Z = reader.ReadUInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3rgb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3rgb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3rgb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3rgb</c> values.</summary>
			public static Vector3rgb[] ReadArrayVector3rgb(this BinaryReader reader, int count) { Vector3rgb[] array = new Vector3rgb[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector3rgb ReadVector3rgb(this BinaryReader reader ) {
					Vector3rgb result;
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
					return result;
				}
							public static void ReadVector3rgb(this BinaryReader reader , out Vector3rgb result) {
					
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4f> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4f(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4f> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4f</c> values.</summary>
			public static Vector4f[] ReadArrayVector4f(this BinaryReader reader, int count) { Vector4f[] array = new Vector4f[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector4f ReadVector4f(this BinaryReader reader ) {
					Vector4f result;
										result.X = reader.ReadSingle();
										result.Y = reader.ReadSingle();
										result.Z = reader.ReadSingle();
										result.W = reader.ReadSingle();
					return result;
				}
							public static void ReadVector4f(this BinaryReader reader , out Vector4f result) {
					
										result.X = reader.ReadSingle();
										result.Y = reader.ReadSingle();
										result.Z = reader.ReadSingle();
										result.W = reader.ReadSingle();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4d> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4d(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4d> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4d</c> values.</summary>
			public static Vector4d[] ReadArrayVector4d(this BinaryReader reader, int count) { Vector4d[] array = new Vector4d[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector4d ReadVector4d(this BinaryReader reader ) {
					Vector4d result;
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
										result.Z = reader.ReadDouble();
										result.W = reader.ReadDouble();
					return result;
				}
							public static void ReadVector4d(this BinaryReader reader , out Vector4d result) {
					
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
										result.Z = reader.ReadDouble();
										result.W = reader.ReadDouble();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4i> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4i(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4i> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4i</c> values.</summary>
			public static Vector4i[] ReadArrayVector4i(this BinaryReader reader, int count) { Vector4i[] array = new Vector4i[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector4i ReadVector4i(this BinaryReader reader ) {
					Vector4i result;
										result.X = reader.ReadInt32();
										result.Y = reader.ReadInt32();
										result.Z = reader.ReadInt32();
										result.W = reader.ReadInt32();
					return result;
				}
							public static void ReadVector4i(this BinaryReader reader , out Vector4i result) {
					
										result.X = reader.ReadInt32();
										result.Y = reader.ReadInt32();
										result.Z = reader.ReadInt32();
										result.W = reader.ReadInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4ui> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4ui(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4ui> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4ui</c> values.</summary>
			public static Vector4ui[] ReadArrayVector4ui(this BinaryReader reader, int count) { Vector4ui[] array = new Vector4ui[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector4ui ReadVector4ui(this BinaryReader reader ) {
					Vector4ui result;
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
										result.Z = reader.ReadUInt32();
										result.W = reader.ReadUInt32();
					return result;
				}
							public static void ReadVector4ui(this BinaryReader reader , out Vector4ui result) {
					
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
										result.Z = reader.ReadUInt32();
										result.W = reader.ReadUInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4b> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4b(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4b> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4b</c> values.</summary>
			public static Vector4b[] ReadArrayVector4b(this BinaryReader reader, int count) { Vector4b[] array = new Vector4b[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector4b ReadVector4b(this BinaryReader reader ) {
					Vector4b result;
										result.X = reader.ReadByte();
										result.Y = reader.ReadByte();
										result.Z = reader.ReadByte();
										result.W = reader.ReadByte();
					return result;
				}
							public static void ReadVector4b(this BinaryReader reader , out Vector4b result) {
					
										result.X = reader.ReadByte();
										result.Y = reader.ReadByte();
										result.Z = reader.ReadByte();
										result.W = reader.ReadByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4nb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4nb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4nb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4nb</c> values.</summary>
			public static Vector4nb[] ReadArrayVector4nb(this BinaryReader reader, int count) { Vector4nb[] array = new Vector4nb[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector4nb ReadVector4nb(this BinaryReader reader ) {
					Vector4nb result;
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
										result.W = reader.ReadNormalizedByte();
					return result;
				}
							public static void ReadVector4nb(this BinaryReader reader , out Vector4nb result) {
					
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
										result.W = reader.ReadNormalizedByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4nsb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4nsb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4nsb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4nsb</c> values.</summary>
			public static Vector4nsb[] ReadArrayVector4nsb(this BinaryReader reader, int count) { Vector4nsb[] array = new Vector4nsb[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector4nsb ReadVector4nsb(this BinaryReader reader ) {
					Vector4nsb result;
										result.X = reader.ReadNormalizedSByte();
										result.Y = reader.ReadNormalizedSByte();
										result.Z = reader.ReadNormalizedSByte();
										result.W = reader.ReadNormalizedSByte();
					return result;
				}
							public static void ReadVector4nsb(this BinaryReader reader , out Vector4nsb result) {
					
										result.X = reader.ReadNormalizedSByte();
										result.Y = reader.ReadNormalizedSByte();
										result.Z = reader.ReadNormalizedSByte();
										result.W = reader.ReadNormalizedSByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4rgba> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4rgba(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4rgba> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4rgba</c> values.</summary>
			public static Vector4rgba[] ReadArrayVector4rgba(this BinaryReader reader, int count) { Vector4rgba[] array = new Vector4rgba[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector4rgba ReadVector4rgba(this BinaryReader reader ) {
					Vector4rgba result;
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
										result.W = reader.ReadNormalizedByte();
					return result;
				}
							public static void ReadVector4rgba(this BinaryReader reader , out Vector4rgba result) {
					
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
										result.W = reader.ReadNormalizedByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4h> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4h(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4h> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4h</c> values.</summary>
			public static Vector4h[] ReadArrayVector4h(this BinaryReader reader, int count) { Vector4h[] array = new Vector4h[count]; reader.ReadArray(array, 0, count); return array; }

							public static Vector4h ReadVector4h(this BinaryReader reader ) {
					Vector4h result;
										result.X = reader.ReadFloat16();
										result.Y = reader.ReadFloat16();
										result.Z = reader.ReadFloat16();
										result.W = reader.ReadFloat16();
					return result;
				}
							public static void ReadVector4h(this BinaryReader reader , out Vector4h result) {
					
										result.X = reader.ReadFloat16();
										result.Y = reader.ReadFloat16();
										result.Z = reader.ReadFloat16();
										result.W = reader.ReadFloat16();
					return;
				}
					
		#endregion BinaryReader
	}
}







