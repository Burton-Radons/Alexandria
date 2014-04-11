using Moki.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compiler {
	/// <summary>
	/// Base class to <see cref="Lexer"/> and <see cref="Parser"/> that provides easy methods for sending messages to the <see cref="MessageHandler"/>.
	/// </summary>
	public abstract class MessageProvider {

		/// <summary>Get the <see cref="Moki.Compiler.MessageHandler"/> in use.</summary>
		public abstract MessageHandler MessageHandler { get; }

		protected void SendMessage(Message message) { MessageHandler.Handle(message); }
	}
}
