using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
	/// <summary>
	/// A level.
	/// </summary>
	public class Level : ObjectWithAttributes {
		/// <summary>
		/// Unknown.
		/// </summary>
		[PackageProperty(0)]
		public int Unknown1 { get; protected set; }

		/// <summary>
		/// Get the actors.
		/// </summary>
		[PackageProperty(0, typeof(DataProcessors.IntCountReferenceList))]
		public List<Reference> Actors { get; protected set; }

		/// <summary>
		/// Get the URL.
		/// </summary>
		[PackageProperty(1, typeof(DataProcessors.LiteralObject<URL>))]
		public URL URL { get; protected set; }

		/// <summary>
		/// Get the model.
		/// </summary>
		[PackageProperty(2)]
		public Reference ModelReference { get; protected set; }

		/// <summary>
		/// Get the reach specs.
		/// </summary>
		[PackageProperty(3, typeof(DataProcessors.UnknownIndex))]
		public int ReachSpecs { get; protected set; }

		/// <summary>
		/// Get the approximate time.
		/// </summary>
		[PackageProperty(4)]
		public float ApproxTime { get; protected set; }

		/// <summary></summary>
		[PackageProperty(5, typeof(DataProcessors.IndexInt))]
		public int FirstDeleted { get; protected set; }

		/// <summary></summary>
		[PackageProperty(6)]
		public Reference TextBlocks0 { get; protected set; }

		/// <summary></summary>
		[PackageProperty(7)]
		public Reference TextBlocks1 { get; protected set; }

		/// <summary></summary>
		[PackageProperty(8)]
		public Reference TextBlocks2 { get; protected set; }

		/// <summary></summary>
		[PackageProperty(9)]
		public Reference TextBlocks3 { get; protected set; }

		/// <summary></summary>
		[PackageProperty(10)]
		public Reference TextBlocks4 { get; protected set; }

		/// <summary></summary>
		[PackageProperty(11)]
		public Reference TextBlocks5 { get; protected set; }

		/// <summary></summary>
		[PackageProperty(12)]
		public Reference TextBlocks6 { get; protected set; }

		/// <summary></summary>
		[PackageProperty(13)]
		public Reference TextBlocks7 { get; protected set; }

		/// <summary></summary>
		[PackageProperty(14)]
		public Reference TextBlocks8 { get; protected set; }

		/// <summary></summary>
		[PackageProperty(15)]
		public Reference TextBlocks9 { get; protected set; }

		/// <summary></summary>
		[PackageProperty(16)]
		public Reference TextBlocksA { get; protected set; }

		/// <summary></summary>
		[PackageProperty(17)]
		public Reference TextBlocksB { get; protected set; }

		/// <summary></summary>
		[PackageProperty(18)]
		public Reference TextBlocksC { get; protected set; }

		/// <summary></summary>
		[PackageProperty(19)]
		public Reference TextBlocksD { get; protected set; }

		/// <summary></summary>
		[PackageProperty(20)]
		public Reference TextBlocksE { get; protected set; }

		/// <summary></summary>
		[PackageProperty(21)]
		public Reference TextBlocksF { get; protected set; }

		/// <summary></summary>
		[PackageProperty(22, typeof(DataProcessors.UnknownIndex))]
		public int TravelInfo { get; protected set; }
	}
}
