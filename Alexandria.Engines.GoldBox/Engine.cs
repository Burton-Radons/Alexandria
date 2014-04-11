using Alexandria.Engines.GoldBox.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.GoldBox {
	public class Engine : Alexandria.Engine {
		public Engine(Plugin plugin)
			: base(plugin) {
			AddFormat(new ArchiveFormat(this));
			AddFormat(new ImageFormat(this));
			AddFormat(new ScriptFormat(this));
		}

		[ThreadStatic]
		static char[] Codes = new char[1024];

		const string DecodeString = "\0ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_\x20!\"#$%&'()*+,-./0123456789:;<=>?";

		public static string ReadCodedString(BinaryReader reader, int maximumBytes = int.MaxValue) {
			Stream stream = reader.BaseStream;
			int length = 0;

			while (true) {
				int a = stream.ReadByte();
				if (a < 0)
					break;

				char code1 = Codes[length++] = DecodeString[(a & 0xFC) >> 2];
				if (code1 == 0 || --maximumBytes == 0)
					break;

				int b = stream.ReadByte();
				if (b < 0)
					break;

				char code2 = Codes[length++] = DecodeString[((a & 0x03) << 4) | ((b & 0xF0) >> 4)];
				if (code2 == 0 || --maximumBytes == 0)
					break;

				int c = stream.ReadByte();
				if (c < 0)
					break;

				char code3 = Codes[length++] = DecodeString[((b & 0x0F) << 2) | ((c & 0xC0) >> 6)];
				char code4 = DecodeString[c & 0x3F];
				if (code3 == 0)
					break;

				Codes[length++] = code4;
				if (code4 == 0 || --maximumBytes == 0)
					break;
			}

			//if (Math.Ceiling(length * 3 / 4.0) != codeLength)
			//throw new Exception();
			return new string(Codes, 0, length - 1);
		}
	}
}
