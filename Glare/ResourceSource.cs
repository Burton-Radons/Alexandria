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

	public class WeakResourceSource<T> : IResourceSource<T> where T : class {
		readonly WeakReference<T> value;

		public WeakResourceSource(WeakReference<T> value) { this.value = value; }
		public WeakResourceSource(T value) { this.value = new WeakReference<T>(value); }

		public T GetResourceValue() { T target; value.TryGetTarget(out target); return target; }
		object IResourceSource.GetResourceValue() { return GetResourceValue(); }
	}
}
