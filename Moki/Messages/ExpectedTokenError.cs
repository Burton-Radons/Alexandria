using Moki.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Messages {
	public class ExpectedTokenError : Error {
		public TokenCode Expected { get; private set; }

		public TokenCode ReceivedTokenCode { get; private set; }

		public string ReceivedTokenName { get; private set; }

		public ExpectedTokenError(Token received, TokenCode expected)
			: this(received, expected, "Expected " + expected + " but received " + received.Name + ".") {
		}

		public ExpectedTokenError(Token received, TokenCode expected, string messageText)
			: base(received.Marker, messageText) {
			Expected = expected;
			ReceivedTokenCode = received.Code;
			ReceivedTokenName = received.Name;
		}
	}
}
