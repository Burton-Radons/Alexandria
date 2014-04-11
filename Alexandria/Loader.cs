using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	public abstract class Loader : Resource {
		Loader(Manager manager, ResourceManager resourceManager) : base(manager, resourceManager) { }

		Loader(Manager manager, string name, string description = null)
			: base(manager, name, description) {
		}

		public Loader(Engine engine) : this(engine.Manager, engine.ResourceManager) { engine.loaders.Add(this); }
		public Loader(Plugin plugin) : this(plugin.Manager, plugin.ResourceManager) { plugin.LoadersMutable.Add(this); }
		public Loader(Game program, string name, string description = null) : this(program.Manager, name, description) { program.loaders.Add(this); }

		/// <summary>Attempt to load the object, returning whether there is a match.</summary>
		/// <param name="reader"></param>
		/// <param name="name">An attempt at a file name for the object, or <c>null</c> if there is no such possibility.</param>
		/// <returns></returns>
		public abstract LoaderMatchLevel Match(BinaryReader reader, string name, LoaderFileOpener opener, Resource context);

		/// <summary>Load the object. This should only be called if <see cref="Match"/> returned a value greater than <see cref="LoaderMatchLevel.None"/>.</summary>
		/// <param name="reader"></param>
		/// <param name="name">An attempt at a file name for the object, or <c>null</c> if there is no such possibility.</param>
		/// <returns></returns>
		public abstract Resource Load(BinaryReader reader, string name, LoaderFileOpener opener, Resource context);

		public static Stream SystemFileOpener(string name, FileMode mode, FileAccess access, FileShare share) {
			return File.Open(name, mode, access, share);
		}
	}

	public delegate Stream LoaderFileOpener(string name, FileMode mode = FileMode.Open, FileAccess access = FileAccess.Read, FileShare share = FileShare.Read);

	/// <summary>
	/// Match level returned by <see cref="Loader.Match"/>. Each is an integer value, and so there can be values in between if necessary.
	/// </summary>
	public enum LoaderMatchLevel {
		/// <summary>(0) The loader does not match the stream.</summary>
		None = 0,

		/// <summary>(10) The loader doesn't match, but can be used as a default. This is used by the binary/text loader.</summary>
		Fallback = 10,

		/// <summary>(20) The match is a poor quality default, such as a text file loader.</summary>
		Generic = 20,

		/// <summary>(30) The match is weak.</summary>
		Weak = 30,

		/// <summary>(40) The match is medium-strong.</summary>
		Medium = 40,

		/// <summary>(50) The match is strong but not comprehensive. If you don't load the entire file but just match a header, you should return this.</summary>
		Strong = 50,

		/// <summary>(75) The match is strong and comprehensive.</summary>
		Comprehensive = 75,

		/// <summary>(100) The object cannot be anything else.</summary>
		Perfect = 100,
	}
}
