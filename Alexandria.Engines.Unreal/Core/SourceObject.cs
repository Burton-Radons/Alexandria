using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;

namespace Alexandria.Engines.Unreal.Core {
    /// <summary>
    /// An object that's related to source code.
    /// </summary>
    public class SourceObject : Object {
        [PackageProperty(0)]
        public Reference ScriptText { get; protected set; }

        [PackageProperty(1), Browsable(false)]
        public Reference FirstChild { get; protected set; }

        [PackageProperty(2)]
        public string FriendlyName { get; protected set; }

        [PackageProperty(3)]
        public int Line { get; protected set; }

        [PackageProperty(4)]
        public int TextPosition { get; protected set; }

        [PackageProperty(5, typeof(StatementListReader))]
        public List<Statement> Statements { get; protected set; }

        [Description("The list of children of this object. For structures and classes, this is usually the fields of the object.")]
        public IEnumerable<Object> Children {
            get {
                for(Object child = (Object)FirstChild.Object; child != null; child = child.Next) {
                    yield return child;
                }
            }
        }

        class StatementListReader : DataProcessor {
            public override object Read(object target, Package package, System.IO.BinaryReader reader, long end) {
                int offset = 0, total = reader.ReadInt32();
                List<Statement> list = null;

                while(offset < total) {
                    if(list == null)
                        list = new List<Statement>();
					var statement = Statement.Load(package, reader, ref offset, end);
                    list.Add(statement);
                }

				/*List<string> result = new List<string>(list != null ? list.Count : 0);
				if(list != null) {
					foreach(var item in list)
						result.Add(item.UnpackedCodeOffset + ": " + item.ToString());
				}*/

				if(offset != total)
					throw new Exception("Didn't read exactly right");
                return list;
            }
        }
    }
}
