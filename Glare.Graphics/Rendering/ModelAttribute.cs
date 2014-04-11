using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Rendering
{
	/// <summary>
	/// A vertex array channel for a <see cref="ModelPart"/>.
	/// </summary>
	public class ModelAttribute
	{
		/// <summary>Get the <see cref="ModelAttributeChannel"/> for the <see cref="ProgramAttribute"/> to bind this to.</summary>
		public readonly ModelAttributeChannel Channel;

		/// <summary>Get the buffer to bind this to.</summary>
		public readonly GraphicsBuffer Buffer;

		/// <summary>Get the zero-based index of the <see cref="Channel"/> to bind to.</summary>
		public readonly int Index;

		/// <summary>Get the name of the channel to bind to.</summary>
		public readonly string Name;

		/// <summary>Get the offset in bytes from the start of the <see cref="Buffer"/> to the attribute of the first vertex.</summary>
		public readonly int OffsetInBytes;

		/// <summary>Get the data format of the attribute. This must be a member of <see cref="VectorFormats"/>.</summary>
		public readonly Format Format;

		/// <summary>Get the number of bytes between each attribute value, or 0 to use the size of the <see cref="Format"/>.</summary>
		public readonly int Stride;

		public ModelAttribute(string name, GraphicsBuffer buffer, int offsetInBytes, Format format, int stride, ModelAttributeChannel channel, int index)
		{
			this.Channel = channel;
			this.Index = index;
			this.Name = name;
			this.Buffer = buffer;
			this.OffsetInBytes = offsetInBytes;
			this.Format = format;
			this.Stride = stride;
		}

		public ModelAttribute(GraphicsBuffer buffer, int offsetInBytes, Format format, int stride, ModelAttributeChannel channel, int index) : this(GetName(channel, index), buffer, offsetInBytes, format, stride, channel, index) { }

		static string GetName(ModelAttributeChannel channel, int index)
		{
			if(index > 0)
				return channel.ToString() + index;
			return channel.ToString();
		}
	}

	/// <summary>
	/// Indentifies the channel for this model part.
	/// </summary>
	public enum ModelAttributeChannel
	{
		Diffuse,
		Normal,
		Position,
		Texel,
		BoneIndices,
		BoneWeights,
	}

	/// <summary>
	/// A source for <see cref="ModelAttributeChannel"/>-based <see cref="ProgramAttribute"/>s.
	/// </summary>
	public interface IChannelAttributeSource
	{
		/// <summary>Get an attribute, or <c>null</c> if it's not supported.</summary>
		/// <param name="attribute">The attribute to read. For a <see cref="ModelAttributeChannel.Position"/> channel, index of 0 must be supported. For all others, 0 should be supported if possible. For <see cref="ModelAttributeChannel.Texel"/>, indices above 0 are for different material channels; for all others, indices above zero have no universal meaning.</param>
		/// <returns>The attribute or <c>null</c>.</returns>
		ProgramAttribute TryGetChannelAttribute(ModelAttribute attribute);
	}
}
