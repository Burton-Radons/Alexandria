using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Alexandria.Engines.Unreal.Core {
	/// <summary>
	/// An UnrealScript class definition.
	/// </summary>
    public class Class : SourceObject {
		/// <summary>
		/// A dependency of the <see cref="Class"/>.
		/// </summary>
        public class Dependency : RootObject {
			/// <summary>
			/// The export that this refers to.
			/// </summary>
            [PackageProperty(0)]
            public Export Class { get; protected set; }

			/// <summary>
			/// 
			/// </summary>
            [PackageProperty(1)]
            public uint Deep { get; protected set; }

			/// <summary>
			/// 
			/// </summary>
            [PackageProperty(2)]
            public uint ScriptTextCRC { get; protected set; }
        }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(0)]
        public ulong ProbeMask { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(1)]
        public ulong IgnoreMask { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(2)]
        public ushort LabelTableOffset { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(3)]
        public uint StateFlags { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(4)]
        public uint ClassFlags { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(5)]
        public Guid Guid { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(6, typeof(DataProcessors.List<Dependency>))]
        public List<Dependency> Dependencies { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(7)]
        public List<string> PackageImports { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(8)]
        public Reference ClassWithin { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(9)]
        public string ClassConfigName { get; protected set; }

		/// <summary>
		/// Uncertain.
		/// </summary>
		[PackageProperty(10, PackageGame.ThiefDeadlyShadows)]
		public string Thief3ClassRealName { get; protected set; }

		/// <summary>
		/// Unknown.
		/// </summary>
		[PackageProperty(11, PackageGame.ThiefDeadlyShadows)]
		public int Thief3Unknown1 { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        [PackageProperty(12)]
        public AttributeDictionary ClassProperties { get; protected set; }
    }
}

