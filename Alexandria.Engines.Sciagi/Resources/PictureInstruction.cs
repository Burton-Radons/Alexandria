using Glare;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;

namespace Alexandria.Engines.Sciagi.Resources {
	public abstract class PictureInstruction {
		#region Reading

		protected static byte PeekByte(Stream stream) {
			int value = stream.ReadByte();
			if (value < 0)
				return 0xFF;
			stream.Seek(-1, SeekOrigin.Current);
			return (byte)value;
		}

		protected static bool PeekContinue(Stream stream) {
			return PeekByte(stream) < 0xF0;
		}

		protected static byte ReadByte(Stream stream) {
			return (byte)stream.ReadByte();
		}

		protected static void Skip(Stream stream, int count = 1) {
			stream.Seek(count, SeekOrigin.Current);
		}

		protected static Vector2i ReadAbsoluteCoordinates(Stream stream) {
			var prefix = ReadByte(stream);
			var x = ReadByte(stream) | ((prefix & 0xF0) << 4);
			var y = ReadByte(stream) | ((prefix & 0x0F) << 8);
			return new Vector2i(x, y);
		}

		protected static Vector2i ReadShortRelativeCoordinates(Stream stream, Vector2i to) {
			var input = ReadByte(stream);
			to.X += ((input & 0x70) >> 4) * ((input & 0x80) == 0 ? 1 : -1);
			to.Y += (input & 0x07) * ((input & 0x08) == 0 ? 1 : -1);
			return to;
		}

		protected static Vector2i ReadMediumRelativeCoordinates(Stream stream, Vector2i start) {
			var code = ReadByte(stream);
			if ((code & 0x80) != 0)
				start.Y -= code & 0x7F;
			else
				start.Y += code;
			start.X += (sbyte)ReadByte(stream);
			return start;
		}

		protected static byte ReadPatternNumber(Stream stream, PicturePatternFlags patternCode, ref byte patternNumber) {
			if ((patternCode & PicturePatternFlags.UsePattern) != 0)
				patternNumber = (byte)((ReadByte(stream) >> 1) & 0x7F);
			return patternNumber;
		}

		protected static List<Vector2i> ReadAbsoluteCoordinatesList(Stream stream) {
			List<Vector2i> list = new List<Vector2i>();
			while (PeekContinue(stream))
				list.Add(ReadAbsoluteCoordinates(stream));
			return list;
		}

		#endregion Reading

		public bool IsQuit { get { return Opcode == PictureOpcode.Quit; } }

		public PictureOpcode Opcode { get; private set; }
		public PictureExtendedOpcode? ExtendedOpcode { get; private set; }
		public string OpcodeName { get { return Opcode == PictureOpcode.ExtendedOpcode ? ExtendedOpcode.Value.ToString() : Opcode.ToString(); } }

		public PictureInstruction(PictureOpcode opcode) {
			Opcode = opcode;
			ExtendedOpcode = null;
		}

		public PictureInstruction(PictureExtendedOpcode extendedOpcode) {
			Opcode = PictureOpcode.ExtendedOpcode;
			ExtendedOpcode = extendedOpcode;
		}

		public abstract void Render(PictureRenderer renderer);

		/// <summary>Set colors to <see cref="DitherPaletteIndex"/> into the current dither palette, then enable visual.</summary>
		public class SetColor : PictureInstruction {
			public byte DitherPaletteIndex { get; private set; }

			public SetColor(Stream stream) : this(ReadByte(stream)) { }
			public SetColor(byte ditherMapIndex) : base(PictureOpcode.SetColor) { DitherPaletteIndex = ditherMapIndex; }

			public override void Render(PictureRenderer renderer) {
				var dither = renderer.DitherPalette[DitherPaletteIndex];
				renderer.Canvas.VisualRaster.SetColor((byte)((dither & 0xF0) >> 4), (byte)(dither & 0x0F));
				renderer.DrawVisual = true;
			}
		}

		/// <summary>Stop drawing to the visual layer.</summary>
		public class DisableVisual : PictureInstruction {
			public DisableVisual() : base(PictureOpcode.DisableVisual) { }
			public override void Render(PictureRenderer renderer) { renderer.DrawVisual = false; }
		}

		/// <summary>Set the priority value to write, enable the draw priority.</summary>
		public class SetPriority : PictureInstruction {
			public byte PriorityValue { get; private set; }

			public SetPriority(Stream stream) : this(ReadByte(stream)) { }
			public SetPriority(byte priorityValue) : base(PictureOpcode.SetPriority) { PriorityValue = priorityValue; }

			public override void Render(PictureRenderer renderer) {
				renderer.Canvas.PriorityRaster.SetColor((byte)(PriorityValue & 15));
				renderer.DrawPriority = true;
			}
		}

		/// <summary>Stop drawing to the priority map.</summary>
		public class DisablePriority : PictureInstruction {
			public DisablePriority() : base(PictureOpcode.DisablePriority) { }
			public override void Render(PictureRenderer renderer) { renderer.DrawPriority = false; }
		}

