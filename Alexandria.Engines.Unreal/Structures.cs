using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Alexandria.Engines.Unreal.Core;
using Glare;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// A bounding box.
	/// </summary>
	public struct Bounds {
		/// <summary>
		/// The bounds of the box.
		/// </summary>
		public Box3f Box;

		/// <summary>
		/// Whether the bounding box is valid.
		/// </summary>
		public bool Valid;

		/// <summary>
		/// Read the <see cref="Bounds"/> from the <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read the <see cref="Bounds"/> from.</param>
		/// <returns>The new <see cref="Bounds"/> object.</returns>
		public static Bounds Read(BinaryReader reader) {
			return new Bounds() {
				Box = new Box3f(reader.ReadVector3f(), reader.ReadVector3f()),
				Valid = reader.ReadByte() != 0
			};
		}
	}

	/// <summary></summary>
	public struct InitialAllianceInfo {
		/// <summary></summary>
		public string AllianceName { get; set; }

		/// <summary></summary>
		public float Level { get; set; }

		/// <summary></summary>
		public bool Permanent { get; set; }

		/// <summary></summary>
		public static InitialAllianceInfo Read(BinaryReader reader, Package package) {
			return new InitialAllianceInfo() { AllianceName = package.ReadNameValue(reader), Level = reader.ReadSingle(), Permanent = reader.ReadByte() != 0 };
		}
	}

	/// <summary></summary>
	public struct PointRegion {
		/// <summary></summary>
		public Reference ZoneReference { get; set; }

		/// <summary></summary>
		public int Leaf { get; set; }

		/// <summary></summary>
		public byte ZoneNumber { get; set; }

		/// <summary>
		/// Read the <see cref="PointRegion"/> from the <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read the <see cref="Bounds"/> from.</param>
		/// <param name="package"></param>
		/// <returns>The new <see cref="PointRegion"/> object.</returns>
		public static PointRegion Read(BinaryReader reader, Package package) {
			return new PointRegion() {
				ZoneReference = package.ReadReference(reader),
				Leaf = reader.ReadInt32(),
				ZoneNumber = reader.ReadByte()
			};
		}
	}

	/// <summary>
	/// Rotation parameters.
	/// </summary>
	public struct Rotator {
		/// <summary>
		/// Pitch (unknown axis)
		/// </summary>
		public float Pitch;

		/// <summary>
		/// Yaw (unknown axis)
		/// </summary>
		public float Yaw;

		/// <summary>
		/// Roll (unknown axis)
		/// </summary>
		public float Roll;

		/// <summary>
		/// Read the <see cref="Rotator"/> from the <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read the <see cref="Bounds"/> from.</param>
		/// <returns>The new <see cref="Rotator"/> object.</returns>
		public static Rotator Read(BinaryReader reader) {
			return new Rotator() { Pitch = reader.ReadSingle(), Yaw = reader.ReadSingle(), Roll = reader.ReadSingle() };
		}
	}

	/// <summary></summary>
	public struct Scale {
		/// <summary></summary>
		public Vector3f ScaleFactor { get; set; }

		/// <summary></summary>
		public float SheerRate { get; set; }

		/// <summary></summary>
		public SheerAxis SheerAxis { get; set; }

		/// <summary>
		/// Read the <see cref="Scale"/> from the <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read the <see cref="Bounds"/> from.</param>
		/// <returns>The new <see cref="Scale"/> object.</returns>
		public static Scale Read(BinaryReader reader) {
			return new Scale() {
				ScaleFactor = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
				SheerRate = reader.ReadSingle(),
				SheerAxis = (SheerAxis)reader.ReadByte()
			};
		}
	}

	/// <summary></summary>
	public class URL : RootObject {
		/// <summary></summary>
		[PackageProperty(0, typeof(DataProcessors.AsciizByteLength))]
		public string Protocol { get; protected set; }

		/// <summary></summary>
		[PackageProperty(1, typeof(DataProcessors.AsciizByteLength))]
		public string Host { get; protected set; }

		/// <summary></summary>
		[PackageProperty(2, typeof(DataProcessors.AsciizByteLength))]
		public string Map { get; protected set; }

		/// <summary></summary>
		[PackageProperty(3, typeof(DataProcessors.UnknownIndex))]
		public int OptionsArrayCount { get; protected set; }

		/// <summary></summary>
		[PackageProperty(4, typeof(DataProcessors.AsciizByteLength))]
		public string Portal { get; protected set; }

		/// <summary></summary>
		[PackageProperty(5)]
		public int Port { get; protected set; }

		/// <summary></summary>
		[PackageProperty(6)]
		public int Valid { get; protected set; }
	}

	/// <summary></summary>
	public enum SheerAxis : byte {
		/// <summary></summary>
		ZX = 5,
	}
}
