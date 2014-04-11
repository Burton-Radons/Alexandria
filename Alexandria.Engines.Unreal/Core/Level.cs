using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
    public class Level : ObjectWithAttributes {
        [PackageProperty(0)]
        public int Unknown1 { get; protected set; }

        [PackageProperty(0, typeof(DataProcessors.IntCountReferenceList))]
        public List<Reference> Actors { get; protected set; }

        [PackageProperty(1, typeof(DataProcessors.LiteralObject<URL>))]
        public URL URL { get; protected set; }

        [PackageProperty(2)]
        public Reference ModelReference { get; protected set; }

        [PackageProperty(3, typeof(DataProcessors.UnknownIndex))]
        public int ReachSpecs { get; protected set; }

        [PackageProperty(4)]
        public float ApproxTime { get; protected set; }

        [PackageProperty(5, typeof(DataProcessors.IndexInt))]
        public int FirstDeleted { get; protected set; }

        [PackageProperty(6)]
        public Reference TextBlocks0 { get; protected set; }

        [PackageProperty(7)]
        public Reference TextBlocks1 { get; protected set; }

        [PackageProperty(8)]
        public Reference TextBlocks2 { get; protected set; }

        [PackageProperty(9)]
        public Reference TextBlocks3 { get; protected set; }

        [PackageProperty(10)]
        public Reference TextBlocks4 { get; protected set; }

        [PackageProperty(11)]
        public Reference TextBlocks5 { get; protected set; }

        [PackageProperty(12)]
        public Reference TextBlocks6 { get; protected set; }

        [PackageProperty(13)]
        public Reference TextBlocks7 { get; protected set; }

        [PackageProperty(14)]
        public Reference TextBlocks8 { get; protected set; }

        [PackageProperty(15)]
        public Reference TextBlocks9 { get; protected set; }

        [PackageProperty(16)]
        public Reference TextBlocksA { get; protected set; }

        [PackageProperty(17)]
        public Reference TextBlocksB { get; protected set; }

        [PackageProperty(18)]
        public Reference TextBlocksC { get; protected set; }

        [PackageProperty(19)]
        public Reference TextBlocksD { get; protected set; }

        [PackageProperty(20)]
        public Reference TextBlocksE { get; protected set; }

        [PackageProperty(21)]
        public Reference TextBlocksF { get; protected set; }

        [PackageProperty(22, typeof(DataProcessors.UnknownIndex))]
        public int TravelInfo { get; protected set; }
    }
}
