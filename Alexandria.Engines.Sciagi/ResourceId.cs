using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	/// <summary>Identifies a resource.</summary>
	public struct ResourceId : IEquatable<ResourceId> {
		/// <summary>
		/// Get the type of the resource masked from the SCI1+ marker.
		/// </summary>
		public ResourceType Type { get { return FullType & ResourceType.Mask; } }

		/// <summary>Get the type of the resource.</summary>
		public readonly ResourceType FullType;

		/// <summary>Get the index of the resource.</summary>
		public readonly int Id;

		/// <summary>Get the offset of the resource's data in its page.</summary>
		public readonly int Offset;

		/// <summary>Get the page of the resource.</summary>
		public readonly int Page;

		/// <summary>Get the version of the resource data.</summary>
		public ResourceMapVersion Version;

		/// <summary>Get the index to match the resource. For SCI0 and earlier, this combines <see cref="Id"/> with <see cref="Type"/>; for all others it's just <see cref="Id"/>.</summary>
		public int CombinedIndex {
			get {
				if (Version <= ResourceMapVersion.Sci0)
					return Id + ((int)Type << 11);
				return Id;
			}
		}

		/// <summary>Get whether this is the end resource.</summary>
		public bool IsEnd { get { return Type == ResourceType.End; } }

		/// <summary>Read the resource identifier.</summary>
		/// <param name="reader"></param>
		/// <param name="version"></param>
		/// <param name="type"></param>
		public ResourceId(BinaryReader reader, ResourceMapVersion version, ResourceType? type = null) {
			Version = version;
			if (version == ResourceMapVersion.Sci0) {
				short a = reader.ReadInt16();
				int b = reader.ReadInt32();

				if (type.HasValue)
					throw new InvalidDataException();
				FullType = (ResourceType)(a >> 11);
				Id = a & 2047;
				Offset = b & ~(~0 << 26);
				Page = (b >> 26) & 63;
				if (a == -1 && b == -1)
					FullType = ResourceType.End;
			} else if(version == ResourceMapVersion.Sci1 || version == ResourceMapVersion.Sci2) {
				FullType = type.Value;
				Id = reader.ReadUInt16();
				int b = reader.ReadInt32();
				Offset = b & ~(~0 << 28);
				Page = (b >> 28) & 15;
			} else
				throw new NotImplementedException();
		}

		/// <summary>Get whether these resource ids are identical, based on the <see cref="Id"/> and the <see cref="Type"/>.</summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(ResourceId other) {
			return Id == other.Id && Type == other.Type;
		}

		/// <summary>Compare this <see cref="ResourceId"/> to another; if <paramref name="obj"/> is not a <see cref="ResourceId"/>, <c>false</c> is returned.</summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj) {
			if (obj is ResourceId)
				return Equals((ResourceId)obj);
			return base.Equals(obj);
		}

		/// <summary>Get a hash code for the resource id based on the <see cref="Type"/> and the <see cref="Id"/>.</summary>
		/// <returns></returns>
		public override int GetHashCode() {
			return Type.GetHashCode() ^ Id.GetHashCode();
		}

		/// <summary>Convert to a string representation of the resource id, which is <c><see cref="Type"/> <see cref="Id"/></c>.</summary>
		/// <returns></returns>
		public override string ToString() {
			return Type + " " + Id;
		}

		/// <summary>Compare the two resource ids, returning whether they are the same based on <see cref="Id"/> and <see cref="Type"/>.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator ==(ResourceId a, ResourceId b) { return a.Id == b.Id && a.Type == b.Type; }

		/// <summary>Compare the two resource ids, returning whether they are the same based on <see cref="Id"/> and <see cref="Type"/>.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator !=(ResourceId a, ResourceId b) { return a.Id != b.Id || a.Type != b.Type; }
	}
}
