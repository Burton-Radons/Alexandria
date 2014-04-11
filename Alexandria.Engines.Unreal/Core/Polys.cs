using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Glare;

namespace Alexandria.Engines.Unreal.Core {
    public class Polys : RootObject {
        public struct Polygon {
            public Vector3f[] Vertices { get; set; }

            public Vector3f Base { get; set; }

            public Vector3f Normal { get; set; }

            public Vector3f TextureU { get; set; }

            public Vector3f TextureV { get; set; }

            public int Flags { get; set; }

            public Reference ActorReference { get; set; }

            public Reference TextureReference { get; set; }

            public string ItemName { get; set; }

            public int Link { get; set; }

            public int BrushPolygon { get; set; }

            public ushort PanU { get; set; }

            public ushort PanV { get; set; }

            public static Polygon Read(BinaryReader reader, Package package) {
                int vertexCount = UIndex.Read(reader);
                Polygon result = new Polygon() {
                    Vertices = new Vector3f[vertexCount],
                    Base = reader.ReadVector3f(),
                    Normal = reader.ReadVector3f(),
                    TextureU = reader.ReadVector3f(),
                    TextureV = reader.ReadVector3f()
                };

                for(var index = 0; index < vertexCount; index++)
                    result.Vertices[index] = reader.ReadVector3f();
                result.Flags = reader.ReadInt32();
                result.ActorReference = package.ReadReference(reader);
                result.TextureReference = package.ReadReference(reader);
                result.ItemName = package.ReadNameValue(reader);
                result.Link = UIndex.Read(reader);
                result.BrushPolygon = UIndex.Read(reader);
                result.PanU = reader.ReadUInt16();
                result.PanV = reader.ReadUInt16();

                return result;
            }
        }

        [PackageProperty(0)]
        public AttributeDictionary Attributes { get; protected set; }

        [PackageProperty(1)]
        public int PolyCount2 { get; protected set; }

        [PackageProperty(2, typeof(PolygonListReader))]
        public Polygon[] Polygons { get; protected set; }

        class PolygonListReader : DataProcessor {
            public override object Read(object target, Package package, BinaryReader reader, long end) {
                var count = reader.ReadInt32();
                var list = new Polygon[count];

                for(var index = 0; index < list.Length; index++)
                    list[index] = Polygon.Read(reader, package);
                return list;
            }
        }
    }
}
