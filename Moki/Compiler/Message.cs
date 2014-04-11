using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compiler {
	public abstract class Message {
		public Marker Marker { get; private set; }
		public string MessageText { get; private set; }

		public Message(Marker marker, string messageText) {
			Marker = marker;
			MessageText = messageText;
		}

		public override string ToString() {
			return string.Format("{0} {1}: {2}", GetType().Name, Marker, MessageText);
		}
	}

	public class Warning : Message {
		public Warning(Marker marker, string messageText) : base(marker, messageText) { }
	}

	public class Error : Message {
		public Error(Marker marker, string messageText) : base(marker, messageText) { }
	}

	public class Fatal : Error {
		public Fatal(Marker marker, string messageText) : base(marker, messageText) { }
	}
}
