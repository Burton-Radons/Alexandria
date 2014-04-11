using Moki.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compiler {
	/// <summary>
	/// Converts <see cref="Source"/> code into a series of <see cref="Token"/>s.
	/// </summary>
	public abstract class Lexer : MessageProvider {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly MessageHandler messageHandler;

		public Marker Marker { get { return Peek().Marker; } }

		public override MessageHandler MessageHandler { get { return messageHandler; } }

		protected Token Peeked;

		/// <summary>The source <see cref="Reader"/> to use.</summary>
		protected readonly Reader Reader;

		/// <summary>The <see cref="Lexer"/> should set this to <see cref="Reader.Point"/> when a token is being read, so that the creation and error message methods work.</summary>
		protected MarkerPoint Start;

		public Lexer(Reader reader, MessageHandler messageHandler) {
			if (reader == null)
				throw new ArgumentNullException("reader");
			if (messageHandler == null)
				throw new ArgumentNullException("messageHandler");

			Reader = reader;
			this.messageHandler = messageHandler;
		}

		public Lexer(Source source, MessageHandler messageHandler)
			: this(new Reader(source), messageHandler) {
		}

		protected Token CreateToken(TokenCode code, object value = null) {
			return Token.Create(this, GetMarker(), code, value);
		}

		public bool Expect(out Token token, TokenCode code) {
			if (Match(out token, code))
				return true;
			ErrorExpectedToken(code);
			return false;
		}

		public bool Expect(TokenCode code) {
			if (Match(code))
				return true;
			ErrorExpectedToken(code);
			return false;
		}

		protected Marker GetMarker() { return new Marker(Reader.Source, Start, Reader.Point); }

		/// <summary>Return the name of a token. The default returns the English C# names; so inheritors might want to translate that into their own natural or computer language.</summary>
		/// <param name="token"></param>
		/// <returns></returns>
		protected internal virtual string GetTokenCodeName(Token token) {
			string value = token.GetValueAsString();
			return string.Format(Properties.Resources.ResourceManager.GetString("TokenCode_" + token.Code + "_Name") ?? token.Code.ToString(), value);
		}

		/// <summary>If the next <see cref="Token"/>'s <see cref="Token.Code"/> (a <see cref="TokenCode"/>) matches, consume and return it; otherwise return <c>null</c>.</summary>
		/// <param name="code">The <see cref="TokenCode"/> to match against the <see cref="Token"/>'s <see cref="Token.Code"/> property.</param>
		/// <returns>Whether a match was made.</returns>
		public bool Match(TokenCode code) {
			Token token = Peeked;

			if (token == null)
				Peeked = token = Read();

			if (token.Code != code)
				return false;

			Peeked = token.PeekNext;
			token.Free();
			return true;
		}

		public bool Match(out Token token, TokenCode code) {
			token = Peeked;

			if (token == null)
				Peeked = token = Read();

			if (token.Code != code) {
				token = null;
				return false;
			}

			Peeked = token.PeekNext;
			token.Free();
			return true;
		}

		public Token Next() {
			Token token = Peeked;

			if (token != null)
				Peeked = token.PeekNext;
			else
				token = Read();

			token.Free();
			return token;
		}

		/// <summary>Peek at the next <see cref="Token"/> without consuming it.</summary>
		/// <returns></returns>
		public Token Peek() {
			Token token = Peeked;

			if (token == null)
				Peeked = token = Read();

			return token;
		}

		/// <summary>Peek after the given peeked <see cref="Token"/>. If it is <c>null</c>, the first <see cref="Peek()"/>ed <see cref="Token"/> is returned.</summary>
		/// <param name="token"></param>
		/// <returns></returns>
		public Token Peek(Token token) {
			if (token == null)
				return Peek();

			Token next = token.PeekNext;
			if (next == null)
				return token.PeekNext = Read();
			return next;
		}

		/// <summary>If <paramref name="token"/> is <c>null</c>, return the first <see cref="Peek()"/>ed <see cref="Token"/>; otherwise return the <paramref name="token"/>.</summary>
		/// <param name="token"></param>
		/// <returns></returns>
		public Token PeekStart(Token token) { return token ?? Peek(); }

		/// <summary>Return whether the next <see cref="Token"/> has the given <see cref="TokenCode"/>.</summary>
		/// <param name="code">The <see cref="TokenCode"/> to check.</param>
		/// <returns>Whether the peeked <see cref="Token"/> has the same <see cref="TokenCode"/> as <paramref name="code"/> in its <see cref="Token.Code"/> property.</returns>
		public bool PeekTest(TokenCode code) { return Peek().Code == code; }

		/// <summary>If the next <see cref="Token"/> has the given <see cref="TokenCode"/> in its <see cref="Token.Code"/> property, then peek past it and return the next <see cref="Token"/>.</summary>
		/// <param name="token">The <see cref="Token"/> to peek at. If this is <c>null</c>, the first peeked <see cref="Token"/> is used. This receives the next <see cref="Token"/> if the <paramref name="code"/> matches.</param>
		/// <param name="next">If the peeked <see cref="Token"/> matches, this receives the next peeked <see cref="Token"/>.</param>
		/// <param name="code">The <see cref="TokenCode"/> to match against the <paramref name="token"/>.</param>
		/// <returns>Whether there was a match.</returns>
		public bool PeekTest(Token token, ref Token next, TokenCode code) {
			Token peek = PeekStart(token);
			if (peek.Code != code)
				return false;
			next = Peek(peek);
			return true;
		}

		/// <summary>If the next <see cref="Token"/> has the given <see cref="TokenCode"/> in its <see cref="Token.Code"/> property, then peek past it and return the next <see cref="Token"/>.</summary>
		/// <param name="token">The <see cref="Token"/> to peek at. If this is <c>null</c>, the first peeked <see cref="Token"/> is used. This receives the next <see cref="Token"/> if the <paramref name="code"/> matches.</param>
		/// <param name="code">The <see cref="TokenCode"/> to match against the <paramref name="token"/>.</param>
		/// <returns>Whether there was a match.</returns>
		public bool PeekTest(ref Token token, TokenCode code) {
			Token peek = PeekStart(token);
			if (peek.Code != code)
				return false;
			token = Peek(peek);
			return true;
		}

		/// <summary>If the next <see cref="Token"/> has the given <see cref="TokenCode"/> in its <see cref="Token.Code"/> property and a matching <see cref="Token.Value"/>, then peek past it and return the next <see cref="Token"/></summary>
		/// <param name="token">The <see cref="Token"/> to peek at. If this is <c>null</c>, the first peeked <see cref="Token"/> is used. This receives the next <see cref="Token"/> if the <paramref name="code"/> matches.</param>
		/// <param name="next">If the peeked <see cref="Token"/> matches, this receives the next peeked <see cref="Token"/>.</param>
		/// <param name="code">The <see cref="TokenCode"/> to match against the <paramref name="token"/>.</param>
		/// <param name="value">The <see cref="Token.Value"/> property to match.</param>
		/// <returns>Whether there was a match.</returns>
		public bool PeekTest(Token token, ref Token next, TokenCode code, object value) {
			Token peek = PeekStart(token);
			if (peek.Code != code || !object.Equals(peek.Value, value))
				return false;
			next = Peek(peek);
			return true;
		}

		/// <summary>If the next <see cref="Token"/> has the given <see cref="TokenCode"/> in its <see cref="Token.Code"/> property and a matching <see cref="Token.Value"/>, then peek past it and return the next <see 		/// <param name="token"></param>
		/// <param name="token">The <see cref="Token"/> to peek at. If this is <c>null</c>, the first peeked <see cref="Token"/> is used. This receives the next <see cref="Token"/> if the <paramref name="code"/> matches.</param>
		/// <param name="code">The <see cref="TokenCode"/> to match against the <paramref name="token"/>.</param>
		/// <param name="value">The <see cref="Token.Value"/> to match against the <paramref name="token"/>.</param>
		/// <returns>Whether there was a match.</returns>
		public bool PeekTest(ref Token token, TokenCode code, object value) {
			Token peek = PeekStart(token);
			if (peek.Code != code || !object.Equals(peek.Value, value))
				return false;
			token = Peek(peek);
			return true;
		}

		public bool PeekTest(Token token, ref Token next, params TokenCode[] codes) {
			Token peek = PeekStart(token);
			foreach (TokenCode code in codes) {
				if (peek.Code == code) {
					next = Peek(peek);
					return true;
				}
			}
			return false;
		}

		public bool PeekTest(ref Token token, params TokenCode[] codes) {
			Token peek = PeekStart(token);
			foreach (TokenCode code in codes) {
				if (peek.Code == code) {
					token = Peek(peek);
					return true;
				}
			}
			return false;
		}

		/// <summary>Read the next <see cref="Token"/> from the <see cref="Reader"/>.</summary>
		/// <returns>The next <see cref="Token"/>.</returns>
		protected abstract Token Read();

		#region Convenience methods for inheritors

		/// <summary>Read a numeric value with no concern for overflow.</summary>
		/// <param name="radix"></param>
		/// <returns></returns>
		BigInteger ReadBigNumericValue(int radix) {
			BigInteger value = BigInteger.Zero;
			int digit;

			while (TryDecodeDigit(Reader.Peek(), radix, out digit))
				value = value * radix + digit;

			return value;
		}

		/// <summary>Convert a digit character into a number. This supports <paramref name="radix"/> values from 2 to 36.</summary>
		/// <param name="value">The character value to convert.</param>
		/// <param name="radix">The radix of the number, meaning how many digits there are in it. Binary is 2, octal is 8, decimal is 10, and hexadecimal is 16.</param>
		/// <param name="result">Receives the resulting value, if conversion was possible. Otherwise it receives zero.</param>
		/// <returns>Whether the conversion was possible.</returns>
		protected bool TryDecodeDigit(char value, int radix, out int result) {
			if (value >= '0' && value <= '9')
				result = value - '0';
			else if (value >= 'a' && value <= 'z')
				result = value - 'a' + 10;
			else if (value >= 'A' && value <= 'Z')
				result = value - 'A' + 10;
			else
				result = radix;

			if (result >= radix) {
				result = 0;
				return false;
			}

			return true;
		}

		/// <summary>Attempt to read a digit from the <see cref="Reader"/>, consuming it if it was successful.</summary>
		/// <param name="radix"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		protected bool TryReadDigit(int radix, out int result) {
			if (!TryDecodeDigit(Reader.Peek(), radix, out result))
				return false;
			Reader.Next();
			return true;
		}

		/// <summary>Read a number with a specific number of digits.</summary>
		/// <param name="value"></param>
		/// <param name="digits"></param>
		/// <param name="radix"></param>
		/// <returns></returns>
		bool TryReadNumericValue(int digits, int radix, out long value) {
			value = 0;
			for (int index = 0; index < digits; index++) {
				int digit;

				if (!TryReadDigit(radix, out digit))
					return false;

				value = value * radix + digit;
			}
			return true;
		}

		#endregion Convenience methods for inheritors

		#region Warnings

		#endregion Warnings

		#region Errors

		protected void ErrorExpectedToken(TokenCode expected) { SendMessage(new ExpectedTokenError(Peek(), expected)); }

		/// <summary>Generate an unknown character error.</summary>
		/// <param name="start">Where the problem started.</param>
		/// <param name="unknownCharacter">The unknown character that has been encountered.</param>
		protected void ErrorUnknownCharacter(char unknownCharacter) { SendMessage(new UnknownCharacterError(GetMarker(), unknownCharacter)); }

		#endregion Errors
	}
}
