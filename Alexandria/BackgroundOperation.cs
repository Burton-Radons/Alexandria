using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	/// <summary>
	/// This describes a background operation that occurs for an <see cref="AlexandriaManager"/>.
	/// </summary>
	public class BackgroundOperation {
		/// <summary>Get a description of the operation being performed, or <c>null</c> for none.</summary>
		public string Description { get; private set; }

		/// <summary>This event is raised when the background operation is run.</summary>
		public event BackgroundOperationCallback Execute;

		/// <summary>Get the name of the operation being performed.</summary>
		public string Name { get; private set; }

		/// <summary>
		/// Initialise the object.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		/// <param name="callbacks"></param>
		public BackgroundOperation(AlexandriaManager manager, string name, string description, BackgroundOperationCallback[] callbacks) {
			if (name == null)
				throw new ArgumentNullException("name");

			Name = name;
			Description = description;

			foreach (BackgroundOperationCallback callback in callbacks)
				Execute += callback;
		}

		internal void Run(Action<double> updateProgress) {
			if (Execute != null)
				Execute(updateProgress);
		}
	}

	/// <summary>The callback to run a <see cref="BackgroundOperation"/>.</summary>
	/// <param name="updateProgress"></param>
	public delegate void BackgroundOperationCallback(Action<double> updateProgress);
}
