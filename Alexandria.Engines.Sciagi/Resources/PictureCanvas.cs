using Glare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi.Resources {
	/// <summary>Manages the four rasters of an SCI game.</summary>
	public class PictureCanvas {
		Vector2i pResolution;
		Palette Palette;

		/// <summary>
		/// Get the visual raster.
		/// </summary>
		public Raster Visual { get; protected set; }

		/// <summary>
		/// Get the priority raster.
		/// </summary>
		public Raster Priority { get; protected set; }

		/// <summary>
		/// Get the control raster.
		/// </summary>
		public Raster Control { get; protected set; }

		/// <summary>
		/// Get the auxiliary raster.
		/// </summary>
		public Raster Auxiliary { get; protected set; }

		/// <summary>Get or set the resolution of the canvas. The default is (320, 190).</summary>
		public Vector2i Resolution {
			get { return pResolution; }

			set {
				if (pResolution != value) {
					pResolution = value;
					Visual = new Raster(value, Palette != null ? Palette.FlatColors : Raster.DefaultBlendedEgaColors, colorBlend: 16);
					Priority = new Raster(value, Raster.DefaultEgaColors);
					Control = new Raster(value, Raster.DefaultEgaColors);
					Auxiliary = new Raster(value, Raster.DefaultEgaColors);
				}
				Clear();
			}
		}

		/// <summary>
		/// Initialise the canvas.
		/// </summary>
		/// <param name="resolution"></param>
		/// <param name="palette"></param>
		public PictureCanvas(Vector2i resolution, Palette palette = null) {
			Palette = palette;
			Resolution = resolution;
		}

		/// <summary>
		/// Initialise the canvas.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public PictureCanvas(int width, int height) : this(new Vector2i(width, height)) { }

		/// <summary>Create a new <see cref="PictureCanvas"/> using the <see cref="Picture"/>'s dimensions, and using the <see cref="Picture"/>'s <see cref="Palette"/>, if any.</summary>
		public PictureCanvas(Picture picture) : this(picture.Dimensions, picture.Palette) { }

		/// <summary>
		/// Clear all of the layers to their default values (by default), or specific layers with specific values if desired. This locks, then unlocks the bitmaps.
		/// </summary>
		/// <param name="visualValue">What to clear the visual map to if Layer.Visual is included in mask.</param>
		/// <param name="priorityValue">What to clear the priority map to if Layer.Priority is included in mask.</param>
		/// <param name="controlValue">What to clear the control map to if Layer.Control is included in mask.</param>
		/// <param name="auxiliaryValue">What to clear the auxiliary map to if Layer.Auxiliary is included in mask.</param>
		/// <param name="mask"></param>
		public virtual void Clear(byte visualValue = 15, byte priorityValue = 0, byte controlValue = 0, byte auxiliaryValue = 0, PictureLayer mask = PictureLayer.All) {
			Lock();
			if ((mask & PictureLayer.Visual) != 0)
				Visual.Clear(visualValue);
			if ((mask & PictureLayer.Priority) != 0)
				Priority.Clear(priorityValue);
			if ((mask & PictureLayer.Control) != 0)
				Control.Clear(controlValue);
			if ((mask & PictureLayer.Auxiliary) != 0)
				Auxiliary.Clear(auxiliaryValue);
			Unlock();
		}

		/// <summary>
		/// Get a raster layer.
		/// </summary>
		/// <param name="layer"></param>
		/// <returns></returns>
		public Raster GetRaster(PictureLayer layer) {
			switch (layer) {
				case PictureLayer.Visual: return Visual;
				case PictureLayer.Priority: return Priority;
				case PictureLayer.Control: return Control;
				case PictureLayer.Auxiliary: return Auxiliary;
				default: throw new ArgumentException(typeof(PictureLayer).Name + " value " + layer + " is not valid.", "layer");
			}
		}
		
		/// <summary>
		/// Lock all the raster images to start drawing to them.
		/// </summary>
		public void Lock() {
			Visual.Lock();
			Priority.Lock();
			Control.Lock();
			Auxiliary.Lock();
		}

		/// <summary>
		/// Unlock all the raster images, flushing their changes.
		/// </summary>
		public void Unlock() {
			Visual.Unlock();
			Priority.Unlock();
			Control.Unlock();
			Auxiliary.Unlock();
		}
	}

	/// <summary>
	/// The layers of the canvas. This acts as a regular enumeration or as a bitmask.
	/// </summary>
	[Flags]
	public enum PictureLayer {
		/// <summary>
		/// No layers.
		/// </summary>
		None = 0,

		/// <summary>
		/// All of the layers.
		/// </summary>
		All = Visual | Priority | Control | Auxiliary,

		/// <summary>
		/// The visual layer.
		/// </summary>
		Visual = 1,

		/// <summary>
		/// The priority layer.
		/// </summary>
		Priority = 2,

		/// <summary>
		/// The control layer.
		/// </summary>
		Control = 4,

		/// <summary>
		/// The auxiliary layer.
		/// </summary>
		Auxiliary = 8,
	}
}
