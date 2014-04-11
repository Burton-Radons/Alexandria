using Moki.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Messages {
	/// <summary>
	/// An unknown character has been encountered in the file.
	/// </summary>
	public class UnknownCharacterError : Error {
		public char UnknownCharacter { get; private set; }

		public UnknownCharacterError(Marker marker, string text, char unknownCharacter) : base(marker, text) { UnknownCharacter = unknownCharacter; }
		public UnknownCharacterError(Marker marker, char unknownCharacter) : this(marker, string.Format("Unknown character '{0}' (\\u{1:X4}) encountered.", unknownCharacter, (int)unknownCharacter), unknownCharacter) { }
	}
}
