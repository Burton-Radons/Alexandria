using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glare.Graphics.Rendering;
using Alexandria.Resources;
using Glare;
using Glare.Graphics;
using GlareModel = Glare.Graphics.Rendering.Model;
using ModelResource = Alexandria.Resources.Model;

namespace Alexandria.Controls {
	public partial class ModelResourceBrowser : UserControl {
		public GlareModel Model { get { return ModelResource.Content; } }
		public readonly ModelResource ModelResource;
		public readonly BasicProgram Program;

		OpenTK.GLControl Renderer;
		Matrix4d Rotation = Matrix4d.Identity;
		System.Drawing.Point LastMouseLocation;
		double ModelScale = 100;

		public ModelResourceBrowser(ModelResource modelResource) {
			ModelResource = modelResource;
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
				Rotation *= new Quaternion(Angle.Degrees(-x), Angle.Zero, Angle.Zero).ToMatrix4d();
				Rotation *= new Quaternion(Angle.Zero, Angle.Degrees(y), Angle.Zero).ToMatrix4d();
				RefreshRenderer();
			} else if ((args.Button & MouseButtons.Right) != 0) {
				ModelScale = Math.Max(1, ModelScale + x / 5.0);
				RefreshRenderer();
			}
		}

		void RendererPaint(object sender, PaintEventArgs args) {
			OpenTK.Graphics.OpenGL4.GL.Viewport(0, 0, Renderer.Width, Renderer.Height);
			Graphics.Clear(new Vector4d(0.5, 0.5, 0.5, 1), 1);

			Graphics.DepthTest = ComparisonFunction.Less;
			Graphics.DepthWrite = true;

			OpenTK.Matrix4 x = OpenTK.Matrix4.CreateRotationX(1);
			OpenTK.Matrix4 y = OpenTK.Matrix4.CreateTranslation(4, 5, 6);
			OpenTK.Matrix4 z = x * y;

			Matrix4d mx = Matrix4d.Rotate(Angle.Zero, Angle.Radians(1), Angle.Zero);
			Matrix4d my = Matrix4d.Translate(4, 5, 6);
			Matrix4d mz = mx * my;

			Program.Projection = Matrix4d.PerspectiveFieldOfView(Angle.Degrees(45), Renderer.Width / (double)Renderer.Height, 1, 2 + Model.Bounds.Radius * 16);
			Program.View = Matrix4d.LookAt(new Vector3d(0, 0, -Model.Bounds.Radius * 3), Vector3d.Zero, Vector3d.UnitY);
			Program.World = Matrix4d.Translate(-Model.Bounds.Position) * Matrix4d.Scale(ModelScale / 100.0) * Rotation;
			Model.Draw(Program);

			Renderer.SwapBuffers();
		}
	}
}
