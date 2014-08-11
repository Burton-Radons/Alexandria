using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glare.Graphics.Rendering;
using Glare.Assets;
using Glare;
using Glare.Graphics;
using GlareModel = Glare.Graphics.Rendering.Model;
using ModelResource = Glare.Assets.ModelAsset;

namespace Glare.Assets.Controls {
	/// <summary>
	/// A generic browser for a 3D view.
	/// </summary>
	public partial class ModelAssetBrowser : UserControl {
		/// <summary>Get a <see cref="BasicProgram"/> that can be used for rendering.</summary>
		public BasicProgram Program { get; private set; }

		OpenTK.GLControl Renderer;
		System.Drawing.Point LastMouseLocation;
		double ModelScale = 100;

		/// <summary>
		/// Initialise the browser.
		/// </summary>
		public ModelAssetBrowser() {
			ProjectionFieldOfView = Angle.Degrees(45);
			ProjectionNearPlaneDistance = 1;
			ProjectionFarPlaneDistance = 1000;

			ViewLookAtDistance = 100;

			Program = new BasicProgram();
			InitializeComponent();

			Renderer = new OpenTK.GLControl(new OpenTK.Graphics.GraphicsMode(new OpenTK.Graphics.ColorFormat(32), 24, 8, 16)) {
				Dock = DockStyle.Fill,
			};

			Renderer.MouseDown += RendererMouseDown;
			Renderer.MouseUp += RendererMouseUp;
			Renderer.MouseMove += RendererMouseMove;
			Renderer.Paint += RendererPaint;

			Renderer.Show();
			Panel.Controls.Add(Renderer);

			foreach (var value in typeof(BasicProgramDisplayMode).GetEnumValues())
				DisplayModeComboBox.Items.Add(value);
			DisplayModeComboBox.SelectedItem = Program.DisplayMode;

			WorldRotation = Matrix4d.Identity;
			WorldTranslation = Vector3d.Zero;
			WorldScale = 1;

			ClearColor = new Vector4d(0.5, 0.5, 0.5, 1);
		}

		/// <summary>
		/// Destroy the browser.
		/// </summary>
		~ModelAssetBrowser() {
		}

		private void DisplayModeComboBox_Click(object sender, EventArgs e) {

		}

		private void OnDisplayModeChanged(object sender, EventArgs e) {
			Program.DisplayMode = (BasicProgramDisplayMode)DisplayModeComboBox.SelectedItem;
			RefreshRenderer();
		}

		void RefreshRenderer() {
			Renderer.Refresh();
		}

		void RendererMouseDown(object sender, MouseEventArgs args) {
			if ((args.Button & (MouseButtons.Left | MouseButtons.Right)) != MouseButtons.None) {
				Renderer.Capture = true;
			}
			LastMouseLocation = args.Location;
		}

		void RendererMouseUp(object sender, MouseEventArgs args) {
			Renderer.Capture = false;
		}

		void RendererMouseMove(object sender, MouseEventArgs args) {
			if (!Renderer.Capture)
				return;

			int x = args.X - LastMouseLocation.X;
			int y = args.Y - LastMouseLocation.Y;
			LastMouseLocation = args.Location;

			if ((args.Button & MouseButtons.Left) != 0) {
				WorldRotation *= new Rotation4d(Angle.Degrees(-x), Angle.Zero, Angle.Zero).ToMatrix4d();
				WorldRotation *= new Rotation4d(Angle.Zero, Angle.Degrees(y), Angle.Zero).ToMatrix4d();
				RefreshRenderer();
			} else if ((args.Button & MouseButtons.Right) != 0) {
				ModelScale = Math.Max(1, ModelScale + x / 5.0);
				RefreshRenderer();
			}
		}

		/// <summary>Get the projection matrix, which by default is composed of the <see cref="ProjectionFieldOfView"/>, the <see cref="ProjectionNearPlaneDistance"/>, and the <see cref="ProjectionFarPlaneDistance"/>.</summary>
		public virtual Matrix4d Projection { get { return Matrix4d.PerspectiveFieldOfView(ProjectionFieldOfView, Renderer.Width / (double)Renderer.Height, ProjectionNearPlaneDistance, ProjectionFarPlaneDistance); } }

