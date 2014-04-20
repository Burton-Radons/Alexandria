using Glare.Graphics.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Scenes.Components {
	public class RenderModelComponent : Component {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Model model;

		public Model Model {
			get { return model; }
			set { SetProperty(ModelChangingArgs, ModelChangedArgs, ref model, ref value); }
		}

		const string
			ModelName = "Model";

		static readonly PropertyChangingEventArgs
			ModelChangingArgs = new PropertyChangingEventArgs(ModelName);

		static readonly PropertyChangedEventArgs
			ModelChangedArgs = new PropertyChangedEventArgs(ModelName);

		public RenderModelComponent(Model model = null) {
			Model = model;
		}
	}
}
