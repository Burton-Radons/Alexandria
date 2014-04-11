using System;
using System.IO;

namespace Glare {
	public static partial class ExtensionMethods {
		#region BinaryReader

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







