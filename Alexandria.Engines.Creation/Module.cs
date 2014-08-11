using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation {
	public class Module : ArchiveAsset {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<string> MastersMutable = new Codex<string>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<FormId> OverridesMutable = new Codex<FormId>();

		/// <summary>Author of the module.</summary>
		public string Author { get; set; }

		public override string DisplayName {
			get {
				return string.Format("{0} (Version {9}, {8} Record[s], Author '{1}'{2}{3}{4}{5}{6}, Next object id: {7})",
					base.Name, // 0
					Author, // 1
					string.IsNullOrWhiteSpace(Description) ? "" : ", Description '" + Description + "'", // 2
					InternalCC.HasValue ? ", InternalCC " + InternalCC.Value : "", // 3
					InternalVersion.HasValue ? ", InternalVersion " + InternalVersion.Value : "", // 4
					Masters.Count == 0 ? "" : ", Masters: (" + string.Join(", ", Masters) + ")", // 5
					Overrides.Count == 0 ? "" : ", Overrides " + Overrides.Count, // 6
					NextAvailableObjectId, // 7
					RecordCount, // 8
					Version); // 9
			}
		}

		/// <summary>Description of the module.</summary>
		public new string Description { get { return base.Description; } set { base.Description = value; } }

		/// <summary>Get the unknown "INCC" value. It's not clear this is required.</summary>
		public int? InternalCC { get; set; }

		/// <summary>Get the unknown "INTV" value. It's not clear this is required.</summary>
		public int? InternalVersion { get; set; }

		public ReadOnlyCodex<string> Masters { get { return MastersMutable; } }

		/// <summary>Get the next available object id.</summary>
		public FormId NextAvailableObjectId { get; private set; }

		/// <summary>Get the list of overriden forms.</summary>
		public ReadOnlyCodex<FormId> Overrides { get { return OverridesMutable; } }

		/// <summary>Get the total number of records and groups in the module, not including the TES4 record.</summary>
		public int RecordCount { get; private set; }

		/// <summary>Get the version number. In Skyrim: 0.94 in most files, 1.7 in recent updates.</summary>
		public float Version { get; private set; }

		internal readonly BinaryReader Reader;

		internal Module(AssetLoader loader)
			: base(loader) {
			loader.Progress = 0;
			Reader = loader.Reader;
			var tes4Record = new RecordHeader(Reader);
			tes4Record.RequireType("TES4");
			using (RecordReader reader = tes4Record.OpenReader(Reader)) {
				while (reader.ReadField()) {
					switch (reader.Field.Type) {
						case "HEDR":
							reader.RequireFieldSize(12);
							Version = reader.ReadSingle();
							RecordCount = reader.ReadInt32();
							NextAvailableObjectId = reader.ReadFormId();
							break;

						case "CNAM": Author = reader.ReadStringBody(); break;
						case "SNAM": Description = reader.ReadStringBody(); break;

						case "MAST": MastersMutable.Add(reader.ReadStringBody()); break;

						case "DATA": // File size of a MAST - always 0, probably vestigial.
							long fileSize = reader.ReadInt64OrInt32Body();
							if (fileSize != 0)
								throw new InvalidDataException();
							break;

						case "ONAM":
							OverridesMutable.Capacity = reader.Field.Size / 4;
							for (int index = 0, count = reader.Field.Size / 4; index < count; index++)
								OverridesMutable.Add(reader.ReadFormId());
							break;

						case "INTV": InternalVersion = reader.ReadInt32Body(); break;
						case "INCC": InternalCC = reader.ReadInt32Body(); break;

						default:
							throw reader.UnknownFieldException();
					}
				}
			}

			while (!loader.AtEnd) {
				loader.SetProgressToPosition();
				AddChild(Group.ReadGroup(this));
			}
		}
	}

	public class ModuleFormat : AssetFormat {
		internal ModuleFormat(Engine engine)
			: base(engine, typeof(Module), canLoad: true, extensions: new string[] { ".esm", ".esp" }) {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			return loader.Length > 8 && loader.Reader.ReadId4() == "TES4" ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		public override Asset Load(AssetLoader loader) {
			return new Module(loader);
		}
	}
}
