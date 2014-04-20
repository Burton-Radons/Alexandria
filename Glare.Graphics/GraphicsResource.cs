using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	public abstract class GraphicsResource : DisposableObject {
		readonly int id;

		public int Id { get { return id; } }

		public GraphicsResource(int id) {
			this.id = id;
		}

		protected void CheckError() {
			Device.CheckError();
		}
	}
}
