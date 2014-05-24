using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare;
using Glare.Internal;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using Glare.Framework;
using Glare.Assets;

namespace Alexandria.Engines.GoldBox.Resources {
	public class Script : Asset {
		public const int AddressOffset = 0x7FFE;

		public long CodeAddressA { get; private set; }
		public long CodeAddressB { get; private set; }
		public long CodeAddressC { get; private set; }
		public long CodeAddressD { get; private set; }
		public long CodeAddressE { get; private set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Codex<ScriptInstruction> InstructionsMutable = new Codex<ScriptInstruction>();

		public string ExceptionEnd { get; private set; }

		public ReadOnlyCodex<ScriptInstruction> Instructions { get { return InstructionsMutable; } }

		string CodeAddressName(long address) {
			if (address == CodeAddressA)
				return "CodeAddressA";
			if (address == CodeAddressB)
				return "CodeAddressB";
			if (address == CodeAddressC)
				return "CodeAddressC";
			if (address == CodeAddressD)
				return "CodeAddressD";
			if (address == CodeAddressE)
				return "CodeAddressE";
			return null;
		}

		public Script(AssetManager manager, BinaryReader reader, string name)
			: base(manager, name) {
			long end = reader.BaseStream.Length;

			ExceptionEnd = "";

			reader.Require(0x01011388);
			CodeAddressA = reader.ReadUInt16();
			reader.Require((ushort)0x0101);
			CodeAddressB = reader.ReadUInt16();
			reader.Require((ushort)0x0101);
			CodeAddressC = reader.ReadUInt16();
			reader.Require((ushort)0x0101);
			CodeAddressD = reader.ReadUInt16();
			reader.Require((ushort)0x0101);
			CodeAddressE = reader.ReadUInt16();

			while (reader.BaseStream.Position < end) {
				try {
					var instruction = ScriptInstruction.Read(this, reader);
					InstructionsMutable.Add(instruction);
				} catch (Exception exception) {
					ExceptionEnd = string.Format("\r\n{0:X}: Exception: {1}\r\nStopping\r\n", reader.BaseStream.Position, exception);
					break;
				}
			}

			Link();
		}

		public override System.Windows.Forms.Control Browse() {
			RichTextBuilder builder = new RichTextBuilder();
			bool indentNext = false;

			//builder.Append(@"{\rtf\ansi{\fonttbl\f0\fswiss Helvetica;}\f0\par ");
			builder.UnderlineStyle = RichTextUnderlineStyle.Double;

			foreach (ScriptInstruction instruction in Instructions) {
				string codeName = CodeAddressName(instruction.Address);

				if (codeName != null)
					builder.AppendFormat("\n==============================\n{0}\n==============================\n", codeName);

				if (instruction.IsTarget)
					builder.IsUnderlined = true;
				builder.AppendFormat("{0:X}h", instruction.Address);
				if (instruction.IsTarget)
					builder.IsUnderlined = false;
				builder.Append(":\t");

				if (indentNext)
					builder.Append("\t");
				instruction.ToRichText(builder);
				//builder.Append(instruction.ToString());
				builder.Append("\n");
				if (instruction.IsBranch && !indentNext)
					builder.Append("\n");
				indentNext = instruction.IsTest;
			}

			builder.Append(ExceptionEnd);
			var output = builder.ToString();

			BetterRichTextBox box = new BetterRichTextBox() {
				ReadOnly = false,
				Multiline = true,
				//ScrollBars = ScrollBars.Vertical,
				Rtf = output,
				WordWrap = false,
			};

			Font normal = box.Font;
			var underlined = new Font(normal, FontStyle.Underline);
			bool recurse = false;

			box.SelectionChanged += (object sender, EventArgs args) => {
				if (recurse)
					return;

				try {
					Point scrollPosition = box.ScrollPosition;
					int resetStart = box.SelectionStart, resetLength = box.SelectionLength;
					box.BeginUpdate();

					recurse = true;
					var start = box.SelectionStart;
					string text = box.Text;

					if (!IsIdentifier(text, start) && !IsIdentifier(text, start - 1))
						return;
					while (IsIdentifier(text, start - 1))
						start--;

					int end = box.SelectionStart;
					while (IsIdentifier(text, end))
						end++;

					string searchText = text.Substring(start, end - start);

					box.SelectAll();
					//box.SelectionFont = normal;
					box.SelectionBackColor = Color.Transparent;

					start = 0;
					while (true) {
						start = text.IndexOf(searchText, start);
						if (start < 0)
							break;

						if (!IsIdentifier(text, start - 1) && !IsIdentifier(text, start + searchText.Length)) {
							box.Select(start, searchText.Length);
							//box.SelectionFont = underlined;
							box.SelectionBackColor = Color.Yellow;
						}

						start += searchText.Length;
					}

					box.Select(resetStart, resetLength);
					box.ScrollPosition = scrollPosition;
				} finally {
					box.EndUpdate();
					box.Invalidate();
					recurse = false;
				}
			};

			box.LinkClicked += (sender, args) => {
				throw new Exception();
			};


			return box;
		}

		static bool IsIdentifier(string text, int index) {
			if (index < 0 || index >= text.Length)
				return false;
			return char.IsLetterOrDigit(text[index]);
		}

		void Link() {
			foreach (ScriptInstruction instruction in Instructions)
				instruction.Link();
		}
	}

	class ScriptFormat : AssetFormat {
		public ScriptFormat(Engine engine)
			: base(engine, typeof(Script), canLoad: true) {
		}

		static bool CheckAddress(BinaryReader reader, long length) {
			int address = reader.ReadUInt16() - Script.AddressOffset;
			return address >= 22 && address < length;
		}

		public override LoadMatchStrength LoadMatch(AssetLoader info) {
			var reader = info.Reader;
			long length = reader.BaseStream.Length;
			if (length < 22)
				return LoadMatchStrength.None;
			if (reader.ReadInt32() != 0x01011388 ||
				!CheckAddress(reader, length) ||
				reader.ReadInt16() != 0x0101 ||
				!CheckAddress(reader, length) ||
				reader.ReadInt16() != 0x0101 ||
				!CheckAddress(reader, length) ||
				reader.ReadInt16() != 0x0101 ||
				!CheckAddress(reader, length))
				return LoadMatchStrength.None;
			return LoadMatchStrength.Medium;
		}

		public override Asset Load(AssetLoader info) {
			return new Script(Manager, info.Reader, info.Name);
		}
	}
}
