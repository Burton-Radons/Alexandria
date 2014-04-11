using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Internal {
	public static partial class ExtensionMethods {
		public static double NextDoubleInRange(this Random self, double min, double max) { return self.NextDouble() * (max - min) + min; }

		public static float NextSingle(this Random self) { return (float)self.NextDouble(); }

		public static uint NextFullUInt32(this Random self) {
			byte[] bytes = GetSharedBytes4();
			return (uint)(bytes[0] | (bytes[1] << 8) | (bytes[2] << 16) | (bytes[3] << 24));
		}
		public static ulong NextFullUInt64(this Random self) { return (ulong)self.NextFullUInt32() | ((ulong)self.NextFullUInt32() << 32); }
	}
}
