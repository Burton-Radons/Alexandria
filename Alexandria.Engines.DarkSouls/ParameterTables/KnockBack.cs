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
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// Named "KNOCKBACK_PARAM_ST" in Dark Souls, from file "KnockBackParam.paramdef" (id 31h).
		/// </remarks>
		public class KnockBack : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "KNOCKBACK_PARAM_ST";

			Single damage_Min_ContTime, damage_S_ContTime, damage_M_ContTime, damage_L_ContTime, damage_BlowS_ContTime, damage_BlowM_ContTime, damage_Strike_ContTime, damage_Uppercut_ContTime, damage_Push_ContTime, damage_Breath_ContTime, damage_HeadShot_ContTime, guard_S_ContTime, guard_L_ContTime, guard_LL_ContTime, guardBrake_ContTime, damage_Min_DecTime, damage_S_DecTime, damage_M_DecTime, damage_L_DecTime, damage_BlowS_DecTime, damage_BlowM_DecTime, damage_Strike_DecTime, damage_Uppercut_DecTime, damage_Push_DecTime, damage_Breath_DecTime, damage_HeadShot_DecTime, guard_S_DecTime, guard_L_DecTime, guard_LL_DecTime, guardBrake_DecTime;
			Byte[] pad;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				Damage_Min_ContTimeProperty = GetProperty<KnockBack>("Damage_Min_ContTime"),
				Damage_S_ContTimeProperty = GetProperty<KnockBack>("Damage_S_ContTime"),
				Damage_M_ContTimeProperty = GetProperty<KnockBack>("Damage_M_ContTime"),
				Damage_L_ContTimeProperty = GetProperty<KnockBack>("Damage_L_ContTime"),
				Damage_BlowS_ContTimeProperty = GetProperty<KnockBack>("Damage_BlowS_ContTime"),
				Damage_BlowM_ContTimeProperty = GetProperty<KnockBack>("Damage_BlowM_ContTime"),
				Damage_Strike_ContTimeProperty = GetProperty<KnockBack>("Damage_Strike_ContTime"),
				Damage_Uppercut_ContTimeProperty = GetProperty<KnockBack>("Damage_Uppercut_ContTime"),
				Damage_Push_ContTimeProperty = GetProperty<KnockBack>("Damage_Push_ContTime"),
				Damage_Breath_ContTimeProperty = GetProperty<KnockBack>("Damage_Breath_ContTime"),
				Damage_HeadShot_ContTimeProperty = GetProperty<KnockBack>("Damage_HeadShot_ContTime"),
				Guard_S_ContTimeProperty = GetProperty<KnockBack>("Guard_S_ContTime"),
				Guard_L_ContTimeProperty = GetProperty<KnockBack>("Guard_L_ContTime"),
				Guard_LL_ContTimeProperty = GetProperty<KnockBack>("Guard_LL_ContTime"),
				GuardBrake_ContTimeProperty = GetProperty<KnockBack>("GuardBrake_ContTime"),
				Damage_Min_DecTimeProperty = GetProperty<KnockBack>("Damage_Min_DecTime"),
				Damage_S_DecTimeProperty = GetProperty<KnockBack>("Damage_S_DecTime"),
				Damage_M_DecTimeProperty = GetProperty<KnockBack>("Damage_M_DecTime"),
				Damage_L_DecTimeProperty = GetProperty<KnockBack>("Damage_L_DecTime"),
				Damage_BlowS_DecTimeProperty = GetProperty<KnockBack>("Damage_BlowS_DecTime"),
				Damage_BlowM_DecTimeProperty = GetProperty<KnockBack>("Damage_BlowM_DecTime"),
				Damage_Strike_DecTimeProperty = GetProperty<KnockBack>("Damage_Strike_DecTime"),
				Damage_Uppercut_DecTimeProperty = GetProperty<KnockBack>("Damage_Uppercut_DecTime"),
				Damage_Push_DecTimeProperty = GetProperty<KnockBack>("Damage_Push_DecTime"),
				Damage_Breath_DecTimeProperty = GetProperty<KnockBack>("Damage_Breath_DecTime"),
				Damage_HeadShot_DecTimeProperty = GetProperty<KnockBack>("Damage_HeadShot_DecTime"),
				Guard_S_DecTimeProperty = GetProperty<KnockBack>("Guard_S_DecTime"),
				Guard_L_DecTimeProperty = GetProperty<KnockBack>("Guard_L_DecTime"),
				Guard_LL_DecTimeProperty = GetProperty<KnockBack>("Guard_LL_DecTime"),
				GuardBrake_DecTimeProperty = GetProperty<KnockBack>("GuardBrake_DecTime"),
				PadProperty = GetProperty<KnockBack>("Pad");

			/// <summary>Minimal damage _ speed maintenance time [s]</summary>
			/// <remarks>
			/// Japanese short name: "極小ダメージ_速度維持時間[s]", Google translated: "Minimal damage _ speed maintenance time [s]".
			/// Japanese description: "極小ダメージアニメの時に使用される維持時間を設定", Google translated: "Set the maintenance time would be used for a very small damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_Min_ContTime", index: 0, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 100, unknown2: 1)]
			[DisplayName("Minimal damage _ speed maintenance time [s]")]
			[Description("Set the maintenance time would be used for a very small damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_Min_ContTime {
				get { return damage_Min_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_Min_ContTime.");
					SetProperty(ref damage_Min_ContTime, ref value, Damage_Min_ContTimeProperty);
				}
			}

			/// <summary>Small damage _ speed maintenance time [s]</summary>
			/// <remarks>
			/// Japanese short name: "小ダメージ_速度維持時間[s]", Google translated: "Small damage _ speed maintenance time [s]".
			/// Japanese description: "小ダメージアニメの時に使用される維持時間を設定", Google translated: "Set the maintenance time would be used for small damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_S_ContTime", index: 1, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 300, unknown2: 1)]
			[DisplayName("Small damage _ speed maintenance time [s]")]
			[Description("Set the maintenance time would be used for small damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_S_ContTime {
				get { return damage_S_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_S_ContTime.");
					SetProperty(ref damage_S_ContTime, ref value, Damage_S_ContTimeProperty);
				}
			}

			/// <summary>Medium Damage _ speed maintenance time [s]</summary>
			/// <remarks>
			/// Japanese short name: "中ダメージ_速度維持時間[s]", Google translated: "Medium Damage _ speed maintenance time [s]".
			/// Japanese description: "中ダメージアニメの時に使用される維持時間を設定", Google translated: "Set the maintenance time would be used for the medium Damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_M_ContTime", index: 2, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 500, unknown2: 1)]
			[DisplayName("Medium Damage _ speed maintenance time [s]")]
			[Description("Set the maintenance time would be used for the medium Damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_M_ContTime {
				get { return damage_M_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_M_ContTime.");
					SetProperty(ref damage_M_ContTime, ref value, Damage_M_ContTimeProperty);
				}
			}

			/// <summary>Large damage _ speed maintenance time [s]</summary>
			/// <remarks>
			/// Japanese short name: "大ダメージ_速度維持時間[s]", Google translated: "Large damage _ speed maintenance time [s]".
			/// Japanese description: "大ダメージアニメの時に使用される維持時間を設定", Google translated: "Set the maintenance time would be used for a great damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_L_ContTime", index: 3, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 700, unknown2: 1)]
			[DisplayName("Large damage _ speed maintenance time [s]")]
			[Description("Set the maintenance time would be used for a great damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_L_ContTime {
				get { return damage_L_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_L_ContTime.");
					SetProperty(ref damage_L_ContTime, ref value, Damage_L_ContTimeProperty);
				}
			}

			/// <summary>The time maintaining speed _ Futtobi small [s]</summary>
			/// <remarks>
			/// Japanese short name: "小吹っ飛び_速度維持時間[s]", Google translated: "The time maintaining speed _ Futtobi small [s]".
			/// Japanese description: "小吹っ飛びダメージアニメの時に使用される維持時間を設定", Google translated: "Setting the maintenance time that is used when the damage animation Futtobi small".
			/// </remarks>
			[ParameterTableRowAttribute("damage_BlowS_ContTime", index: 4, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 900, unknown2: 1)]
			[DisplayName("The time maintaining speed _ Futtobi small [s]")]
			[Description("Setting the maintenance time that is used when the damage animation Futtobi small")]
			[DefaultValue((Single)0)]
			public Single Damage_BlowS_ContTime {
				get { return damage_BlowS_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_BlowS_ContTime.");
					SetProperty(ref damage_BlowS_ContTime, ref value, Damage_BlowS_ContTimeProperty);
				}
			}

			/// <summary>The time maintaining speed _ Futtobi large [s]</summary>
			/// <remarks>
			/// Japanese short name: "大吹っ飛び_速度維持時間[s]", Google translated: "The time maintaining speed _ Futtobi large [s]".
			/// Japanese description: "大吹っ飛びダメージアニメの時に使用される維持時間を設定", Google translated: "Setting the maintenance time that is used when the damage animation Futtobi large".
			/// </remarks>
			[ParameterTableRowAttribute("damage_BlowM_ContTime", index: 5, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1100, unknown2: 1)]
			[DisplayName("The time maintaining speed _ Futtobi large [s]")]
			[Description("Setting the maintenance time that is used when the damage animation Futtobi large")]
			[DefaultValue((Single)0)]
			public Single Damage_BlowM_ContTime {
				get { return damage_BlowM_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_BlowM_ContTime.");
					SetProperty(ref damage_BlowM_ContTime, ref value, Damage_BlowM_ContTimeProperty);
				}
			}

			/// <summary>The time maintaining speed _ slammed [s]</summary>
			/// <remarks>
			/// Japanese short name: "叩きつけ_速度維持時間[s]", Google translated: "The time maintaining speed _ slammed [s]".
			/// Japanese description: "叩きつけダメージアニメの時に使用される維持時間を設定", Google translated: "Setting the maintenance time that is used when the damage animation slam".
			/// </remarks>
			[ParameterTableRowAttribute("damage_Strike_ContTime", index: 6, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1300, unknown2: 1)]
			[DisplayName("The time maintaining speed _ slammed [s]")]
			[Description("Setting the maintenance time that is used when the damage animation slam")]
			[DefaultValue((Single)0)]
			public Single Damage_Strike_ContTime {
				get { return damage_Strike_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_Strike_ContTime.");
					SetProperty(ref damage_Strike_ContTime, ref value, Damage_Strike_ContTimeProperty);
				}
			}

			/// <summary>The time maintaining speed _ launch [s]</summary>
			/// <remarks>
			/// Japanese short name: "打ち上げ_速度維持時間[s]", Google translated: "The time maintaining speed _ launch [s]".
			/// Japanese description: "打ち上げダメージアニメの時に使用される維持時間を設定", Google translated: "Setting the maintenance time that is used when the damage animation Launch".
			/// </remarks>
			[ParameterTableRowAttribute("damage_Uppercut_ContTime", index: 7, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1500, unknown2: 1)]
			[DisplayName("The time maintaining speed _ launch [s]")]
			[Description("Setting the maintenance time that is used when the damage animation Launch")]
			[DefaultValue((Single)0)]
			public Single Damage_Uppercut_ContTime {
				get { return damage_Uppercut_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_Uppercut_ContTime.");
					SetProperty(ref damage_Uppercut_ContTime, ref value, Damage_Uppercut_ContTimeProperty);
				}
			}

			/// <summary>Push _ speed maintenance time [s]</summary>
			/// <remarks>
			/// Japanese short name: "プッシュ_速度維持時間[s]", Google translated: "Push _ speed maintenance time [s]".
			/// Japanese description: "プッシュダメージアニメの時に使用される維持時間を設定", Google translated: "Set the maintenance time would be used for push damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_Push_ContTime", index: 8, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1700, unknown2: 1)]
			[DisplayName("Push _ speed maintenance time [s]")]
			[Description("Set the maintenance time would be used for push damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_Push_ContTime {
				get { return damage_Push_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_Push_ContTime.");
					SetProperty(ref damage_Push_ContTime, ref value, Damage_Push_ContTimeProperty);
				}
			}

			/// <summary>Breath _ speed maintenance time [s]</summary>
			/// <remarks>
			/// Japanese short name: "ブレス_速度維持時間[s]", Google translated: "Breath _ speed maintenance time [s]".
			/// Japanese description: "ブレスダメージアニメの時に使用される維持時間を設定", Google translated: "Setting the maintenance time that is used when the breath damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_Breath_ContTime", index: 9, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1810, unknown2: 1)]
			[DisplayName("Breath _ speed maintenance time [s]")]
			[Description("Setting the maintenance time that is used when the breath damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_Breath_ContTime {
				get { return damage_Breath_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_Breath_ContTime.");
					SetProperty(ref damage_Breath_ContTime, ref value, Damage_Breath_ContTimeProperty);
				}
			}

			/// <summary>Headshot _ speed maintenance time [s]</summary>
			/// <remarks>
			/// Japanese short name: "ヘッドショット_速度維持時間[s]", Google translated: "Headshot _ speed maintenance time [s]".
			/// Japanese description: "ヘッドショットダメージアニメの時に使用される維持時間を設定", Google translated: "Setting the maintenance time that is used when the head shot damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_HeadShot_ContTime", index: 10, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1900, unknown2: 1)]
			[DisplayName("Headshot _ speed maintenance time [s]")]
			[Description("Setting the maintenance time that is used when the head shot damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_HeadShot_ContTime {
				get { return damage_HeadShot_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_HeadShot_ContTime.");
					SetProperty(ref damage_HeadShot_ContTime, ref value, Damage_HeadShot_ContTimeProperty);
				}
			}

			/// <summary>Time maintaining speed _ Small received guard [s]</summary>
			/// <remarks>
			/// Japanese short name: "ガード受け小_速度維持時間[s]", Google translated: "Time maintaining speed _ Small received guard [s]".
			/// Japanese description: "ガード受け小アニメの時に使用される維持時間を設定", Google translated: "Set the maintenance time would be used for a small animated receiving guard".
			/// </remarks>
			[ParameterTableRowAttribute("guard_S_ContTime", index: 11, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 2100, unknown2: 1)]
			[DisplayName("Time maintaining speed _ Small received guard [s]")]
			[Description("Set the maintenance time would be used for a small animated receiving guard")]
			[DefaultValue((Single)0)]
			public Single Guard_S_ContTime {
				get { return guard_S_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Guard_S_ContTime.");
					SetProperty(ref guard_S_ContTime, ref value, Guard_S_ContTimeProperty);
				}
			}

			/// <summary>Time maintaining speed _ University received guard [s]</summary>
			/// <remarks>
			/// Japanese short name: "ガード受け大_速度維持時間[s]", Google translated: "Time maintaining speed _ University received guard [s]".
			/// Japanese description: "ガード受け大アニメの時に使用される維持時間を設定", Google translated: "Set the maintenance time would be used for a large animated receiving guard".
			/// </remarks>
			[ParameterTableRowAttribute("guard_L_ContTime", index: 12, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 2300, unknown2: 1)]
			[DisplayName("Time maintaining speed _ University received guard [s]")]
			[Description("Set the maintenance time would be used for a large animated receiving guard")]
			[DefaultValue((Single)0)]
			public Single Guard_L_ContTime {
				get { return guard_L_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Guard_L_ContTime.");
					SetProperty(ref guard_L_ContTime, ref value, Guard_L_ContTimeProperty);
				}
			}

			/// <summary>Received extra large guard _ speed maintenance time [s]</summary>
			/// <remarks>
			/// Japanese short name: "ガード受け特大_速度維持時間[s]", Google translated: "Received extra large guard _ speed maintenance time [s]".
			/// Japanese description: "ガード受け特大アニメの時に使用される維持時間を設定", Google translated: "Setting maintenance time that is used when oversized anime received guard".
			/// </remarks>
			[ParameterTableRowAttribute("guard_LL_ContTime", index: 13, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 2410, unknown2: 1)]
			[DisplayName("Received extra large guard _ speed maintenance time [s]")]
			[Description("Setting maintenance time that is used when oversized anime received guard")]
			[DefaultValue((Single)0)]
			public Single Guard_LL_ContTime {
				get { return guard_LL_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Guard_LL_ContTime.");
					SetProperty(ref guard_LL_ContTime, ref value, Guard_LL_ContTimeProperty);
				}
			}

			/// <summary>Is broken down guard _ speed maintenance time [s]</summary>
			/// <remarks>
			/// Japanese short name: "ガードくずされ_速度維持時間[s]", Google translated: "Is broken down guard _ speed maintenance time [s]".
			/// Japanese description: "ガードくずされアニメの時に仕様される維持時間を設定", Google translated: "Setting the maintenance time , which is the specification when the animation to be deformed Guard".
			/// </remarks>
			[ParameterTableRowAttribute("guardBrake_ContTime", index: 14, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 2500, unknown2: 1)]
			[DisplayName("Is broken down guard _ speed maintenance time [s]")]
			[Description("Setting the maintenance time , which is the specification when the animation to be deformed Guard")]
			[DefaultValue((Single)0)]
			public Single GuardBrake_ContTime {
				get { return guardBrake_ContTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for GuardBrake_ContTime.");
					SetProperty(ref guardBrake_ContTime, ref value, GuardBrake_ContTimeProperty);
				}
			}

			/// <summary>Minimal damage _ deceleration time [s]</summary>
			/// <remarks>
			/// Japanese short name: "極小ダメージ_減速時間[s]", Google translated: "Minimal damage _ deceleration time [s]".
			/// Japanese description: "極小ダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for a very small damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_Min_DecTime", index: 15, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 200, unknown2: 1)]
			[DisplayName("Minimal damage _ deceleration time [s]")]
			[Description("Setting the deceleration time would be used for a very small damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_Min_DecTime {
				get { return damage_Min_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_Min_DecTime.");
					SetProperty(ref damage_Min_DecTime, ref value, Damage_Min_DecTimeProperty);
				}
			}

			/// <summary>Small damage _ deceleration time [s]</summary>
			/// <remarks>
			/// Japanese short name: "小ダメージ_減速時間[s]", Google translated: "Small damage _ deceleration time [s]".
			/// Japanese description: "小ダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for small damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_S_DecTime", index: 16, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 400, unknown2: 1)]
			[DisplayName("Small damage _ deceleration time [s]")]
			[Description("Setting the deceleration time would be used for small damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_S_DecTime {
				get { return damage_S_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_S_DecTime.");
					SetProperty(ref damage_S_DecTime, ref value, Damage_S_DecTimeProperty);
				}
			}

			/// <summary>Medium Damage _ deceleration time [s]</summary>
			/// <remarks>
			/// Japanese short name: "中ダメージ_減速時間[s]", Google translated: "Medium Damage _ deceleration time [s]".
			/// Japanese description: "中ダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for the medium Damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_M_DecTime", index: 17, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 600, unknown2: 1)]
			[DisplayName("Medium Damage _ deceleration time [s]")]
			[Description("Setting the deceleration time would be used for the medium Damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_M_DecTime {
				get { return damage_M_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_M_DecTime.");
					SetProperty(ref damage_M_DecTime, ref value, Damage_M_DecTimeProperty);
				}
			}

			/// <summary>Large damage _ deceleration time [s]</summary>
			/// <remarks>
			/// Japanese short name: "大ダメージ_減速時間[s]", Google translated: "Large damage _ deceleration time [s]".
			/// Japanese description: "大ダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for a great damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_L_DecTime", index: 18, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 800, unknown2: 1)]
			[DisplayName("Large damage _ deceleration time [s]")]
			[Description("Setting the deceleration time would be used for a great damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_L_DecTime {
				get { return damage_L_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_L_DecTime.");
					SetProperty(ref damage_L_DecTime, ref value, Damage_L_DecTimeProperty);
				}
			}

			/// <summary>The deceleration time _ Futtobi small [s]</summary>
			/// <remarks>
			/// Japanese short name: "小吹っ飛び_減速時間[s]", Google translated: "The deceleration time _ Futtobi small [s]".
			/// Japanese description: "小吹っ飛びダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for damage animation Futtobi small".
			/// </remarks>
			[ParameterTableRowAttribute("damage_BlowS_DecTime", index: 19, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1000, unknown2: 1)]
			[DisplayName("The deceleration time _ Futtobi small [s]")]
			[Description("Setting the deceleration time would be used for damage animation Futtobi small")]
			[DefaultValue((Single)0)]
			public Single Damage_BlowS_DecTime {
				get { return damage_BlowS_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_BlowS_DecTime.");
					SetProperty(ref damage_BlowS_DecTime, ref value, Damage_BlowS_DecTimeProperty);
				}
			}

			/// <summary>The deceleration time _ Futtobi large [s]</summary>
			/// <remarks>
			/// Japanese short name: "大吹っ飛び_減速時間[s]", Google translated: "The deceleration time _ Futtobi large [s]".
			/// Japanese description: "大吹っ飛びダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for damage animation Futtobi large".
			/// </remarks>
			[ParameterTableRowAttribute("damage_BlowM_DecTime", index: 20, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1200, unknown2: 1)]
			[DisplayName("The deceleration time _ Futtobi large [s]")]
			[Description("Setting the deceleration time would be used for damage animation Futtobi large")]
			[DefaultValue((Single)0)]
			public Single Damage_BlowM_DecTime {
				get { return damage_BlowM_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_BlowM_DecTime.");
					SetProperty(ref damage_BlowM_DecTime, ref value, Damage_BlowM_DecTimeProperty);
				}
			}

			/// <summary>The deceleration time _ slammed [s]</summary>
			/// <remarks>
			/// Japanese short name: "叩きつけ_減速時間[s]", Google translated: "The deceleration time _ slammed [s]".
			/// Japanese description: "叩きつけダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for damage animation slam".
			/// </remarks>
			[ParameterTableRowAttribute("damage_Strike_DecTime", index: 21, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1400, unknown2: 1)]
			[DisplayName("The deceleration time _ slammed [s]")]
			[Description("Setting the deceleration time would be used for damage animation slam")]
			[DefaultValue((Single)0)]
			public Single Damage_Strike_DecTime {
				get { return damage_Strike_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_Strike_DecTime.");
					SetProperty(ref damage_Strike_DecTime, ref value, Damage_Strike_DecTimeProperty);
				}
			}

			/// <summary>The deceleration time launch _ [s]</summary>
			/// <remarks>
			/// Japanese short name: "打ち上げ_減速時間[s]", Google translated: "The deceleration time launch _ [s]".
			/// Japanese description: "打ち上げダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for damage animation Launch".
			/// </remarks>
			[ParameterTableRowAttribute("damage_Uppercut_DecTime", index: 22, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1600, unknown2: 1)]
			[DisplayName("The deceleration time launch _ [s]")]
			[Description("Setting the deceleration time would be used for damage animation Launch")]
			[DefaultValue((Single)0)]
			public Single Damage_Uppercut_DecTime {
				get { return damage_Uppercut_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_Uppercut_DecTime.");
					SetProperty(ref damage_Uppercut_DecTime, ref value, Damage_Uppercut_DecTimeProperty);
				}
			}

			/// <summary>Push _ deceleration time [s]</summary>
			/// <remarks>
			/// Japanese short name: "プッシュ_減速時間[s]", Google translated: "Push _ deceleration time [s]".
			/// Japanese description: "プッシュダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for push damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_Push_DecTime", index: 23, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1800, unknown2: 1)]
			[DisplayName("Push _ deceleration time [s]")]
			[Description("Setting the deceleration time would be used for push damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_Push_DecTime {
				get { return damage_Push_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_Push_DecTime.");
					SetProperty(ref damage_Push_DecTime, ref value, Damage_Push_DecTimeProperty);
				}
			}

			/// <summary>Breath _ deceleration time [s]</summary>
			/// <remarks>
			/// Japanese short name: "ブレス_減速時間[s]", Google translated: "Breath _ deceleration time [s]".
			/// Japanese description: "ブレスダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for a breath damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_Breath_DecTime", index: 24, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 1820, unknown2: 1)]
			[DisplayName("Breath _ deceleration time [s]")]
			[Description("Setting the deceleration time would be used for a breath damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_Breath_DecTime {
				get { return damage_Breath_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_Breath_DecTime.");
					SetProperty(ref damage_Breath_DecTime, ref value, Damage_Breath_DecTimeProperty);
				}
			}

			/// <summary>Headshot _ deceleration time [s]</summary>
			/// <remarks>
			/// Japanese short name: "ヘッドショット_減速時間[s]", Google translated: "Headshot _ deceleration time [s]".
			/// Japanese description: "ヘッドショットダメージアニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for a head shot damage Animation".
			/// </remarks>
			[ParameterTableRowAttribute("damage_HeadShot_DecTime", index: 25, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 2000, unknown2: 1)]
			[DisplayName("Headshot _ deceleration time [s]")]
			[Description("Setting the deceleration time would be used for a head shot damage Animation")]
			[DefaultValue((Single)0)]
			public Single Damage_HeadShot_DecTime {
				get { return damage_HeadShot_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Damage_HeadShot_DecTime.");
					SetProperty(ref damage_HeadShot_DecTime, ref value, Damage_HeadShot_DecTimeProperty);
				}
			}

			/// <summary>Deceleration time _ Small received guard [s]</summary>
			/// <remarks>
			/// Japanese short name: "ガード受け小_減速時間[s]", Google translated: "Deceleration time _ Small received guard [s]".
			/// Japanese description: "ガード受け小アニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for a small animated receiving guard".
			/// </remarks>
			[ParameterTableRowAttribute("guard_S_DecTime", index: 26, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 2200, unknown2: 1)]
			[DisplayName("Deceleration time _ Small received guard [s]")]
			[Description("Setting the deceleration time would be used for a small animated receiving guard")]
			[DefaultValue((Single)0)]
			public Single Guard_S_DecTime {
				get { return guard_S_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Guard_S_DecTime.");
					SetProperty(ref guard_S_DecTime, ref value, Guard_S_DecTimeProperty);
				}
			}

			/// <summary>Deceleration time _ University received guard [s]</summary>
			/// <remarks>
			/// Japanese short name: "ガード受け大_減速時間[s]", Google translated: "Deceleration time _ University received guard [s]".
			/// Japanese description: "ガード受け大アニメの時に使用される減速時間を設定", Google translated: "Setting the deceleration time would be used for a large animated receiving guard".
			/// </remarks>
			[ParameterTableRowAttribute("guard_L_DecTime", index: 27, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 2400, unknown2: 1)]
			[DisplayName("Deceleration time _ University received guard [s]")]
			[Description("Setting the deceleration time would be used for a large animated receiving guard")]
			[DefaultValue((Single)0)]
			public Single Guard_L_DecTime {
				get { return guard_L_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Guard_L_DecTime.");
					SetProperty(ref guard_L_DecTime, ref value, Guard_L_DecTimeProperty);
				}
			}

			/// <summary>Received extra large guard _ deceleration time [s]</summary>
			/// <remarks>
			/// Japanese short name: "ガード受け特大_減速時間[s]", Google translated: "Received extra large guard _ deceleration time [s]".
			/// Japanese description: "ガード受け特大アニメの時に使用される減速時間を設定", Google translated: "Set the deceleration time that is used when oversized anime received guard".
			/// </remarks>
			[ParameterTableRowAttribute("guard_LL_DecTime", index: 28, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 2420, unknown2: 1)]
			[DisplayName("Received extra large guard _ deceleration time [s]")]
			[Description("Set the deceleration time that is used when oversized anime received guard")]
			[DefaultValue((Single)0)]
			public Single Guard_LL_DecTime {
				get { return guard_LL_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for Guard_LL_DecTime.");
					SetProperty(ref guard_LL_DecTime, ref value, Guard_LL_DecTimeProperty);
				}
			}

			/// <summary>Is broken down guard _ deceleration time [s]</summary>
			/// <remarks>
			/// Japanese short name: "ガードくずされ_減速時間[s]", Google translated: "Is broken down guard _ deceleration time [s]".
			/// Japanese description: "ガードくずされアニメの時に仕様される減速時間を設定", Google translated: "Setting the deceleration time that is the specification at the time of animation to be deformed Guard".
			/// </remarks>
			[ParameterTableRowAttribute("guardBrake_DecTime", index: 29, minimum: 0, maximum: 9.99, step: 0.01, sortOrder: 2600, unknown2: 1)]
			[DisplayName("Is broken down guard _ deceleration time [s]")]
			[Description("Setting the deceleration time that is the specification at the time of animation to be deformed Guard")]
			[DefaultValue((Single)0)]
			public Single GuardBrake_DecTime {
				get { return guardBrake_DecTime; }
				set {
					if ((double)value < 0 || (double)value > 9.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9.99 for GuardBrake_DecTime.");
					SetProperty(ref guardBrake_DecTime, ref value, GuardBrake_DecTimeProperty);
				}
			}

			/// <summary>pading</summary>
			/// <remarks>
			/// Japanese short name: "pading", Google translated: "pading".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad[8]", index: 30, minimum: 0, maximum: 0, step: 0, sortOrder: 2601, unknown2: 0)]
			[DisplayName("pading")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal KnockBack(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				Damage_Min_ContTime = reader.ReadSingle();
				Damage_S_ContTime = reader.ReadSingle();
				Damage_M_ContTime = reader.ReadSingle();
				Damage_L_ContTime = reader.ReadSingle();
				Damage_BlowS_ContTime = reader.ReadSingle();
				Damage_BlowM_ContTime = reader.ReadSingle();
				Damage_Strike_ContTime = reader.ReadSingle();
				Damage_Uppercut_ContTime = reader.ReadSingle();
				Damage_Push_ContTime = reader.ReadSingle();
				Damage_Breath_ContTime = reader.ReadSingle();
				Damage_HeadShot_ContTime = reader.ReadSingle();
				Guard_S_ContTime = reader.ReadSingle();
				Guard_L_ContTime = reader.ReadSingle();
				Guard_LL_ContTime = reader.ReadSingle();
				GuardBrake_ContTime = reader.ReadSingle();
				Damage_Min_DecTime = reader.ReadSingle();
				Damage_S_DecTime = reader.ReadSingle();
				Damage_M_DecTime = reader.ReadSingle();
				Damage_L_DecTime = reader.ReadSingle();
				Damage_BlowS_DecTime = reader.ReadSingle();
				Damage_BlowM_DecTime = reader.ReadSingle();
				Damage_Strike_DecTime = reader.ReadSingle();
				Damage_Uppercut_DecTime = reader.ReadSingle();
				Damage_Push_DecTime = reader.ReadSingle();
				Damage_Breath_DecTime = reader.ReadSingle();
				Damage_HeadShot_DecTime = reader.ReadSingle();
				Guard_S_DecTime = reader.ReadSingle();
				Guard_L_DecTime = reader.ReadSingle();
				Guard_LL_DecTime = reader.ReadSingle();
				GuardBrake_DecTime = reader.ReadSingle();
				Pad = reader.ReadBytes(8);
			}

			internal KnockBack(ParameterTable table, int index)
				: base(table, index) {
				Damage_Min_ContTime = (Single)0;
				Damage_S_ContTime = (Single)0;
				Damage_M_ContTime = (Single)0;
				Damage_L_ContTime = (Single)0;
				Damage_BlowS_ContTime = (Single)0;
				Damage_BlowM_ContTime = (Single)0;
				Damage_Strike_ContTime = (Single)0;
				Damage_Uppercut_ContTime = (Single)0;
				Damage_Push_ContTime = (Single)0;
				Damage_Breath_ContTime = (Single)0;
				Damage_HeadShot_ContTime = (Single)0;
				Guard_S_ContTime = (Single)0;
				Guard_L_ContTime = (Single)0;
				Guard_LL_ContTime = (Single)0;
				GuardBrake_ContTime = (Single)0;
				Damage_Min_DecTime = (Single)0;
				Damage_S_DecTime = (Single)0;
				Damage_M_DecTime = (Single)0;
				Damage_L_DecTime = (Single)0;
				Damage_BlowS_DecTime = (Single)0;
				Damage_BlowM_DecTime = (Single)0;
				Damage_Strike_DecTime = (Single)0;
				Damage_Uppercut_DecTime = (Single)0;
				Damage_Push_DecTime = (Single)0;
				Damage_Breath_DecTime = (Single)0;
				Damage_HeadShot_DecTime = (Single)0;
				Guard_S_DecTime = (Single)0;
				Guard_L_DecTime = (Single)0;
				Guard_LL_DecTime = (Single)0;
				GuardBrake_DecTime = (Single)0;
				Pad = new Byte[8];
			}

			/// <summary>Write the <see cref="KnockBack"/> row.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(Damage_Min_ContTime);
				writer.Write(Damage_S_ContTime);
				writer.Write(Damage_M_ContTime);
				writer.Write(Damage_L_ContTime);
				writer.Write(Damage_BlowS_ContTime);
				writer.Write(Damage_BlowM_ContTime);
				writer.Write(Damage_Strike_ContTime);
				writer.Write(Damage_Uppercut_ContTime);
				writer.Write(Damage_Push_ContTime);
				writer.Write(Damage_Breath_ContTime);
				writer.Write(Damage_HeadShot_ContTime);
				writer.Write(Guard_S_ContTime);
				writer.Write(Guard_L_ContTime);
				writer.Write(Guard_LL_ContTime);
				writer.Write(GuardBrake_ContTime);
				writer.Write(Damage_Min_DecTime);
				writer.Write(Damage_S_DecTime);
				writer.Write(Damage_M_DecTime);
				writer.Write(Damage_L_DecTime);
				writer.Write(Damage_BlowS_DecTime);
				writer.Write(Damage_BlowM_DecTime);
				writer.Write(Damage_Strike_DecTime);
				writer.Write(Damage_Uppercut_DecTime);
				writer.Write(Damage_Push_DecTime);
				writer.Write(Damage_Breath_DecTime);
				writer.Write(Damage_HeadShot_DecTime);
				writer.Write(Guard_S_DecTime);
				writer.Write(Guard_L_DecTime);
				writer.Write(Guard_LL_DecTime);
				writer.Write(GuardBrake_DecTime);
				writer.Write(Pad);
			}
		}
	}
}
