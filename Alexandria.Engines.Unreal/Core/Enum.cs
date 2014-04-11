using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
    public class Enum : Object {
        [PackageProperty(0)]
        public List<string> Values { get; protected set; }
    }
}
