using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
    public class StateFrame : RootObject {
        [PackageProperty(0)]
        public Reference Node { get; protected set; }

        [PackageProperty(1)]
        public Reference StateNode { get; protected set; }

        [PackageProperty(2)]
        public ulong ProbeMask { get; protected set; }

        [PackageProperty(3)]
        public int LatentAction { get; protected set; }

        [PackageProperty(4, typeof(DataProcessors.IndexInt))]
        public int NodeOffset { get; protected set; }

        [PackageProperty(5)]
        public AttributeDictionary Attributes { get; protected set; }
    }
}
