using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compiler {
	public class MessageHandler {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ObservableCollection<Error> ErrorsMutable = new ObservableCollection<Error>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ObservableCollection<Message> MessagesMutable = new ObservableCollection<Message>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ObservableCollection<Warning> WarningsMutable = new ObservableCollection<Warning>();

		public ReadOnlyObservableCollection<Error> Errors { get; private set; }

		public ReadOnlyObservableCollection<Message> Messages { get; private set; }

		public ReadOnlyObservableCollection<Warning> Warnings { get; private set; }

		public MessageHandler() {
			Errors = new ReadOnlyObservableCollection<Error>(ErrorsMutable);
			Messages = new ReadOnlyObservableCollection<Message>(MessagesMutable);
			WarningsMutable = new ObservableCollection<Warning>(WarningsMutable);
		}

		public virtual void Handle(Message message) {
			if (message == null)
				throw new ArgumentNullException("message");

			MessagesMutable.Add(message);
			if (message is Warning)
				WarningsMutable.Add((Warning)message);
			if (message is Error)
				ErrorsMutable.Add((Error)message);
			Console.WriteLine(message);
		}
	}
}
