using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
	/// <summary>
	/// A resource loading candidate.
	/// </summary>
	public interface IResourceLoader {
		double Identify(string path, Stream stream);

		object Load(string path, Stream stream, bool closeStream);

		void LoadInto(object target, string path, Stream stream, bool closeStream);
	}

	public static class ResourceLoader {
		internal static readonly ArrayBackedList<IResourceLoader> loaders = new ArrayBackedList<IResourceLoader>();

		public static ReadOnlyList<IResourceLoader> Loaders { get { return loaders; } }

		public static void AddLoader(IResourceLoader loader) {
			if (loader == null)
				throw new ArgumentNullException("loader");
			loaders.Add(loader);
		}

		public static IResourceLoader Identify(string path, Stream stream, out double matchStrength) {
			IResourceLoader match = null;
			matchStrength = 0;

			long reset = stream.Position;
			foreach (IResourceLoader loader in loaders) {
				double strength = loader.Identify(path, stream);
				stream.Position = reset;

				if (strength > matchStrength) {
					match = loader;
					matchStrength = strength;
				}
			}

			return match;
		}

		public static IResourceLoader Identify(string path, Stream stream) {
			double matchStrength;
			return Identify(path, stream, out matchStrength);
		}

		public static object Load(string path, Stream stream, bool closeStream = true) {
			return MustIdentify(path, stream).Load(path, stream, closeStream);
		}

		public static object Load(string path) { return Load(path, File.OpenRead(path)); }

		public static T Load<T>(string path, Stream stream, bool closeStream = true) { return (T)Load(path, stream, closeStream); }
		public static T Load<T>(string path) { return (T)Load(path); }

		public static void LoadInto(object target, string path) {
			LoadInto(target, path, File.OpenRead(path));
		}

		public static void LoadInto(object target, string path, Stream stream, bool closeStream = true) {
			MustIdentify(path, stream).LoadInto(target, path, stream, closeStream);
		}

		public static IResourceLoader MustIdentify(string path, Stream stream) {
			var loader = Identify(path, stream);
			if (loader == null)
				throw new Exception("Could not identify a matching loader for '" + path + "'.");
			return loader;
		}
	}

	public interface IResourceSource {
		object GetResourceValue();
	}

	public interface IResourceSource<T> : IResourceSource {
		new T GetResourceValue();
	}

	public class LiteralResourceSource<T> : IResourceSource<T> {
		readonly T value;

		public LiteralResourceSource(T value) {
			this.value = value;
		}

		public T GetResourceValue() { return value; }

		object IResourceSource.GetResourceValue() { return value; }
	}
}
