using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	public class GraphicsVersion {
		readonly int major, minor, revision;
		readonly string info;

		public int Major { get { return major; } }
		public int Minor { get { return minor; } }
		public int Revision { get { return revision; } }
		public string Info { get { return info; } }

		public GraphicsVersion(string value) {
			int split, next;

			split = value.IndexOf('.');
			major = int.Parse(value.Substring(0, split));
			next = value.IndexOf('.', split + 1);
			if (next < 0) {
				next = value.IndexOf(' ', split + 1);
				if (next < 0) next = value.Length;
			}

			minor = int.Parse(value.Substring(split + 1, next - split - 1));
			if (next < value.Length && value[next] == '.') {
				split = value.IndexOf(' ', next + 1);
				if (split < 0) split = value.Length + 1;
				revision = int.Parse(value.Substring(next + 1, split - next - 1));
				next = split - 1;
			} else
				revision = 0;

			info = value.Substring(Math.Min(next, value.Length)).Trim();
		}

		public bool Check(int major, int minor = 0, int revision = 0) {
			return Major > major || (Major == major && (Minor > minor || (Minor == minor && Revision >= revision)));
		}

		public void Require(int major, int minor = 0, int revision = 0) {
			if (!Check(major, minor, revision))
				throw new Exception("OpenGL " + major + "." + minor + " required; " + this + " is supported.");
		}

		public override string ToString() {
			string text = major + "." + minor;

			if (revision > 0)
				text += "." + revision;
			if (info != null && info.Length > 0)
				text += " " + info;
			return text;
		}
	}
}
