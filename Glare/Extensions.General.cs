using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
	/// <summary>
	/// Extension methods relating to the classes introduced in <see cref="Glare"/>. Glare has a more extensive extension methods set in <see cref="Glare.Internal"/>.
	/// </summary>
	public static partial class ExtensionMethods {
		#region Random

		public static Matrix4d NextTranslationMatrix4d(this Random self, double min, double max) { Matrix4d result; self.NextTranslationMatrix4d(min, max, out result); return result; }

		public static void NextTranslationMatrix4d(this Random self, double min, double max, out Matrix4d result) {
			Vector3d vmin, vmax;
			vmin.X = vmin.Y = vmin.Z = min;
			vmax.X = vmax.Y = vmax.Z = max;
			self.NextTranslationMatrix4d(ref vmin, ref vmax, out result);
		}

		public static void NextTranslationMatrix4d(this Random self, ref Vector3d min, ref Vector3d max, out Matrix4d result) {
			Vector3d amount;
			self.NextVector3dInRange(ref min, ref max, out amount);
			Matrix4d.Translate(ref amount, out result);
		}

		public static Matrix4d NextTranslationMatrix4d(this Random self, Vector3d min, Vector3d max) { Matrix4d result; self.NextTranslationMatrix4d(ref min, ref max, out result); return result; }

		public static void NextVector3dInRange(this Random self, ref Vector3d min, ref Vector3d max, out Vector3d result) {
			result.X = self.NextDoubleInRange(min.X, max.X);
			result.Y = self.NextDoubleInRange(min.Y, max.Y);
			result.Z = self.NextDoubleInRange(min.Z, max.Z);
		}

		public static Vector3d NextVector3dInRange(this Random self, Vector3d min, Vector3d max) { Vector3d result; self.NextVector3dInRange(ref min, ref max, out result); return result; }

		public static Vector4d NextVector4d(this Random self) { return new Vector4d(self.NextDouble(), self.NextDouble(), self.NextDouble(), self.NextDouble()); }

		#endregion Random

		public static Float16 ReadFloat16(this BinaryReader reader) { return Float16.CreateCoded(reader.ReadUInt16()); }
		public static Length ReadLength(this BinaryReader reader) { return Length.Metres(reader.ReadDouble()); }
		public static NormalizedByte ReadNormalizedByte(this BinaryReader reader) { return NormalizedByte.CreateCoded(reader.ReadByte()); }
		public static NormalizedSByte ReadNormalizedSByte(this BinaryReader reader) { return NormalizedSByte.CreateCoded(reader.ReadSByte()); }

		public static Plane3f ReadPlane3f(this BinaryReader reader) { return new Plane3f(reader.ReadVector3f(), reader.ReadSingle()); }

		public static Sphere3f ReadSphere3f(this BinaryReader reader) { return new Sphere3f(reader.ReadVector3f(), reader.ReadSingle()); }
	}
}
