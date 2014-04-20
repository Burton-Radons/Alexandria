using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	/// <summary>
	/// A type of <see cref="Engine"/> that refers to a physical system, such as a Commodore 64 or a Nintendo Entertainment System.
	/// </summary>
	public class Platform : Engine {
		public Platform(AlexandriaPlugin plugin)
			: base(plugin) {
		}
	}
}
