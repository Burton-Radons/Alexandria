using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	/// <summary>Encountered at the start of every FBX type.</summary>
	[FbxClass(FbxFileTypeName)]
	public class FbxHeaderExtension {
		public const string FbxFileTypeName = "FBXHeaderExtension";

		[FbxProperty("FBXHeaderVersion", 0)]
		public int HeaderVersion { get; set; }

		[FbxProperty("FBXVersion", 1)]
		public int Version { get; set; }

		[FbxProperty("Created", 2)]
		public DateTime CreationTimeStamp { get; set; }

		[FbxProperty(3)]
		public string Creator { get; set; }

		public FbxHeaderExtension(FbxSection section) {
			FbxSection child;

			section.RequireName(FbxFileTypeName);
			while (section.ReadChild(out child)) {
				switch (child.Name) {
					case "FBXHeaderVersion": HeaderVersion = child.ReadInt32Value(); break;
					case "FBXVersion": Version = child.ReadInt32Value(); break;
					case "EncryptionType":
						var encryptionType = child.ReadInt32Value();
						if (encryptionType != 0)
							throw new InvalidDataException("Unsupported encryption type " + encryptionType + ".");
						break;

					case "CreationTimeStamp": CreationTimeStamp = child.ReadTimeStamp(); break;

					case "Creator": Creator = child.ReadStringValue(); break;

					case "SceneInfo":
						child.SeekToEnd();
						break;
						/*
	SceneInfo: "SceneInfo::GlobalInfo", "UserData" {
		Type: "UserData"
		Version: 100
		MetaData:  {
			Version: 100
			Title: ""
			Subject: ""
			Author: ""
			Keywords: ""
			Revision: ""
			Comment: ""
		}
		Properties70:  {
			P: "DocumentUrl", "KString", "Url", "", "D:\fucka.FBX"
			P: "SrcDocumentUrl", "KString", "Url", "", "D:\fucka.FBX"
			P: "Original", "Compound", "", ""
			P: "Original|ApplicationVendor", "KString", "", "", "Autodesk"
			P: "Original|ApplicationName", "KString", "", "", "3ds Design"
			P: "Original|ApplicationVersion", "KString", "", "", "2013"
			P: "Original|DateTime_GMT", "DateTime", "", "", "09/04/2014 19:19:33.481"
			P: "Original|FileName", "KString", "", "", "D:\fucka.FBX"
			P: "LastSaved", "Compound", "", ""
			P: "LastSaved|ApplicationVendor", "KString", "", "", "Autodesk"
			P: "LastSaved|ApplicationName", "KString", "", "", "3ds Design"
			P: "LastSaved|ApplicationVersion", "KString", "", "", "2013"
			P: "LastSaved|DateTime_GMT", "DateTime", "", "", "09/04/2014 19:19:33.481"
		}
	}
}*/

					default:
						throw new NotImplementedException();
				}

				child.RequireEnd();
			}
		}
	}

	public class FbxSceneInfo {
	}
}
