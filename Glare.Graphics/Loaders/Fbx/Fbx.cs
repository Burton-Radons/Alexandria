using Glare;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	public abstract class Fbx {
		public const string Magic = "Kaydara FBX Binary\x20\x20\x00\x1A\x00";
		public const int MinimumSupportedVersion = 7300;
		public const int MaximumSupportedVersion = 7300;
	}

	public class FbxReader : Fbx {
		public DateTime Created { get; private set; }

		public int Version { get; private set; }

		static string ReadString(BinaryReader reader) {
			byte length = reader.ReadByte();
			if (length > 0x7F)
				throw new Exception();
			return reader.ReadString(length, Encoding.UTF8);
		}

		public FbxReader(BinaryReader reader) {
			reader.RequireMagic(Magic);
			Version = reader.ReadInt32();
			if (Version < MinimumSupportedVersion || Version > MaximumSupportedVersion)
				throw new InvalidDataException("Unsupported FBX version " + Version + "; supported versions are " + MinimumSupportedVersion + " to " + MaximumSupportedVersion + ".");

			FbxSection section;

			while ((section = new FbxSection(reader)).IsValid) {
				switch (section.Name) {
					case FbxHeaderExtension.FbxFileTypeName:
						new FbxHeaderExtension(section);
						break;

					case "FileId": // Seems to be a GUID in raw data.
					case "CreationTime": // Same as in header.
					case "Creator": // Same as in header.
					case "GlobalSettings": // Don't care right now.
						section.SeekToEnd();
						break;

					case "Documents":
					case "References":
					case "Definitions":
						section.SeekToEnd();
						break;


					default:
						throw new InvalidDataException("Unexpected root section '" + section.Name + "'.");
				}

				section.RequireEnd();
			}
		}

		void ReadHeader(BinaryReader reader) {
			var header = new FbxHeaderExtension(new FbxSection(reader));
		}
	}

}
