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

		/// <summary>Create a random translation matrix.</summary>
		/// <param name="self"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static Matrix4d NextTranslationMatrix4d(this Random self, double min, double max) { Matrix4d result; self.NextTranslationMatrix4d(min, max, out result); return result; }

		/// <summary>Create a random translation matrix.</summary>
		/// <param name="self"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <param name="result"></param>
		public static void NextTranslationMatrix4d(this Random self, double min, double max, out Matrix4d result) {
			Vector3d vmin, vmax;
			vmin.X = vmin.Y = vmin.Z = min;
			vmax.X = vmax.Y = vmax.Z = max;
			self.NextTranslationMatrix4d(ref vmin, ref vmax, out result);
		}

		/// <summary>Create a random translation matrix.</summary>
		/// <param name="self"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <param name="result"></param>
		public static void NextTranslationMatrix4d(this Random self, ref Vector3d min, ref Vector3d max, out Matrix4d result) {
			Vector3d amount;
			self.NextVector3dInRange(ref min, ref max, out amount);
			Matrix4d.Translate(ref amount, out result);
		}

		/// <summary>Create a random translation matrix.</summary>
		/// <param name="self"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static Matrix4d NextTranslationMatrix4d(this Random self, Vector3d min, Vector3d max) { Matrix4d result; self.NextTranslationMatrix4d(ref min, ref max, out result); return result; }

		/// <summary>Create a random vector.</summary>
		/// <param name="self"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <param name="result"></param>
		public static void NextVector3dInRange(this Random self, ref Vector3d min, ref Vector3d max, out Vector3d result) {
			result.X = self.NextDoubleInRange(min.X, max.X);
			result.Y = self.NextDoubleInRange(min.Y, max.Y);
			result.Z = self.NextDoubleInRange(min.Z, max.Z);
		}

		/// <summary>Create a random vector.</summary>
		/// <param name="self"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static Vector3d NextVector3dInRange(this Random self, Vector3d min, Vector3d max) { Vector3d result; self.NextVector3dInRange(ref min, ref max, out result); return result; }

		/// <summary>Create a random vector.</summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector4d NextVector4d(this Random self) { return new Vector4d(self.NextDouble(), self.NextDouble(), self.NextDouble(), self.NextDouble()); }

		#endregion Random

		/// <summary>Read a 16-bit float.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static Float16 ReadFloat16(this BinaryReader reader) { return Float16.CreateCoded(reader.ReadUInt16()); }

		/// <summary>Read a length.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static Length ReadLength(this BinaryReader reader) { return Length.Metres(reader.ReadDouble()); }

		/// <summary>Read a normalized byte.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static NormalizedByte ReadNormalizedByte(this BinaryReader reader) { return NormalizedByte.CreateCoded(reader.ReadByte()); }

		/// <summary>Read a <see cref="NormalizedSByte"/>.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static NormalizedSByte ReadNormalizedSByte(this BinaryReader reader) { return NormalizedSByte.CreateCoded(reader.ReadSByte()); }

		/// <summary>Read a <see cref="NormalizedInt16"/>.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static NormalizedInt16 ReadNormalizedInt16(this BinaryReader reader) { return NormalizedInt16.CreateCoded(reader.ReadInt16()); }

		/// <summary>Read a <see cref="NormalizedInt32"/>.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static NormalizedInt32 ReadNormalizedInt32(this BinaryReader reader) { return NormalizedInt32.CreateCoded(reader.ReadInt32()); }

		/// <summary>Read a <see cref="NormalizedUInt16"/>.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static NormalizedUInt16 ReadNormalizedUInt16(this BinaryReader reader) { return NormalizedUInt16.CreateCoded(reader.ReadUInt16()); }

		/// <summary>Read a <see cref="NormalizedUInt32"/>.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static NormalizedUInt32 ReadNormalizedUInt32(this BinaryReader reader) { return NormalizedUInt32.CreateCoded(reader.ReadUInt32()); }

		/// <summary>Read a <see cref="Plane3f"/>.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static Plane3f ReadPlane3f(this BinaryReader reader) { return new Plane3f(reader.ReadVector3f(), reader.ReadSingle()); }

		/// <summary>Read a <see cref="Sphere3f"/>.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static Sphere3f ReadSphere3f(this BinaryReader reader) { return new Sphere3f(reader.ReadVector3f(), reader.ReadSingle()); }
	}
}