		/// <summary>Get or set the field of view for the default <see cref="Projection"/> matrix, which is by default 45 degrees.</summary>
		public virtual Angle ProjectionFieldOfView { get; set; }

		/// <summary>Get or set the near plane distance for the default <see cref="Projection"/> matrix, which is by default 1.</summary>
		public virtual double ProjectionNearPlaneDistance { get; set; }

		/// <summary>Get or set the far plane distance for the default <see cref="Projection"/> matrix, which is by default 1.</summary>
		public virtual double ProjectionFarPlaneDistance { get; set; }

		/// <summary>Get the view matrix. The default looks at the origin from <see cref="ViewLookAtDistance"/>.</summary>
		public virtual Matrix4d View { get { return Matrix4d.LookAt(new Vector3d(0, 0, -ViewLookAtDistance), Vector3d.Zero, Vector3d.UnitY); } }

		/// <summary>Get how far to be from the target when the camera is in look-at mode.</summary>
		public double ViewLookAtDistance { get; set; }

		/// <summary>Get the world matrix. The default combines <see cref="WorldTranslation"/>, <see cref="WorldScale"/>, and <see cref="WorldRotation"/>.</summary>
		public virtual Matrix4d World { get { return Matrix4d.Translate(WorldTranslation) * Matrix4d.Scale(WorldScale) * WorldRotation; } }

		/// <summary>The rotation to apply to the <see cref="World"/> matrix.</summary>
		public Matrix4d WorldRotation { get; set; }

		/// <summary>The scale to apply to the <see cref="World"/> matrix.</summary>
		public double WorldScale { get; set; }

		/// <summary>The translation to apply to the <see cref="World"/> matrix.</summary>
		public Vector3d WorldTranslation { get; set; }

		/// <summary>The color to clear the rendering viewport to. The default is (0.5, 0.5, 0.5, 1).</summary>
		public Vector4d ClearColor { get; set; }

		/// <summary>Start rendering. The default sets up the viewport, clears (using <see cref="ClearColor"/>), sets the depth test to less and turns on depth writing, then sets the <see cref="Program"/>'s <see cref="Projection"/>, <see cref="View"/>, and <see cref="World"/> matrices.</summary>
		protected void RenderStart() {
			OpenTK.Graphics.OpenGL4.GL.Viewport(0, 0, Renderer.Width, Renderer.Height);
			Device.Clear(ClearColor, 1);

			Device.DepthTest = ComparisonFunction.Less;
			Device.DepthWrite = true;

			Program.Projection = Projection;
			Program.View = View;
			Program.World = World;
		}

		/// <summary>Finish up rendering. The default swaps buffers.</summary>
		protected void RenderEnd() {
			Renderer.SwapBuffers();
		}

		/// <summary>Perform any rendering.</summary>
		protected virtual void Render() {
		}
		
		/// <summary>Handle the renderer paint message. The default calls <see cref="RenderStart"/>, <see cref="Render"/>, then <see cref="RenderEnd"/>.</summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		protected virtual void RendererPaint(object sender, PaintEventArgs args) {
			RenderStart();
			Render();
			RenderEnd();
		}
	}

	/// <summary>A browser for a model asset.</summary>
	public class GlareModelAssetBrowser : ModelAssetBrowser {
		// FarPlaneDistance: 2 + Model.Bounds.Radius * 16

		/// <summary>The model to render.</summary>
		public GlareModel Model { get { return ModelAsset.Content; } }

		/// <summary>Get the model to render.</summary>
		public readonly ModelAsset ModelAsset;

		/// <summary>
		/// Initialise the browser.
		/// </summary>
		/// <param name="asset"></param>
		public GlareModelAssetBrowser(ModelAsset asset) {
			if (asset == null)
				throw new ArgumentNullException("asset");
			ModelAsset = asset;

			ViewLookAtDistance = Model.Bounds.Radius * 3;
			WorldTranslation = -Model.Bounds.Position;
		}

		/// <summary>Render the model.</summary>
		protected override void Render() {
			Model.Draw(Program);
		}
	}
}
