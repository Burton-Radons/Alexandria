using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
	/// <summary>
	/// A <see cref="NamedObject"/> that can also be disposed, sends an event when disposed, and can be tested for disposal state.
	/// </summary>
	public abstract class DisposableObject : NamedObject, IDisposable {
		bool isDisposed;

		/// <summary>This event is activated when the <see cref="DisposableObject"/> has had <see cref="Dispose"/> caleld on it.</summary>
		public event EventHandler Disposed;

		/// <summary>Get whether this <see cref="DisposableObject"/> has been disposed of.</summary>
		public bool IsDisposed { get { return isDisposed; } }

		/// <summary>Dispose of the object.</summary>
		~DisposableObject() { Dispose(); }

		/// <summary>
		/// Dispose of the object.
		/// </summary>
		public void Dispose() {
			if (isDisposed)
				return;
			isDisposed = true;
			DisposeBase();
			if (Disposed != null)
				Disposed.Invoke(this, new EventArgs());
		}

		/// <summary>Dispose of the object.</summary>
		protected abstract void DisposeBase();
	}
}
