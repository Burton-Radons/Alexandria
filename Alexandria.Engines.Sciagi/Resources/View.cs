using Alexandria.Engines.Sciagi.Controls;
using Glare;
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
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi.Resources {
	/// <summary>
	/// A view resource, which is a collection of animations.
	/// </summary>
	public class View : ResourceData {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<ViewAnimation> AnimationsMutable = new Codex<ViewAnimation>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<ViewCell> CellsMutable = new Codex<ViewCell>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<ViewGroup> GroupsMutable = new Codex<ViewGroup>();

		/// <summary>Get the animations of the view.</summary>
		public Codex<ViewAnimation> Animations { get { return AnimationsMutable; } }

		/// <summary>Get the cells in the view.</summary>
		public Codex<ViewCell> Cells { get { return CellsMutable; } }

		/// <summary>Get the groups in the view.</summary>
		public Codex<ViewGroup> Groups { get { return GroupsMutable; } }

		internal View(AssetLoader loader)
			: base(loader) {
			using (BinaryReader reader = loader.Reader) {
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
					var animation = FindAnimation(loader, reader.ReadUInt16());
					GroupsMutable.Add(new ViewGroup(animation, (mirroredFlags & (1 << index)) != 0));
				}
			}
		}

		/// <summary>Create a control to browse the view.</summary>
		/// <returns></returns>
		public override System.Windows.Forms.Control Browse(Action<double> progressUpdateCallback = null) {
			return new ViewBrowser(this);
		}

		internal ViewAnimation FindAnimation(AssetLoader loader, ushort offset) {
			BinaryReader reader = loader.Reader;

			foreach (var item in Animations)
				if (item.DataOffset == offset)
					return item;

			reader.BaseStream.Position = offset;
			var animation = new ViewAnimation(this, loader);
			AnimationsMutable.Add(animation);
			return animation;
		}

		internal ViewCell FindCell(AssetLoader loader, ushort offset) {
			BinaryReader reader = loader.Reader;

			foreach (var item in Cells)
				if (item.DataOffset == offset)
					return item;

			reader.BaseStream.Position = offset;
			var cell = new ViewCell(this, loader);
			CellsMutable.Add(cell);
			return cell;
		}
	}

	/// <summary>
	/// A group in a <see cref="View"/>.
	/// </summary>
	public class ViewGroup {
		/// <summary>Get the animation for the group.</summary>
		public ViewAnimation Animation { get; private set; }

		/// <summary>Get the zero-based index of this group.</summary>
		public int Index { get { return View.Groups.IndexOf(this); } }

		/// <summary>Get whether to mirror this group.</summary>
		public bool IsMirrored { get; private set; }

		/// <summary>Get the view this is contained in.</summary>
		public View View { get { return Animation.View; } }

		internal ViewGroup(ViewAnimation animation, bool isMirrored) {
			IsMirrored = isMirrored;
			Animation = animation;
		}
	}

	/// <summary>An animation in a <see cref="View"/>.</summary>
	public class ViewAnimation : Asset {
		internal ushort DataOffset;

		/// <summary>Get the <see cref="ViewCell"/> collection.</summary>
		public Codex<ViewCell> Cells { get; private set; }

		/// <summary>Get the zero-based index of this animation in the <see cref="View"/>.</summary>
		public int Index { get { return View.Animations.IndexOf(this); } }

		/// <summary>Get the containing <see cref="View"/>.</summary>
		public View View { get; private set; }

		internal ViewAnimation(View view, AssetLoader loader)
			: base(loader) {
			BinaryReader reader = loader.Reader;

			View = view;

			var cells = new Codex<ViewCell>();
			Cells = cells;

			DataOffset = (ushort)reader.BaseStream.Position;
			var count = reader.ReadUInt16();
			Unknowns.ReadInt16s(reader, 1);
			for (int index = 0; index < count; index++) {
				reader.BaseStream.Position = DataOffset + 4 + index * 2;
				cells.Add(view.FindCell(loader, reader.ReadUInt16()));
			}
		}
	}

	/// <summary>A <see cref="View"/> cell.</summary>
	public class ViewCell : Asset {
		internal ushort DataOffset;

		/// <summary>Get the zero-based index of this cell.</summary>
		public int Index { get { return View.Cells.IndexOf(this); } }

		/// <summary>
		/// Get the offset to display the cell at.
		/// </summary>
		public Vector2i Offset { get; private set; }

		/// <summary>Get the raster containing this view's image data.</summary>
		public Raster Raster { get; private set; }

		/// <summary>Get the containing view.</summary>
		public View View { get; private set; }

		internal ViewCell(View view, AssetLoader loader)
			: base(loader) {
			BinaryReader reader = loader.Reader;

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
				for (int count = 0; count < repeat && offset < pixels.Length; count++)
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
