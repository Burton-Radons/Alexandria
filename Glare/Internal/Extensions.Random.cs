using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Internal {
	public static partial class ExtensionMethods {
		/// <summary>
		/// Get the next double value within a range.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static double NextDoubleInRange(this Random self, double min, double max) { return self.NextDouble() * (max - min) + min; }

		/// <summary>
		/// Get the next single value.
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static float NextSingle(this Random self) { return (float)self.NextDouble(); }

		/// <summary>
		/// Get the next <see cref="UInt32"/> value.
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static uint NextFullUInt32(this Random self) {
			byte[] bytes = GetSharedBytes4();
			self.NextBytes(bytes);
			return (uint)(bytes[0] | (bytes[1] << 8) | (bytes[2] << 16) | (bytes[3] << 24));
		}
		/// <summary>
		/// Get the next <see cref="UInt64"/> value.
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ulong NextFullUInt64(this Random self) { return (ulong)self.NextFullUInt32() | ((ulong)self.NextFullUInt32() << 32); }
	}
}
