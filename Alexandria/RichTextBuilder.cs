using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria {
	public class RichTextBuilder {
		static readonly Dictionary<RichTextUnderlineStyle, string> UnderlineStyleCodes = new Dictionary<RichTextUnderlineStyle, string>() {
				{ RichTextUnderlineStyle.None, @"\ul0 " },
				{ RichTextUnderlineStyle.Continuous, @"\ul " },
				{ RichTextUnderlineStyle.Dot, @"\ul\uld " },
				{ RichTextUnderlineStyle.Dash, @"\ul\uldash " },
				{ RichTextUnderlineStyle.DashDot, @"\ul\uldash\uldashd " },
				{ RichTextUnderlineStyle.DashDotDot, @"\ul\uldash\uldashd\uldashdd " },
				{ RichTextUnderlineStyle.Double, @"\ul\uldb " },
				{ RichTextUnderlineStyle.HeavyWave, @"\ul\ulhwave " },
				{ RichTextUnderlineStyle.LongDash, @"\ul\ulldash " },
				{ RichTextUnderlineStyle.Thick, @"\ul\ulth " },
				{ RichTextUnderlineStyle.ThickDot, @"\ul\ulth\ulthd " },
				{ RichTextUnderlineStyle.ThickDash, @"\ul\ulth\ulthdash " },
				{ RichTextUnderlineStyle.ThickDashDot, @"\ul\ulth\ulthdash\ulthdashd " },
				{ RichTextUnderlineStyle.ThickDashDotDot, @"\ul\ulthdash\ulthdashd\ulthdashdd " },
				{ RichTextUnderlineStyle.DoubleWave, @"\ul\ulthwave\uldbwave " },
				{ RichTextUnderlineStyle.Word, @"\ul\ulw " },
				{ RichTextUnderlineStyle.Wave, @"\ul\ulwave " },
			};

		readonly StringBuilder Prefix = new StringBuilder();
		readonly StringBuilder FontTable = new StringBuilder();
		readonly StringBuilder ColorTable = new StringBuilder();

		readonly StringBuilder Content = new StringBuilder();
		readonly StringBuilder Suffix = new StringBuilder();

		float CurrentFontSize;
		Font CurrentFont;
		Color CurrentForegroundColor;
		RichTextUnderlineStyle CurrentUnderlineStyle;

		readonly List<Color> UsedColors = new List<Color>();
		readonly List<Font> UsedFonts = new List<Font>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Font font;

		public Font Font {
			get { return font; }

			set {
				font = font ?? Control.DefaultFont;
				FontSize = font.SizeInPoints;
			}
		}

		public float FontSize { get; set; }

		public Color ForegroundColor { get; set; }

		public bool IsUnderlined { get; set; }

		public RichTextUnderlineStyle UnderlineStyle { get; set; }

		public RichTextBuilder() {
			Prefix.Append(@"{\rtf1\ansi");
			FontTable.Append(@"{\fonttbl");
			Suffix.Append(@"\par}");
			Font = null; // Set to default.
			ForegroundColor = CurrentForegroundColor = Color.Black;
		}

		public void Append(char value) {
			switch (value) {
				case '\\': Content.Append(@"\\"); break;
				case '{': Content.Append(@"\{"); break;
				case '}': Content.Append(@"\}"); break;
				case '\n': Content.Append("\\par\r\n"); break;
				case '\r': break;

				default:
					if(value > 127) {
						Content.Append(@"\u");
						Content.Append((int)value);
						Content.Append('?');
					} else
						Content.Append(value);
					break;
			}
		}

		public void Append(string text) {
			if (string.IsNullOrEmpty(text))
				return;

			UpdateFontFormattingProperties();

			if (IsStringSafe(text))
				Content.Append(text);
			else {
				// "Ensure" there's enough space; 17/16 expansion is completely arbitrary, but likely more than enough for English text without being so much that it causes problems. Non-English text can deal, it's not an optimization that works everywhere.
				Content.EnsureCapacity(Content.Length + text.Length * 17 / 16);

				foreach (char ch in text)
					Append(ch);
			}
		}

		public void Append(object value) { if (value != null) Append(value.ToString()); }

		public void AppendFormat(string format, params object[] args) { Append(string.Format(format, args)); }
		public void AppendFormat(string format, object arg0) { Append(string.Format(format, arg0)); }
		public void AppendFormat(string format, object arg0, object arg1) { Append(string.Format(format, arg0, arg1)); }
		public void AppendFormat(string format, object arg0, object arg1, object arg2) { Append(string.Format(format, arg0, arg1, arg2)); }
		public void AppendFormat(IFormatProvider provider, string format, params object[] args) { Append(string.Format(provider, format, args)); }

		void EmitCodeIndex(string code, int index) {
			EmitCodeIndex(Content, code, index);
		}

		static void EmitCodeIndex(StringBuilder builder, string code, int index) {
			builder.EnsureCapacity(builder.Length + 8 + code.Length);
			builder.Append('\\');
			builder.Append(code);
			builder.Append(index);
			builder.Append(' ');
		}

		void EmitSData(IList<byte> data) { EmitSData(Content, data); }
		void EmitSData(IList<byte> data, int offset, int length) { EmitSData(Content, data, offset, length); }

		static void EmitSData(StringBuilder builder, IList<byte> data) { EmitSData(builder, data, 0, data.Count); }

		const string HexCodes = "0123456789abcdef";

		static void EmitSData(StringBuilder builder, IList<byte> data, int offset, int length) {
			builder.EnsureCapacity(builder.Length + length * 2);

			for (int index = 0; index < length; index++) {
				byte value = data[offset + index];
				if (index > 0 && index % 64 == 0)
					builder.Append('\n');
				builder.Append(HexCodes[value >> 4]);
				builder.Append(HexCodes[value & 15]);
			}
		}

		void EmitColorProperty(string code, ref Color property, Color value) {
			if (property == value)
				return;
			property = value;
			EmitCodeIndex(code, GetColorIndex(value));
		}

		int GetColorIndex(Color color) {
			color = Color.FromArgb(color.R, color.G, color.B);
			int index = UsedColors.IndexOf(color);

			if (index < 0) {
				index = UsedColors.Count;
				UsedColors.Add(color);

				if(ColorTable.Length == 0)
					ColorTable.Append(@"{\colortbl ");
				ColorTable.AppendFormat(@"\red{0}\green{1}\blue{2};", color.R, color.G, color.B);
			}

			return index;
		}

		void UpdateFontFormattingProperties() {
			if (CurrentFont != font) {
				int index = UsedFonts.IndexOf(CurrentFont);
				if (index < 0) {
					index = UsedFonts.Count;
					UsedFonts.Add(font);
					FontTable.AppendFormat(@"{{\f{0}", index);

					// Try to guess the font family
					if (font.FontFamily == FontFamily.GenericMonospace)
						FontTable.Append(@"\fmodern");
					else if (font.FontFamily == FontFamily.GenericSansSerif || font.Name == "Microsoft Sans Serif")
						FontTable.Append(@"\fswiss"); // Proportionally spaced sans serif fonts
					else if (font.FontFamily == FontFamily.GenericSerif)
						FontTable.Append(@"\froman"); // Proportionally spaced serif fonts
					else
						FontTable.Append(@"\fnil");

					if (font.IsSystemFont)
						FontTable.AppendFormat(@"{{\*\falt {0}}}", font.SystemFontName);

					// Default charset; hopefully that means Unicode.
					FontTable.Append(@"\fcharset1");

					FontTable.AppendFormat(@" {0};}}", font.Name);
				}

				EmitCodeIndex("f", index);
				CurrentFont = font;
			}

			if (CurrentFontSize != FontSize) {
				CurrentFontSize = FontSize;
				EmitCodeIndex("fs", (int)(FontSize * 2));
			}

			RichTextUnderlineStyle underlineStyle = IsUnderlined ? UnderlineStyle : RichTextUnderlineStyle.None;

			if (underlineStyle != CurrentUnderlineStyle) {
				CurrentUnderlineStyle = underlineStyle;
				Content.Append(UnderlineStyleCodes[underlineStyle]);
			}

			EmitColorProperty("cf", ref CurrentForegroundColor, ForegroundColor);
		}

		static bool IsStringSafe(string text) {
			foreach (char ch in text) {
				switch (ch) {
					case '\r':
					case '\n':
					case '\\':
					case '{':
					case '}':
						return false;

					default:
						if (ch > 127)
							return false;
						break;
				}
			}
			return true;
		}

		public override string ToString() {
			StringBuilder sum = new StringBuilder(Prefix.Length + FontTable.Length + ColorTable.Length + Content.Length + Suffix.Length + 8);
			sum.Append(Prefix);

			if (FontTable.Length > 0) {
				sum.Append(FontTable);
				sum.Append(@"}");
			}

			if (ColorTable.Length > 0) {
				sum.Append(ColorTable);
				sum.Append(@"}");
			}

			// End of prefix
			sum.Append(@"\pard ");

			sum.Append(Content);
			sum.Append(Suffix);
			return sum.ToString();
		}
	}

	public enum RichTextUnderlineStyle {
		/// <summary>No underline (\ul0 or \ulnone in RTF).</summary>
		None,

		/// <summary>Continuous underline (\ul in RTF).</summary>
		Continuous,

		/// <summary>Dotted underline (\uld in RTF).</summary>
		Dot,

		/// <summary>Dashed underline (\uldash in RTF).</summary>
		Dash,

		/// <summary>Dash-dotted underline (\uldashd in RTF).</summary>
		DashDot,

		/// <summary>Dash-dot-dotted underline (\uldashdd in RTF).</summary>
		DashDotDot,

		/// <summary>Double underline (\uldb in RTF).</summary>
		Double,

		/// <summary>Heavy wave underline (\ulhwave in RTF).</summary>
		HeavyWave,

		/// <summary>Long dashed underline (\ulldash in RTF).</summary>
		LongDash,

		/// <summary>Thick underline (\ulth in RTF).</summary>
		Thick,

		/// <summary>Thick dotted underline (\ulthd in RTF).</summary>
		ThickDot,

		/// <summary>Thick dashed underline (\ulthdash in RTF).</summary>
		ThickDash,

		/// <summary>Thick dash-dotted underline (\ulthdashd in RTF).</summary>
		ThickDashDot,

		/// <summary>Thick dash-dot-dotted underline (\ulthdashdd in RTF).</summary>
		ThickDashDotDot,

		/// <summary>Double wave underline (\ululdbwave in RTF).</summary>
		DoubleWave,

		/// <summary>Word underline (\ulw in RTF).</summary>
		Word,

		/// <summary>Wave underline (\ulwave in RTF).</summary>
		Wave,
	}
}
