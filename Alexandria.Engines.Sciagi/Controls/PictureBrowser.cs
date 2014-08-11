using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alexandria.Engines.Sciagi.Resources;

namespace Alexandria.Engines.Sciagi.Controls {
	/// <summary>
	/// Browses a <see cref="Picture"/>.
	/// </summary>
	public partial class PictureBrowser : UserControl {
		PictureLayer layer;

		static bool BlendColorsDefault = true;

		/// <summary>
		/// Get the canvas to render onto.
		/// </summary>
		public PictureCanvas Canvas { get; private set; }

		/// <summary>
		/// Get the <see cref="Picture"/> this views.
		/// </summary>
		public Picture Picture { get; private set; }

		/// <summary>
		/// Get or set whether to dither-blend the colours. This has no effect on a VGA picture.
		/// </summary>
		public bool BlendColors {
			get { return Canvas.Visual.DitherBlend; }

			set {
				BlendColorsDefault = value;
				Canvas.Visual.DitherBlend = value;
				SetStipplingSpitButtonImage();
				Redraw();
			}
		}

		/// <summary>
		/// Get or set the visual to display.
		/// </summary>
		public PictureLayer Layer {
			get { return layer; }

			set {
				layer = value;
				picture.Image = Canvas.GetRaster(value).Image4x;
				visualButton.CheckState = (value == PictureLayer.Visual) ? CheckState.Checked : CheckState.Unchecked;
				priorityButton.CheckState = (value == PictureLayer.Priority) ? CheckState.Checked : CheckState.Unchecked;
				controlButton.CheckState = (value == PictureLayer.Control) ? CheckState.Checked : CheckState.Unchecked;
				auxiliaryButton.CheckState = (value == PictureLayer.Auxiliary) ? CheckState.Checked : CheckState.Unchecked;
			}
		}

		/// <summary>
		/// Initialise the browser.
		/// </summary>
		/// <param name="picture"></param>
		public PictureBrowser(Picture picture) {
			Picture = picture;
			Canvas = new PictureCanvas(picture);
			Canvas.Visual.DitherBlend = BlendColorsDefault;

			InitializeComponent();
			SetStipplingSpitButtonImage();

			instructionTrackBar.Width = Math.Max(picture.Instructions.Count, 200);
			instructionTrackBar.Maximum = picture.Instructions.Count;
			instructionTrackBar.Value = picture.Instructions.Count;

			this.picture.Width = (int)(Canvas.Resolution.X * 4 / 1.333);
			this.picture.Height = Canvas.Resolution.Y * 4;

			Layer = PictureLayer.Visual;
		}

		void SetStipplingSpitButtonImage() { stipplingSplitButton.Image = BlendColors ? Properties.Resources.PictureBlendTrue : Properties.Resources.PictureBlendFalse; }

		private void OnInstructionValueChanged(object sender, EventArgs e) {
			if (Picture == null)
				return;

			int value = instructionTrackBar.Value;
			instructionCounter.Text = value + " of " + Picture.Instructions.Count;
			Redraw();
		}

		private void OnVisualClicked(object sender, EventArgs e) { Layer = PictureLayer.Visual; }
		private void OnPriorityClicked(object sender, EventArgs e) { Layer = PictureLayer.Priority; }
		private void OnControlClicked(object sender, EventArgs e) { Layer = PictureLayer.Control; }
		private void OnAuxiliaryClicked(object sender, EventArgs e) { Layer = PictureLayer.Auxiliary; }

		private void OnStipplingSetFalse(object sender, EventArgs e) { BlendColors = false; }
		private void OnStipplingSetTrue(object sender, EventArgs e) { BlendColors = true; }

		private void OnStipplingButtonClick(object sender, EventArgs e) { BlendColors = !BlendColors; }

		void Redraw() {
			new PictureRenderer(Canvas).Render(Picture.Instructions, instructionTrackBar.Value);
			picture.Refresh();
		}
	}
}
