using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	/// <summary>The base class of the <see cref="Texture2D"/> hierarchy and the <see cref="Sampler"/>.</summary>
	public abstract class SamplerResource : GraphicsResource
	{
		/// <summary>Get or set the comparison function to use for depth textures.</summary>
		public ComparisonFunction CompareFunction
		{
			get { return (ComparisonFunction)Get1i(SamplerParameterName.TextureCompareFunc); }
			set { Set(SamplerParameterName.TextureCompareFunc, (int)value); }
		}

		/// <summary>Get or set whether to compare the texture's x channel with the depth value.</summary>
		public TextureCompareMode CompareMode
		{
			get { return (TextureCompareMode)Get1i(SamplerParameterName.TextureCompareMode); }
			set { Set(SamplerParameterName.TextureCompareMode, (int)value); }
		}

		/// <summary>Get or set a fixed bias value that is to be added to the level-of-detail parameter for the texture before texture sampling. The specified value is added to the shader-supplied bias value (if any) and subsequently clamped into the implementation-defined range [-biasmax, biasmax], where biasmax is the value of the implementation defined constant <see cref="Capabilities.MaxTextureLodBias"/>​. The initial value is 0.0.</summary>
		public double LodBias
		{
			get { return Get1d(SamplerParameterName.TextureLodBias); }
			set { Set(SamplerParameterName.TextureLodBias, value); }
		}

		/// <summary>Get or set the between-texel filter to use when the texture is magnified.</summary>
		public TextureFilter MagnificationFilter
		{
			get { return (TextureFilter)Get1i(SamplerParameterName.TextureMagFilter); }
			set { Set(SamplerParameterName.TextureMagFilter, (int)value); }
		}

		/// <summary> Get or set the maximum level-of-detail parameter (initially 1000). This limits the selection of the lowest resolution mipmap.</summary>
		public double MaxLod
		{
			get { return Get1d(SamplerParameterName.TextureMaxLod); }
			set { Set(SamplerParameterName.TextureMaxLod, value); }
		}

		/// <summary>Get or set the between-mipmap level filter to use when the texture is minified.</summary>
		public TextureFilter MipmapFilter
		{
			get { return GetMipmapFilter((TextureMinFilter)Get1i(SamplerParameterName.TextureMinFilter)); }
			set { Set(SamplerParameterName.TextureMinFilter, (int)JoinMinFilter(value, MinificationFilter)); }
		}

		/// <summary>Get or set the between-texel filter to use when the texture is minified.</summary>
		public TextureFilter MinificationFilter
		{
			get { return GetMinificationFilter((TextureMinFilter)Get1i(SamplerParameterName.TextureMinFilter)); }
			set { Set(SamplerParameterName.TextureMinFilter, (int)JoinMinFilter(MipmapFilter, MinificationFilter)); }
		}

		/// <summary> Get or set the minimum level-of-detail parameter (initially -1000). This limits the selection of the highest resolution mipmap.</summary>
		public double MinLod
		{
			get { return Get1d(SamplerParameterName.TextureMinLod); }
			set { Set(SamplerParameterName.TextureMinLod, value); }
		}

		/// <summary>Get or set how to handle sample coordinates that are out of range on the first axis.</summary>
		public TextureWrap WrapX
		{
			get { return (TextureWrap)Get1i(SamplerParameterName.TextureWrapS); }
			set { Set(SamplerParameterName.TextureWrapS, (int)value); }
		}

		/// <summary>Get or set how to handle sample coordinates that are out of range on the second axis.</summary>
		public TextureWrap WrapY
		{
			get { return (TextureWrap)Get1i(SamplerParameterName.TextureWrapT); }
			set { Set(SamplerParameterName.TextureWrapT, (int)value); }
		}

		/// <summary>Get or set how to handle sample coordinates that are out of range on the third axis.</summary>
		public TextureWrap WrapZ
		{
			get { return (TextureWrap)Get1i(SamplerParameterName.TextureWrapR); }
			set { Set(SamplerParameterName.TextureWrapR, (int)value); }
		}

		internal SamplerResource(int id)
			: base(id) { }

		protected abstract double Get1d(SamplerParameterName pname);
		protected abstract int Get1i(SamplerParameterName pname);

		internal static TextureFilter GetMinificationFilter(TextureMinFilter filter)
		{
			switch (filter)
			{
				case TextureMinFilter.Nearest:
				case TextureMinFilter.NearestMipmapNearest:
				case TextureMinFilter.NearestMipmapLinear:
					return TextureFilter.Nearest;
				case TextureMinFilter.Linear:
				case TextureMinFilter.LinearMipmapNearest:
				case TextureMinFilter.LinearMipmapLinear:
					return TextureFilter.Linear;
				default: throw new NotSupportedException();
			}
		}

		internal static TextureFilter GetMipmapFilter(TextureMinFilter filter)
		{
			switch (filter)
			{
				case TextureMinFilter.Linear:
				case TextureMinFilter.Nearest:
					return TextureFilter.None;
				case TextureMinFilter.LinearMipmapNearest:
				case TextureMinFilter.NearestMipmapNearest:
					return TextureFilter.Nearest;
				case TextureMinFilter.LinearMipmapLinear:
				case TextureMinFilter.NearestMipmapLinear:
					return TextureFilter.Linear;
				default: throw new NotSupportedException();
			}
		}

		internal static TextureMinFilter JoinMinFilter(TextureFilter mipmap, TextureFilter minification)
		{
			switch (minification)
			{
				case TextureFilter.Nearest:
					switch (mipmap)
					{
						case TextureFilter.None: return TextureMinFilter.Nearest;
						case TextureFilter.Nearest: return TextureMinFilter.NearestMipmapNearest;
						case TextureFilter.Linear: return TextureMinFilter.NearestMipmapLinear;
						default: throw new ArgumentException("minification");
					}

				case TextureFilter.Linear:
					switch (mipmap)
					{
						case TextureFilter.None: return TextureMinFilter.Linear;
						case TextureFilter.Nearest: return TextureMinFilter.LinearMipmapNearest;
						case TextureFilter.Linear: return TextureMinFilter.LinearMipmapLinear;
						default: throw new ArgumentException("minification");
					}

				default:
					throw new ArgumentException("mipmap");
			}
		}

		protected abstract void Set(SamplerParameterName pname, double value);
		protected abstract void Set(SamplerParameterName pname, int value);
	}
}
