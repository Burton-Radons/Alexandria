using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// An effect file for Dark Souls (extension ".ffx").
	/// </summary>
	public class Effect : Asset {
		/// <summary>
		/// A magic string that all effect files start with.
		/// </summary>
		public const string Magic = "FXR\0";

		internal const int ContentStart = 0x20;

		/// <summary>Get the list of tokens comprising the file.</summary>
		public Codex<EffectToken> Tokens { get; private set; }

		/// <summary>Get the list of instructions for the file.</summary>
		public Codex<EffectInstruction> Instructions { get; private set; }

		internal Effect(AssetLoader loader)
			: base(loader) {
			var reader = loader.Reader;

			loader.ExpectMagic(Magic);
			loader.Expect(0x10000);
			loader.Expect(ContentStart);

			int offsetOffset = reader.ReadInt32();
			int pointerCount = reader.ReadInt32();
			HashSet<int> pointers = new HashSet<int>();
			int functionCount = reader.ReadInt32();
			HashSet<int> functions = new HashSet<int>();

			loader.Position = offsetOffset;
			for (int index = 0; index < pointerCount; index++)
				pointers.Add(reader.ReadInt32());
			for (int index = 0; index < functionCount; index++)
				functions.Add(reader.ReadInt32());

			loader.Position = ContentStart;
			int tokenCount = (offsetOffset - ContentStart) / 4;
			Tokens = new Codex<EffectToken>(tokenCount);
			for (int index = 0; index < tokenCount; index++)
				Tokens.Add(new EffectToken(reader, pointers, functions));
			for (int index = 0; index < tokenCount; index++)
				Tokens[index].Link(Tokens);

			Instructions = new Codex<EffectInstruction>();
			for (int index = 0; index < tokenCount; )
				Instructions.Add(new EffectInstruction(Tokens, ref index));
		}

		static bool IsIdentifier(string text, int index) {
			if (index < 0 || index >= text.Length)
				return false;
			return char.IsLetterOrDigit(text[index]);
		}

		/// <summary>Produce a rich text document that examines the file.</summary>
		/// <returns></returns>
		public override System.Windows.Forms.Control Browse(Action<double> progressUpdateCallback = null) {
			var box = new BetterRichTextBox() {
				Multiline = true,
				ReadOnly = true,
				WordWrap = true,
			};

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

			RichTextBuilder builder = new RichTextBuilder();

			foreach (EffectInstruction instruction in Instructions) {
				instruction.ToRichText(builder);
				builder.Append("\n");
			}

			box.Rtf = builder.ToString();
			return box;
		}

		internal static string FormatAddress(int address) {
			return string.Format("{0:X3}h", address);
		}
	}

	/// <summary>Consists of an opcode and a set of operands.</summary>
	public class EffectInstruction {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Codex<EffectToken> OperandsMutable = new Codex<EffectToken>();

		/// <summary>Get the address of the <see cref="Opcode"/>.</summary>
		public int Address { get { return Opcode.Address; } }

		/// <summary>Get an appropriate string representation of the <see cref="Address"/>.</summary>
		public string AddressString { get { return Opcode.AddressString; } }

		/// <summary>Get the opcode token.</summary>
		public EffectToken Opcode { get; private set; }

		/// <summary>Get the operand tokens.</summary>
		public ReadOnlyCodex<EffectToken> Operands { get { return OperandsMutable; } }

		static readonly Dictionary<int, string> InstructionFormats = new Dictionary<int, string>() {
			{ 0x01, "i" },
			{ 0x02, "pi" },
			{ 0x06, "ppi" },
			{ 0x05, "ppi" },
			{ 0x07, "s" },
			{ 0x09, "ppi" },
			{ 0x0B, "ppi" },
			{ 0x12, "ppi" },
			{ 0x13, "pp" },
			{ 0x25, "ipz" },
			{ 0x26, "ipz" },
			{ 0x3B, "i" },
			{ 0x46, "s" },
			{ 0x4F, "ii" },
			{ 0x51, "ss" },
			{ 0x55, "ss" },
			{ 0x85, "i" }, // Header opcode.
			{ 0x88, "" }, // Data opcode.
		};

		internal EffectInstruction(IList<EffectToken> tokens, ref int index) {
			Opcode = tokens[index++];

			string format;

			if (Opcode.MaybeInt32 && InstructionFormats.TryGetValue(Opcode.AsInt32, out format)) {
				if (Opcode.AsInt32 == 0x85) {
					while (index < tokens.Count && !tokens[index].IsFunction)
						OperandsMutable.Add(tokens[index++]);
				} else if(Opcode.AsInt32 == 0x88) {
					while (index < tokens.Count)
						OperandsMutable.Add(tokens[index++]);
				} else foreach (char operandType in format) {
						if (index >= tokens.Count) {
							Opcode.AddError("Expected '{0}'; end of tokens.", operandType);
							break;
						}

						if (tokens[index].IsFunction) {
							tokens[index].AddError("Last instruction expected '{0}', not a function.", operandType);
							break;
						}

						var operand = tokens[index++];
						OperandsMutable.Add(operand);

						switch (operandType) {
							case 'i':
								if (!operand.MaybeInt32)
									operand.AddError("Expected an integer.");
								break;

							case 'p':
								if (!operand.MaybePointer)
									operand.AddError("Expected a pointer.");
								break;

							case 'z':
								if (!operand.IsZero)
									operand.AddError("Expected zero.");
								break;

							case 's':
								if (!operand.MaybeSingle)
									operand.AddError("Expected a Single.");
								break;
						}
					}
			} else {
				Opcode.AddError("Opcode is unknown or invalid; operands are crap.");
				while (index < tokens.Count && !(tokens[index].MaybeInt32 && InstructionFormats.ContainsKey(tokens[index].AsInt32)) && !tokens[index].IsFunction)
					OperandsMutable.Add(tokens[index++]);
			}
		}

		/// <summary>
		/// Convert to rich text.
		/// </summary>
		/// <param name="builder"></param>
		public void ToRichText(RichTextBuilder builder) {
			builder.AppendFormat("{0}: ", AddressString);
			Opcode.ToRichText(builder);

			for (int index = 0; index < Operands.Count; index++) {
				builder.Append(index == 0 ? " " : ", ");
				Operands[index].ToRichText(builder);
			}
		}

		/// <summary>Convert to a descriptive string.</summary>
		/// <returns></returns>
		public override string ToString() {
			string text = "";

			text += string.Format("{0}: {1}", AddressString, Opcode);

			for (int index = 0; index < Operands.Count; index++) {
				text += index == 0 ? " " : ", ";
				text += Operands[index].ToString();
			}

			return text;
		}
	}

	/// <summary>A four-byte value in an <see cref="Effect"/>.</summary>
	public class EffectToken {
		int intValue;
		float floatValue;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<EffectToken> TargetedByMutable = new Codex<EffectToken>();

		/// <summary>Get the address associated with this token.</summary>
		public int Address { get; private set; }

		/// <summary>Convert the <see cref="Address"/> to the appropriate string format.</summary>
		public string AddressString { get { return Effect.FormatAddress(Address); } }

		/// <summary>Get the category this token is.</summary>
		public EffectTokenCategory Category { get; private set; }

		/// <summary>Convert this token into an integer, casting if necessary.</summary>
		public int AsInt32 {
			get {
				if (Category == EffectTokenCategory.Single)
					return (int)floatValue;
				return intValue;
			}
		}

		/// <summary>Convert this token into a <see cref="Single"/>, casting if necessary.</summary>
		public float AsSingle {
			get {
				if (Category == EffectTokenCategory.Single)
					return floatValue;
				return intValue;
			}
		}

		/// <summary>Get a newline-separated list of errors associated with this effect token, or <c>null</c> if there hasn't been any.</summary>
		public string Errors { get; private set; }

		/// <summary>Get whether this has been identified as a function.</summary>
		public bool IsFunction { get; private set; }

		/// <summary>Get whether there are any <see cref="Errors"/>.</summary>
		public bool IsInvalid { get { return !string.IsNullOrEmpty(Errors); } }

		/// <summary>Get whether this is a target of a pointer token.</summary>
		public bool IsTarget { get { return TargetedByMutable.Count != 0; } }

		/// <summary>Get whether this <see cref="Category"/> is <see cref="EffectTokenCategory.Int32"/> or <see cref="EffectTokenCategory.Zero"/>.</summary>
		public bool MaybeInt32 { get { return Category == EffectTokenCategory.Int32 || Category == EffectTokenCategory.Zero; } }

		/// <summary>Get whether this <see cref="Category"/> is <see cref="EffectTokenCategory.Pointer"/> or <see cref="EffectTokenCategory.Zero"/>.</summary>
		public bool MaybePointer { get { return Category == EffectTokenCategory.Pointer || Category == EffectTokenCategory.Zero; } }

		/// <summary>Get whether this <see cref="Category"/> is <see cref="EffectTokenCategory.Single"/> or <see cref="EffectTokenCategory.Zero"/>.</summary>
		public bool MaybeSingle { get { return Category == EffectTokenCategory.Single || Category == EffectTokenCategory.Zero; } }

		/// <summary>Get whether this <see cref="Category"/> is <see cref="EffectTokenCategory.Int32"/>.</summary>
		public bool IsInt32 { get { return Category == EffectTokenCategory.Int32; } }

		/// <summary>Get whether this <see cref="Category"/> is <see cref="EffectTokenCategory.Pointer"/>.</summary>
		public bool IsPointer { get { return Category == EffectTokenCategory.Pointer; } }

		/// <summary>Get whether this <see cref="Category"/> is <see cref="EffectTokenCategory.Single"/>.</summary>
		public bool IsSingle { get { return Category == EffectTokenCategory.Single; } }

		/// <summary>Get whether this <see cref="Category"/> is <see cref="EffectTokenCategory.Zero"/>.</summary>
		public bool IsZero { get { return Category == EffectTokenCategory.Zero; } }

		/// <summary>Get the <see cref="EffectToken"/>s that target this <see cref="EffectToken"/>; empty if not <see cref="IsTarget"/>.</summary>
		public ReadOnlyCodex<EffectToken> TargetedBy { get { return TargetedByMutable; } }

		internal EffectToken(BinaryReader reader, HashSet<int> pointers, HashSet<int> functions) {
			Address = checked((int)reader.BaseStream.Position);

			intValue = reader.ReadInt32();
			floatValue = new PackedSingle(intValue).SingleValue;

			IsFunction = functions.Contains(Address);

			if (pointers.Contains(Address))
				Category = EffectTokenCategory.Pointer;
			else if ((intValue & 0xFF000000) != 0 && !float.IsNaN(floatValue) && !float.IsInfinity(floatValue))
				Category = EffectTokenCategory.Single;
			else if (intValue == 0)
				Category = EffectTokenCategory.Zero;
			else
				Category = EffectTokenCategory.Int32;
		}

		internal void AddError(string format, params object[] args) {
			string error = string.Format(format, args);

			if (Errors == null)
				Errors = error;
			else
				Errors += "\n" + error;
		}

		internal void Link(IList<EffectToken> list) {
			if (IsPointer) {
				var target = list[(AsInt32 - Effect.ContentStart) / 4];
				if (target.Address != AsInt32)
					throw new InvalidDataException();
				target.TargetedByMutable.Add(this);
			}
		}

		/// <summary>Convert to a rich text form with the builder.</summary>
		/// <param name="builder"></param>
		public void ToRichText(RichTextBuilder builder) {
			if (IsTarget) {
				builder.ForegroundColor = Color.Blue;
				builder.AppendFormat("<{0}> ", AddressString);
			}

			if (IsFunction)
				builder.ForegroundColor = Color.DarkGreen;
			else if (IsPointer)
				builder.ForegroundColor = Color.Purple;
			builder.Append(ToStringValue());

			if (IsInvalid) {
				builder.ForegroundColor = Color.Red;
				builder.Append(" <<" + Errors.Replace("\n", "; ") + ">>");
			}

			builder.ForegroundColor = Color.Black;
		}

		/// <summary>Convert just the value to a string.</summary>
		/// <returns></returns>
		public string ToStringValue() {
			string text;

			if (IsPointer)
				text = string.Format("[{0}]", Effect.FormatAddress(intValue));
			else if (IsSingle) {
				text = string.Format("{0}", AsSingle);
				if (!text.Contains('.') && text.IndexOf("e", StringComparison.InvariantCultureIgnoreCase) < 0)
					text += ".0";
			} else {
				text = string.Format("{0}", AsInt32);
				if (AsInt32 > 15)
					text += string.Format("/{0:X}h", AsInt32);
			}

			return text;
		}

		/// <summary>Convert to a descriptive string.</summary>
		/// <returns></returns>
		public override string ToString() {
			string text = "";

			if (IsTarget)
				text = string.Format("<{0}> ", AddressString);
			text += ToStringValue();

			if (IsInvalid)
				text += " <<" + Errors.Replace("\n", "; ") + ">>";
			return text;
		}
	}

	/// <summary>Specifies what types an <see cref="EffectToken"/> might be.</summary>
	public enum EffectTokenCategory {
		/// <summary>The value is zero, so it could be a zero <see cref="Single"/>, a <c>null</c> pointer, or a zero integer.</summary>
		Zero,

		/// <summary>The value appears to be a <see cref="Single"/>.</summary>
		Single,

		/// <summary>The value is identified as a pointer by the link table.</summary>
		Pointer,

		/// <summary>The value appears to be a regular integer.</summary>
		Int32,
	}

	/// <summary>
	/// Asset format for the <see cref="Effect"/>.
	/// </summary>
	public class EffectFormat : AssetFormat {
		/// <summary>
		/// Initialise the format.
		/// </summary>
		/// <param name="engine"></param>
		public EffectFormat(Engine engine)
			: base(engine, typeof(Effect), canLoad: true, extension: ".ffx") {
		}

		/// <summary>Determine match strength with a loader.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			if (loader.Length < 0x40 || !loader.Reader.MatchMagic(Effect.Magic))
				return LoadMatchStrength.None;
			return LoadMatchStrength.Medium;
		}

		/// <summary>Load the effect.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override Asset Load(AssetLoader loader) {
			return new Effect(loader);
		}
	}
}
