using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	public abstract class Query : GraphicsResource {
		int Index;

		protected ulong BaseValue { get { return Get1ul(GetQueryObjectParam.QueryResult); } }
		protected ulong BaseValueNoWait { get { return Get1ul(GetQueryObjectParam.QueryResultNoWait); } }

		protected virtual bool Indexed { get { return false; } }

		/// <summary>Get the number of bits of precision of the value.</summary>
		public int PrecisionBits { get { return Get1i(GetQueryParam.QueryCounterBits); } }

		protected abstract QueryTarget Target { get; }

		public Query(int index)
			: base(AllocateId()) {
			Index = index;
		}

		static int AllocateId() {
			using (Context.Lock())
				return GL.GenQuery();
		}

		public virtual void Begin() {
			using (Context.Lock())
				if (Indexed)
					GL.BeginQueryIndexed(Target, Index, Id);
				else
					GL.BeginQuery(Target, Id);
		}

		protected override void DisposeBase() {
			GL.DeleteQuery(Id);
		}

		public virtual void End() {
			using (Context.Lock())
				if (Indexed)
					GL.EndQueryIndexed(Target, Index);
				else
					GL.EndQuery(Target);
		}

		protected int Get1i(GetQueryParam param) {
			int result;
			using (Context.Lock())
				if (Indexed)
					GL.GetQueryIndexed(Target, Index, param, out result);
				else
					GL.GetQuery(Target, param, out result);
			return result;
		}

		protected ulong Get1ul(GetQueryObjectParam param) {
			ulong result;

			using(Context.Lock())
				GL.GetQueryObject((uint)Id, param, out result);
			return result;
		}
	}

	public abstract class Query<T> : Query where T : struct {
		/// <summary>Get whether the <see cref="Value"/> is available immediately without waiting.</summary>
		public bool IsValueAvailable { get { return Get1ul(GetQueryObjectParam.QueryResultAvailable) != 0; } }

		/// <summary>Get the value; this may stall the thread until the value is available. To get the value immediately, use <see cref="ValueNoWait"/> or <see cref="ValueIfAvailable"/>.</summary>
		public abstract T Value { get; }

		/// <summary>If the value is available (<see cref="IsValueAvailable"/>), then it is returned; otherwise <c>null</c> is returned.</summary>
		public T? ValueIfAvailable { get { return IsValueAvailable ? Value : (T?)null; } }

		/// <summary>Get the value; if the value is not available (<see cref="IsValueAvailable"/>) then zero/false/no time is returned.</summary>
		public abstract T ValueNoWait { get; }

		public Query(int index) : base(index) { }

		/// <summary>Try to get the value. If it's available, store it and return <c>true</c>; otherwise store zero/false/no time and return <c>false</c>.</summary>
		/// <param name="value">Receives the value if available, or else receives zero/false/no time.</param>
		/// <returns>Whether the value was available.</returns>
		public bool TryGetValue(out T value) {
			if (IsValueAvailable) {
				value = Value;
				return true;
			}

			value = default(T);
			return false;
		}
	}

	public abstract class BooleanQuery : Query<bool> {
		public BooleanQuery(int index = 0) : base(index) { }
		public override bool Value { get { return BaseValue != 0; } }
		public override bool ValueNoWait { get { return BaseValueNoWait != 0; } }
	}

	public abstract class Int64Query : Query<long> {
		public Int64Query(int index = 0) : base(index) { }
		public override long Value { get { return checked((long)BaseValue); } }
		public override long ValueNoWait { get { return checked((long)BaseValueNoWait); } }
	}

	public abstract class TimeSpanQuery : Query<TimeSpan> {
		public TimeSpanQuery(int index = 0) : base(index) { }
		public override TimeSpan Value { get { return TimeSpan.FromSeconds(BaseValue * 10e-9); } }
		public override TimeSpan ValueNoWait { get { return TimeSpan.FromSeconds(BaseValueNoWait * 10e-9); } }

		/// <summary>Get the actual value in nanoseconds, because <see cref="TimeSpan"/> only works on milliseconds.</summary>
		public ulong ValueNanoseconds { get { return BaseValue; } }

		public ulong ValueNanosecondsNoWait { get { return BaseValueNoWait; } }

		public ulong? ValueNanosecondsIfAvailable { get { return IsValueAvailable ? ValueNanoseconds : (ulong?)null; } }

		public bool TryGetValueNanoseconds(out TimeSpan value) {
			if (IsValueAvailable) {
				value = Value;
				return true;
			}

			value = default(TimeSpan);
			return false;
		}
	}

	public static class Queries {
		/// <summary>On <see cref="Query.Begin"/>, <see cref="BooleanQuery.Value"/> is set to <c>false</c> and is set to <c>true</c> if any sample passes the depth test. This is done before stencil tests, alpha tests, and shader discard come into play.</summary>
		public class AnySamplesPassed : BooleanQuery {
			public AnySamplesPassed() : base() { }
			protected override QueryTarget Target { get { return QueryTarget.AnySamplesPassed; } }
		}

		/// <summary>On <see cref="Query.Begin"/>, <see cref="BooleanQuery.Value"/> is set to <c>false</c> and is set to <c>true</c> if any sample passes the depth test. This is done before stencil tests, alpha tests, and shader discard come into play. This can use an algorithm that produces more false positives than <see cref="AnySamplesPassed"/>.</summary>
		public class AnySamplesPassedConservative : BooleanQuery {
			public AnySamplesPassedConservative() : base() { }
			protected override QueryTarget Target { get { return QueryTarget.AnySamplesPassedConservative; } }
		}

		/// <summary>On <see cref="Query.Begin"/>, <see cref="Int64Query.Value"/> is set to zero and then incremented for every primitive that is sent to a geometry shader output stream.</summary>
		public class PrimitivesGenerated : Int64Query {
			public PrimitivesGenerated(int outputStream = 0) : base(outputStream) { }
			protected override QueryTarget Target { get { return QueryTarget.PrimitivesGenerated; } }
		}

		/// <summary>On <see cref="Query.Begin"/>, <see cref="Int64Query.Value"/> is set to zero and then incremented for every sample that passes the depth test. This is done before stencil tests, alpha tests, and shader discard come into play.</summary>
		public class SamplesPassed : Int64Query {
			public SamplesPassed() : base() { }
			protected override QueryTarget Target { get { return QueryTarget.SamplesPassed; } }
		}

		public class TimeElapsed : TimeSpanQuery {
			public TimeElapsed() : base() { }
			protected override QueryTarget Target { get { return QueryTarget.TimeElapsed; } }
		}

		/// <summary>This query gets the current time stamp of the GPU after executing all previously specified commands. It is not used with <see cref="Begin"/>/<see cref="End"/> as normal; rather, <see cref="TimeSpanQuery.Value"/> is used directly.</summary>
		public class TimeStamp : TimeSpanQuery {
			public TimeStamp() : base() { }

			protected override QueryTarget Target { get { return QueryTarget.Timestamp; } }

			/// <summary>This is invalid to call on a timestamp query..</summary>
			[Obsolete("Begin cannot be used on a TimeStamp query.")]
			public override void Begin() { throw new InvalidOperationException(); }

			/// <summary>This is invalid to call on a timestamp query.</summary>
			[Obsolete("End cannot be used on a TimeStamp query.")]
			public override void End() { throw new InvalidOperationException(); }
		}

		/// <summary>On <see cref="Query.Begin"/>, <see cref="Int64Query.Value"/> is set to zero and then incremented for every primitive written by a geometry shader into a transform feedback object by the scoped rendering commands.</summary>
		public class TransformFeedbackPrimitivesWritten : Int64Query {
			public TransformFeedbackPrimitivesWritten(int outputStream = 0) : base(outputStream) { }
			protected override QueryTarget Target { get { return QueryTarget.TransformFeedbackPrimitivesWritten; } }
		}
	}
}
