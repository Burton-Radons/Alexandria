using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>An error that has been recorded while loading an <see cref="Asset"/>. This is collected in <see cref="AssetLoader"/>'s <see cref="AssetLoader.Errors"/> property.</summary>
	public class AssetLoadError {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly string message;

		/// <summary>Get the load info. The <see cref="AssetLoader.Reader"/>/<see cref="AssetLoader.Stream"/> may be closed.</summary>
		public AssetLoader Loader { get; private set; }

		/// <summary>Get a description of the problem. This may be localized.</summary>
		public virtual string Message { get { return message; } }

		/// <summary>Get the position the <see cref="AssetLoader.Stream"/> was at when this error was generated.</summary>
		public long Offset { get; private set; }

		/// <summary>Get the offset in hexadecimal.</summary>
		public string OffsetHex {
			get {
				return Math.Abs(Offset) < 10 ? Offset.ToString() : string.Format("{1}{0}/{0:X}h", Math.Abs(Offset), Offset < 0 ? "-" : "");
			}
		}

		/// <summary>Initialise the load error.</summary>
		/// <param name="loader"></param>
		/// <param name="offset"></param>
		/// <param name="message"></param>
		public AssetLoadError(AssetLoader loader, long? offset, string message) {
			Loader = loader;
			Offset = offset.HasValue ? offset.Value : loader.Position;
			this.message = message;
		}

		/// <summary>Convert to a string representation of the error.</summary>
		/// <returns></returns>
		public override string ToString() {
			string message = "";

			try {
				message = Message;
			} catch (Exception exception) {
				message = "Message exception: " + exception;
			}

			return string.Format("{0} (offset {1}/{1:X}h): {2}", Loader.Name, Offset, message);
		}

		#region Inner types

		/// <summary>
		/// Invalid data were encountered.
		/// </summary>
		public class InvalidData : AssetLoadError {
			/// <summary>A description of the expected value.</summary>
			public string Expected { get; private set; }

			/// <summary>A description of the received value.</summary>
			public string Received { get; private set; }

			/// <summary>Describes the problem.</summary>
			public override string Message { get { return string.Format(Properties.Resources.AssetLoadError_InvalidData, Expected, Received); } }

			/// <summary>Initialise the error.</summary>
			/// <param name="info"></param>
			/// <param name="offset"></param>
			/// <param name="expected"></param>
			/// <param name="received"></param>
			public InvalidData(AssetLoader info, long offset, string expected, string received)
				: base(info, offset, null) {
				Expected = expected;
				Received = received;
			}
		}

		/// <summary>The file was not as big as it needed to be.</summary>
		public class UnexpectedEndOfFile : AssetLoadError {
			/// <summary>Describes the problem.</summary>
			public override string Message { get { return Properties.Resources.AssetLoadError_UnexpectedEndOfFile; } }

			/// <summary>Initialises the object.</summary>
			/// <param name="info"></param>
			public UnexpectedEndOfFile(AssetLoader info) : base(info, null, null) { }
		}

		/// <summary>The file position was not where it should be.</summary>
		public class UnexpectedPosition : AssetLoadError {
			/// <summary>The expected position.</summary>
			public long Expected { get; private set; }

			/// <summary>The actual position.</summary>
			public long Received { get; private set; }

			/// <summary>Describes the problem.</summary>
			public override string Message { get { return string.Format(Properties.Resources.AssetLoadError_UnexpectedPosition, Expected, Received); } }

			/// <summary>Initialises the error.</summary>
			/// <param name="loader"></param>
			/// <param name="expected"></param>
			public UnexpectedPosition(AssetLoader loader, long expected)
				: base(loader, null, null) {
				Expected = expected;
				Received = loader.Position;
			}
		}

		#endregion Inner types
	}
}
