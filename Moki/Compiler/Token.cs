using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compiler {
	public class Token {
		public Marker Marker { get; private set; }

		bool Freed;
		Token Next;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal Token PeekNext {
			get {
				if (Freed)
					throw new InvalidOperationException("This " + typeof(Token) + " has already been freed and cannot be peeked; there is something seriously wrong with you.");
				return Next;
			}

			set {
				if (Freed)
					throw new InvalidOperationException("This " + typeof(Token) + " has already been freed, holy crap what is going on this is the end of times.");
				Next = value;
			}
		}

		public TokenCode Code { get; private set; }

		public string Name { get { return Lexer.GetTokenCodeName(this); } }

		public Lexer Lexer { get; private set; }

		public object Value { get; private set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string ValueString { get { return Value as String; } }

		[ThreadStatic]
		static Token firstFreeToken;

		Token() {
		}

		public static Token Create(Lexer lexer, Marker marker, TokenCode code, object value = null) {
			Token token = firstFreeToken;

			if (lexer == null)
				throw new ArgumentNullException("lexer");

			if (token != null) {
				firstFreeToken = token.Next;
				token.Next = null;
				token.Freed = false;
			} else
				token = new Token();

			token.Lexer = lexer;
			token.Marker = marker;
			token.Code = code;
			token.Value = value;
			return token;
		}

		internal void Free() {
			Next = firstFreeToken;
			firstFreeToken = this;
			Freed = true;
		}

		public string GetValueAsString() {
			if (Value == null)
				return "null";
			if (Value is string && Code != TokenCode.Identifier)
				return "\"" + Value + "\"";
			return Value.ToString();
		}

		public override string ToString() {
			string text = string.Format("{0}: {1}", Marker, Code);

			switch(Code) {
				case TokenCode.Identifier:
					return text + " " + Value;

				default:
					if (Value != null) {
						if (Value is string)
							return text + " \"" + Value + "\"";
						return text + " " + Value;
					}

					return text;
			}
		}
	}
}
