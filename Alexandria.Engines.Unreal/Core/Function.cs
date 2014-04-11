using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
    public class Function : SourceObject {
        /// <summary>
        /// Native function index, or 0 if this is a user function.
        /// </summary>
        [PackageProperty(0)]
        public ushort NativeIndex { get; protected set; }

        [PackageProperty(1)]
        public byte OperatorPrecedence { get; protected set; }

        [PackageProperty(2)]
        public uint FunctionFlags { get; protected set; }
    }
}