		/// <summary>Draw patterns.</summary>
		public class DrawPatterns : PictureInstruction {
			public List<PicturePatternDrawLocation> DrawLocations { get; private set; }
			public PicturePatternFlags PatternFlags { get; private set; }

			public DrawPatterns(PictureOpcode opcode, Stream stream, PicturePatternFlags patternFlags, ref byte patternNumber)
				: this(opcode, patternFlags, ReadPositions(stream, opcode, patternFlags, ref patternNumber)) {
			}

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

			public override void Render(PictureRenderer renderer) {
				foreach (PicturePatternDrawLocation location in DrawLocations) {
					if ((PatternFlags & PicturePatternFlags.UsePattern) != 0)
						renderer.PatternNumber = location.PatternNumber;
					renderer.DrawPatternAt(location.Position);
				}
			}
		}

		/// <summary>Draw lines.</summary>
		public class DrawLines : PictureInstruction {
			public List<Vector2i> Points { get; private set; }
			public Vector2i Start { get; private set; }

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

			public override void Render(PictureRenderer renderer) {
				renderer.DrawMoveTo(Start);
				foreach (Vector2i point in Points)
					renderer.DrawLineTo(point);
			}
		}

		public class DrawFloodfill : PictureInstruction {
			public List<Vector2i> Points { get; private set; }

			public DrawFloodfill(Stream stream) : this(ReadAbsoluteCoordinatesList(stream)) { }
			public DrawFloodfill(List<Vector2i> points)
				: base(PictureOpcode.DrawFloodfill) {
				if (points == null)
					throw new ArgumentNullException("points");
				Points = points;
			}

			public override void Render(PictureRenderer renderer) {
				foreach (Vector2i point in Points)
					renderer.DrawFloodfill(point);
			}
		}

		public class SetPatternFlags : PictureInstruction {
			public PicturePatternFlags PatternFlags { get; private set; }

			public SetPatternFlags(Stream stream, ref PicturePatternFlags patternCode) : this(Read(stream, ref patternCode)) { }
			public SetPatternFlags(PicturePatternFlags patternFlags) : base(PictureOpcode.SetPatternFlags) { PatternFlags = patternFlags; }

			static PicturePatternFlags Read(Stream stream, ref PicturePatternFlags patternCode) {
				return patternCode = (PicturePatternFlags)(ReadByte(stream) & 0x37);
			}

			public override void Render(PictureRenderer renderer) {
				renderer.PatternFlags = PatternFlags;
			}
		}

		/// <summary>Set the control value to write and enable writing to control.</summary>
		public class SetControl : PictureInstruction {
			public byte ControlMapValue { get; private set; }

			public SetControl(Stream stream) : this(ReadByte(stream)) { }
			public SetControl(byte controlMapValue) : base(PictureOpcode.SetControl) { ControlMapValue = controlMapValue; }

			public override void Render(PictureRenderer renderer) {
				renderer.Canvas.ControlRaster.SetColor((byte)(ControlMapValue & 15));
				renderer.DrawControl = true;
			}
		}

		/// <summary>Disable drawing to the control plane.</summary>
		public class DisableControl : PictureInstruction {
			public DisableControl() : base(PictureOpcode.DisableControl) { }
			public override void Render(PictureRenderer renderer) { renderer.DrawControl = false; }
		}

		public class SetPalette : PictureInstruction {
			public List<PictureSetPaletteEntry> Entries { get; private set; }

			public SetPalette(Stream stream, PictureExtendedOpcode opcode) : this(opcode, ReadEntries(stream, opcode)) { }
			public SetPalette(PictureExtendedOpcode opcode, params PictureSetPaletteEntry[] entries) : this(opcode, (IEnumerable<PictureSetPaletteEntry>)entries) { }
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

			public override void Render(PictureRenderer renderer) {
				foreach (PictureSetPaletteEntry entry in Entries)
					renderer.DitherPalette[entry.Index] = entry.Color;
			}
		}

		/// <summary>Stop processing; end of the picture.</summary>
		public class Quit : PictureInstruction {
			public Quit() : base(PictureOpcode.Quit) { }
			public override void Render(PictureRenderer renderer) { }
		}

		public class Mono0 : PictureInstruction {
			public byte[] Unknown { get; private set; }

			public Mono0(Stream stream)
				: base(PictureExtendedOpcode.Mono0) {
				Unknown = stream.ReadSharedBytes(41);
			}

			public override void Render(PictureRenderer renderer) { }
		}

		public class Mono123 : PictureInstruction {
			public byte Unknown { get; private set; }

			public Mono123(Stream stream, PictureExtendedOpcode opcode)
				: base(opcode) {
				Unknown = ReadByte(stream);
			}

			public override void Render(PictureRenderer renderer) { }
		}

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

	public struct PicturePatternDrawLocation {
		public readonly byte PatternNumber;
		public readonly Vector2i Position;

		public PicturePatternDrawLocation(byte patternNumber, Vector2i position) {
			PatternNumber = patternNumber;
			Position = position;
		}
	}

	public struct PictureSetPaletteEntry {
		public byte Index;
		public byte Color;

		public PictureSetPaletteEntry(byte index, byte color) { Index = index; Color = color; }
	}
}
