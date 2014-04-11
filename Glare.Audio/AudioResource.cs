using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Audio
{
	public abstract class AudioResource : DisposableObject
	{
		AudioContext context;
		int id;

		public int Id { get { return id; } }

		public AudioContext Context { get { return context; } }

		public AudioDevice Device { get { return context.Device; } }

		internal AudioResource(AudioContext context, int alId)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			this.context = context;
			this.id = alId;
		}

		protected override void DisposeBase()
		{
			id = 0;
		}
	}
}
