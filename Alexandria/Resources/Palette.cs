using Glare;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Resources {
	public class Palette : Resource {
		protected readonly ArrayBackedList<Color> ColorsMutable = new ArrayBackedList<Color>();

		public ReadOnlyList<Color> Colors { get { return ColorsMutable; } }

		public Palette(Manager manager, string name, params Color[] colors) : this(manager, name, (IEnumerable<Color>)colors) { }
		public Palette(Folder folder, string name, params Color[] colors) : this(folder, name, (IEnumerable<Color>)colors) { }

		public Palette(Manager manager, string name, IEnumerable<Color> colors)
			: base(manager, name) {
			if (colors != null)
				ColorsMutable.AddRange(colors);
		}

		public Palette(Folder folder, string name, IEnumerable<Color> colors)
			: base(folder, name) {
			if (colors != null)
				ColorsMutable.AddRange(colors);
		}

		/// <summary>
		/// Read a <see cref="Palette"/> from the <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="reader"></param>
		/// <param name="count"></param>
		/// <param name="max"></param>
		/// <param name="transparentIndex"></param>
		/// <returns></returns>
		public static Palette ReadRgb(Manager manager, string name, BinaryReader reader, int count, int max, int transparentIndex = -1, IEnumerable<Color> leftPadding = null, IEnumerable<Color> rightPadding = null) {
			Palette palette = new Palette(manager, name);

			if (leftPadding != null)
				foreach (Color color in leftPadding)
					palette.ColorsMutable.Add(color);

			for (int index = 0; index < count; index++) {
				int red = Math.Min(255, reader.ReadByte() * 255 / max);
				int green = Math.Min(255, reader.ReadByte() * 255 / max);
				int blue = Math.Min(255, reader.ReadByte() * 255 / max);

				palette.ColorsMutable.Add(index == transparentIndex ? Color.Transparent : Color.FromArgb(red, green, blue));
			}

			if (rightPadding != null)
				foreach (Color color in rightPadding)
					palette.ColorsMutable.Add(color);

			return palette;
		}
	}
}
