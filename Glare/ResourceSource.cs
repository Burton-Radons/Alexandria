using Glare.Framework;
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
	/// An object that can produce a resource object on request.
	/// </summary>
	public interface IResourceSource {
		/// <summary>Get the resource value.</summary>
		/// <returns></returns>
		object GetResourceValue();
	}

	/// <summary>An object that can produce a resource object on request.</summary>
	/// <typeparam name="T"></typeparam>
	public interface IResourceSource<T> : IResourceSource {
		/// <summary>Get the resource value.</summary>
		/// <returns></returns>
		new T GetResourceValue();
	}

	/// <summary>Implements <see cref="IResourceSource{T}"/> by providing a literal value.</summary>
	/// <typeparam name="T"></typeparam>
	public class LiteralResourceSource<T> : IResourceSource<T> {
		readonly T value;

		/// <summary>
		/// Initialise the source.
		/// </summary>
		/// <param name="value"></param>
		public LiteralResourceSource(T value) {
			this.value = value;
		}

		/// <summary>
		/// Get the value.
		/// </summary>
		/// <returns></returns>
		public T GetResourceValue() { return value; }

		object IResourceSource.GetResourceValue() { return value; }
	}

	/// <summary>
	/// An implementation of <see cref="IResourceSource{T}"/> that retains a ewak reference to the value.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class WeakResourceSource<T> : IResourceSource<T> where T : class {
		readonly WeakReference value;

		/// <summary>Initialise the source.</summary>
		/// <param name="value"></param>
		public WeakResourceSource(WeakReference value) { this.value = value; }

		/// <summary>Initialise the source.</summary>
		/// <param name="value"></param>
		public WeakResourceSource(T value) { this.value = new WeakReference(value); }

		/// <summary>Get the resource value or <c>null</c> if it's been collected.</summary>
		/// <returns></returns>
		public T GetResourceValue() { return (T)value.Target; }
		object IResourceSource.GetResourceValue() { return GetResourceValue(); }
	}
}
