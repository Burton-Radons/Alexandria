using Moki.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Messages {
	public class ExpectedNodeError : Error {
		public Type ExpectedNodeType { get; private set; }

		public TokenCode ReceivedInsteadCode { get; private set; }

		public string ReceivedInsteadCodeName { get; private set; }

		public ExpectedNodeError(Type expectedNodeType, Token receivedInstead) :
			this(expectedNodeType, receivedInstead, "Expected " + expectedNodeType.Name + " but received " + receivedInstead.Name + ".") { }

		public ExpectedNodeError(Type expectedNodeType, Token receivedInstead, string text)
			: base(receivedInstead.Marker, text) {
			ReceivedInsteadCode = receivedInstead.Code;
			ReceivedInsteadCodeName = receivedInstead.Name;
		}
	}
}
