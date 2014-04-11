using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	/// <summary>
	/// Describes blending state. There are presets available in <see cref="BlendStates"/>.
	/// </summary>
	public struct BlendState
	{
		/// <summary>Get or set the <see cref="BlendFactor"/> to multiply against the source RGB value.</summary>
		public BlendFactor SourceRgb;

		/// <summary>Get or set the <see cref="BlendFactor"/> to multiply against the source Alpha value.</summary>
		public BlendFactor SourceAlpha;

		/// <summary>Get or set the <see cref="BlendFactor"/> to multiply against the destination RGB value.</summary>
		public BlendFactor DestinationRgb;

		/// <summary>Get or set the <see cref="BlendFactor"/> to multiply against the destination Alpha value.</summary>
		public BlendFactor DestinationAlpha;

		/// <summary>Get or set the <see cref="BlendEquation"/> to use on the RGB components.</summary>
		public BlendEquation EquationRgb;

		/// <summary>Get or set the <see cref="BlendEquation"/> to use on the Alpha component.</summary>
		public BlendEquation EquationAlpha;

		public static readonly BlendState Opaque = new BlendState(BlendFactor.One, BlendFactor.Zero);

		public BlendState(BlendFactor sourceFactor, BlendFactor destinationFactor) : this(sourceFactor, destinationFactor, BlendEquation.Add) { }

		public BlendState(BlendFactor source, BlendFactor destination, BlendEquation equation) : this(source, source, destination, destination, equation, equation) { }
		
		public BlendState(BlendFactor sourceRgb, BlendFactor sourceAlpha, BlendFactor destinationRgb, BlendFactor destinationAlpha, BlendEquation equationRgb, BlendEquation equationAlpha)
		{
			SourceRgb = sourceRgb;
			SourceAlpha = sourceAlpha;
			DestinationRgb = destinationRgb;
			DestinationAlpha = destinationAlpha;
			EquationRgb = equationRgb;
			EquationAlpha = equationAlpha;
		}
	}

	public static class BlendStates
	{
		/// <summary>Add the source with non-premultiplied alpha to the destination.</summary>
		public static readonly BlendState Add = new BlendState(BlendFactor.SourceAlpha, BlendFactor.One);

		/// <summary>A blend state that outputs the source value.</summary>
		public static readonly BlendState Opaque = new BlendState(BlendFactor.One, BlendFactor.Zero);

		/// <summary>Transparency using non-premultiplied alpha.</summary>
		public static readonly BlendState Alpha = new BlendState(BlendFactor.SourceAlpha, BlendFactor.OneMinusSourceAlpha);

		/// <summary>Transparency using premultiplied alpha.</summary>
		public static readonly BlendState PremultipliedAlpha = new BlendState(BlendFactor.One, BlendFactor.OneMinusSourceAlpha);

		/// <summary>Subtract the source with non-premultiplied alpha from the destination.</summary>
		public static readonly BlendState Subtract = new BlendState(BlendFactor.SourceAlpha, BlendFactor.One, BlendEquation.ReverseSubtract);
	}

	public enum BlendEquation
	{
		/// <summary>Add the components together.</summary>
		Add = BlendEquationMode.FuncAdd,

		/// <summary>Subtract the computed destination value from the computed source value.</summary>
		Subtract = BlendEquationMode.FuncSubtract,

		/// <summary>Subtract the computed source value from the computed destination value.</summary>
		ReverseSubtract = BlendEquationMode.FuncReverseSubtract,

		/// <summary>Compute the minimum of both computed values.</summary>
		Min = BlendEquationMode.Min,

		/// <summary>Compute the maximum of both computed values.</summary>
		Max = BlendEquationMode.Max,
	}

	public enum BlendFactor
	{
		/// <summary>Zero for all factors.</summary>
		Zero = BlendingFactorSrc.Zero,

		/// <summary>One for all factors.</summary>
		One = BlendingFactorSrc.One,

		/// <summary>Source color.</summary>
		Source = BlendingFactorDest.SrcColor,

		/// <summary>One minus the source color.</summary>
		OneMinusSource = BlendingFactorDest.OneMinusSrcColor,

		/// <summary>Destination color.</summary>
		Destination = BlendingFactorSrc.DstColor,

		/// <summary>One minus the destination color.</summary>
		OneMinusDestination = BlendingFactorSrc.OneMinusDstColor,

		/// <summary>Source alpha for all factors.</summary>
		SourceAlpha = BlendingFactorSrc.SrcAlpha,

		/// <summary>One minus the source alpha for all factors.</summary>
		OneMinusSourceAlpha = BlendingFactorSrc.OneMinusSrcAlpha,

		/// <summary>Destination alpha for all factors.</summary>
		DestinationAlpha = BlendingFactorSrc.DstAlpha,

		/// <summary>One minus the destination alpha for all factors.</summary>
		OneMinusDestinationAlpha = BlendingFactorSrc.OneMinusDstAlpha,

		/// <summary>Constant color.</summary>
		Constant = BlendingFactorSrc.ConstantColor,

		/// <summary>One minus the constant color.</summary>
		OneMinusConstant = BlendingFactorSrc.OneMinusConstantColor,

		/// <summary>Constant alpha for all factors.</summary>
		ConstantAlpha = BlendingFactorSrc.ConstantAlpha,

		/// <summary>One minus the constant alpha for all factors.</summary>
		OneMinusConstantAlpha = BlendingFactorSrc.OneMinusConstantAlpha,

		/// <summary>The minimum of the source alpha and one minus the destination alpha (min(Source.Alpha, (1 - Destination.Alpha))).</summary>
		SourceAlphaSaturate = BlendingFactorSrc.SrcAlphaSaturate,

		/// <summary>Second source color.</summary>
		Second = BlendingFactorSrc.Src1Color,

		/// <summary>One minus the second source color.</summary>
		OneMinusSecond = BlendingFactorSrc.OneMinusSrc1Color,

		/// <summary>Second source alpha for all factors.</summary>
		SecondAlpha = BlendingFactorSrc.Src1Alpha,

		/// <summary>One minus the second source alpha for all factors.</summary>
		OneMinusSecondAlpha = BlendingFactorSrc.OneMinusSrc1Alpha,
	}
}
