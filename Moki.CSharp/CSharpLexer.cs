using Moki.Compiler;
using Moki.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compilers.CSharp {
	public class CSharpLexer : Lexer {
		StringBuilder StringBuilder = new StringBuilder();

		public CSharpLexer(Source source, MessageHandler handler)
			: base(source, handler) {
		}

		/// <summary>Get whether this character is part of a middle of an identifier.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static bool IsIdentifierPartCharacter(char value) {
			switch (char.GetUnicodeCategory(value)) {
				case UnicodeCategory.UppercaseLetter: // letter-character: Lu, Ll, Lt, Lm, Lo, Nl
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.LetterNumber:
				case UnicodeCategory.DecimalDigitNumber: // decimal-digit-character: Nd
				case UnicodeCategory.ConnectorPunctuation: // connecting-character: Pc
				case UnicodeCategory.SpacingCombiningMark: // combining-character: Mn or Mc
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.Format: // formatting-character: Cf
					return true;
				default:
					return false;
			}
		}

		/// <summary>Get whether this is an identifier start character.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static bool IsIdentifierStartCharacter(char value) {
			return IsLetterCharacter(value) || value == '_';
		}

		/// <summary>Get whether this is a letter character.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static bool IsLetterCharacter(char value) {
			switch (char.GetUnicodeCategory(value)) {
				case UnicodeCategory.UppercaseLetter: // letter-character: Lu, Ll, Lt, Lm, Lo, Nl
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.LetterNumber:
					return true;
				default:
					return false;
			}
		}

		protected override Token Read() {
			while (true) {
				Start = Reader.Point;
				char ch = Reader.Next();

				switch (ch) {
					// End-of-source
					case '\0': return CreateToken(TokenCode.EndOfSource);

					// Whitespace
					case ' ':
					case '\t':
					case '\n':
					case '\r': break;

					// Operators
					case '{': return CreateToken(TokenCode.CurlyOpen);
					case '}': return CreateToken(TokenCode.CurlyClose);
					case '[': return CreateToken(TokenCode.BracketOpen);
					case ']': return CreateToken(TokenCode.BracketClose);
					case '(': return CreateToken(TokenCode.ParenthesisOpen);
					case ')': return CreateToken(TokenCode.ParenthesisClose);
					case '.': return CreateToken(TokenCode.Dot);
					case ',': return CreateToken(TokenCode.Comma);
					case ':': return CreateToken(
						Reader.Match(':') ? TokenCode.DoubleColon :
						TokenCode.Colon);
					case ';': return CreateToken(TokenCode.Semicolon);
					case '+': return CreateToken(
						Reader.Match('=') ? TokenCode.AssignAdd :
						Reader.Match('+') ? TokenCode.Increment :
						TokenCode.Plus);
					case '-': return CreateToken(
						Reader.Match('=') ? TokenCode.AssignSubtract :
						Reader.Match('>') ? TokenCode.RightArrow :
						Reader.Match('-') ? TokenCode.Decrement :
						TokenCode.Minus);
					case '*': return CreateToken(
						Reader.Match('=') ? TokenCode.AssignMultiply :
						TokenCode.Multiply);
					case '/': return CreateToken(
						Reader.Match('=') ? TokenCode.AssignDivide :
						TokenCode.Divide);
					case '%': return CreateToken(
						Reader.Match('=') ? TokenCode.AssignModulus : 
						TokenCode.Modulus);
					case '&': return CreateToken(
						Reader.Match('=') ? TokenCode.AssignBitwiseAnd :
						Reader.Match('&') ? TokenCode.LogicalAnd : TokenCode.BitwiseAnd);
					case '|': return CreateToken(
						Reader.Match('=') ? TokenCode.AssignBitwiseOr :
						Reader.Match('|') ? TokenCode.LogicalOr : TokenCode.BitwiseOr);
					case '^': return CreateToken(
						Reader.Match('=') ? TokenCode.AssignBitwiseExclusiveOr :
						TokenCode.BitwiseExclusiveOr);
					case '!': return CreateToken(
						Reader.Match('=') ? TokenCode.CompareNotEqual :
						TokenCode.LogicalNot);
					case '~': return CreateToken(TokenCode.BitwiseNot);
					case '=': return CreateToken(
						Reader.Match('>') ? TokenCode.Closure :
						Reader.Match('=') ? TokenCode.CompareEqual :
						TokenCode.Assign);
					case '<': return CreateToken(
						Reader.Match('<') ?
							(Reader.Match('=') ? TokenCode.AssignShiftLeft :
							TokenCode.ShiftLeft) :
						Reader.Match('=') ? TokenCode.CompareLessOrEqual :
						TokenCode.CompareLess);
					case '>': return CreateToken(
						Reader.Match('>') ?
							(Reader.Match('=') ? TokenCode.AssignShiftRight :
							TokenCode.ShiftRight) :
						Reader.Match('=') ? TokenCode.CompareGreaterOrEqual :
						TokenCode.CompareGreater);
					case '?': return CreateToken(
						Reader.Match('?') ? TokenCode.IfNull :
						TokenCode.Trinary);



					case '@': return ReadIdentifier(true);

					// Make the common identifiers a tiny bit faster.
					case 'a': case 'b': case 'c': case 'd': case 'e': case 'f': case 'g': case 'h': case 'i': case 'j': case 'k': case 'l': case 'm': case 'n': case 'o': case 'p': case 'q': case 'r': case 's': case 't': case 'u': case 'v': case 'w': case 'x': case 'y': case 'z':
					case 'A': case 'B': case 'C': case 'D': case 'E': case 'F': case 'G': case 'H': case 'I': case 'J': case 'K': case 'L': case 'M': case 'N': case 'O': case 'P': case 'Q': case 'R': case 'S': case 'T': case 'U': case 'V': case 'W': case 'X': case 'Y': case 'Z':
					case '_': case '\\':
						Reader.Point = Start;
						return ReadIdentifier(false);

					default:
						if (ch == '\\' || IsIdentifierStartCharacter(ch)) {
							Reader.Point = Start;
							return ReadIdentifier(false);
						}

						ErrorUnknownCharacter(ch);
						break;
				}
			}
		}

		enum KeywordType { Keyword, SystemType, Literal }

		struct KeywordValue {
			public readonly KeywordType Type;
			public TokenCode Keyword;
			public object Literal;
			public Type SystemType;

			KeywordValue(TokenCode keyword) : this() { Type = KeywordType.Keyword; Keyword = keyword; }
			KeywordValue(object literal) : this() { Type = KeywordType.Literal; Literal = literal; }
			KeywordValue(Type systemType) : this() { Type = KeywordType.SystemType; SystemType = systemType; }

			public static implicit operator KeywordValue(TokenCode tokenCode) { return new KeywordValue(tokenCode); }
			public static implicit operator KeywordValue(bool literal) { return new KeywordValue(literal); }
			public static implicit operator KeywordValue(Type systemType) { return new KeywordValue(systemType); }
		}

		readonly static Dictionary<string, KeywordValue> Keywords = new Dictionary<string, KeywordValue>() {
			// Keywords
			{ "abstract", TokenCode.Abstract }, { "as", TokenCode.As }, { "base", TokenCode.Base }, { "break", TokenCode.Break }, { "case", TokenCode.Case }, { "catch", TokenCode.Catch }, { "checked", TokenCode.Checked }, { "class", TokenCode.Class }, { "const", TokenCode.Const }, { "continue", TokenCode.Continue }, { "default", TokenCode.Default }, { "delegate", TokenCode.Delegate }, { "do", TokenCode.Do }, { "else", TokenCode.Else }, { "enum", TokenCode.Enum }, { "event", TokenCode.Event }, { "explicit", TokenCode.Explicit }, { "finally", TokenCode.Finally }, { "fixed", TokenCode.Fixed }, { "for", TokenCode.For }, { "foreach", TokenCode.Foreach }, { "goto", TokenCode.Goto }, { "if", TokenCode.If }, { "implicit", TokenCode.Implicit }, { "in", TokenCode.In }, { "interface", TokenCode.Interface }, { "internal", TokenCode.Internal }, { "is", TokenCode.Is }, { "lock", TokenCode.Lock }, { "namespace", TokenCode.Namespace }, { "new", TokenCode.New }, { "operator", TokenCode.Operator }, { "out", TokenCode.Out }, { "override", TokenCode.Override }, { "params", TokenCode.Params }, { "private", TokenCode.Private }, { "protected", TokenCode.Protected }, { "public", TokenCode.Public }, { "readonly", TokenCode.Readonly }, { "ref", TokenCode.Ref }, { "return", TokenCode.Return }, { "sealed", TokenCode.Sealed }, { "sizeof", TokenCode.Sizeof }, { "stackalloc", TokenCode.Stackalloc }, { "static", TokenCode.Static }, { "struct", TokenCode.Struct }, { "switch", TokenCode.Switch }, { "this", TokenCode.This }, { "throw", TokenCode.Throw }, { "try", TokenCode.Typeof }, { "unchecked", TokenCode.Unchecked }, { "unsafe", TokenCode.Unsafe }, { "using", TokenCode.Using }, { "virtual", TokenCode.Virtual }, { "volatile", TokenCode.Volatile }, { "while", TokenCode.While },

			// System types
			{ "bool", typeof(Boolean) },
			{ "byte", typeof(Byte) },
			{ "char", typeof(Char) },
			{ "decimal", typeof(Decimal) },
			{ "double", typeof(Double) },
			{ "float", typeof(Single) },
			{ "int", typeof(Int32) },
			{ "long", typeof(Int64) },
			{ "object", typeof(Object) },
			{ "sbyte", typeof(SByte) },
			{ "short", typeof(Int16) },
			{ "string", typeof(String) },
			{ "uint", typeof(UInt32) },
			{ "ulong", typeof(UInt64) },
			{ "ushort", typeof(UInt16) },
			{ "void", typeof(void) },

			// Literals
			{ "true", true },
			{ "false", false },
			{ "null", null },
		};

		protected Token ReadIdentifier(bool preventKeyword) {
			MarkerPoint start = Reader.Point;
			StringBuilder builder = StringBuilder;

			builder.Clear();
			if (!ReadIdentifierCharacter(builder, ref preventKeyword, true))
				throw new NotImplementedException();
			while (ReadIdentifierCharacter(builder, ref preventKeyword, false)) ;

			string id = builder.ToString();

			if (!preventKeyword) {
				KeywordValue value;

				if (Keywords.TryGetValue(id, out value)) {
					switch (value.Type) {
						case KeywordType.Keyword: return CreateToken(value.Keyword);
						case KeywordType.Literal: return CreateToken(TokenCode.Literal, value);
						case KeywordType.SystemType: return CreateToken(TokenCode.TypeLiteral, value.SystemType);
						default: throw new NotImplementedException();
					}
				}
			}
			return CreateToken(TokenCode.Identifier, string.Intern(id));
		}

		protected bool ReadIdentifierCharacter(StringBuilder builder, ref bool preventKeyword, bool start) {
			MarkerPoint reset = Reader.Point;
			char value = Reader.Next();

			if (value == '\\') // Unicode character escapes
				throw new NotImplementedException();

			bool match = start ? IsIdentifierStartCharacter(value) : IsIdentifierPartCharacter(value);

			if (!match)
				Reader.Point = reset;
			else
				builder.Append(value);

			return match;
		}
	}
}
