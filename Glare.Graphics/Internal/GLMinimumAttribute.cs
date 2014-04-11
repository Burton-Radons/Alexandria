using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Internal
{
	public class GLMinimumAttribute : Attribute
	{
		readonly int major, minor;

		public int Major { get { return major; } }

		public int Minor { get { return minor; } }

		public GLMinimumAttribute(int major, int minor)
		{
			this.major = major;
			this.minor = minor;
		}
	}
}
