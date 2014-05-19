using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Rendering {
	partial class ModelBuilder {
		public delegate double LineGenerator(double input);
		public delegate double LineGenerator2(ref Vector2d input);
		public delegate Vector2d CurveGenerator(double input);
		public delegate Vector3d SurfaceGenerator(ref Vector2d input, out Vector3d normal);
		public delegate Vector3d SimpleSurfaceGenerator(ref Vector2d input);
		public delegate Vector2d ChannelGenerator(ref Vector2d input, ref Vector3d position, ref Vector3d normal);

		static double ToRange(double value, double min, double max) { return value * (max - min) + min; }

		public static class CurveGenerators {
			public static readonly CurveGenerator Circle = (double input) => { double r = ToRange(input, -Math.PI / 2, Math.PI / 2); return new Vector2d(Math.Cos(r), Math.Sin(r)); };
		}

		public static class SurfaceGenerators {
			public static SimpleSurfaceGenerator Lathe(CurveGenerator original) {
				return (ref Vector2d input) => {
					Vector2d plane = original(input.Y);
					double r = input.X * Math.PI * 2, c = Math.Cos(r), s = Math.Sin(r);

					return new Vector3d(plane.X * c, plane.X * s, plane.Y);
				};
			}

			public static SimpleSurfaceGenerator Lathe(LineGenerator original) { return Lathe((input) => new Vector2d(original(input), input)); }

			public static Vector3d Sphere(ref Vector2d input, out Vector3d normal) {
				double x = input.X * Math.PI * 2, y = input.Y * Math.PI;
				Vector3d result = new Vector3d(
				    Math.Cos(x) * Math.Sin(y),
				    Math.Sin(x) * Math.Sin(y),
				    Math.Cos(y));
				normal = result;
				return result;
			}

			public static SimpleSurfaceGenerator Torus(double tubeRadiusRatio) {
				return (ref Vector2d input) => {
					double x = input.X * Math.PI * 2, y = input.Y * Math.PI * 2;
					return new Vector3d(
						(1 + tubeRadiusRatio * Math.Cos(x)) * Math.Cos(y),
						(1 + tubeRadiusRatio * Math.Cos(x)) * Math.Sin(y),
						tubeRadiusRatio * Math.Sin(x));
				};
			}
		}

		public ProceduralParameters StartLathe(LineGenerator original) { return StartProcedural(SurfaceGenerators.Lathe(original)); }
		public ProceduralParameters StartLathe(CurveGenerator original) { return StartProcedural(SurfaceGenerators.Lathe(original)); }

		public ProceduralParameters StartProcedural(SurfaceGenerator generator) { return new ProceduralParameters(generator, this); }
		public ProceduralParameters StartProcedural(SimpleSurfaceGenerator generator) { return new ProceduralParameters(generator, this); }

		public ProceduralParameters StartSphere() { return StartProcedural(SurfaceGenerators.Sphere); }
		public ProceduralParameters StartSphere(double radius) { return StartSphere().SetSize(radius); }
		public ProceduralParameters StartSphere(Sphere3d sphere) { return StartSphere(sphere.Radius).SetCentre(sphere.Position); }

		public ProceduralParameters StartTorus(double radius, double tubeRadius) { return StartProcedural(SurfaceGenerators.Torus(tubeRadius / radius)); }

		public class ProceduralParameters {
			public readonly ModelBuilder Builder;
			public Vector3d Centre = Vector3d.Zero;
			public Vector2i DetailLevel = new Vector2i(64, 64);
			public ModelMaterial Material;
			public Vector2d MaxInput = Vector2d.One;
			public Vector2d MinInput = Vector2d.Zero;
			public Vector3d Size = Vector3d.One;
			public SurfaceGenerator SurfaceGenerator;
			public ChannelGenerator TexelGenerator;
			public Matrix4d Transform = Matrix4d.Identity;

			static Vector2d BasicTexelGenerator(ref Vector2d input, ref Vector3d position, ref Vector3d normal) { return input; }

			static readonly ChannelGenerator BasicTexelGeneratorValue = BasicTexelGenerator;

			public static SurfaceGenerator Convert(SimpleSurfaceGenerator source) {
				if (source == null)
					throw new ArgumentNullException("source");
				return (ref Vector2d input, out Vector3d normal) => {
					const double sampleDistance = 1e-6;
					Vector3d position = source(ref input);
					Vector2d right = input + new Vector2d(sampleDistance, 0), down = input + new Vector2d(0, sampleDistance);
					Vector3d s2 = source(ref right) - position;
					Vector3d s3 = source(ref down) - position;
					normal = s2.Cross(s3).Normalized;
					return position;
				};
			}

			public ProceduralParameters(SimpleSurfaceGenerator surfaceGenerator, ModelBuilder builder) : this(Convert(surfaceGenerator), builder) { }

			public ProceduralParameters(SurfaceGenerator surfaceGenerator, ModelBuilder builder) {
				if (surfaceGenerator == null) throw new ArgumentNullException("surfaceGenerator");
				if (builder == null) throw new ArgumentNullException("builder");
				Builder = builder;
				SurfaceGenerator = surfaceGenerator;
				TexelGenerator = BasicTexelGeneratorValue;
			}

			public ProceduralParameters Build() {
				Vector2i detailLevel = new Vector2i(
				    Math.Max(DetailLevel.X + 1, 3),
				    Math.Max(DetailLevel.Y + 1, 3));
				Vector2d inputSpan = MaxInput - MinInput;

				Matrix4d transform = Transform * Matrix4d.Scale(Size) * Matrix4d.Translate(Centre);

				// Write the vertices.
				int vo = Builder.VertexCount;
				for (int y = 0, i = 0; y < detailLevel.Y; y++)
					for (int x = 0; x < detailLevel.X; x++, i++) {
						Vector2d input = (new Vector2d(x, y) * inputSpan + MinInput) / (Vector2d)(detailLevel - 1);
						Vector3d normal;
						Vector3d position = transform * SurfaceGenerator(ref input, out normal);

						Vector2d texel = TexelGenerator(ref input, ref position, ref normal);

						Builder.AddVertex(position);
						Builder.SetNormal(normal);
						Builder.SetTexel(texel);
					}

				// Write the indices.
				for (int y = 0, i = 0; y < detailLevel.Y - 1; y++) {
					int s = vo + y * detailLevel.X, t = s + detailLevel.X;

					for (int x = 0; x < detailLevel.X - 1; x++, i += 6, s++, t++)
						Builder.AddIndices(
							s, s + 1, t + 1,
							s, t + 1, t);
				}

				Builder.FinishPart(Primitive.Triangles, Material ?? new ModelMaterial());
				return this;
			}

			public ProceduralParameters SetCentre(Vector3d value) { Centre = value; return this; }
			public ProceduralParameters SetCentre(double x, double y, double z) { Centre = new Vector3d(x, y, z); return this; }
			public ProceduralParameters SetDetailLevel(Vector2i value) { DetailLevel = value; return this; }
			public ProceduralParameters SetDetailLevel(int x, int y) { DetailLevel = new Vector2i(x, y); return this; }
			public ProceduralParameters SetMaterial(ModelMaterial value) { Material = value; return this; }
			public ProceduralParameters SetMaxInput(Vector2d value) { MaxInput = value; return this; }
			public ProceduralParameters SetMaxInput(double value) { MaxInput = new Vector2d(value); return this; }
			public ProceduralParameters SetMaxInput(double x, double y) { MaxInput = new Vector2d(x, y); return this; }
			public ProceduralParameters SetMinInput(Vector2d value) { MinInput = value; return this; }
			public ProceduralParameters SetMinInput(double value) { MinInput = new Vector2d(value); return this; }
			public ProceduralParameters SetMinInput(double x, double y) { MinInput = new Vector2d(x, y); return this; }
			public ProceduralParameters SetSize(Vector3d value) { Size = value; return this; }
			public ProceduralParameters SetSize(double size) { Size = new Vector3d(size); return this; }
			public ProceduralParameters SetSize(double x, double y, double z) { Size = new Vector3d(x, y, z); return this; }
			public ProceduralParameters SetSurfaceGenerator(SurfaceGenerator value) { if (value == null) throw new ArgumentNullException("value"); SurfaceGenerator = value; return this; }
			public ProceduralParameters SetSurfaceGenerator(SimpleSurfaceGenerator value) { return SetSurfaceGenerator(Convert(value)); }
			public ProceduralParameters SetTexelGenerator(ChannelGenerator value) { TexelGenerator = value; return this; }
			public ProceduralParameters SetTransform(Matrix4d value) { Transform = value; return this; }
		}

		/// <summary>Create a model using a simple procedural generator that uses a regular grid over the value range. This can cause too much detail in some areas and too little in others depending upon the shape.</summary>
		/// <param name="parameters"></param>
		public void Procedural(ProceduralParameters parameters) {
			if (parameters == null)
				throw new ArgumentNullException("parameters");
			parameters.Build();
		}
	}
}
