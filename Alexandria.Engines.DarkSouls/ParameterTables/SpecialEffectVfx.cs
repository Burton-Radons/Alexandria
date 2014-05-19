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
		/// Defined as "SP_EFFECT_VFX_PARAM_ST" in Dark Souls in the file "SpEffectVfx.paramdef" (id 20h).
		/// </remarks>
		public class SpecialEffectVfx : ParameterTableRow {
			public const string TableName = "SP_EFFECT_VFX_PARAM_ST";

			Int32 midstSfxId, midstSeId, initSfxId, initSeId, finishSfxId, finishSeId, transformProtectorId;
			Single camouflageBeginDist, camouflageEndDist;
			Int16 midstDmyId, initDmyId, finishDmyId;
			SpecialEffectVfxEffectType effectType;
			SpecialEffectVfxSoulParameterType soulParamIdForWepEnchant;
			SpecialEffectVfxPlayCategory playCategory;
			Byte playPriority;
			Byte[] pad;

			public static readonly PropertyInfo
				MidstSfxIdProperty = GetProperty<SpecialEffectVfx>("MidstSfxId"),
				MidstSeIdProperty = GetProperty<SpecialEffectVfx>("MidstSeId"),
				InitSfxIdProperty = GetProperty<SpecialEffectVfx>("InitSfxId"),
				InitSeIdProperty = GetProperty<SpecialEffectVfx>("InitSeId"),
				FinishSfxIdProperty = GetProperty<SpecialEffectVfx>("FinishSfxId"),
				FinishSeIdProperty = GetProperty<SpecialEffectVfx>("FinishSeId"),
				CamouflageBeginDistProperty = GetProperty<SpecialEffectVfx>("CamouflageBeginDist"),
				CamouflageEndDistProperty = GetProperty<SpecialEffectVfx>("CamouflageEndDist"),
				TransformProtectorIdProperty = GetProperty<SpecialEffectVfx>("TransformProtectorId"),
				MidstDmyIdProperty = GetProperty<SpecialEffectVfx>("MidstDmyId"),
				InitDmyIdProperty = GetProperty<SpecialEffectVfx>("InitDmyId"),
				FinishDmyIdProperty = GetProperty<SpecialEffectVfx>("FinishDmyId"),
				EffectTypeProperty = GetProperty<SpecialEffectVfx>("EffectType"),
				SoulParamIdForWepEnchantProperty = GetProperty<SpecialEffectVfx>("SoulParamIdForWepEnchant"),
				PlayCategoryProperty = GetProperty<SpecialEffectVfx>("PlayCategory"),
				PlayPriorityProperty = GetProperty<SpecialEffectVfx>("PlayPriority"),
				ExistEffectForLargeProperty = GetProperty<SpecialEffectVfx>("ExistEffectForLarge"),
				ExistEffectForSoulProperty = GetProperty<SpecialEffectVfx>("ExistEffectForSoul"),
				EffectInvisibleAtCamouflageProperty = GetProperty<SpecialEffectVfx>("EffectInvisibleAtCamouflage"),
				UseCamouflageProperty = GetProperty<SpecialEffectVfx>("UseCamouflage"),
				InvisibleAtFriendCamouflageProperty = GetProperty<SpecialEffectVfx>("InvisibleAtFriendCamouflage"),
				AddMapAreaBlockOffsetProperty = GetProperty<SpecialEffectVfx>("AddMapAreaBlockOffset"),
				HalfCamouflageProperty = GetProperty<SpecialEffectVfx>("HalfCamouflage"),
				IsFullBodyTransformProtectorIdProperty = GetProperty<SpecialEffectVfx>("IsFullBodyTransformProtectorId"),
				IsInvisibleWeaponProperty = GetProperty<SpecialEffectVfx>("IsInvisibleWeapon"),
				IsSilenceProperty = GetProperty<SpecialEffectVfx>("IsSilence"),
				Pad_1Property = GetProperty<SpecialEffectVfx>("Pad_1"),
				PadProperty = GetProperty<SpecialEffectVfx>("Pad");

			/// <summary>Effect in SfxID</summary>
			/// <remarks>
			/// Japanese short name: "効果中SfxID", Google translated: "Effect in SfxID".
			/// Japanese description: "効果中SfxID(-1：無効)", Google translated: "Effect in SfxID (-1: disabled)".
			/// </remarks>
			[ParameterTableRowAttribute("midstSfxId", index: 0, minimum: -1, maximum: 1E+09, step: 1, order: 1100, unknown2: 1)]
			[DisplayName("Effect in SfxID")]
			[Description("Effect in SfxID (-1: disabled)")]
			[DefaultValue((Int32)(-1))]
			public Int32 MidstSfxId {
				get { return midstSfxId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + MidstSfxIdProperty.Name + ".");
					SetProperty(ref midstSfxId, ref value, MidstSfxIdProperty);
				}
			}

			/// <summary>Effect in SeID</summary>
			/// <remarks>
			/// Japanese short name: "効果中SeID", Google translated: "Effect in SeID".
			/// Japanese description: "効果中SeID(-1：無効)", Google translated: "Effect in SeID (-1: disabled)".
			/// </remarks>
			[ParameterTableRowAttribute("midstSeId", index: 1, minimum: -1, maximum: 1E+09, step: 1, order: 1200, unknown2: 1)]
			[DisplayName("Effect in SeID")]
			[Description("Effect in SeID (-1: disabled)")]
			[DefaultValue((Int32)(-1))]
			public Int32 MidstSeId {
				get { return midstSeId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + MidstSeIdProperty.Name + ".");
					SetProperty(ref midstSeId, ref value, MidstSeIdProperty);
				}
			}

			/// <summary>SfxID upon activation</summary>
			/// <remarks>
			/// Japanese short name: "発動時SfxID", Google translated: "SfxID upon activation".
			/// Japanese description: "発動時SfxID(-1：無効)", Google translated: "SfxID upon activation (-1 : disabled)".
			/// </remarks>
			[ParameterTableRowAttribute("initSfxId", index: 2, minimum: -1, maximum: 1E+09, step: 1, order: 1500, unknown2: 1)]
			[DisplayName("SfxID upon activation")]
			[Description("SfxID upon activation (-1 : disabled)")]
			[DefaultValue((Int32)(-1))]
			public Int32 InitSfxId {
				get { return initSfxId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + InitSfxIdProperty.Name + ".");
					SetProperty(ref initSfxId, ref value, InitSfxIdProperty);
				}
			}

			/// <summary>SeID upon activation</summary>
			/// <remarks>
			/// Japanese short name: "発動時SeID", Google translated: "SeID upon activation".
			/// Japanese description: "発動時SeID(-1：無効)", Google translated: "SeID upon activation (-1 : disabled)".
			/// </remarks>
			[ParameterTableRowAttribute("initSeId", index: 3, minimum: -1, maximum: 1E+09, step: 1, order: 1600, unknown2: 1)]
			[DisplayName("SeID upon activation")]
			[Description("SeID upon activation (-1 : disabled)")]
			[DefaultValue((Int32)(-1))]
			public Int32 InitSeId {
				get { return initSeId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + InitSeIdProperty.Name + ".");
					SetProperty(ref initSeId, ref value, InitSeIdProperty);
				}
			}

			/// <summary>SfxID when released</summary>
			/// <remarks>
			/// Japanese short name: "解除時SfxID", Google translated: "SfxID when released".
			/// Japanese description: "解除時SfxID(-1：無効)", Google translated: "SfxID when releasing (-1 : disabled)".
			/// </remarks>
			[ParameterTableRowAttribute("finishSfxId", index: 4, minimum: -1, maximum: 1E+09, step: 1, order: 1900, unknown2: 1)]
			[DisplayName("SfxID when released")]
			[Description("SfxID when releasing (-1 : disabled)")]
			[DefaultValue((Int32)(-1))]
			public Int32 FinishSfxId {
				get { return finishSfxId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + FinishSfxIdProperty.Name + ".");
					SetProperty(ref finishSfxId, ref value, FinishSfxIdProperty);
				}
			}

			/// <summary>SeID when released</summary>
			/// <remarks>
			/// Japanese short name: "解除時SeID", Google translated: "SeID when released".
			/// Japanese description: "解除時SeID(-1：無効)", Google translated: "SeID when releasing (-1 : disabled)".
			/// </remarks>
			[ParameterTableRowAttribute("finishSeId", index: 5, minimum: -1, maximum: 1E+09, step: 1, order: 2000, unknown2: 1)]
			[DisplayName("SeID when released")]
			[Description("SeID when releasing (-1 : disabled)")]
			[DefaultValue((Int32)(-1))]
			public Int32 FinishSeId {
				get { return finishSeId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + FinishSeIdProperty.Name + ".");
					SetProperty(ref finishSeId, ref value, FinishSeIdProperty);
				}
			}

			/// <summary>Figure hidden start distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "姿隠し開始距離[m]", Google translated: "Figure hidden start distance [m]".
			/// Japanese description: "カムフラージュ開始距離です", Google translated: "Camouflage is the start distance".
			/// </remarks>
			[ParameterTableRowAttribute("camouflageBeginDist", index: 6, minimum: -1, maximum: 100, step: 0.1, order: 2100, unknown2: 1)]
			[DisplayName("Figure hidden start distance [m]")]
			[Description("Camouflage is the start distance")]
			[DefaultValue((Single)(-1))]
			public Single CamouflageBeginDist {
				get { return camouflageBeginDist; }
				set {
					if ((double)value < -1 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 100 for " + CamouflageBeginDistProperty.Name + ".");
					SetProperty(ref camouflageBeginDist, ref value, CamouflageBeginDistProperty);
				}
			}

			/// <summary>Figure hidden end distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "姿隠し終了距離[m]", Google translated: "Figure hidden end distance [m]".
			/// Japanese description: "カムフラージュ終了距離です", Google translated: "Camouflage is the end distance".
			/// </remarks>
			[ParameterTableRowAttribute("camouflageEndDist", index: 7, minimum: -1, maximum: 100, step: 0.1, order: 2200, unknown2: 1)]
			[DisplayName("Figure hidden end distance [m]")]
			[Description("Camouflage is the end distance")]
			[DefaultValue((Single)(-1))]
			public Single CamouflageEndDist {
				get { return camouflageEndDist; }
				set {
					if ((double)value < -1 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 100 for " + CamouflageEndDistProperty.Name + ".");
					SetProperty(ref camouflageEndDist, ref value, CamouflageEndDistProperty);
				}
			}

			/// <summary>Transformation Armor ID</summary>
			/// <remarks>
			/// Japanese short name: "変身防具ID", Google translated: "Transformation Armor ID".
			/// Japanese description: "変身防具ID(-1：なし)", Google translated: "Transformation and armor ID (-1 : none)".
			/// </remarks>
			[ParameterTableRowAttribute("transformProtectorId", index: 8, minimum: -1, maximum: 1E+09, step: 1, order: 2300, unknown2: 1)]
			[DisplayName("Transformation Armor ID")]
			[Description("Transformation and armor ID (-1 : none)")]
			[DefaultValue((Int32)(-1))]
			public Int32 TransformProtectorId {
				get { return transformProtectorId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + TransformProtectorIdProperty.Name + ".");
					SetProperty(ref transformProtectorId, ref value, TransformProtectorIdProperty);
				}
			}

			/// <summary>Effect in Damipori ID</summary>
			/// <remarks>
			/// Japanese short name: "効果中ダミポリID", Google translated: "Effect in Damipori ID".
			/// Japanese description: "効果中ダミポリID(-1：ルート)", Google translated: "Effect in Damipori ID (-1: root )".
			/// </remarks>
			[ParameterTableRowAttribute("midstDmyId", index: 9, minimum: -1, maximum: 255, step: 1, order: 1000, unknown2: 1)]
			[DisplayName("Effect in Damipori ID")]
			[Description("Effect in Damipori ID (-1: root )")]
			[DefaultValue((Int16)(-1))]
			public Int16 MidstDmyId {
				get { return midstDmyId; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for " + MidstDmyIdProperty.Name + ".");
					SetProperty(ref midstDmyId, ref value, MidstDmyIdProperty);
				}
			}

			/// <summary>Damipori ID upon activation</summary>
			/// <remarks>
			/// Japanese short name: "発動時ダミポリID", Google translated: "Damipori ID upon activation".
			/// Japanese description: "発動時ダミポリID(-1：ルート)", Google translated: "Damipori ID upon activation (-1 : root )".
			/// </remarks>
			[ParameterTableRowAttribute("initDmyId", index: 10, minimum: -1, maximum: 255, step: 1, order: 1400, unknown2: 1)]
			[DisplayName("Damipori ID upon activation")]
			[Description("Damipori ID upon activation (-1 : root )")]
			[DefaultValue((Int16)(-1))]
			public Int16 InitDmyId {
				get { return initDmyId; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for " + InitDmyIdProperty.Name + ".");
					SetProperty(ref initDmyId, ref value, InitDmyIdProperty);
				}
			}

			/// <summary>Damipori ID when released</summary>
			/// <remarks>
			/// Japanese short name: "解除時ダミポリID", Google translated: "Damipori ID when released".
			/// Japanese description: "解除時ダミポリID(-1：ルート)", Google translated: "Damipori ID when releasing (-1 : root )".
			/// </remarks>
			[ParameterTableRowAttribute("finishDmyId", index: 11, minimum: -1, maximum: 255, step: 1, order: 1800, unknown2: 1)]
			[DisplayName("Damipori ID when released")]
			[Description("Damipori ID when releasing (-1 : root )")]
			[DefaultValue((Int16)(-1))]
			public Int16 FinishDmyId {
				get { return finishDmyId; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for " + FinishDmyIdProperty.Name + ".");
					SetProperty(ref finishDmyId, ref value, FinishDmyIdProperty);
				}
			}

			/// <summary>Effect type</summary>
			/// <remarks>
			/// Japanese short name: "エフェクトタイプ", Google translated: "Effect type".
			/// Japanese description: "エフェクトタイプ", Google translated: "Effect type".
			/// </remarks>
			[ParameterTableRowAttribute("effectType", index: 12, minimum: 0, maximum: 255, step: 1, order: 100, unknown2: 1)]
			[DisplayName("Effect type")]
			[Description("Effect type")]
			[DefaultValue((SpecialEffectVfxEffectType)0)]
			public SpecialEffectVfxEffectType EffectType {
				get { return effectType; }
				set { SetProperty(ref effectType, ref value, EffectTypeProperty); }
			}

			/// <summary>Weapon Enchantment for Souruparamu ID</summary>
			/// <remarks>
			/// Japanese short name: "武器エンチャント用ソウルパラムID", Google translated: "Weapon Enchantment for Souruparamu ID".
			/// Japanese description: "武器エンチャント用ソウルパラムID(-1：なし)", Google translated: "Weapon Enchantment for Souruparamu ID and (-1 : none)".
			/// </remarks>
			[ParameterTableRowAttribute("soulParamIdForWepEnchant", index: 13, minimum: 0, maximum: 127, step: 1, order: 200, unknown2: 0)]
			[DisplayName("Weapon Enchantment for Souruparamu ID")]
			[Description("Weapon Enchantment for Souruparamu ID and (-1 : none)")]
			[DefaultValue((SpecialEffectVfxSoulParameterType)0)]
			public SpecialEffectVfxSoulParameterType SoulParamIdForWepEnchant {
				get { return soulParamIdForWepEnchant; }
				set { SetProperty(ref soulParamIdForWepEnchant, ref value, SoulParamIdForWepEnchantProperty); }
			}

			/// <summary>Play VFX category</summary>
			/// <remarks>
			/// Japanese short name: "VFX再生カテゴリ", Google translated: "Play VFX category".
			/// Japanese description: "重複効果によるエフェクト再生を制御します", Google translated: "It controls the playback effects by overlapping effect".
			/// </remarks>
			[ParameterTableRowAttribute("playCategory", index: 14, minimum: 0, maximum: 255, step: 1, order: 10, unknown2: 1)]
			[DisplayName("Play VFX category")]
			[Description("It controls the playback effects by overlapping effect")]
			[DefaultValue((SpecialEffectVfxPlayCategory)0)]
			public SpecialEffectVfxPlayCategory PlayCategory {
				get { return playCategory; }
				set { SetProperty(ref playCategory, ref value, PlayCategoryProperty); }
			}

			/// <summary>Category priority</summary>
			/// <remarks>
			/// Japanese short name: "カテゴリ内優先度", Google translated: "Category priority".
			/// Japanese description: "カテゴリ一致した場合の再生優先度を設定(低い方が優先)", Google translated: "( Lower priority ) to set the playback priority in the case of the category match".
			/// </remarks>
			[ParameterTableRowAttribute("playPriority", index: 15, minimum: 0, maximum: 255, step: 1, order: 20, unknown2: 1)]
			[DisplayName("Category priority")]
			[Description("( Lower priority ) to set the playback priority in the case of the category match")]
			[DefaultValue((Byte)0)]
			public Byte PlayPriority {
				get { return playPriority; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + PlayPriorityProperty.Name + ".");
					SetProperty(ref playPriority, ref value, PlayPriorityProperty);
				}
			}

			/// <summary>If there is a large effect for</summary>
			/// <remarks>
			/// Japanese short name: "大型用エフェクトがあるか", Google translated: "If there is a large effect for".
			/// Japanese description: "大型用エフェクトがあるか", Google translated: "If there is a large effect for".
			/// </remarks>
			[ParameterTableRowAttribute("existEffectForLarge:1", index: 16, minimum: 0, maximum: 1, step: 1, order: 300, unknown2: 1)]
			[DisplayName("If there is a large effect for")]
			[Description("If there is a large effect for")]
			[DefaultValue(false)]
			public Boolean ExistEffectForLarge {
				get { return GetBitProperty(0, 1, ExistEffectForLargeProperty) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, ExistEffectForLargeProperty); }
			}

			/// <summary>If there is a material effect for Seoul</summary>
			/// <remarks>
			/// Japanese short name: "ソウル体用エフェクトがあるか", Google translated: "If there is a material effect for Seoul".
			/// Japanese description: "ソウル体用エフェクトがあるか", Google translated: "If there is a material effect for Seoul".
			/// </remarks>
			[ParameterTableRowAttribute("existEffectForSoul:1", index: 17, minimum: 0, maximum: 1, step: 1, order: 400, unknown2: 1)]
			[DisplayName("If there is a material effect for Seoul")]
			[Description("If there is a material effect for Seoul")]
			[DefaultValue(false)]
			public Boolean ExistEffectForSoul {
				get { return GetBitProperty(1, 1, ExistEffectForSoulProperty) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, ExistEffectForSoulProperty); }
			}

			/// <summary>You can hide the effects to figure hidden during</summary>
			/// <remarks>
			/// Japanese short name: "姿隠し時にエフェクトを非表示にするか", Google translated: "You can hide the effects to figure hidden during".
			/// Japanese description: "姿隠し時にエフェクトを非表示にするか", Google translated: "You can hide the effects to figure hidden during".
			/// </remarks>
			[ParameterTableRowAttribute("effectInvisibleAtCamouflage:1", index: 18, minimum: 0, maximum: 1, step: 1, order: 500, unknown2: 1)]
			[DisplayName("You can hide the effects to figure hidden during")]
			[Description("You can hide the effects to figure hidden during")]
			[DefaultValue(false)]
			public Boolean EffectInvisibleAtCamouflage {
				get { return GetBitProperty(2, 1, EffectInvisibleAtCamouflageProperty) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, EffectInvisibleAtCamouflageProperty); }
			}

			/// <summary>Or figure hidden</summary>
			/// <remarks>
			/// Japanese short name: "姿隠しするか", Google translated: "Or figure hidden".
			/// Japanese description: "姿隠しするか", Google translated: "Or figure hidden".
			/// </remarks>
			[ParameterTableRowAttribute("useCamouflage:1", index: 19, minimum: 0, maximum: 1, step: 1, order: 600, unknown2: 1)]
			[DisplayName("Or figure hidden")]
			[Description("Or figure hidden")]
			[DefaultValue(false)]
			public Boolean UseCamouflage {
				get { return GetBitProperty(3, 1, UseCamouflageProperty) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, UseCamouflageProperty); }
			}

			/// <summary>Or hidden in favor of figure hidden during</summary>
			/// <remarks>
			/// Japanese short name: "姿隠し時に味方でも非表示か", Google translated: "Or hidden in favor of figure hidden during".
			/// Japanese description: "姿隠し時に味方でも非表示か", Google translated: "Or hidden in favor of figure hidden during".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleAtFriendCamouflage:1", index: 20, minimum: 0, maximum: 1, step: 1, order: 700, unknown2: 1)]
			[DisplayName("Or hidden in favor of figure hidden during")]
			[Description("Or hidden in favor of figure hidden during")]
			[DefaultValue(false)]
			public Boolean InvisibleAtFriendCamouflage {
				get { return GetBitProperty(4, 1, InvisibleAtFriendCamouflageProperty) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, InvisibleAtFriendCamouflageProperty); }
			}

			/// <summary>Or add the number to map SfxID</summary>
			/// <remarks>
			/// Japanese short name: "SfxIDにマップ番号を足すか", Google translated: "Or add the number to map SfxID".
			/// Japanese description: "SfxIDにマップ番号(AAB0)を加算します", Google translated: "I Adds (AAB0) map number to SfxID".
			/// </remarks>
			[ParameterTableRowAttribute("addMapAreaBlockOffset:1", index: 21, minimum: 0, maximum: 1, step: 1, order: 800, unknown2: 1)]
			[DisplayName("Or add the number to map SfxID")]
			[Description("I Adds (AAB0) map number to SfxID")]
			[DefaultValue(false)]
			public Boolean AddMapAreaBlockOffset {
				get { return GetBitProperty(5, 1, AddMapAreaBlockOffsetProperty) != 0; }
				set { SetBitProperty(5, 1, value ? 1 : 0, AddMapAreaBlockOffsetProperty); }
			}

			/// <summary>Hidden or appearance of translucent</summary>
			/// <remarks>
			/// Japanese short name: "半透明の姿隠しか", Google translated: "Hidden or appearance of translucent".
			/// Japanese description: "半透明の姿隠しか", Google translated: "Hidden or appearance of translucent".
			/// </remarks>
			[ParameterTableRowAttribute("halfCamouflage:1", index: 22, minimum: 0, maximum: 1, step: 1, order: 750, unknown2: 1)]
			[DisplayName("Hidden or appearance of translucent")]
			[Description("Hidden or appearance of translucent")]
			[DefaultValue(false)]
			public Boolean HalfCamouflage {
				get { return GetBitProperty(6, 1, HalfCamouflageProperty) != 0; }
				set { SetBitProperty(6, 1, value ? 1 : 0, HalfCamouflageProperty); }
			}

			/// <summary>Transformation armor ID or whole-body</summary>
			/// <remarks>
			/// Japanese short name: "変身防具IDが全身用か", Google translated: "Transformation armor ID or whole-body".
			/// Japanese description: "変身防具IDが全身用か", Google translated: "Transformation armor ID or whole-body".
			/// </remarks>
			[ParameterTableRowAttribute("isFullBodyTransformProtectorId:1", index: 23, minimum: 0, maximum: 1, step: 1, order: 2310, unknown2: 1)]
			[DisplayName("Transformation armor ID or whole-body")]
			[Description("Transformation armor ID or whole-body")]
			[DefaultValue(false)]
			public Boolean IsFullBodyTransformProtectorId {
				get { return GetBitProperty(7, 1, IsFullBodyTransformProtectorIdProperty) != 0; }
				set { SetBitProperty(7, 1, value ? 1 : 0, IsFullBodyTransformProtectorIdProperty); }
			}

			/// <summary>Or weapon enchantment for invisible Weapon</summary>
			/// <remarks>
			/// Japanese short name: "武器エンチャント用インビジブルウェポンか", Google translated: "Or weapon enchantment for invisible Weapon".
			/// Japanese description: "武器エンチャント用インビジブルウェポンか(0:武器表示, 1:武器非表示)", Google translated: "( : Weapon display , 1 : weapon hidden 0) or weapon enchantment for invisible Weapon".
			/// </remarks>
			[ParameterTableRowAttribute("isInvisibleWeapon:1", index: 24, minimum: 0, maximum: 1, step: 1, order: 250, unknown2: 1)]
			[DisplayName("Or weapon enchantment for invisible Weapon")]
			[Description("( : Weapon display , 1 : weapon hidden 0) or weapon enchantment for invisible Weapon")]
			[DefaultValue(false)]
			public Boolean IsInvisibleWeapon {
				get { return GetBitProperty(8, 1, IsInvisibleWeaponProperty) != 0; }
				set { SetBitProperty(8, 1, value ? 1 : 0, IsInvisibleWeaponProperty); }
			}

			/// <summary>Or silence</summary>
			/// <remarks>
			/// Japanese short name: "サイレンスか", Google translated: "Or silence".
			/// Japanese description: "サイレンスか(0:ちがう, 1:そう)", Google translated: "Or silence (0: no, 1 : yes )".
			/// </remarks>
			[ParameterTableRowAttribute("isSilence:1", index: 25, minimum: 0, maximum: 1, step: 1, order: 775, unknown2: 1)]
			[DisplayName("Or silence")]
			[Description("Or silence (0: no, 1 : yes )")]
			[DefaultValue(false)]
			public Boolean IsSilence {
				get { return GetBitProperty(9, 1, IsSilenceProperty) != 0; }
				set { SetBitProperty(9, 1, value ? 1 : 0, IsSilenceProperty); }
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "パディング", Google translated: "Padding".
			/// </remarks>
			[ParameterTableRowAttribute("pad_1:6", index: 26, minimum: 0, maximum: 255, step: 1, order: 2311, unknown2: 1)]
			[DisplayName("Padding")]
			[Description("Padding")]
			[DefaultValue((Byte)0)]
			[Browsable(false)]
			public Byte Pad_1 {
				get { return (Byte)GetBitProperty(10, 6, Pad_1Property); }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + Pad_1Property.Name + ".");
					SetBitProperty(10, 6, (int)value, Pad_1Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "パディング", Google translated: "Padding".
			/// </remarks>
			[ParameterTableRowAttribute("pad[16]", index: 27, minimum: 0, maximum: 255, step: 1, order: 2312, unknown2: 1)]
			[DisplayName("Padding")]
			[Description("Padding")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal SpecialEffectVfx(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				MidstSfxId = reader.ReadInt32();
				MidstSeId = reader.ReadInt32();
				InitSfxId = reader.ReadInt32();
				InitSeId = reader.ReadInt32();
				FinishSfxId = reader.ReadInt32();
				FinishSeId = reader.ReadInt32();
				CamouflageBeginDist = reader.ReadSingle();
				CamouflageEndDist = reader.ReadSingle();
				TransformProtectorId = reader.ReadInt32();
				MidstDmyId = reader.ReadInt16();
				InitDmyId = reader.ReadInt16();
				FinishDmyId = reader.ReadInt16();
				EffectType = (SpecialEffectVfxEffectType)reader.ReadByte();
				SoulParamIdForWepEnchant = (SpecialEffectVfxSoulParameterType)reader.ReadByte();
				PlayCategory = (SpecialEffectVfxPlayCategory)reader.ReadByte();
				PlayPriority = reader.ReadByte();
				BitFields = reader.ReadBytes(2);
				Pad = reader.ReadBytes(16);
			}

			internal SpecialEffectVfx(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[2];
				MidstSfxId = (Int32)(-1);
				MidstSeId = (Int32)(-1);
				InitSfxId = (Int32)(-1);
				InitSeId = (Int32)(-1);
				FinishSfxId = (Int32)(-1);
				FinishSeId = (Int32)(-1);
				CamouflageBeginDist = (Single)(-1);
				CamouflageEndDist = (Single)(-1);
				TransformProtectorId = (Int32)(-1);
				MidstDmyId = (Int16)(-1);
				InitDmyId = (Int16)(-1);
				FinishDmyId = (Int16)(-1);
				EffectType = (SpecialEffectVfxEffectType)0;
				SoulParamIdForWepEnchant = (SpecialEffectVfxSoulParameterType)0;
				PlayCategory = (SpecialEffectVfxPlayCategory)0;
				PlayPriority = (Byte)0;
				ExistEffectForLarge = false;
				ExistEffectForSoul = false;
				EffectInvisibleAtCamouflage = false;
				UseCamouflage = false;
				InvisibleAtFriendCamouflage = false;
				AddMapAreaBlockOffset = false;
				HalfCamouflage = false;
				IsFullBodyTransformProtectorId = false;
				IsInvisibleWeapon = false;
				IsSilence = false;
				Pad_1 = (Byte)0;
				Pad = new Byte[16];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(MidstSfxId);
				writer.Write(MidstSeId);
				writer.Write(InitSfxId);
				writer.Write(InitSeId);
				writer.Write(FinishSfxId);
				writer.Write(FinishSeId);
				writer.Write(CamouflageBeginDist);
				writer.Write(CamouflageEndDist);
				writer.Write(TransformProtectorId);
				writer.Write(MidstDmyId);
				writer.Write(InitDmyId);
				writer.Write(FinishDmyId);
				writer.Write((Byte)EffectType);
				writer.Write((Byte)SoulParamIdForWepEnchant);
				writer.Write((Byte)PlayCategory);
				writer.Write(PlayPriority);
				writer.Write(BitFields);
				writer.Write(Pad);
			}
		}
	}
}
