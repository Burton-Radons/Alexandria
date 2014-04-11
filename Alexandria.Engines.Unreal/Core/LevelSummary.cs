using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
    public class LevelSummary : RootObject {
        [PackageProperty(0)]
        public AttributeDictionary Attributes { get; protected set; }
    }
}
