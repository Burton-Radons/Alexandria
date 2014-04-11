using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	/// <summary>
	/// Provides state which is used in shaders to govern how textures are sampled.
	/// </summary>
	public class Sampler : SamplerResource {
		public Sampler()
			: base(AllocateId()) {

		}

		static int AllocateId() {
			using (Context.Lock())
				return GL.GenSampler();
		}

		protected override void DisposeBase() {
			GL.DeleteSampler(Id);
		}

		protected override double Get1d(SamplerParameterName pname) { float result; using (Context.Lock()) GL.GetSamplerParameter(Id, pname, out result); return result; }
		protected override int Get1i(SamplerParameterName pname) { int result; using (Context.Lock()) GL.GetSamplerParameter(Id, pname, out result); return result; }

		protected override void Set(SamplerParameterName pname, double value) { using (Context.Lock()) GL.SamplerParameter(Id, pname, checked((float)value)); }
		protected override void Set(SamplerParameterName pname, int value) { using (Context.Lock()) GL.SamplerParameter(Id, pname, value); }
	}
}
