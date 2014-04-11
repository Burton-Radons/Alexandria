using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compiler {
	/// <summary>A span of <see cref="Source"/> code.</summary>
	public struct Marker {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Source source;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly MarkerPoint start, end;

		public MarkerPoint End { get { return end; } }
		public Source Source { get { return source; } }
		public MarkerPoint Start { get { return start; } }


		public Marker(Source source, MarkerPoint start, MarkerPoint end)
			: this() {
			this.source = source;
			this.start = start;
			this.end = end;
		}

		public override string ToString() {
			string text = string.Format("\"{0}\" ({1}:{2}", Source.Name, Start.Line, Start.Column);

			if(Start != End)
				text += string.Format(" to {0}:{1}", End.Line, End.Column);
			return text + ")";
		}
	}

	public struct MarkerPoint : IEquatable<MarkerPoint> {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly int column, line, offset;

		public int Column { get { return column; } }

		public int Line { get { return line; } }

		public int Offset { get { return offset; } }

		public MarkerPoint(int column, int line, int offset) {
			this.column = column;
			this.line = line;
			this.offset = offset;
		}

		public bool Equals(MarkerPoint other) { return column == other.column && line == other.line && offset == other.offset; }

		public override bool Equals(object obj) {
			if (obj is MarkerPoint)
				return Equals((MarkerPoint)obj);
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return (column.GetHashCode() * 23) ^ (line.GetHashCode() * 11) ^ offset.GetHashCode();
		}

		public override string ToString() {
			return string.Format("(Offset {0}, Line {1}, Column {2})", Offset, Line, Column);
		}

		public static bool operator ==(MarkerPoint a, MarkerPoint b) { return a.Equals(b); }
		public static bool operator !=(MarkerPoint a, MarkerPoint b) { return !a.Equals(b); }
	}
}
