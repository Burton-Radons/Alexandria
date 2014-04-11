using Glare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi.Resources {
	public class PictureCanvas {

		Vector2i pResolution;

		public Raster VisualRaster { get; protected set; }
		public Raster PriorityRaster { get; protected set; }
		public Raster ControlRaster { get; protected set; }
		public Raster AuxiliaryRaster { get; protected set; }

		public Vector2i Resolution {
			get { return pResolution; }

			set {
				if (pResolution != value) {
					pResolution = value;
					VisualRaster = new Raster(value, Raster.DefaultBlendedEgaColors, colorBlend: true);
					PriorityRaster = new Raster(value, Raster.DefaultEgaColors);
					ControlRaster = new Raster(value, Raster.DefaultEgaColors);
					AuxiliaryRaster = new Raster(value, Raster.DefaultEgaColors);
				}
				Clear();
			}
		}

		public PictureCanvas() {
			Resolution = new Vector2i(320, 190);
		}

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
				VisualRaster.Clear(visualValue);
			if ((mask & PictureLayer.Priority) != 0)
				PriorityRaster.Clear(priorityValue);
			if ((mask & PictureLayer.Control) != 0)
				ControlRaster.Clear(controlValue);
			if ((mask & PictureLayer.Auxiliary) != 0)
				AuxiliaryRaster.Clear(auxiliaryValue);
			Unlock();
		}

		public Raster GetRaster(PictureLayer layer) {
			switch (layer) {
				case PictureLayer.Visual: return VisualRaster;
				case PictureLayer.Priority: return PriorityRaster;
				case PictureLayer.Control: return ControlRaster;
				case PictureLayer.Auxiliary: return AuxiliaryRaster;
				default: throw new ArgumentException(typeof(PictureLayer).Name + " value " + layer + " is not valid.", "layer");
			}
		}
		
		/// <summary>
		/// Lock all the raster images to start drawing to them.
		/// </summary>
		public void Lock() {
			VisualRaster.Lock();
			PriorityRaster.Lock();
			ControlRaster.Lock();
			AuxiliaryRaster.Lock();
		}

		/// <summary>
		/// Unlock all the raster images, flushing their changes.
		/// </summary>
		public void Unlock() {
			VisualRaster.Unlock();
			PriorityRaster.Unlock();
			ControlRaster.Unlock();
			AuxiliaryRaster.Unlock();
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

		Visual = 1,
		Priority = 2,
		Control = 4,
		Auxiliary = 8,
	}
}
