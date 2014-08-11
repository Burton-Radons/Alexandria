using System;
using System.Collections.Generic;
using System.IO;

namespace Glare {
	public static partial class ExtensionMethods {
		#region BinaryReader

					/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2</c> values.</summary>
			public static Vector2[] ReadArrayVector2(this BinaryReader reader, int count) { Vector2[] array = new Vector2[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2"/>.</summary>
				public static Vector2 ReadVector2(this BinaryReader reader ) {
					Vector2 result;
										result.X = reader.ReadLength();
										result.Y = reader.ReadLength();
					return result;
				}
							/// <summary>Read a <see cref="Vector2"/>.</summary>
				public static void ReadVector2(this BinaryReader reader , out Vector2 result) {
					
										result.X = reader.ReadLength();
										result.Y = reader.ReadLength();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2f> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2f(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2f> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2f</c> values.</summary>
			public static Vector2f[] ReadArrayVector2f(this BinaryReader reader, int count) { Vector2f[] array = new Vector2f[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2f"/>.</summary>
				public static Vector2f ReadVector2f(this BinaryReader reader ) {
					Vector2f result;
										result.X = reader.ReadSingle();
										result.Y = reader.ReadSingle();
					return result;
				}
							/// <summary>Read a <see cref="Vector2f"/>.</summary>
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

							/// <summary>Read a <see cref="Vector2d"/>.</summary>
				public static Vector2d ReadVector2d(this BinaryReader reader ) {
					Vector2d result;
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
					return result;
				}
							/// <summary>Read a <see cref="Vector2d"/>.</summary>
				public static void ReadVector2d(this BinaryReader reader , out Vector2d result) {
					
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2s> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2s(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2s> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2s</c> values.</summary>
			public static Vector2s[] ReadArrayVector2s(this BinaryReader reader, int count) { Vector2s[] array = new Vector2s[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2s"/>.</summary>
				public static Vector2s ReadVector2s(this BinaryReader reader ) {
					Vector2s result;
										result.X = reader.ReadInt16();
										result.Y = reader.ReadInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector2s"/>.</summary>
				public static void ReadVector2s(this BinaryReader reader , out Vector2s result) {
					
										result.X = reader.ReadInt16();
										result.Y = reader.ReadInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2us> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2us(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2us> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2us</c> values.</summary>
			public static Vector2us[] ReadArrayVector2us(this BinaryReader reader, int count) { Vector2us[] array = new Vector2us[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2us"/>.</summary>
				public static Vector2us ReadVector2us(this BinaryReader reader ) {
					Vector2us result;
										result.X = reader.ReadUInt16();
										result.Y = reader.ReadUInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector2us"/>.</summary>
				public static void ReadVector2us(this BinaryReader reader , out Vector2us result) {
					
										result.X = reader.ReadUInt16();
										result.Y = reader.ReadUInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2i> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2i(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2i> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2i</c> values.</summary>
			public static Vector2i[] ReadArrayVector2i(this BinaryReader reader, int count) { Vector2i[] array = new Vector2i[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2i"/>.</summary>
				public static Vector2i ReadVector2i(this BinaryReader reader ) {
					Vector2i result;
										result.X = reader.ReadInt32();
										result.Y = reader.ReadInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector2i"/>.</summary>
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

							/// <summary>Read a <see cref="Vector2ui"/>.</summary>
				public static Vector2ui ReadVector2ui(this BinaryReader reader ) {
					Vector2ui result;
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector2ui"/>.</summary>
				public static void ReadVector2ui(this BinaryReader reader , out Vector2ui result) {
					
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2b> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2b(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2b> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2b</c> values.</summary>
			public static Vector2b[] ReadArrayVector2b(this BinaryReader reader, int count) { Vector2b[] array = new Vector2b[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2b"/>.</summary>
				public static Vector2b ReadVector2b(this BinaryReader reader ) {
					Vector2b result;
										result.X = reader.ReadByte();
										result.Y = reader.ReadByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector2b"/>.</summary>
				public static void ReadVector2b(this BinaryReader reader , out Vector2b result) {
					
										result.X = reader.ReadByte();
										result.Y = reader.ReadByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2nb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2nb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2nb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2nb</c> values.</summary>
			public static Vector2nb[] ReadArrayVector2nb(this BinaryReader reader, int count) { Vector2nb[] array = new Vector2nb[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2nb"/>.</summary>
				public static Vector2nb ReadVector2nb(this BinaryReader reader ) {
					Vector2nb result;
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector2nb"/>.</summary>
				public static void ReadVector2nb(this BinaryReader reader , out Vector2nb result) {
					
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2nsb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2nsb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2nsb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2nsb</c> values.</summary>
			public static Vector2nsb[] ReadArrayVector2nsb(this BinaryReader reader, int count) { Vector2nsb[] array = new Vector2nsb[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2nsb"/>.</summary>
				public static Vector2nsb ReadVector2nsb(this BinaryReader reader ) {
					Vector2nsb result;
										result.X = reader.ReadNormalizedSByte();
										result.Y = reader.ReadNormalizedSByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector2nsb"/>.</summary>
				public static void ReadVector2nsb(this BinaryReader reader , out Vector2nsb result) {
					
										result.X = reader.ReadNormalizedSByte();
										result.Y = reader.ReadNormalizedSByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2ns> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2ns(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2ns> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2ns</c> values.</summary>
			public static Vector2ns[] ReadArrayVector2ns(this BinaryReader reader, int count) { Vector2ns[] array = new Vector2ns[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2ns"/>.</summary>
				public static Vector2ns ReadVector2ns(this BinaryReader reader ) {
					Vector2ns result;
										result.X = reader.ReadNormalizedInt16();
										result.Y = reader.ReadNormalizedInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector2ns"/>.</summary>
				public static void ReadVector2ns(this BinaryReader reader , out Vector2ns result) {
					
										result.X = reader.ReadNormalizedInt16();
										result.Y = reader.ReadNormalizedInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2ni> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2ni(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2ni> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2ni</c> values.</summary>
			public static Vector2ni[] ReadArrayVector2ni(this BinaryReader reader, int count) { Vector2ni[] array = new Vector2ni[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2ni"/>.</summary>
				public static Vector2ni ReadVector2ni(this BinaryReader reader ) {
					Vector2ni result;
										result.X = reader.ReadNormalizedInt32();
										result.Y = reader.ReadNormalizedInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector2ni"/>.</summary>
				public static void ReadVector2ni(this BinaryReader reader , out Vector2ni result) {
					
										result.X = reader.ReadNormalizedInt32();
										result.Y = reader.ReadNormalizedInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2nus> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2nus(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2nus> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2nus</c> values.</summary>
			public static Vector2nus[] ReadArrayVector2nus(this BinaryReader reader, int count) { Vector2nus[] array = new Vector2nus[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2nus"/>.</summary>
				public static Vector2nus ReadVector2nus(this BinaryReader reader ) {
					Vector2nus result;
										result.X = reader.ReadNormalizedUInt16();
										result.Y = reader.ReadNormalizedUInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector2nus"/>.</summary>
				public static void ReadVector2nus(this BinaryReader reader , out Vector2nus result) {
					
										result.X = reader.ReadNormalizedUInt16();
										result.Y = reader.ReadNormalizedUInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2nui> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2nui(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2nui> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2nui</c> values.</summary>
			public static Vector2nui[] ReadArrayVector2nui(this BinaryReader reader, int count) { Vector2nui[] array = new Vector2nui[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2nui"/>.</summary>
				public static Vector2nui ReadVector2nui(this BinaryReader reader ) {
					Vector2nui result;
										result.X = reader.ReadNormalizedUInt32();
										result.Y = reader.ReadNormalizedUInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector2nui"/>.</summary>
				public static void ReadVector2nui(this BinaryReader reader , out Vector2nui result) {
					
										result.X = reader.ReadNormalizedUInt32();
										result.Y = reader.ReadNormalizedUInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2h> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2h(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2h> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2h</c> values.</summary>
			public static Vector2h[] ReadArrayVector2h(this BinaryReader reader, int count) { Vector2h[] array = new Vector2h[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2h"/>.</summary>
				public static Vector2h ReadVector2h(this BinaryReader reader ) {
					Vector2h result;
										result.X = reader.ReadFloat16();
										result.Y = reader.ReadFloat16();
					return result;
				}
							/// <summary>Read a <see cref="Vector2h"/>.</summary>
				public static void ReadVector2h(this BinaryReader reader , out Vector2h result) {
					
										result.X = reader.ReadFloat16();
										result.Y = reader.ReadFloat16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2sb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector2sb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector2sb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector2sb</c> values.</summary>
			public static Vector2sb[] ReadArrayVector2sb(this BinaryReader reader, int count) { Vector2sb[] array = new Vector2sb[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector2sb"/>.</summary>
				public static Vector2sb ReadVector2sb(this BinaryReader reader ) {
					Vector2sb result;
										result.X = reader.ReadSByte();
										result.Y = reader.ReadSByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector2sb"/>.</summary>
				public static void ReadVector2sb(this BinaryReader reader , out Vector2sb result) {
					
										result.X = reader.ReadSByte();
										result.Y = reader.ReadSByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3</c> values.</summary>
			public static Vector3[] ReadArrayVector3(this BinaryReader reader, int count) { Vector3[] array = new Vector3[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3"/>.</summary>
				public static Vector3 ReadVector3(this BinaryReader reader ) {
					Vector3 result;
										result.X = reader.ReadLength();
										result.Y = reader.ReadLength();
										result.Z = reader.ReadLength();
					return result;
				}
							/// <summary>Read a <see cref="Vector3"/>.</summary>
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

							/// <summary>Read a <see cref="Vector3f"/>.</summary>
				public static Vector3f ReadVector3f(this BinaryReader reader ) {
					Vector3f result;
										result.X = reader.ReadSingle();
										result.Y = reader.ReadSingle();
										result.Z = reader.ReadSingle();
					return result;
				}
							/// <summary>Read a <see cref="Vector3f"/>.</summary>
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

							/// <summary>Read a <see cref="Vector3d"/>.</summary>
				public static Vector3d ReadVector3d(this BinaryReader reader ) {
					Vector3d result;
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
										result.Z = reader.ReadDouble();
					return result;
				}
							/// <summary>Read a <see cref="Vector3d"/>.</summary>
				public static void ReadVector3d(this BinaryReader reader , out Vector3d result) {
					
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
										result.Z = reader.ReadDouble();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3s> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3s(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3s> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3s</c> values.</summary>
			public static Vector3s[] ReadArrayVector3s(this BinaryReader reader, int count) { Vector3s[] array = new Vector3s[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3s"/>.</summary>
				public static Vector3s ReadVector3s(this BinaryReader reader ) {
					Vector3s result;
										result.X = reader.ReadInt16();
										result.Y = reader.ReadInt16();
										result.Z = reader.ReadInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector3s"/>.</summary>
				public static void ReadVector3s(this BinaryReader reader , out Vector3s result) {
					
										result.X = reader.ReadInt16();
										result.Y = reader.ReadInt16();
										result.Z = reader.ReadInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3us> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3us(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3us> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3us</c> values.</summary>
			public static Vector3us[] ReadArrayVector3us(this BinaryReader reader, int count) { Vector3us[] array = new Vector3us[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3us"/>.</summary>
				public static Vector3us ReadVector3us(this BinaryReader reader ) {
					Vector3us result;
										result.X = reader.ReadUInt16();
										result.Y = reader.ReadUInt16();
										result.Z = reader.ReadUInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector3us"/>.</summary>
				public static void ReadVector3us(this BinaryReader reader , out Vector3us result) {
					
										result.X = reader.ReadUInt16();
										result.Y = reader.ReadUInt16();
										result.Z = reader.ReadUInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3i> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3i(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3i> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3i</c> values.</summary>
			public static Vector3i[] ReadArrayVector3i(this BinaryReader reader, int count) { Vector3i[] array = new Vector3i[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3i"/>.</summary>
				public static Vector3i ReadVector3i(this BinaryReader reader ) {
					Vector3i result;
										result.X = reader.ReadInt32();
										result.Y = reader.ReadInt32();
										result.Z = reader.ReadInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector3i"/>.</summary>
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

							/// <summary>Read a <see cref="Vector3ui"/>.</summary>
				public static Vector3ui ReadVector3ui(this BinaryReader reader ) {
					Vector3ui result;
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
										result.Z = reader.ReadUInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector3ui"/>.</summary>
				public static void ReadVector3ui(this BinaryReader reader , out Vector3ui result) {
					
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
										result.Z = reader.ReadUInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3b> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3b(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3b> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3b</c> values.</summary>
			public static Vector3b[] ReadArrayVector3b(this BinaryReader reader, int count) { Vector3b[] array = new Vector3b[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3b"/>.</summary>
				public static Vector3b ReadVector3b(this BinaryReader reader ) {
					Vector3b result;
										result.X = reader.ReadByte();
										result.Y = reader.ReadByte();
										result.Z = reader.ReadByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector3b"/>.</summary>
				public static void ReadVector3b(this BinaryReader reader , out Vector3b result) {
					
										result.X = reader.ReadByte();
										result.Y = reader.ReadByte();
										result.Z = reader.ReadByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3nb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3nb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3nb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3nb</c> values.</summary>
			public static Vector3nb[] ReadArrayVector3nb(this BinaryReader reader, int count) { Vector3nb[] array = new Vector3nb[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3nb"/>.</summary>
				public static Vector3nb ReadVector3nb(this BinaryReader reader ) {
					Vector3nb result;
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector3nb"/>.</summary>
				public static void ReadVector3nb(this BinaryReader reader , out Vector3nb result) {
					
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3nsb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3nsb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3nsb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3nsb</c> values.</summary>
			public static Vector3nsb[] ReadArrayVector3nsb(this BinaryReader reader, int count) { Vector3nsb[] array = new Vector3nsb[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3nsb"/>.</summary>
				public static Vector3nsb ReadVector3nsb(this BinaryReader reader ) {
					Vector3nsb result;
										result.X = reader.ReadNormalizedSByte();
										result.Y = reader.ReadNormalizedSByte();
										result.Z = reader.ReadNormalizedSByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector3nsb"/>.</summary>
				public static void ReadVector3nsb(this BinaryReader reader , out Vector3nsb result) {
					
										result.X = reader.ReadNormalizedSByte();
										result.Y = reader.ReadNormalizedSByte();
										result.Z = reader.ReadNormalizedSByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3ns> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3ns(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3ns> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3ns</c> values.</summary>
			public static Vector3ns[] ReadArrayVector3ns(this BinaryReader reader, int count) { Vector3ns[] array = new Vector3ns[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3ns"/>.</summary>
				public static Vector3ns ReadVector3ns(this BinaryReader reader ) {
					Vector3ns result;
										result.X = reader.ReadNormalizedInt16();
										result.Y = reader.ReadNormalizedInt16();
										result.Z = reader.ReadNormalizedInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector3ns"/>.</summary>
				public static void ReadVector3ns(this BinaryReader reader , out Vector3ns result) {
					
										result.X = reader.ReadNormalizedInt16();
										result.Y = reader.ReadNormalizedInt16();
										result.Z = reader.ReadNormalizedInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3ni> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3ni(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3ni> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3ni</c> values.</summary>
			public static Vector3ni[] ReadArrayVector3ni(this BinaryReader reader, int count) { Vector3ni[] array = new Vector3ni[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3ni"/>.</summary>
				public static Vector3ni ReadVector3ni(this BinaryReader reader ) {
					Vector3ni result;
										result.X = reader.ReadNormalizedInt32();
										result.Y = reader.ReadNormalizedInt32();
										result.Z = reader.ReadNormalizedInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector3ni"/>.</summary>
				public static void ReadVector3ni(this BinaryReader reader , out Vector3ni result) {
					
										result.X = reader.ReadNormalizedInt32();
										result.Y = reader.ReadNormalizedInt32();
										result.Z = reader.ReadNormalizedInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3nus> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3nus(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3nus> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3nus</c> values.</summary>
			public static Vector3nus[] ReadArrayVector3nus(this BinaryReader reader, int count) { Vector3nus[] array = new Vector3nus[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3nus"/>.</summary>
				public static Vector3nus ReadVector3nus(this BinaryReader reader ) {
					Vector3nus result;
										result.X = reader.ReadNormalizedUInt16();
										result.Y = reader.ReadNormalizedUInt16();
										result.Z = reader.ReadNormalizedUInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector3nus"/>.</summary>
				public static void ReadVector3nus(this BinaryReader reader , out Vector3nus result) {
					
										result.X = reader.ReadNormalizedUInt16();
										result.Y = reader.ReadNormalizedUInt16();
										result.Z = reader.ReadNormalizedUInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3nui> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3nui(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3nui> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3nui</c> values.</summary>
			public static Vector3nui[] ReadArrayVector3nui(this BinaryReader reader, int count) { Vector3nui[] array = new Vector3nui[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3nui"/>.</summary>
				public static Vector3nui ReadVector3nui(this BinaryReader reader ) {
					Vector3nui result;
										result.X = reader.ReadNormalizedUInt32();
										result.Y = reader.ReadNormalizedUInt32();
										result.Z = reader.ReadNormalizedUInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector3nui"/>.</summary>
				public static void ReadVector3nui(this BinaryReader reader , out Vector3nui result) {
					
										result.X = reader.ReadNormalizedUInt32();
										result.Y = reader.ReadNormalizedUInt32();
										result.Z = reader.ReadNormalizedUInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3rgb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3rgb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3rgb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3rgb</c> values.</summary>
			public static Vector3rgb[] ReadArrayVector3rgb(this BinaryReader reader, int count) { Vector3rgb[] array = new Vector3rgb[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3rgb"/>.</summary>
				public static Vector3rgb ReadVector3rgb(this BinaryReader reader ) {
					Vector3rgb result;
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector3rgb"/>.</summary>
				public static void ReadVector3rgb(this BinaryReader reader , out Vector3rgb result) {
					
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3h> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3h(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3h> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3h</c> values.</summary>
			public static Vector3h[] ReadArrayVector3h(this BinaryReader reader, int count) { Vector3h[] array = new Vector3h[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3h"/>.</summary>
				public static Vector3h ReadVector3h(this BinaryReader reader ) {
					Vector3h result;
										result.X = reader.ReadFloat16();
										result.Y = reader.ReadFloat16();
										result.Z = reader.ReadFloat16();
					return result;
				}
							/// <summary>Read a <see cref="Vector3h"/>.</summary>
				public static void ReadVector3h(this BinaryReader reader , out Vector3h result) {
					
										result.X = reader.ReadFloat16();
										result.Y = reader.ReadFloat16();
										result.Z = reader.ReadFloat16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3sb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector3sb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector3sb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector3sb</c> values.</summary>
			public static Vector3sb[] ReadArrayVector3sb(this BinaryReader reader, int count) { Vector3sb[] array = new Vector3sb[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector3sb"/>.</summary>
				public static Vector3sb ReadVector3sb(this BinaryReader reader ) {
					Vector3sb result;
										result.X = reader.ReadSByte();
										result.Y = reader.ReadSByte();
										result.Z = reader.ReadSByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector3sb"/>.</summary>
				public static void ReadVector3sb(this BinaryReader reader , out Vector3sb result) {
					
										result.X = reader.ReadSByte();
										result.Y = reader.ReadSByte();
										result.Z = reader.ReadSByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4</c> values.</summary>
			public static Vector4[] ReadArrayVector4(this BinaryReader reader, int count) { Vector4[] array = new Vector4[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4"/>.</summary>
				public static Vector4 ReadVector4(this BinaryReader reader ) {
					Vector4 result;
										result.X = reader.ReadLength();
										result.Y = reader.ReadLength();
										result.Z = reader.ReadLength();
										result.W = reader.ReadLength();
					return result;
				}
							/// <summary>Read a <see cref="Vector4"/>.</summary>
				public static void ReadVector4(this BinaryReader reader , out Vector4 result) {
					
										result.X = reader.ReadLength();
										result.Y = reader.ReadLength();
										result.Z = reader.ReadLength();
										result.W = reader.ReadLength();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4f> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4f(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4f> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4f</c> values.</summary>
			public static Vector4f[] ReadArrayVector4f(this BinaryReader reader, int count) { Vector4f[] array = new Vector4f[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4f"/>.</summary>
				public static Vector4f ReadVector4f(this BinaryReader reader ) {
					Vector4f result;
										result.X = reader.ReadSingle();
										result.Y = reader.ReadSingle();
										result.Z = reader.ReadSingle();
										result.W = reader.ReadSingle();
					return result;
				}
							/// <summary>Read a <see cref="Vector4f"/>.</summary>
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

							/// <summary>Read a <see cref="Vector4d"/>.</summary>
				public static Vector4d ReadVector4d(this BinaryReader reader ) {
					Vector4d result;
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
										result.Z = reader.ReadDouble();
										result.W = reader.ReadDouble();
					return result;
				}
							/// <summary>Read a <see cref="Vector4d"/>.</summary>
				public static void ReadVector4d(this BinaryReader reader , out Vector4d result) {
					
										result.X = reader.ReadDouble();
										result.Y = reader.ReadDouble();
										result.Z = reader.ReadDouble();
										result.W = reader.ReadDouble();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4s> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4s(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4s> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4s</c> values.</summary>
			public static Vector4s[] ReadArrayVector4s(this BinaryReader reader, int count) { Vector4s[] array = new Vector4s[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4s"/>.</summary>
				public static Vector4s ReadVector4s(this BinaryReader reader ) {
					Vector4s result;
										result.X = reader.ReadInt16();
										result.Y = reader.ReadInt16();
										result.Z = reader.ReadInt16();
										result.W = reader.ReadInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector4s"/>.</summary>
				public static void ReadVector4s(this BinaryReader reader , out Vector4s result) {
					
										result.X = reader.ReadInt16();
										result.Y = reader.ReadInt16();
										result.Z = reader.ReadInt16();
										result.W = reader.ReadInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4us> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4us(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4us> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4us</c> values.</summary>
			public static Vector4us[] ReadArrayVector4us(this BinaryReader reader, int count) { Vector4us[] array = new Vector4us[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4us"/>.</summary>
				public static Vector4us ReadVector4us(this BinaryReader reader ) {
					Vector4us result;
										result.X = reader.ReadUInt16();
										result.Y = reader.ReadUInt16();
										result.Z = reader.ReadUInt16();
										result.W = reader.ReadUInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector4us"/>.</summary>
				public static void ReadVector4us(this BinaryReader reader , out Vector4us result) {
					
										result.X = reader.ReadUInt16();
										result.Y = reader.ReadUInt16();
										result.Z = reader.ReadUInt16();
										result.W = reader.ReadUInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4i> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4i(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4i> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4i</c> values.</summary>
			public static Vector4i[] ReadArrayVector4i(this BinaryReader reader, int count) { Vector4i[] array = new Vector4i[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4i"/>.</summary>
				public static Vector4i ReadVector4i(this BinaryReader reader ) {
					Vector4i result;
										result.X = reader.ReadInt32();
										result.Y = reader.ReadInt32();
										result.Z = reader.ReadInt32();
										result.W = reader.ReadInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector4i"/>.</summary>
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

							/// <summary>Read a <see cref="Vector4ui"/>.</summary>
				public static Vector4ui ReadVector4ui(this BinaryReader reader ) {
					Vector4ui result;
										result.X = reader.ReadUInt32();
										result.Y = reader.ReadUInt32();
										result.Z = reader.ReadUInt32();
										result.W = reader.ReadUInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector4ui"/>.</summary>
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

							/// <summary>Read a <see cref="Vector4b"/>.</summary>
				public static Vector4b ReadVector4b(this BinaryReader reader ) {
					Vector4b result;
										result.X = reader.ReadByte();
										result.Y = reader.ReadByte();
										result.Z = reader.ReadByte();
										result.W = reader.ReadByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector4b"/>.</summary>
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

							/// <summary>Read a <see cref="Vector4nb"/>.</summary>
				public static Vector4nb ReadVector4nb(this BinaryReader reader ) {
					Vector4nb result;
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
										result.W = reader.ReadNormalizedByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector4nb"/>.</summary>
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

							/// <summary>Read a <see cref="Vector4nsb"/>.</summary>
				public static Vector4nsb ReadVector4nsb(this BinaryReader reader ) {
					Vector4nsb result;
										result.X = reader.ReadNormalizedSByte();
										result.Y = reader.ReadNormalizedSByte();
										result.Z = reader.ReadNormalizedSByte();
										result.W = reader.ReadNormalizedSByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector4nsb"/>.</summary>
				public static void ReadVector4nsb(this BinaryReader reader , out Vector4nsb result) {
					
										result.X = reader.ReadNormalizedSByte();
										result.Y = reader.ReadNormalizedSByte();
										result.Z = reader.ReadNormalizedSByte();
										result.W = reader.ReadNormalizedSByte();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4ns> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4ns(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4ns> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4ns</c> values.</summary>
			public static Vector4ns[] ReadArrayVector4ns(this BinaryReader reader, int count) { Vector4ns[] array = new Vector4ns[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4ns"/>.</summary>
				public static Vector4ns ReadVector4ns(this BinaryReader reader ) {
					Vector4ns result;
										result.X = reader.ReadNormalizedInt16();
										result.Y = reader.ReadNormalizedInt16();
										result.Z = reader.ReadNormalizedInt16();
										result.W = reader.ReadNormalizedInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector4ns"/>.</summary>
				public static void ReadVector4ns(this BinaryReader reader , out Vector4ns result) {
					
										result.X = reader.ReadNormalizedInt16();
										result.Y = reader.ReadNormalizedInt16();
										result.Z = reader.ReadNormalizedInt16();
										result.W = reader.ReadNormalizedInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4ni> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4ni(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4ni> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4ni</c> values.</summary>
			public static Vector4ni[] ReadArrayVector4ni(this BinaryReader reader, int count) { Vector4ni[] array = new Vector4ni[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4ni"/>.</summary>
				public static Vector4ni ReadVector4ni(this BinaryReader reader ) {
					Vector4ni result;
										result.X = reader.ReadNormalizedInt32();
										result.Y = reader.ReadNormalizedInt32();
										result.Z = reader.ReadNormalizedInt32();
										result.W = reader.ReadNormalizedInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector4ni"/>.</summary>
				public static void ReadVector4ni(this BinaryReader reader , out Vector4ni result) {
					
										result.X = reader.ReadNormalizedInt32();
										result.Y = reader.ReadNormalizedInt32();
										result.Z = reader.ReadNormalizedInt32();
										result.W = reader.ReadNormalizedInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4nus> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4nus(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4nus> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4nus</c> values.</summary>
			public static Vector4nus[] ReadArrayVector4nus(this BinaryReader reader, int count) { Vector4nus[] array = new Vector4nus[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4nus"/>.</summary>
				public static Vector4nus ReadVector4nus(this BinaryReader reader ) {
					Vector4nus result;
										result.X = reader.ReadNormalizedUInt16();
										result.Y = reader.ReadNormalizedUInt16();
										result.Z = reader.ReadNormalizedUInt16();
										result.W = reader.ReadNormalizedUInt16();
					return result;
				}
							/// <summary>Read a <see cref="Vector4nus"/>.</summary>
				public static void ReadVector4nus(this BinaryReader reader , out Vector4nus result) {
					
										result.X = reader.ReadNormalizedUInt16();
										result.Y = reader.ReadNormalizedUInt16();
										result.Z = reader.ReadNormalizedUInt16();
										result.W = reader.ReadNormalizedUInt16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4nui> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4nui(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4nui> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4nui</c> values.</summary>
			public static Vector4nui[] ReadArrayVector4nui(this BinaryReader reader, int count) { Vector4nui[] array = new Vector4nui[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4nui"/>.</summary>
				public static Vector4nui ReadVector4nui(this BinaryReader reader ) {
					Vector4nui result;
										result.X = reader.ReadNormalizedUInt32();
										result.Y = reader.ReadNormalizedUInt32();
										result.Z = reader.ReadNormalizedUInt32();
										result.W = reader.ReadNormalizedUInt32();
					return result;
				}
							/// <summary>Read a <see cref="Vector4nui"/>.</summary>
				public static void ReadVector4nui(this BinaryReader reader , out Vector4nui result) {
					
										result.X = reader.ReadNormalizedUInt32();
										result.Y = reader.ReadNormalizedUInt32();
										result.Z = reader.ReadNormalizedUInt32();
										result.W = reader.ReadNormalizedUInt32();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4rgba> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4rgba(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4rgba> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4rgba</c> values.</summary>
			public static Vector4rgba[] ReadArrayVector4rgba(this BinaryReader reader, int count) { Vector4rgba[] array = new Vector4rgba[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4rgba"/>.</summary>
				public static Vector4rgba ReadVector4rgba(this BinaryReader reader ) {
					Vector4rgba result;
										result.X = reader.ReadNormalizedByte();
										result.Y = reader.ReadNormalizedByte();
										result.Z = reader.ReadNormalizedByte();
										result.W = reader.ReadNormalizedByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector4rgba"/>.</summary>
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

							/// <summary>Read a <see cref="Vector4h"/>.</summary>
				public static Vector4h ReadVector4h(this BinaryReader reader ) {
					Vector4h result;
										result.X = reader.ReadFloat16();
										result.Y = reader.ReadFloat16();
										result.Z = reader.ReadFloat16();
										result.W = reader.ReadFloat16();
					return result;
				}
							/// <summary>Read a <see cref="Vector4h"/>.</summary>
				public static void ReadVector4h(this BinaryReader reader , out Vector4h result) {
					
										result.X = reader.ReadFloat16();
										result.Y = reader.ReadFloat16();
										result.Z = reader.ReadFloat16();
										result.W = reader.ReadFloat16();
					return;
				}
								/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4sb> values, int start, int count) { for (int index = 0; index < count; index++) values[start + index] = reader.ReadVector4sb(); }

			/// <summary>Read a part of a list.</summary>
			public static void ReadArray(this BinaryReader reader, IList<Vector4sb> values) { reader.ReadArray(values, 0, values.Count); }

			/// <summary>Read an array of <c>Vector4sb</c> values.</summary>
			public static Vector4sb[] ReadArrayVector4sb(this BinaryReader reader, int count) { Vector4sb[] array = new Vector4sb[count]; reader.ReadArray(array, 0, count); return array; }

							/// <summary>Read a <see cref="Vector4sb"/>.</summary>
				public static Vector4sb ReadVector4sb(this BinaryReader reader ) {
					Vector4sb result;
										result.X = reader.ReadSByte();
										result.Y = reader.ReadSByte();
										result.Z = reader.ReadSByte();
										result.W = reader.ReadSByte();
					return result;
				}
							/// <summary>Read a <see cref="Vector4sb"/>.</summary>
				public static void ReadVector4sb(this BinaryReader reader , out Vector4sb result) {
					
										result.X = reader.ReadSByte();
										result.Y = reader.ReadSByte();
										result.Z = reader.ReadSByte();
										result.W = reader.ReadSByte();
					return;
				}
					
		#endregion BinaryReader
	}
}





