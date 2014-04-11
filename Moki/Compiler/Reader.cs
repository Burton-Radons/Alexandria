using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compiler {
	/// <summary>
	/// Converts a <see cref="Source"/> into a stream of characters.
	/// </summary>
	public class Reader {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly string code;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int column;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool isEnded;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int line;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		char next;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Source source;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly int sourceLength;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int offset;

		/// <summary>Get whether the reader is at the end of the <see cref="Source"/>.</summary>
		public bool IsEnded { get { return isEnded; } }

		public MarkerPoint Point {
			get { return new MarkerPoint(column, line, offset); }

			set {
				column = value.Column;
				line = value.Line;
				offset = value.Offset;

				isEnded = offset >= sourceLength;
				next = isEnded ? '\0' : code[offset];
			}
		}

		/// <summary>Get the <see cref="Source"/> code this is reading.</summary>
		public Source Source { get { return source; } }

		public Reader(Source source) {
			if (source == null)
				throw new ArgumentNullException("source");

			code = source.Code;
			column = 1;
			sourceLength = source.Length;
			line = 1;
			offset = 0;
			this.source = source;

			isEnded = offset >= sourceLength;
			next = isEnded ? '\0' : code[offset];
		}

		char Advance() {
			char value = next;

			if (value == '\n') {
				line++;
				column = 1;
			} else
				column++;

			offset++;
			isEnded = offset >= sourceLength;
			next = isEnded ? '\0' : code[offset];
			return value;
		}

		/// <summary>If the next character matches, consume it and return <c>true</c>; otherwise return <c>false</c>.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Match(char value) {
			if (next != value || offset >= sourceLength)
				return false;
			Advance();
			return true;
		}

		/// <summary>Consume the next character and advance the line/column, returning the character or <c>'\0'</c> if we're at the end of the source.</summary>
		/// <returns></returns>
		public char Next() { return isEnded ? '\0' : Advance(); }

		/// <summary>Peek at the next character without consuming it, or return <c>'\0'</c>.</summary>
		/// <returns></returns>
		public char Peek() { return next; }
	}
}
