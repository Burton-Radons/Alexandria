/*
For ParameterTable.cs under ParameterTableRow.ReadRow:
For ParameterDefinition.cs under ParameterDefinitionRow.GetDotNetType():
For Enumerations.cs:
*/
using Glare.Assets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	partial class TableRows {
		/// <summary></summary>
		/// <remarks>
		/// Defined as "SKELETON_PARAM_ST" in Dark Souls in the file "SkeletonParam.paramdef" (id 2Ah).
		/// </remarks>
		public class Skeleton : ParameterTableRow {
			public const string TableName = "SKELETON_PARAM_ST";

			Single neckTurnGain;
			Int16 originalGroundHeightMS, minAnkleHeightMS, maxAnkleHeightMS, cosineMaxKneeAngle, cosineMinKneeAngle, footPlantedAnkleHeightMS, footRaisedAnkleHeightMS, raycastDistanceUp, raycastDistanceDown, footEndLS_X, footEndLS_Y, footEndLS_Z, onOffGain, groundAscendingGain, groundDescendingGain, footRaisedGain, footPlantedGain, footUnlockGain;
			SkeletonKneeAxisDirection kneeAxisType, twistKneeAxisType;
			bool useFootLocking, footPlacementOn;
			SByte neckTurnPriority;
			Byte neckTurnMaxAngle;
			Byte[] pad1;

			public static readonly PropertyInfo
				NeckTurnGainProperty = GetProperty<Skeleton>("NeckTurnGain"),
				OriginalGroundHeightMSProperty = GetProperty<Skeleton>("OriginalGroundHeightMS"),
				MinAnkleHeightMSProperty = GetProperty<Skeleton>("MinAnkleHeightMS"),
				MaxAnkleHeightMSProperty = GetProperty<Skeleton>("MaxAnkleHeightMS"),
				CosineMaxKneeAngleProperty = GetProperty<Skeleton>("CosineMaxKneeAngle"),
				CosineMinKneeAngleProperty = GetProperty<Skeleton>("CosineMinKneeAngle"),
				FootPlantedAnkleHeightMSProperty = GetProperty<Skeleton>("FootPlantedAnkleHeightMS"),
				FootRaisedAnkleHeightMSProperty = GetProperty<Skeleton>("FootRaisedAnkleHeightMS"),
				RaycastDistanceUpProperty = GetProperty<Skeleton>("RaycastDistanceUp"),
				RaycastDistanceDownProperty = GetProperty<Skeleton>("RaycastDistanceDown"),
				FootEndLS_XProperty = GetProperty<Skeleton>("FootEndLS_X"),
				FootEndLS_YProperty = GetProperty<Skeleton>("FootEndLS_Y"),
				FootEndLS_ZProperty = GetProperty<Skeleton>("FootEndLS_Z"),
				OnOffGainProperty = GetProperty<Skeleton>("OnOffGain"),
				GroundAscendingGainProperty = GetProperty<Skeleton>("GroundAscendingGain"),
				GroundDescendingGainProperty = GetProperty<Skeleton>("GroundDescendingGain"),
				FootRaisedGainProperty = GetProperty<Skeleton>("FootRaisedGain"),
				FootPlantedGainProperty = GetProperty<Skeleton>("FootPlantedGain"),
				FootUnlockGainProperty = GetProperty<Skeleton>("FootUnlockGain"),
				KneeAxisTypeProperty = GetProperty<Skeleton>("KneeAxisType"),
				UseFootLockingProperty = GetProperty<Skeleton>("UseFootLocking"),
				FootPlacementOnProperty = GetProperty<Skeleton>("FootPlacementOn"),
				TwistKneeAxisTypeProperty = GetProperty<Skeleton>("TwistKneeAxisType"),
				NeckTurnPriorityProperty = GetProperty<Skeleton>("NeckTurnPriority"),
				NeckTurnMaxAngleProperty = GetProperty<Skeleton>("NeckTurnMaxAngle"),
				Pad1Property = GetProperty<Skeleton>("Pad1");

			/// <summary>Swing gain</summary>
			/// <remarks>
			/// Japanese short name: "首振りゲイン", Google translated: "Swing gain".
			/// Japanese description: "首振りゲイン。高いほど早く回る", Google translated: "Swing gain . I quickly turn higher".
			/// </remarks>
			[ParameterTableRowAttribute("neckTurnGain", index: 0, minimum: 0, maximum: 1, step: 0.01, order: 400, unknown2: 1)]
			[DisplayName("Swing gain")]
			[Description("Swing gain . I quickly turn higher")]
			[DefaultValue((Single)0)]
			public Single NeckTurnGain {
				get { return neckTurnGain; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for " + NeckTurnGainProperty.Name + ".");
					SetProperty(ref neckTurnGain, ref value, NeckTurnGainProperty);
				}
			}

			/// <summary>From the origin to the ground height [cm]</summary>
			/// <remarks>
			/// Japanese short name: "原点から地面への高さ[cm]", Google translated: "From the origin to the ground height [cm]".
			/// Japanese description: "原点から地面への高さ[cm]", Google translated: "From the origin to the ground height [cm]".
			/// </remarks>
			[ParameterTableRowAttribute("originalGroundHeightMS", index: 1, minimum: -10000, maximum: 10000, step: 1, order: 1000, unknown2: 1)]
			[DisplayName("From the origin to the ground height [cm]")]
			[Description("From the origin to the ground height [cm]")]
			[DefaultValue((Int16)0)]
			public Int16 OriginalGroundHeightMS {
				get { return originalGroundHeightMS; }
				set {
					if ((double)value < -10000 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 10000 for " + OriginalGroundHeightMSProperty.Name + ".");
					SetProperty(ref originalGroundHeightMS, ref value, OriginalGroundHeightMSProperty);
				}
			}

			/// <summary>The minimum is raised ankle height [cm]</summary>
			/// <remarks>
			/// Japanese short name: "足首を上げられる最低の高さ[cm]", Google translated: "The minimum is raised ankle height [cm]".
			/// Japanese description: "足首を上げられる最低の高さ[cm]", Google translated: "The minimum is raised ankle height [cm]".
			/// </remarks>
			[ParameterTableRowAttribute("minAnkleHeightMS", index: 2, minimum: -10000, maximum: 10000, step: 1, order: 1100, unknown2: 1)]
			[DisplayName("The minimum is raised ankle height [cm]")]
			[Description("The minimum is raised ankle height [cm]")]
			[DefaultValue((Int16)(-30))]
			public Int16 MinAnkleHeightMS {
				get { return minAnkleHeightMS; }
				set {
					if ((double)value < -10000 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 10000 for " + MinAnkleHeightMSProperty.Name + ".");
					SetProperty(ref minAnkleHeightMS, ref value, MinAnkleHeightMSProperty);
				}
			}

			/// <summary>Maximum height is increased the ankle [cm]</summary>
			/// <remarks>
			/// Japanese short name: "足首を上げられる最大の高さ[cm]", Google translated: "Maximum height is increased the ankle [cm]".
			/// Japanese description: "足首を上げられる最大の高さ[cm]", Google translated: "Maximum height is increased the ankle [cm]".
			/// </remarks>
			[ParameterTableRowAttribute("maxAnkleHeightMS", index: 3, minimum: -10000, maximum: 10000, step: 1, order: 1200, unknown2: 1)]
			[DisplayName("Maximum height is increased the ankle [cm]")]
			[Description("Maximum height is increased the ankle [cm]")]
			[DefaultValue((Int16)70)]
			public Int16 MaxAnkleHeightMS {
				get { return maxAnkleHeightMS; }
				set {
					if ((double)value < -10000 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 10000 for " + MaxAnkleHeightMSProperty.Name + ".");
					SetProperty(ref maxAnkleHeightMS, ref value, MaxAnkleHeightMSProperty);
				}
			}

			/// <summary>(The value of the cosine ) the maximum angle of bend your knees</summary>
			/// <remarks>
			/// Japanese short name: "ひざを曲げられる最大の角度(コサインの値)", Google translated: "(The value of the cosine ) the maximum angle of bend your knees".
			/// Japanese description: "ひざを曲げられる最大の角度(コサインの値)", Google translated: "(The value of the cosine ) the maximum angle of bend your knees".
			/// </remarks>
			[ParameterTableRowAttribute("cosineMaxKneeAngle", index: 4, minimum: -100, maximum: 100, step: 1, order: 1300, unknown2: 1)]
			[DisplayName("(The value of the cosine ) the maximum angle of bend your knees")]
			[Description("(The value of the cosine ) the maximum angle of bend your knees")]
			[DefaultValue((Int16)(-95))]
			public Int16 CosineMaxKneeAngle {
				get { return cosineMaxKneeAngle; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + CosineMaxKneeAngleProperty.Name + ".");
					SetProperty(ref cosineMaxKneeAngle, ref value, CosineMaxKneeAngleProperty);
				}
			}

			/// <summary>Angle of minimum bend your knees</summary>
			/// <remarks>
			/// Japanese short name: "ひざを曲げられる最小の角度", Google translated: "Angle of minimum bend your knees".
			/// Japanese description: "ひざを曲げられる最小の角度", Google translated: "Angle of minimum bend your knees".
			/// </remarks>
			[ParameterTableRowAttribute("cosineMinKneeAngle", index: 5, minimum: -100, maximum: 100, step: 1, order: 1400, unknown2: 1)]
			[DisplayName("Angle of minimum bend your knees")]
			[Description("Angle of minimum bend your knees")]
			[DefaultValue((Int16)55)]
			public Int16 CosineMinKneeAngle {
				get { return cosineMinKneeAngle; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + CosineMinKneeAngleProperty.Name + ".");
					SetProperty(ref cosineMinKneeAngle, ref value, CosineMinKneeAngleProperty);
				}
			}

			/// <summary>Ankle is clinging assume that the lowest position [cm]</summary>
			/// <remarks>
			/// Japanese short name: "足首がくっついていると仮定する最低位置[cm]", Google translated: "Ankle is clinging assume that the lowest position [cm]".
			/// Japanese description: "足首がこの位置よりも低い場合には足がくっついていると仮定する[cm]", Google translated: "Legs and clinging if ankle is lower than this position assume that [cm]".
			/// </remarks>
			[ParameterTableRowAttribute("footPlantedAnkleHeightMS", index: 6, minimum: -100, maximum: 100, step: 1, order: 1500, unknown2: 1)]
			[DisplayName("Ankle is clinging assume that the lowest position [cm]")]
			[Description("Legs and clinging if ankle is lower than this position assume that [cm]")]
			[DefaultValue((Int16)1)]
			public Int16 FootPlantedAnkleHeightMS {
				get { return footPlantedAnkleHeightMS; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + FootPlantedAnkleHeightMSProperty.Name + ".");
					SetProperty(ref footPlantedAnkleHeightMS, ref value, FootPlantedAnkleHeightMSProperty);
				}
			}

			/// <summary>Ankle and are separated assume the highest position [cm]</summary>
			/// <remarks>
			/// Japanese short name: "足首が離れていると仮定する最高位置[cm]", Google translated: "Ankle and are separated assume the highest position [cm]".
			/// Japanese description: "足首がこの位置よりも高い場合には足が離れていると仮定する[cm]", Google translated: "Feet and are separated when the ankle is higher than this position assume that [cm]".
			/// </remarks>
			[ParameterTableRowAttribute("footRaisedAnkleHeightMS", index: 7, minimum: -100, maximum: 100, step: 1, order: 1600, unknown2: 1)]
			[DisplayName("Ankle and are separated assume the highest position [cm]")]
			[Description("Feet and are separated when the ankle is higher than this position assume that [cm]")]
			[DefaultValue((Int16)30)]
			public Int16 FootRaisedAnkleHeightMS {
				get { return footRaisedAnkleHeightMS; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + FootRaisedAnkleHeightMSProperty.Name + ".");
					SetProperty(ref footRaisedAnkleHeightMS, ref value, FootRaisedAnkleHeightMSProperty);
				}
			}

			/// <summary>Or ray cast from the top how much from ankle [cm]</summary>
			/// <remarks>
			/// Japanese short name: "足首よりどれぐらい上からレイキャストするか[cm]", Google translated: "Or ray cast from the top how much from ankle [cm]".
			/// Japanese description: "足首よりどれぐらい上からレイキャストするか[cm]", Google translated: "Or ray cast from the top how much from ankle [cm]".
			/// </remarks>
			[ParameterTableRowAttribute("raycastDistanceUp", index: 8, minimum: -10000, maximum: 10000, step: 1, order: 1700, unknown2: 1)]
			[DisplayName("Or ray cast from the top how much from ankle [cm]")]
			[Description("Or ray cast from the top how much from ankle [cm]")]
			[DefaultValue((Int16)70)]
			public Int16 RaycastDistanceUp {
				get { return raycastDistanceUp; }
				set {
					if ((double)value < -10000 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 10000 for " + RaycastDistanceUpProperty.Name + ".");
					SetProperty(ref raycastDistanceUp, ref value, RaycastDistanceUpProperty);
				}
			}

			/// <summary>How long lay cast from ankle to the bottom [cm]</summary>
			/// <remarks>
			/// Japanese short name: "足首からどれぐらい下までレイキャストするか[cm]", Google translated: "How long lay cast from ankle to the bottom [cm]".
			/// Japanese description: "足首からどれぐらい下までレイキャストするか[cm]", Google translated: "How long lay cast from ankle to the bottom [cm]".
			/// </remarks>
			[ParameterTableRowAttribute("raycastDistanceDown", index: 9, minimum: -10000, maximum: 10000, step: 1, order: 1800, unknown2: 1)]
			[DisplayName("How long lay cast from ankle to the bottom [cm]")]
			[Description("How long lay cast from ankle to the bottom [cm]")]
			[DefaultValue((Int16)55)]
			public Int16 RaycastDistanceDown {
				get { return raycastDistanceDown; }
				set {
					if ((double)value < -10000 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 10000 for " + RaycastDistanceDownProperty.Name + ".");
					SetProperty(ref raycastDistanceDown, ref value, RaycastDistanceDownProperty);
				}
			}

			/// <summary>Toe position X [cm]</summary>
			/// <remarks>
			/// Japanese short name: "つま先位置X[cm]", Google translated: "Toe position X [cm]".
			/// Japanese description: "つま先位置X[cm]", Google translated: "Toe position X [cm]".
			/// </remarks>
			[ParameterTableRowAttribute("footEndLS_X", index: 10, minimum: -10000, maximum: 10000, step: 1, order: 1900, unknown2: 1)]
			[DisplayName("Toe position X [cm]")]
			[Description("Toe position X [cm]")]
			[DefaultValue((Int16)0)]
			public Int16 FootEndLS_X {
				get { return footEndLS_X; }
				set {
					if ((double)value < -10000 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 10000 for " + FootEndLS_XProperty.Name + ".");
					SetProperty(ref footEndLS_X, ref value, FootEndLS_XProperty);
				}
			}

			/// <summary>Toe position Y [cm]</summary>
			/// <remarks>
			/// Japanese short name: "つま先位置Y[cm]", Google translated: "Toe position Y [cm]".
			/// Japanese description: "つま先位置Y[cm]", Google translated: "Toe position Y [cm]".
			/// </remarks>
			[ParameterTableRowAttribute("footEndLS_Y", index: 11, minimum: -10000, maximum: 10000, step: 1, order: 2000, unknown2: 1)]
			[DisplayName("Toe position Y [cm]")]
			[Description("Toe position Y [cm]")]
			[DefaultValue((Int16)0)]
			public Int16 FootEndLS_Y {
				get { return footEndLS_Y; }
				set {
					if ((double)value < -10000 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 10000 for " + FootEndLS_YProperty.Name + ".");
					SetProperty(ref footEndLS_Y, ref value, FootEndLS_YProperty);
				}
			}

			/// <summary>Toe position Z [cm]</summary>
			/// <remarks>
			/// Japanese short name: "つま先位置Z[cm]", Google translated: "Toe position Z [cm]".
			/// Japanese description: "つま先位置Z[cm]", Google translated: "Toe position Z [cm]".
			/// </remarks>
			[ParameterTableRowAttribute("footEndLS_Z", index: 12, minimum: -10000, maximum: 10000, step: 1, order: 2100, unknown2: 1)]
			[DisplayName("Toe position Z [cm]")]
			[Description("Toe position Z [cm]")]
			[DefaultValue((Int16)0)]
			public Int16 FootEndLS_Z {
				get { return footEndLS_Z; }
				set {
					if ((double)value < -10000 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 10000 for " + FootEndLS_ZProperty.Name + ".");
					SetProperty(ref footEndLS_Z, ref value, FootEndLS_ZProperty);
				}
			}

			/// <summary>Gain when turning on and off the ground in the foot [1 / 100]</summary>
			/// <remarks>
			/// Japanese short name: "足の接地をオンオフするときのゲイン[1/100]", Google translated: "Gain when turning on and off the ground in the foot [1 / 100]".
			/// Japanese description: "足の接地をオンオフするときのゲイン[1/100]", Google translated: "Gain when turning on and off the ground in the foot [1 / 100]".
			/// </remarks>
			[ParameterTableRowAttribute("onOffGain", index: 13, minimum: 0, maximum: 100, step: 1, order: 2200, unknown2: 1)]
			[DisplayName("Gain when turning on and off the ground in the foot [1 / 100]")]
			[Description("Gain when turning on and off the ground in the foot [1 / 100]")]
			[DefaultValue((Int16)18)]
			public Int16 OnOffGain {
				get { return onOffGain; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + OnOffGainProperty.Name + ".");
					SetProperty(ref onOffGain, ref value, OnOffGainProperty);
				}
			}

			/// <summary>Gain when the ground level is higher [ 1 / 100 ]</summary>
			/// <remarks>
			/// Japanese short name: "地面の高さが高くなったときのゲイン[1/100]", Google translated: "Gain when the ground level is higher [ 1 / 100 ]".
			/// Japanese description: "地面の高さが高くなったときのゲイン[1/100]", Google translated: "Gain when the ground level is higher [ 1 / 100 ]".
			/// </remarks>
			[ParameterTableRowAttribute("groundAscendingGain", index: 14, minimum: 0, maximum: 1000, step: 1, order: 2300, unknown2: 1)]
			[DisplayName("Gain when the ground level is higher [ 1 / 100 ]")]
			[Description("Gain when the ground level is higher [ 1 / 100 ]")]
			[DefaultValue((Int16)100)]
			public Int16 GroundAscendingGain {
				get { return groundAscendingGain; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for " + GroundAscendingGainProperty.Name + ".");
					SetProperty(ref groundAscendingGain, ref value, GroundAscendingGainProperty);
				}
			}

			/// <summary>Gain when the ground level is lower [ 1 / 100 ]</summary>
			/// <remarks>
			/// Japanese short name: "地面の高さが低くなったときのゲイン[1/100]", Google translated: "Gain when the ground level is lower [ 1 / 100 ]".
			/// Japanese description: "地面の高さが低くなったときのゲイン[1/100]", Google translated: "Gain when the ground level is lower [ 1 / 100 ]".
			/// </remarks>
			[ParameterTableRowAttribute("groundDescendingGain", index: 15, minimum: 0, maximum: 1000, step: 1, order: 2400, unknown2: 1)]
			[DisplayName("Gain when the ground level is lower [ 1 / 100 ]")]
			[Description("Gain when the ground level is lower [ 1 / 100 ]")]
			[DefaultValue((Int16)100)]
			public Int16 GroundDescendingGain {
				get { return groundDescendingGain; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for " + GroundDescendingGainProperty.Name + ".");
					SetProperty(ref groundDescendingGain, ref value, GroundDescendingGainProperty);
				}
			}

			/// <summary>Gain when the foot is raised [1 / 100]</summary>
			/// <remarks>
			/// Japanese short name: "足が上がったときのゲイン[1/100]", Google translated: "Gain when the foot is raised [1 / 100]".
			/// Japanese description: "足が上がったときのゲイン[1/100]", Google translated: "Gain when the foot is raised [1 / 100]".
			/// </remarks>
			[ParameterTableRowAttribute("footRaisedGain", index: 16, minimum: 0, maximum: 100, step: 1, order: 2500, unknown2: 1)]
			[DisplayName("Gain when the foot is raised [1 / 100]")]
			[Description("Gain when the foot is raised [1 / 100]")]
			[DefaultValue((Int16)20)]
			public Int16 FootRaisedGain {
				get { return footRaisedGain; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + FootRaisedGainProperty.Name + ".");
					SetProperty(ref footRaisedGain, ref value, FootRaisedGainProperty);
				}
			}

			/// <summary>Gain when the foot contacts the ground [1 / 100]</summary>
			/// <remarks>
			/// Japanese short name: "足が接地したときのゲイン[1/100]", Google translated: "Gain when the foot contacts the ground [1 / 100]".
			/// Japanese description: "足が接地したときのゲイン[1/100]", Google translated: "Gain when the foot contacts the ground [1 / 100]".
			/// </remarks>
			[ParameterTableRowAttribute("footPlantedGain", index: 17, minimum: 0, maximum: 100, step: 1, order: 2600, unknown2: 1)]
			[DisplayName("Gain when the foot contacts the ground [1 / 100]")]
			[Description("Gain when the foot contacts the ground [1 / 100]")]
			[DefaultValue((Int16)100)]
			public Int16 FootPlantedGain {
				get { return footPlantedGain; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + FootPlantedGainProperty.Name + ".");
					SetProperty(ref footPlantedGain, ref value, FootPlantedGainProperty);
				}
			}

			/// <summary>Gain when the foot is locked / unlock [1 / 100]</summary>
			/// <remarks>
			/// Japanese short name: "足がロック/アンロックしたときのゲイン[1/100]", Google translated: "Gain when the foot is locked / unlock [1 / 100]".
			/// Japanese description: "足がロック/アンロックしたときのゲイン[1/100]", Google translated: "Gain when the foot is locked / unlock [1 / 100]".
			/// </remarks>
			[ParameterTableRowAttribute("footUnlockGain", index: 18, minimum: 0, maximum: 100, step: 1, order: 2700, unknown2: 1)]
			[DisplayName("Gain when the foot is locked / unlock [1 / 100]")]
			[Description("Gain when the foot is locked / unlock [1 / 100]")]
			[DefaultValue((Int16)80)]
			public Int16 FootUnlockGain {
				get { return footUnlockGain; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + FootUnlockGainProperty.Name + ".");
					SetProperty(ref footUnlockGain, ref value, FootUnlockGainProperty);
				}
			}

			/// <summary>The axial direction of the knee</summary>
			/// <remarks>
			/// Japanese short name: "ひざの軸方向", Google translated: "The axial direction of the knee".
			/// Japanese description: "ひざの軸方向", Google translated: "The axial direction of the knee".
			/// </remarks>
			[ParameterTableRowAttribute("kneeAxisType", index: 19, minimum: 0, maximum: 6, step: 1, order: 900, unknown2: 1)]
			[DisplayName("The axial direction of the knee")]
			[Description("The axial direction of the knee")]
			[DefaultValue((SkeletonKneeAxisDirection)4)]
			public SkeletonKneeAxisDirection KneeAxisType {
				get { return kneeAxisType; }
				set { SetProperty(ref kneeAxisType, ref value, KneeAxisTypeProperty); }
			}

			/// <summary>Lock or (fixed ?) Foot / ankle</summary>
			/// <remarks>
			/// Japanese short name: "足/足首をロック（固定？）するか", Google translated: "Lock or (fixed ?) Foot / ankle".
			/// Japanese description: "足/足首をロック（固定？）するか", Google translated: "Lock or (fixed ?) Foot / ankle".
			/// </remarks>
			[ParameterTableRowAttribute("useFootLocking", index: 20, minimum: 0, maximum: 1, step: 1, order: 1850, unknown2: 1)]
			[DisplayName("Lock or (fixed ?) Foot / ankle")]
			[Description("Lock or (fixed ?) Foot / ankle")]
			[DefaultValue(false)]
			public bool UseFootLocking {
				get { return useFootLocking; }
				set { SetProperty(ref useFootLocking, ref value, UseFootLockingProperty); }
			}

			/// <summary>Ground of the foot is valid</summary>
			/// <remarks>
			/// Japanese short name: "足の接地が有効か", Google translated: "Ground of the foot is valid".
			/// Japanese description: "足の接地が有効か", Google translated: "Ground of the foot is valid".
			/// </remarks>
			[ParameterTableRowAttribute("footPlacementOn", index: 21, minimum: 0, maximum: 1, step: 1, order: 2650, unknown2: 1)]
			[DisplayName("Ground of the foot is valid")]
			[Description("Ground of the foot is valid")]
			[DefaultValue(true)]
			public bool FootPlacementOn {
				get { return footPlacementOn; }
				set { SetProperty(ref footPlacementOn, ref value, FootPlacementOnProperty); }
			}

			/// <summary>Axial direction of twisting the knee joint</summary>
			/// <remarks>
			/// Japanese short name: "ひねり用ひざ関節の軸方向", Google translated: "Axial direction of twisting the knee joint".
			/// Japanese description: "ひねり用ひざ関節の回転を無視する軸方向", Google translated: "Axial direction to ignore the rotation of the twist for the knee joint".
			/// </remarks>
			[ParameterTableRowAttribute("twistKneeAxisType", index: 22, minimum: 0, maximum: 3, step: 1, order: 3100, unknown2: 1)]
			[DisplayName("Axial direction of twisting the knee joint")]
			[Description("Axial direction to ignore the rotation of the twist for the knee joint")]
			[DefaultValue((SkeletonKneeAxisDirection)1)]
			public SkeletonKneeAxisDirection TwistKneeAxisType {
				get { return twistKneeAxisType; }
				set { SetProperty(ref twistKneeAxisType, ref value, TwistKneeAxisTypeProperty); }
			}

			/// <summary>Swing priority</summary>
			/// <remarks>
			/// Japanese short name: "首振り優先度", Google translated: "Swing priority".
			/// Japanese description: "低いほど先に回る。-1で首振りしない", Google translated: "I turn first as low as possible. Do not swing at -1".
			/// </remarks>
			[ParameterTableRowAttribute("neckTurnPriority", index: 23, minimum: -1, maximum: 127, step: 1, order: 100, unknown2: 1)]
			[DisplayName("Swing priority")]
			[Description("I turn first as low as possible. Do not swing at -1")]
			[DefaultValue((SByte)0)]
			public SByte NeckTurnPriority {
				get { return neckTurnPriority; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for " + NeckTurnPriorityProperty.Name + ".");
					SetProperty(ref neckTurnPriority, ref value, NeckTurnPriorityProperty);
				}
			}

			/// <summary>Maximum swing angle</summary>
			/// <remarks>
			/// Japanese short name: "首振り最大角度", Google translated: "Maximum swing angle".
			/// Japanese description: "この関節の最大旋回角度。この角度以上はまがらない", Google translated: "Maximum turning angle of the joint . It does not bend angle is greater than or equal to this".
			/// </remarks>
			[ParameterTableRowAttribute("neckTurnMaxAngle", index: 24, minimum: 0, maximum: 180, step: 1, order: 200, unknown2: 1)]
			[DisplayName("Maximum swing angle")]
			[Description("Maximum turning angle of the joint . It does not bend angle is greater than or equal to this")]
			[DefaultValue((Byte)0)]
			public Byte NeckTurnMaxAngle {
				get { return neckTurnMaxAngle; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for " + NeckTurnMaxAngleProperty.Name + ".");
					SetProperty(ref neckTurnMaxAngle, ref value, NeckTurnMaxAngleProperty);
				}
			}

			/// <summary>Padding 1</summary>
			/// <remarks>
			/// Japanese short name: "パディング1", Google translated: "Padding 1".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad1[2]", index: 25, minimum: 0, maximum: 0, step: 0, order: 99999999, unknown2: 0)]
			[DisplayName("Padding 1")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad1 {
				get { return pad1; }
				set { SetProperty(ref pad1, ref value, Pad1Property); }
			}

			internal Skeleton(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				NeckTurnGain = reader.ReadSingle();
				OriginalGroundHeightMS = reader.ReadInt16();
				MinAnkleHeightMS = reader.ReadInt16();
				MaxAnkleHeightMS = reader.ReadInt16();
				CosineMaxKneeAngle = reader.ReadInt16();
				CosineMinKneeAngle = reader.ReadInt16();
				FootPlantedAnkleHeightMS = reader.ReadInt16();
				FootRaisedAnkleHeightMS = reader.ReadInt16();
				RaycastDistanceUp = reader.ReadInt16();
				RaycastDistanceDown = reader.ReadInt16();
				FootEndLS_X = reader.ReadInt16();
				FootEndLS_Y = reader.ReadInt16();
				FootEndLS_Z = reader.ReadInt16();
				OnOffGain = reader.ReadInt16();
				GroundAscendingGain = reader.ReadInt16();
				GroundDescendingGain = reader.ReadInt16();
				FootRaisedGain = reader.ReadInt16();
				FootPlantedGain = reader.ReadInt16();
				FootUnlockGain = reader.ReadInt16();
				KneeAxisType = (SkeletonKneeAxisDirection)reader.ReadByte();
				UseFootLocking = reader.ReadByte() != 0;
				FootPlacementOn = reader.ReadByte() != 0;
				TwistKneeAxisType = (SkeletonKneeAxisDirection)reader.ReadByte();
				NeckTurnPriority = reader.ReadSByte();
				NeckTurnMaxAngle = reader.ReadByte();
				Pad1 = reader.ReadBytes(2);
			}

			internal Skeleton(ParameterTable table, int index)
				: base(table, index) {
				NeckTurnGain = (Single)0;
				OriginalGroundHeightMS = (Int16)0;
				MinAnkleHeightMS = (Int16)(-30);
				MaxAnkleHeightMS = (Int16)70;
				CosineMaxKneeAngle = (Int16)(-95);
				CosineMinKneeAngle = (Int16)55;
				FootPlantedAnkleHeightMS = (Int16)1;
				FootRaisedAnkleHeightMS = (Int16)30;
				RaycastDistanceUp = (Int16)70;
				RaycastDistanceDown = (Int16)55;
				FootEndLS_X = (Int16)0;
				FootEndLS_Y = (Int16)0;
				FootEndLS_Z = (Int16)0;
				OnOffGain = (Int16)18;
				GroundAscendingGain = (Int16)100;
				GroundDescendingGain = (Int16)100;
				FootRaisedGain = (Int16)20;
				FootPlantedGain = (Int16)100;
				FootUnlockGain = (Int16)80;
				KneeAxisType = (SkeletonKneeAxisDirection)4;
				UseFootLocking = false;
				FootPlacementOn = true;
				TwistKneeAxisType = (SkeletonKneeAxisDirection)1;
				NeckTurnPriority = (SByte)0;
				NeckTurnMaxAngle = (Byte)0;
				Pad1 = new Byte[2];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(NeckTurnGain);
				writer.Write(OriginalGroundHeightMS);
				writer.Write(MinAnkleHeightMS);
				writer.Write(MaxAnkleHeightMS);
				writer.Write(CosineMaxKneeAngle);
				writer.Write(CosineMinKneeAngle);
				writer.Write(FootPlantedAnkleHeightMS);
				writer.Write(FootRaisedAnkleHeightMS);
				writer.Write(RaycastDistanceUp);
				writer.Write(RaycastDistanceDown);
				writer.Write(FootEndLS_X);
				writer.Write(FootEndLS_Y);
				writer.Write(FootEndLS_Z);
				writer.Write(OnOffGain);
				writer.Write(GroundAscendingGain);
				writer.Write(GroundDescendingGain);
				writer.Write(FootRaisedGain);
				writer.Write(FootPlantedGain);
				writer.Write(FootUnlockGain);
				writer.Write((Byte)KneeAxisType);
				writer.Write((Byte)(UseFootLocking ? 1 : 0));
				writer.Write((Byte)(FootPlacementOn ? 1 : 0));
				writer.Write((Byte)TwistKneeAxisType);
				writer.Write(NeckTurnPriority);
				writer.Write(NeckTurnMaxAngle);
				writer.Write(Pad1);
			}
		}
	}
}
