using Alexandria.Engines.Sciagi.Controls;
using Glare;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi.Resources {
	public class View : ResourceData {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly RichList<ViewAnimation> AnimationsMutable = new RichList<ViewAnimation>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly RichList<ViewCell> CellsMutable = new RichList<ViewCell>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly RichList<ViewGroup> GroupsMutable = new RichList<ViewGroup>();

		public ReadOnlyList<ViewAnimation> Animations { get { return AnimationsMutable; } }

		public ReadOnlyList<ViewCell> Cells { get { return CellsMutable; } }

		public ReadOnlyList<ViewGroup> Groups { get { return GroupsMutable; } }

		public View(BinaryReader reader, Resource resource)
			: base(resource) {
			using (reader) {
				// 8-byte header
				var count = reader.ReadByte();
				var flags = reader.ReadByte(); // Bit 0x80 means palette is set
				bool isCompressed = (flags & 0x40) == 0;
				ushort mirroredFlags = reader.ReadUInt16();
				Unknowns.ReadInt16s(reader, 1);
				ushort paletteOffset = reader.ReadUInt16();

				if (paletteOffset != 0 && paletteOffset != 0x100) {
					throw new NotImplementedException();
				}

				for (int index = 0; index < count; index++) {
					reader.BaseStream.Position = 8 + index * 2;
					var animation = FindAnimation(reader, reader.ReadUInt16());
					GroupsMutable.Add(new ViewGroup(animation, (mirroredFlags & (1 << index)) != 0));
				}
			}
		}

		public override System.Windows.Forms.Control Browse() {
			return new ViewBrowser(this);
		}

		internal ViewAnimation FindAnimation(BinaryReader reader, ushort offset) {
			foreach (var item in Animations)
				if (item.DataOffset == offset)
					return item;

			reader.BaseStream.Position = offset;
			var animation = new ViewAnimation(this, reader);
			AnimationsMutable.Add(animation);
			return animation;
		}

		internal ViewCell FindCell(BinaryReader reader, ushort offset) {
			foreach (var item in Cells)
				if (item.DataOffset == offset)
					return item;

			reader.BaseStream.Position = offset;
			var cell = new ViewCell(this, reader);
			CellsMutable.Add(cell);
			return cell;
		}
	}

	public class ViewGroup {
		public ViewAnimation Animation { get; private set; }
		public int Index { get { return View.Groups.IndexOf(this); } }
		public bool IsMirrored { get; private set; }
		public View View { get { return Animation.View; } }

		internal ViewGroup(ViewAnimation animation, bool isMirrored) {
			IsMirrored = isMirrored;
			Animation = animation;
		}
	}

	public class ViewAnimation {
		internal ushort DataOffset;

		public ushort U1 { get; private set; }

		public ReadOnlyList<ViewCell> Cells { get; private set; }

		public int Index { get { return View.Animations.IndexOf(this); } }

		public View View { get; private set; }

		internal ViewAnimation(View view, BinaryReader reader) {
			View = view;

			var cells = new RichList<ViewCell>();
			Cells = cells;

			DataOffset = (ushort)reader.BaseStream.Position;
			var count = reader.ReadUInt16();
			U1 = reader.ReadUInt16();
			for (int index = 0; index < count; index++) {
				reader.BaseStream.Position = DataOffset + 4 + index * 2;
				cells.Add(view.FindCell(reader, reader.ReadUInt16()));
			}
		}
	}

	public class ViewCell {
		internal ushort DataOffset;

		public int Index { get { return View.Cells.IndexOf(this); } }

		/// <summary>
		/// Get the offset to display the cell at.
		/// </summary>
		public Vector2i Offset { get; private set; }

		public Raster Raster { get; private set; }

		public View View { get; private set; }

		internal ViewCell(View view, BinaryReader reader) {
			View = view;

			DataOffset = (ushort)reader.BaseStream.Position;
			var sizeX = reader.ReadUInt16();
			var sizeY = reader.ReadUInt16();
			Offset = new Vector2i(reader.ReadSByte(), reader.ReadSByte());
			var transparentColor = reader.ReadByte();

			byte[] pixels = new byte[sizeX * sizeY];

			for (int offset = 0; offset < pixels.Length && reader.BaseStream.Position < reader.BaseStream.Length; ) {
				int code = reader.ReadByte();
				int color = code & 15, repeat = code >> 4;

				int x = offset % sizeX;
				int y = offset / sizeX;


				if (color == transparentColor)
					color = 16;
				for(int count = 0; count < repeat && offset < pixels.Length; count++)
					pixels[offset++] = (byte)color;
			}

			Raster = new Raster(sizeX, sizeY, Raster.DefaultEgaColorsWithTransparent, data: pixels);
#if false
				// For VGA
				// Width:WORD Height:WORD DisplaceX:BYTE DisplaceY:BYTE ClearKey:BYTE Unknown:BYTE RLEData starts now directly
				// For EGA
				// Width:WORD Height:WORD DisplaceX:BYTE DisplaceY:BYTE ClearKey:BYTE EGAData starts now directly
				cel = &_loop[loopNo].cel[celNo];
				cel->scriptWidth = cel->width = READ_LE_UINT16(celData);
				cel->scriptHeight = cel->height = READ_LE_UINT16(celData + 2);
				cel->displaceX = (signed char)celData[4];
				cel->displaceY = celData[5];
				cel->clearKey = celData[6];

				if (g_sci->getGameId() == GID_QFG3 && g_sci->isDemo() && resourceId == 39)
					cel->displaceY = 98;

				if (isEGA) {
					cel->offsetEGA = celOffset + 7;
					cel->offsetRLE = 0;
					cel->offsetLiteral = 0;
				} else {
					cel->offsetEGA = 0;
					if (isCompressed) {
						cel->offsetRLE = celOffset + 8;
						cel->offsetLiteral = 0;
					} else {
						cel->offsetRLE = 0;
						cel->offsetLiteral = celOffset + 8;
					}
				}
				cel->rawBitmap = 0;
				if (_loop[loopNo].mirrorFlag)
					cel->displaceX = -cel->displaceX;
#endif
		}
	}
}
