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

	public struct InitialAllianceInfo {
		public string AllianceName { get; set; }
		public float Level { get; set; }
		public bool Permanent { get; set; }

		public static InitialAllianceInfo Read(BinaryReader reader, Package package) {
			return new InitialAllianceInfo() { AllianceName = package.ReadNameValue(reader), Level = reader.ReadSingle(), Permanent = reader.ReadByte() != 0 };
		}
	}

	public struct PointRegion {
		public Reference ZoneReference { get; set; }

		public int Leaf { get; set; }

		public byte ZoneNumber { get; set; }

		/// <summary>
		/// Read the <see cref="PointRegion"/> from the <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read the <see cref="Bounds"/> from.</param>
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

	public struct Scale {
		public Vector3f ScaleFactor { get; set; }
		public float SheerRate { get; set; }
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

	public class URL : RootObject {
		[PackageProperty(0, typeof(DataProcessors.AsciizByteLength))]
		public string Protocol { get; protected set; }

		[PackageProperty(1, typeof(DataProcessors.AsciizByteLength))]
		public string Host { get; protected set; }

		[PackageProperty(2, typeof(DataProcessors.AsciizByteLength))]
		public string Map { get; protected set; }

		[PackageProperty(3, typeof(DataProcessors.UnknownIndex))]
		public int OptionsArrayCount { get; protected set; }

		[PackageProperty(4, typeof(DataProcessors.AsciizByteLength))]
		public string Portal { get; protected set; }

		[PackageProperty(5)]
		public int Port { get; protected set; }

		[PackageProperty(6)]
		public int Valid { get; protected set; }
	}

	public enum SheerAxis : byte {
		ZX = 5,
	}
}
