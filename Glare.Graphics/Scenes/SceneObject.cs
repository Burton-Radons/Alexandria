using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Scenes {
	public abstract class SceneObject : INotifyPropertyChanged, INotifyPropertyChanging {
		public event PropertyChangedEventHandler PropertyChanged;
		public event PropertyChangingEventHandler PropertyChanging;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object tag;

		/// <summary>Get or set an opaque object that can be assigned to the object.</summary>
		public object Tag {
			get { return tag; }
			set { SetProperty(TagChangingArgs, TagChangedArgs, ref tag, ref value); }
		}

		const string
			TagName = "Tag";

		static readonly PropertyChangingEventArgs
			TagChangingArgs = new PropertyChangingEventArgs(TagName);

		static readonly PropertyChangedEventArgs
			TagChangedArgs = new PropertyChangedEventArgs(TagName);

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
			if (PropertyChanged != null)
				PropertyChanged(this, args);
		}

		protected virtual void OnPropertyChanging(PropertyChangingEventArgs args) {
			if (PropertyChanging != null)
				PropertyChanging(this, args);
		}

		protected void SetProperty<T>(PropertyChangingEventArgs changingArgs, PropertyChangedEventArgs changedArgs, ref T slot, ref T value) {
			if (EqualityComparer<T>.Default.Equals(slot, value))
				return;
			OnPropertyChanging(changingArgs);
			slot = value;
			OnPropertyChanged(changedArgs);
		}
	}
}
