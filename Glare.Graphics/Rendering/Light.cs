using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Rendering {
	/// <summary>The basic parameters of a light.</summary>
	/// <remarks>
	/// Lights have a number of ways in which their intensity attenuates. All of them have a form of a <see cref="LightAttenuation"/> parameter and start and end parameters, and use these to compute a factor between 1.0 (fully bright) to 0.0 (black) that are multiplied together with the light's brightness to get the final brightness. Directional lights, spotlights, omnidirectional lights, beam lights, and any hybrid form of all of them can be represented with these parameters. <see cref="LightAngleAttenuation"/> for <see cref="Angle"/>s and <see cref="LightLengthAttenuation"/> for <see cref="Length"/>s are used. <see cref="LightLengthAttenuation"/> also has the <see cref="LightLengthAttenuation.DistanceFunction"/> parameter to determine how distance is calculated, using the <see cref="LightDistanceFunction"/> values: <see cref="LightDistanceFunction.Plane"/> for distance from the plane defined by <see cref="Position"/> and <see cref="Direction"/>, and <see cref="LightDistanceFunction.Sphere"/> for distance from the <see cref="Position"/> point.
	/// 
	/// The <see cref="LightLengthAttenuation"/> parameters for <see cref="DistanceAttenuation"/> define regular distance attenuation. Planar distance attenuation can be used for odd effects, while spherical attenuation is used for spotlights, beam lights, and omnidirectional lights.
	/// 
	/// The <see cref="LightLengthAttenuation"/> parameters for <see cref="NearAttenuation"/> and <see cref="FarAttenuation"/> add layers onto distance attenuation. Near attenuation is backwards, so that points at the start distance have a factor of 0 and points at the end distance have a factor greater than zero. This is used to avoid harsh clipping for objects that are too close to lights, and for special effects. Far attenuation is the same as <see cref="DistanceAttenuation"/>, but is commonly used to clamp a light's realistic distance attenuation so that it can be smoothly clipped to zero instead of emanating light to infinity.
	/// 
	/// The <see cref="LightAngleAttenuation"/> parameters for <see cref="AngleAttenuation"/> use the angle from the position and the ray defined by the <see cref="Position"/> and <see cref="Direction"/> of the light. This is used for spotlights. <see cref="LightAngleAttenuation.Radius"/> can be used to make the spotlight cone wider at its base, or even make the spotlight into a column or beam.
	/// 
	/// <see cref=""/>
	/// </remarks>
	public class Light : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		LightAngleAttenuation angleAttenuation;
		LightLengthAttenuation distanceAttenuation, nearAttenuation, farAttenuation;
		Vector4d diffuseColor = Vector4d.One, specularColor = Vector4d.One;
		Vector3d direction;
		Vector3d position = Vector3d.Zero;
		Angle angleAttenuationStart = Angle.Turn, angleAttenuationEnd = Angle.Turn;

		/// <summary>Get or set the attenuation parameters for angle attenuation. This uses the angular difference between a point and the <see cref="Position"/> and <see cref="Direction"/>, which is used for spotlights. This is disabled if <see cref="Direction"/> is zero. The default is none.</summary>
		public LightAngleAttenuation AngleAttenuation {
			get { return angleAttenuation; }
			set { SetProperty("AngleAttenuation", ref angleAttenuation, ref value); }
		}

		/// <summary>Get or set the color of the <see cref="Light"/>.</summary>
		public Vector4d DiffuseColor {
			get { return diffuseColor; }
			set { SetProperty("DiffuseColor", ref diffuseColor, ref value); }
		}

		/// <summary>Get or set the direction of the <see cref="Light"/>. This is automatically normalized on assignment. Setting this to an identity vector turns the light nondirectional.</summary>
		public Vector3d Direction {
			get { return direction; }

			set {
				if (value.MagnitudeSquared != 0)
					value.NormalizeInPlace();
				SetProperty("Direction", ref direction, ref value);
			}
		}

		/// <summary>Get or set the parameters for distance attenuation. This uses distance from the <see cref="Position"/> or the plane defined by the <see cref="Position"/> and <see cref="Direction"/> for attenuation. The default is none.</summary>
		public LightLengthAttenuation DistanceAttenuation {
			get { return distanceAttenuation; }
			set { SetProperty("Attenuation", ref distanceAttenuation, ref value); }
		}

		/// <summary>Get or set the parameters for a second form of distance attenuation, after <see cref="DistanceAttenuation"/>. This uses distance from the <see cref="Position"/> or the plane defined by the <see cref="Position"/> and <see cref="Direction"/> for attenuation. The default is none. This is commonly used with a sharp cutoff attenuation method when <see cref="DistanceAttenuation"/> uses some other attenuation method that is infinite in distance before reaching zero.</summary>
		public LightLengthAttenuation FarAttenuation {
			get { return farAttenuation; }
			set { SetProperty("FarAttenuation", ref farAttenuation, ref value); }
		}

		/// <summary>Get or set the parameters for near distance attenuation. This uses distance from the <see cref="Position"/> or the plane defined by the <see cref="Position"/> and <see cref="Direction"/> for attenuation, in reverse. The default is none. This is used to darken lights immediately before them to avoid harsh clipping for objects that are too close to the light.</summary>
		public LightLengthAttenuation NearAttenuation {
			get { return nearAttenuation; }
			set { SetProperty("NearAttenuation", ref nearAttenuation, ref value); }
		}

		/// <summary>Get or set the position of the <see cref="Light"/>.</summary>
		public Vector3d Position {
			get { return position; }
			set { SetProperty("Position", ref position, ref value); }
		}

		/// <summary>Get or set the color of the <see cref="Light"/>'s reflection on objects.</summary>
		public Vector4d SpecularColor {
			get { return specularColor; }
			set { SetProperty("SpecularColor", ref specularColor, ref value); }
		}

		/// <summary>Set the <see cref="Light"/>'s <see cref="Position"/> and <see cref="Direction"/> parameters to look at a target.</summary>
		/// <param name="source">The <see cref="Position"/> of the <see cref="Light"/>.</param>
		/// <param name="target">Where the <see cref="Light"/> is oriented.</param>
		public void LookAt(Vector3d source, Vector3d target) {
			Position = source;
			Direction = target - source;
		}

		protected void OnPropertyChanged(string propertyName) {
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void SetProperty<T>(string propertyName, ref T container, ref T value) {
			container = value;
			OnPropertyChanged(propertyName);
		}

		public void Setup(Plane3d plane) { Direction = plane.Normal; Position = plane.Distance * direction; }

		/// <summary>Set the <see cref="Position"/> and <see cref="Direction"/> parameters using the <see cref="Ray3"/>.</summary>
		/// <param name="ray"></param>
		public void Setup(Ray3d ray) { Position = ray.Origin; Direction = ray.Normal; }
	}

	/// <summary>
	/// The type of distance attenuation to use for a <see cref="Light"/>. <see cref="InverseSquared"/> is physically correct.
	/// </summary>
	public enum LightAttenuation {
		/// <summary>Do not attenuate.</summary>
		None,

		/// <summary>Linearly interpolate between the start and end points.</summary>
		Linear,

		/// <summary>Interpolate between the start and end points using smoothstep, which avoids the harsh angle of <see cref="Linear"/> interpolation.</summary>
		SmoothStep,

		/// <summary>Interpolate between the start and end points using smootherstep, which avoids the harsh angle of <see cref="Linear"/> interpolation and gives a more pleasing curve than <see cref="SmoothStep"/>.</summary>
		SmootherStep,

		/// <summary>Use ((end - start) / (distance - start)) for attenuation.</summary>
		Inverse,

		/// <summary>Use ((end - start) / (distance - start) ** 2) for attenuation.</summary>
		InverseSquared,
	}

	public interface ILightAttenuationParameters<T> {
		/// <summary>Get or set the method to use for attenuating values.</summary>
		LightAttenuation Method { get; set; }

		/// <summary>Get or set the start of the attenuation. Values below the start will have a factor of 1. Setting this value to greater than <see cref="End"/> causes <see cref="End"/> to be adjusted to this value.</summary>
		T Start { get; set; }

		/// <summary>Get or set the end marker of the attenuation.</summary>
		T End { get; set; }
	}

	public struct LightAngleAttenuation : ILightAttenuationParameters<Angle> {
		Length radius;
		Angle start, end;

		public LightAttenuation Method { get; set; }

		/// <summary>Get or set the radius of the cone at the origin.</summary>
		public Length Radius {
			get { return Radius; }
			set { radius = value.Max(Length.Zero); }
		}

		public Angle Start {
			get { return start; }
			set { if ((start = value.Max(Angle.Zero)) > end) end = start; }
		}

		public Angle End {
			get { return end; }
			set { if (start > (end = value.Max(Angle.Zero))) start = end; }
		}

		public LightAngleAttenuation(LightAttenuation method, Angle start, Angle end)
			: this() {
			Method = method;
			Start = start;
			End = end;
		}

		public static LightAngleAttenuation Degrees(LightAttenuation method, double start, double end) { return new LightAngleAttenuation(method, Angle.Degrees(start), Angle.Degrees(end)); }
		public static LightAngleAttenuation Radians(LightAttenuation method, double start, double end) { return new LightAngleAttenuation(method, Angle.Radians(start), Angle.Radians(end)); }
	}

	/// <summary>
	/// This specifies how distance is calculated for <see cref="LightLengthAttenuation"/>.
	/// </summary>
	public enum LightDistanceFunction {
		/// <summary>
		/// Use the distance from the plane defined by the <see cref="Light"/>'s <see cref="Light.Position"/> and <see cref="Light.Direction"/> parameters.
		/// </summary>
		Plane,

		/// <summary>Use the distance from the <see cref="Light"/>'s <see cref="Light.Position"/>.</summary>
		Sphere,
	}

	public struct LightLengthAttenuation : ILightAttenuationParameters<Length> {
		Length start, end;

		/// <summary>Get or set how distance is calculated.</summary>
		public LightDistanceFunction DistanceFunction { get; set; }

		public LightAttenuation Method { get; set; }

		public Length Start {
			get { return start; }
			set { if ((start = value.Max(Length.Zero)) > end) end = start; }
		}

		public Length End {
			get { return end; }
			set { if (start > (end = value.Max(Length.Zero))) start = end; }
		}

		public LightLengthAttenuation(LightAttenuation method, Length start, Length end, LightDistanceFunction distanceFunction)
			: this() {
			Method = method;
			Start = start;
			End = end;
			DistanceFunction = distanceFunction;
		}

		public static LightLengthAttenuation Metres(LightAttenuation method, double start, double end, LightDistanceFunction distanceFunction = LightDistanceFunction.Sphere) { return new LightLengthAttenuation(method, Length.Metres(start), Length.Metres(end), distanceFunction); }
	}
}
