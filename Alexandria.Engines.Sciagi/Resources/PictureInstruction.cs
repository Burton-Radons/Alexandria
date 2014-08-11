using Glare;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;

namespace Alexandria.Engines.Sciagi.Resources {
	/// <summary>An operation used for drawing a <see cref="Picture"/> with a <see cref="PictureRenderer"/>.</summary>
	public abstract class PictureInstruction {
		#region Reading

		/// <summary>Peek at a byte without reading it.</summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		protected static byte PeekByte(Stream stream) {
			int value = stream.ReadByte();
			if (value < 0)
				return 0xFF;
			stream.Seek(-1, SeekOrigin.Current);
			return (byte)value;
		}

		/// <summary>Peek at the next byte and return whether it's not an opcode.</summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		protected static bool PeekContinue(Stream stream) {
			return PeekByte(stream) < 0xF0;
		}

		/// <summary>Read a byte from the stream.</summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		protected static byte ReadByte(Stream stream) {
			return (byte)stream.ReadByte();
		}

		/// <summary>Skip a number of bytes in the stream.</summary>
		/// <param name="stream"></param>
		/// <param name="count"></param>
		protected static void Skip(Stream stream, int count = 1) {
			stream.Seek(count, SeekOrigin.Current);
		}

		/// <summary>Read absolute coordinates from the stream.</summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		protected static Vector2i ReadAbsoluteCoordinates(Stream stream) {
			var prefix = ReadByte(stream);
			var x = ReadByte(stream) | ((prefix & 0xF0) << 4);
			var y = ReadByte(stream) | ((prefix & 0x0F) << 8);
			return new Vector2i(x, y);
		}

		/// <summary>Read short relative coordinates from the stream.</summary>
		/// <param name="stream"></param>
		/// <param name="to"></param>
		/// <returns></returns>
		protected static Vector2i ReadShortRelativeCoordinates(Stream stream, Vector2i to) {
			var input = ReadByte(stream);
			to.X += ((input & 0x70) >> 4) * ((input & 0x80) == 0 ? 1 : -1);
			to.Y += (input & 0x07) * ((input & 0x08) == 0 ? 1 : -1);
			return to;
		}

		/// <summary>Read medium relative coordinates from the stream.</summary>
		/// <param name="stream"></param>
		/// <param name="start"></param>
		/// <returns></returns>
		protected static Vector2i ReadMediumRelativeCoordinates(Stream stream, Vector2i start) {
			var code = ReadByte(stream);
			if ((code & 0x80) != 0)
				start.Y -= code & 0x7F;
			else
				start.Y += code;
			start.X += (sbyte)ReadByte(stream);
			return start;
		}

		/// <summary>Read a pattern number from the stream.</summary>
		/// <param name="stream"></param>
		/// <param name="patternCode"></param>
		/// <param name="patternNumber"></param>
		/// <returns></returns>
		protected static byte ReadPatternNumber(Stream stream, PicturePatternFlags patternCode, ref byte patternNumber) {
			if ((patternCode & PicturePatternFlags.UsePattern) != 0)
				patternNumber = (byte)((ReadByte(stream) >> 1) & 0x7F);
			return patternNumber;
		}

		/// <summary>Read a list of absolute coordinates from the stream.</summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		protected static List<Vector2i> ReadAbsoluteCoordinatesList(Stream stream) {
			List<Vector2i> list = new List<Vector2i>();
			while (PeekContinue(stream))
				list.Add(ReadAbsoluteCoordinates(stream));
			return list;
		}

		#endregion Reading

		/// <summary>
		/// Get whether the <see cref="Opcode"/> is <see cref="PictureOpcode.Quit"/>.
		/// </summary>
		public bool IsQuit { get { return Opcode == PictureOpcode.Quit; } }

		/// <summary>Get the <see cref="PictureOpcode"/> of this <see cref="PictureInstruction"/>.</summary>
		public PictureOpcode Opcode { get; private set; }

		/// <summary>Get the extended <see cref="PictureExtendedOpcode"/> of this <see cref="PictureInstruction"/>, or <c>null</c> if this is a regular opcode.</summary>
		public PictureExtendedOpcode? ExtendedOpcode { get; private set; }

		/// <summary>Get the name of the opcode, using the <see cref="ExtendedOpcode"/> if present of else <see cref="Opcode"/>.</summary>
		public string OpcodeName { get { return Opcode == PictureOpcode.ExtendedOpcode ? ExtendedOpcode.Value.ToString() : Opcode.ToString(); } }

		/// <summary>Initialise the instruction.</summary>
		/// <param name="opcode"></param>
		public PictureInstruction(PictureOpcode opcode) {
			Opcode = opcode;
			ExtendedOpcode = null;
		}

		/// <summary>Initialise the instruction.</summary>
		/// <param name="extendedOpcode"></param>
		public PictureInstruction(PictureExtendedOpcode extendedOpcode) {
			Opcode = PictureOpcode.ExtendedOpcode;
			ExtendedOpcode = extendedOpcode;
		}

		/// <summary>Render the instruction.</summary>
		/// <param name="renderer"></param>
		public abstract void Render(PictureRenderer renderer);

		/// <summary>Set colors to <see cref="DitherPaletteIndex"/> into the current dither palette, then enable visual.</summary>
		public class SetColor : PictureInstruction {
			/// <summary>The dither palette index to use.</summary>
			public byte DitherPaletteIndex { get; private set; }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="stream"></param>
			public SetColor(Stream stream) : this(ReadByte(stream)) { }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="ditherMapIndex"></param>
			public SetColor(byte ditherMapIndex) : base(PictureOpcode.SetColor) { DitherPaletteIndex = ditherMapIndex; }

			/// <summary>Render the instruction.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) {
				var dither = renderer.DitherPalette[DitherPaletteIndex];
				renderer.Canvas.Visual.SetColor((byte)((dither & 0xF0) >> 4), (byte)(dither & 0x0F));
				renderer.DrawVisual = true;
			}
		}

		/// <summary>Stop drawing to the visual layer.</summary>
		public class DisableVisual : PictureInstruction {
			/// <summary>Initialise the instruction.</summary>
			public DisableVisual() : base(PictureOpcode.DisableVisual) { }

			/// <summary>Render the instruction.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) { renderer.DrawVisual = false; }
		}

		/// <summary>Set the priority value to write, enable the draw priority.</summary>
		public class SetPriority : PictureInstruction {
			/// <summary>The priority value to assign.</summary>
			public byte PriorityValue { get; private set; }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="stream"></param>
			public SetPriority(Stream stream) : this(ReadByte(stream)) { }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="priorityValue"></param>
			public SetPriority(byte priorityValue) : base(PictureOpcode.SetPriority) { PriorityValue = priorityValue; }

			/// <summary>Render the instruction.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) {
				renderer.Canvas.Priority.SetColor((byte)(PriorityValue & 15));
				renderer.DrawPriority = true;
			}
		}

		/// <summary>Stop drawing to the priority map.</summary>
		public class DisablePriority : PictureInstruction {
			/// <summary>Initialise the instruction.</summary>
			public DisablePriority() : base(PictureOpcode.DisablePriority) { }

			/// <summary>Render the instruction by disabling the priority map.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) { renderer.DrawPriority = false; }
		}

		/// <summary>Draw patterns.</summary>
		public class DrawPatterns : PictureInstruction {
			/// <summary>Get the list of locations to draw the pattern at.</summary>
			public List<PicturePatternDrawLocation> DrawLocations { get; private set; }

			/// <summary>Get the pattern flags.</summary>
			public PicturePatternFlags PatternFlags { get; private set; }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="opcode"></param>
			/// <param name="stream"></param>
			/// <param name="patternFlags"></param>
			/// <param name="patternNumber"></param>
			public DrawPatterns(PictureOpcode opcode, Stream stream, PicturePatternFlags patternFlags, ref byte patternNumber)
				: this(opcode, patternFlags, ReadPositions(stream, opcode, patternFlags, ref patternNumber)) {
			}

			/// <summary>Initialise the instruction.</summary>
			/// <param name="opcode"></param>
			/// <param name="patternFlags"></param>
			/// <param name="drawLocations"></param>
			public DrawPatterns(PictureOpcode opcode, PicturePatternFlags patternFlags, List<PicturePatternDrawLocation> drawLocations)
				: base(opcode) {
				if (drawLocations == null)
					throw new ArgumentNullException("drawLocations");
				this.PatternFlags = patternFlags;
				this.DrawLocations = drawLocations;
			}

			static List<PicturePatternDrawLocation> ReadPositions(Stream stream, PictureOpcode opcode, PicturePatternFlags patternFlags, ref byte patternNumber) {
				List<PicturePatternDrawLocation> list = new List<PicturePatternDrawLocation>();
				Vector2i position;

				ReadPatternNumber(stream, patternFlags, ref patternNumber);
				position = ReadAbsoluteCoordinates(stream);
				list.Add(new PicturePatternDrawLocation(patternNumber, position));

				while (PeekContinue(stream)) {
					ReadPatternNumber(stream, patternFlags, ref patternNumber);

					switch (opcode) {
						case PictureOpcode.DrawRelativePatterns: position = ReadShortRelativeCoordinates(stream, position); break;
						case PictureOpcode.DrawAbsolutePatterns: position = ReadAbsoluteCoordinates(stream); break;
						case PictureOpcode.DrawRelativeMediumPatterns: position = ReadMediumRelativeCoordinates(stream, position); break;
						default: throw new NotSupportedException();
					}
					list.Add(new PicturePatternDrawLocation(patternNumber, position));
				}
				return list;
			}

			/// <summary>Render the instruction by drawing the patterns.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) {
				foreach (PicturePatternDrawLocation location in DrawLocations) {
					if ((PatternFlags & PicturePatternFlags.UsePattern) != 0)
						renderer.PatternNumber = location.PatternNumber;
					renderer.DrawPatternAt(location.Position);
				}
			}
		}

		/// <summary>Draw a line strip.</summary>
		public class DrawLines : PictureInstruction {
			/// <summary>Get the list of points in the line.</summary>
			public List<Vector2i> Points { get; private set; }

			/// <summary>Get the starting point of the line.</summary>
			public Vector2i Start { get; private set; }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="opcode"></param>
			/// <param name="stream"></param>
			public DrawLines(PictureOpcode opcode, Stream stream)
				: base(opcode) {
				Points = new List<Vector2i>();
				Start = ReadAbsoluteCoordinates(stream);

				Vector2i point = Start;

				while (PeekContinue(stream)) {
					switch (opcode) {
						case PictureOpcode.DrawRelativeMediumLines: point = ReadMediumRelativeCoordinates(stream, point); break;
						case PictureOpcode.DrawRelativeLongLines: point = ReadAbsoluteCoordinates(stream); break;
						case PictureOpcode.DrawRelativeShortLines: point = ReadShortRelativeCoordinates(stream, point); break;
						default: throw new NotSupportedException();
					}
					Points.Add(point);
				}
			}

			/// <summary>Render the instruction by drawing the line strip.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) {
				renderer.DrawMoveTo(Start);
				foreach (Vector2i point in Points)
					renderer.DrawLineTo(point);
			}
		}

		/// <summary>Floodfill an area.</summary>
		public class DrawFloodfill : PictureInstruction {
			/// <summary>Get the points to fill.</summary>
			public List<Vector2i> Points { get; private set; }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="stream"></param>
			public DrawFloodfill(Stream stream) : this(ReadAbsoluteCoordinatesList(stream)) { }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="points"></param>
			public DrawFloodfill(List<Vector2i> points)
				: base(PictureOpcode.DrawFloodfill) {
				if (points == null)
					throw new ArgumentNullException("points");
				Points = points;
			}

			/// <summary>Render the instruction by filling the points.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) {
				foreach (Vector2i point in Points)
					renderer.DrawFloodfill(point);
			}
		}

		/// <summary>Set pattern flags.</summary>
		public class SetPatternFlags : PictureInstruction {
			/// <summary>Get the pattern flags to assign.</summary>
			public PicturePatternFlags PatternFlags { get; private set; }

			/// <summary>Read the instruction.</summary>
			/// <param name="stream"></param>
			/// <param name="patternCode"></param>
			public SetPatternFlags(Stream stream, ref PicturePatternFlags patternCode) : this(Read(stream, ref patternCode)) { }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="patternFlags"></param>
			public SetPatternFlags(PicturePatternFlags patternFlags) : base(PictureOpcode.SetPatternFlags) { PatternFlags = patternFlags; }

			static PicturePatternFlags Read(Stream stream, ref PicturePatternFlags patternCode) {
				return patternCode = (PicturePatternFlags)(ReadByte(stream) & 0x37);
			}

			/// <summary>Render the instruction by assigning the pattern flags.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) {
				renderer.PatternFlags = PatternFlags;
			}
		}

		/// <summary>Set the control value to write and enable writing to control.</summary>
		public class SetControl : PictureInstruction {
			/// <summary>The control map value to assign.</summary>
			public byte ControlMapValue { get; private set; }

			/// <summary>Read the instruction from the stream.</summary>
			/// <param name="stream"></param>
			public SetControl(Stream stream) : this(ReadByte(stream)) { }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="controlMapValue"></param>
			public SetControl(byte controlMapValue) : base(PictureOpcode.SetControl) { ControlMapValue = controlMapValue; }

			/// <summary>Render the instruction by setting the control colour and enabling the control map.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) {
				renderer.Canvas.Control.SetColor((byte)(ControlMapValue & 15));
				renderer.DrawControl = true;
			}
		}

		/// <summary>Disable drawing to the control plane.</summary>
		public class DisableControl : PictureInstruction {
			/// <summary>Initialise the instruction.</summary>
			public DisableControl() : base(PictureOpcode.DisableControl) { }

			/// <summary>Render the instruction by disabling the control map.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) { renderer.DrawControl = false; }
		}

		/// <summary>Assign a palette entry.</summary>
		public class SetPalette : PictureInstruction {
			/// <summary>Get the list of entries to assign.</summary>
			public List<PictureSetPaletteEntry> Entries { get; private set; }

			/// <summary>Read the instruction.</summary>
			/// <param name="stream"></param>
			/// <param name="opcode"></param>
			public SetPalette(Stream stream, PictureExtendedOpcode opcode) : this(opcode, ReadEntries(stream, opcode)) { }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="opcode"></param>
			/// <param name="entries"></param>
			public SetPalette(PictureExtendedOpcode opcode, params PictureSetPaletteEntry[] entries) : this(opcode, (IEnumerable<PictureSetPaletteEntry>)entries) { }

			/// <summary>Initialise the instruction.</summary>
			/// <param name="opcode"></param>
			/// <param name="entries"></param>
			public SetPalette(PictureExtendedOpcode opcode, IEnumerable<PictureSetPaletteEntry> entries) : base(opcode) { Entries = new List<PictureSetPaletteEntry>(entries); }

			static List<PictureSetPaletteEntry> ReadEntries(Stream stream, PictureExtendedOpcode opcode) {
				List<PictureSetPaletteEntry> list = new List<PictureSetPaletteEntry>();

				switch (opcode) {
					case PictureExtendedOpcode.SetPaletteEntry:
						while (PeekContinue(stream))
							list.Add(new PictureSetPaletteEntry(ReadByte(stream), ReadByte(stream)));
						break;

					case PictureExtendedOpcode.SetPalette:
						var paletteIndex = ReadByte(stream) * PictureRenderer.EgaDitherPaletteSize;
						for (var colorIndex = 0; colorIndex < PictureRenderer.EgaDitherPaletteSize; colorIndex++)
							list.Add(new PictureSetPaletteEntry((byte)(paletteIndex + colorIndex), ReadByte(stream)));
						break;
				}

				return list;
			}

			/// <summary>Render the instruction by setting the dither palette.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) {
				foreach (PictureSetPaletteEntry entry in Entries)
					renderer.DitherPalette[entry.Index] = entry.Color;
			}
		}

		/// <summary>Stop processing; end of the picture.</summary>
		public class Quit : PictureInstruction {
			/// <summary>Initialise the instruction.</summary>
			public Quit() : base(PictureOpcode.Quit) { }

			/// <summary>Render the instruction; this does nothing.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) { }
		}

		/// <summary>Used by monochrome rendering, unknown.</summary>
		public class Mono0 : PictureInstruction {
			/// <summary>Get the unknown parameters.</summary>
			public byte[] Unknown { get; private set; }

			/// <summary>Read the instruction.</summary>
			/// <param name="stream"></param>
			public Mono0(Stream stream)
				: base(PictureExtendedOpcode.Mono0) {
				Unknown = stream.ReadSharedBytes(41);
			}

			/// <summary>Does nothing.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) { }
		}

		/// <summary>Used by monochrome rendering; unknown.</summary>
		public class Mono123 : PictureInstruction {
			/// <summary>Get the unknown parameters.</summary>
			public byte Unknown { get; private set; }

			/// <summary>Read the instruction.</summary>
			/// <param name="stream"></param>
			/// <param name="opcode"></param>
			public Mono123(Stream stream, PictureExtendedOpcode opcode)
				: base(opcode) {
				Unknown = ReadByte(stream);
			}

			/// <summary>Does nothing.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) { }
		}

		/// <summary>A synthetic instruction to draw a <see cref="PictureCel"/>.</summary>
		public class DrawCel : PictureInstruction {
			/// <summary>Get the cel to draw.</summary>
			public PictureCel Cel { get; private set; }

			/// <summary>
			/// Initialise the instruction.
			/// </summary>
			/// <param name="cel"></param>
			public DrawCel(PictureCel cel)
				: base(PictureExtendedOpcode.DrawCel) {
				if (cel == null)
					throw new ArgumentNullException("cel");
				Cel = cel;
			}

			/// <summary>Draw the cel.</summary>
			/// <param name="renderer"></param>
			public override void Render(PictureRenderer renderer) {
				renderer.DrawCel(Cel);
			}
		}

		/// <summary>Read an instruction from the stream.</summary>
		/// <param name="stream"></param>
		/// <param name="patternFlags"></param>
		/// <param name="patternNumber"></param>
		/// <returns></returns>
		public static PictureInstruction ReadInstruction(Stream stream, ref PicturePatternFlags patternFlags, ref byte patternNumber) {
			PictureOpcode opcode = (PictureOpcode)ReadByte(stream);

			switch (opcode) {
				case PictureOpcode.SetColor: return new SetColor(stream);
				case PictureOpcode.DisableVisual: return new DisableVisual();
				case PictureOpcode.SetPriority: return new SetPriority(stream);
				case PictureOpcode.DisablePriority: return new DisablePriority();
				case PictureOpcode.DrawFloodfill: return new DrawFloodfill(stream);
				case PictureOpcode.SetPatternFlags: return new SetPatternFlags(stream, ref patternFlags);
				case PictureOpcode.SetControl: return new SetControl(stream);
				case PictureOpcode.DisableControl: return new DisableControl();

				case PictureOpcode.DrawRelativeMediumLines:
				case PictureOpcode.DrawRelativeLongLines:
				case PictureOpcode.DrawRelativeShortLines: return new DrawLines(opcode, stream);

				case PictureOpcode.DrawRelativeMediumPatterns:
				case PictureOpcode.DrawRelativePatterns:
				case PictureOpcode.DrawAbsolutePatterns: return new DrawPatterns(opcode, stream, patternFlags, ref patternNumber);

				case PictureOpcode.ExtendedOpcode:
					var extendedOpcode = (PictureExtendedOpcode)ReadByte(stream);
					switch (extendedOpcode) {
						case PictureExtendedOpcode.SetPaletteEntry:
						case PictureExtendedOpcode.SetPalette: return new SetPalette(stream, extendedOpcode);

						case PictureExtendedOpcode.Mono0: return new Mono0(stream);
						case PictureExtendedOpcode.Mono1:
						case PictureExtendedOpcode.Mono2:
						case PictureExtendedOpcode.Mono3: return new Mono123(stream, extendedOpcode);

						default: throw new Exception(String.Format("Unknown extended opcode 0x{0:X2} ({1})", (int)opcode, opcode));
					}

				case PictureOpcode.Quit: return new Quit();
				default: throw new Exception(String.Format("Unknown opcode 0x{0:X2} ({1}).", (int)opcode, opcode));
			}
		}
	}

	/// <summary>Used by <see cref="PictureInstruction.DrawPatterns"/>.</summary>
	public struct PicturePatternDrawLocation {
		/// <summary>Get the pattern number to use.</summary>
		public readonly byte PatternNumber;

		/// <summary>Get the position to draw at.</summary>
		public readonly Vector2i Position;

		/// <summary>Set the properties.</summary>
		/// <param name="patternNumber"></param>
		/// <param name="position"></param>
		public PicturePatternDrawLocation(byte patternNumber, Vector2i position) {
			PatternNumber = patternNumber;
			Position = position;
		}
	}

	/// <summary>Used by <see cref="PictureInstructions.SetPalette"/>.</summary>
	public struct PictureSetPaletteEntry {
		/// <summary>Get the index in the dither palette to change.</summary>
		public byte Index;

		/// <summary>Get the new value for the dither palette.</summary>
		public byte Color;

		/// <summary>Initialise the object.</summary>
		/// <param name="index"></param>
		/// <param name="color"></param>
		public PictureSetPaletteEntry(byte index, byte color) { Index = index; Color = color; }
	}
}
