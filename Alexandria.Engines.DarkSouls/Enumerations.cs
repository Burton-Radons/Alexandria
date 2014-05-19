using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	/// <summary></summary>
	/// <remarks>"ACCESSORY_CATEGORY" in Dark Souls.</remarks>
	public enum AccessoryCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"ACTION_PATTERN" in Dark Souls.</remarks>
	public enum ActionPattern : byte {
	}

	/// <summary></summary>
	/// <remarks>"PROTECTOR_CATEGORY" in Dark Souls.</remarks>
	public enum ArmorCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"ATK_PARAM_HIT_TYPE" in Dark Souls.</remarks>
	public enum AttackHitType : byte {
	}

	/// <summary></summary>
	/// <remarks>"ATK_PARAM_MAP_HIT" in Dark Souls.</remarks>
	public enum AttackMapHit : byte {
		/// <summary>Used for punching and kicking on ladders, falling, parrying.</summary>
		Common = 0,

		/// <summary>Throwing knives, molotov cocktails, arrows, bolts.</summary>
		Ranged = 1,
	}

	/// <summary>Physical attributes to by set for the attack.</summary>
	/// <remarks>"ATKPARAM_ATKATTR_TYPE" in Dark Souls.</remarks>
	public enum AttackAttackAttributes : byte {
	}


	/// <summary></summary>
	/// <remarks>"ATK_PARAM_HIT_SOURCE" in Dark Souls.</remarks>
	public enum AttackHitSource : byte {
	}

	/// <summary></summary>
	/// <remarks>"ATKPARAM_REP_DMGTYPE" in Dark Souls.</remarks>
	public enum ATKPARAM_REP_DMGTYPE : sbyte {
	}

	/// <summary></summary>
	/// <remarks>"ATK_PATAM_THROWFLAG_TYPE" in Dark Souls.</remarks>
	public enum AttackThrowFlag : byte {
	}
	
	/// <summary></summary>
	/// <remarks>"ATK_TYPE" in Dark Souls.</remarks>
	public enum AttackType : byte {
	}

	/// <summary></summary>
	/// <remarks>"ATK_SIZE" in Dark Souls.</remarks>
	public enum AttackSize : byte {
	}

	/// <summary></summary>
	/// <remarks>"BULLET_LAUNCH_CONDITION_TYPE" in Dark Souls.</remarks>
	public enum BulletLaunchConditionType : byte {
	}

	/// <summary></summary>
	/// <remarks>"BULLET_FOLLOW_TYPE" in Dark Souls.</remarks>
	public enum BulletFollowType : byte {
	}

	/// <summary></summary>
	/// <remarks>"BULLET_EMITTE_POS_TYPE" in Dark Souls.</remarks>
	public enum BulletEmitterPosition : byte {
	}

	/// <summary></summary>
	/// <remarks>"BULLET_ATTACH_EFFECT_TYPE" in Dark Souls.</remarks>
	public enum BulletAttachEffect : byte {
	}

	/// <summary></summary>
	/// <remarks>"ATKPARAM_SPATTR_TYPE" in Dark Souls.</remarks>
	public enum AttackParameterSpecialAttributes : byte {
	}

	/// <summary></summary>
	/// <remarks>"ATK_PARAM_PARTSDMGTYPE" in Dark Souls.</remarks>
	public enum AttackParameterPartDamageType : byte {
	}

	/// <summary></summary>
	/// <remarks>"BEHAVIOR_ATK_SIZE" in Dark Souls.</remarks>
	public enum BehaviorAttackSize : byte {
	}

	/// <summary></summary>
	/// <remarks>"BEHAVIOR_ATK_TYPE" in Dark Souls.</remarks>
	public enum BehaviorAttackType : byte {
		Slash = 0,

		/// <summary>Ladder punches and kicks.</summary>
		Pound = 1,

		/// <summary>Throwing knives, arrows, bolts, stabbing with daggers and swords.</summary>
		Pierce = 2,
	}

	/// <summary></summary>
	/// <remarks>"BEHAVIOR_CATEGORY" in Dark Souls.</remarks>
	public enum BehaviorCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"BEHAVIOR_REF_TYPE" in Dark Souls.</remarks>
	public enum BehaviorRefType : byte {
	}

	/// <summary></summary>
	/// <remarks>"CHARACTER_INIT_SEX" in Dark Souls.</remarks>
	public enum CharacterInitialSex : byte {
	}

	/// <summary></summary>
	/// <remarks>"ChrType" in Dark Souls.</remarks>
	public enum CharacterTyep : int {
	}

	/// <summary></summary>
	/// <remarks>"CHRINIT_VOW_TYPE" in Dark Souls.</remarks>
	public enum CharacterInitialVow : byte {
	}

	/// <summary></summary>
	/// <remarks>"DURABILITY_DIVERGENCE_CATEGORY" in Dark Souls.</remarks>
	public enum DurabilityDivergenceCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"ENEMY_BEHAVIOR_ID" in Dark Souls.</remarks>
	public enum EnemyBehaviorId : int {
	}

	/// <summary></summary>
	/// <remarks>"EQUIP_MODEL_CATEGORY" in Dark Souls.</remarks>
	public enum EquipModelCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"EQUIP_MODEL_GENDER" in Dark Souls.</remarks>
	public enum EquipModelGender : byte {
	}

	/// <summary></summary>
	/// <remarks>"FACE_PARAM_HAIRCOLOR_TYPE" in Dark Souls.</remarks>
	public enum FaceHairColor : byte {
	}

	/// <summary></summary>
	/// <remarks>"FACE_PARAM_HAIRSTYLE_TYPE" in Dark Souls.</remarks>
	public enum FaceHairStyle : byte {
	}

	/// <summary></summary>
	/// <remarks>"GUARDMOTION_CATEGORY" in Dark Souls.</remarks>
	public enum GuardMotionCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"HMP_FOOT_EFFECT_HEIGHT_TYPE" in Dark Souls.</remarks>
	public enum HitMaterialFootEffectHeight : byte {
	}

	/// <summary></summary>
	/// <remarks>"HMP_FOOT_EFFECT_DIR_TYPE" in Dark Souls.</remarks>
	public enum HitMaterialFootEffectDirection : byte {
	}

	/// <summary></summary>
	/// <remarks>"HMP_FLOOR_HEIGHT_TYPE" in Dark Souls.</remarks>
	public enum HitMaterialFloorHeight : byte {
	}

	/// <summary></summary>
	/// <remarks>"GOODS_CATEGORY" in Dark Souls.</remarks>
	public enum ItemCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"GOODS_TYPE" in Dark Souls.</remarks>
	public enum ItemType : byte {
		General = 0,

		/// <summary>Ember; also used for the Peculiar Doll.</summary>
		Ember = 1,

		Titanite = 2,

		Humanity = 4,

		Scroll = 5,
	}

	/// <summary></summary>
	/// <remarks>"GOODS_USE_ANIM" in Dark Souls.</remarks>
	public enum ItemUseAnimation : byte {
	}

	/// <summary></summary>
	/// <remarks>"GOODS_OPEN_MENU" in Dark Souls.</remarks>
	public enum ItemUseMenu : byte {
		/// <summary>Simply use the item; or there is no menu.</summary>
		None = 0,
	}

	public enum Language {
		English,
		French,
		German,
		Italian,
		Japanese,
		Korean,
		Polish,
		Russian,
		Spanish,
		Chinese,
	}

	/// <summary></summary>
	/// <remarks>"NPC_BURN_TYPE" in Dark Souls.</remarks>
	public enum NpcBurnType : byte {
	}

	/// <summary></summary>
	/// <remarks>"NPC_ITEMDROP_TYPE" in Dark Souls.</remarks>
	public enum NpcItemDropType : byte {
	}

	/// <summary></summary>
	/// <remarks>"NPC_DRAW_TYPE" in Dark Souls.</remarks>
	public enum NpcDrawType : byte {
	}

	/// <summary></summary>
	/// <remarks>"NPC_HITSTOP_TYPE" in Dark Souls.</remarks>
	public enum NpcHitStopType : byte {
	}

	/// <summary></summary>
	/// <remarks>"NPC_MOVE_TYPE" in Dark Souls.</remarks>
	public enum NpcMoveType : byte {
	}

	/// <summary></summary>
	/// <remarks>"NPC_SFX_SIZE" in Dark Souls.</remarks>
	public enum NpcSfxSize : byte {
	}

	/// <summary></summary>
	/// <remarks>"NPC_TEMA_TYPE" in Dark Souls.</remarks>
	public enum NpcTemaType : byte {
	}

	/// <summary></summary>
	/// <remarks>"NPC_TYPE" in Dark Souls.</remarks>
	public enum NpcType : byte {
	}

	/// <summary></summary>
	/// <remarks>"REPLACE_CATEGORY" in Dark Souls.</remarks>
	public enum ReplacementCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"ITEMLOT_ITEMCATEGORY" in Dark Souls.</remarks>
	public enum RewardItemCategory : int {
		None = -1,
		Weapon = 0,
		Armor = 0x10000000,
		Ring = 0x20000000,
		Item = 0x40000000,
	}

	/// <summary></summary>
	/// <remarks>"ITEMLOT_ENABLE_LUCK" in Dark Souls.</remarks>
	public enum RewardEnableLuck : ushort {
	}

	/// <summary></summary>
	/// <remarks>"ITEMLOT_CUMULATE_RESET" in Dark Souls.</remarks>
	public enum RewardCumulateReset : ushort {
	}

	/// <summary></summary>
	/// <remarks>"MAGIC_CATEGORY" in Dark Souls.</remarks>
	public enum MagicCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"MAGIC_MOTION_TYPE" in Dark Souls.</remarks>
	public enum MagicMotion : byte {
	}

	/// <summary></summary>
	/// <remarks>"NPC_THINK_GOAL_ACTION" in Dark Souls.</remarks>
	public enum NpcThoughtGoalAction : byte {
	}

	/// <summary></summary>
	/// <remarks>"NPC_THINK_REPLY_BEHAVIOR_TYPE" in Dark Souls.</remarks>
	public enum NpcThoughtReplyBehavior : byte {
	}

	/// <summary></summary>
	/// <remarks>"OBJACT_SP_QUALIFIED_TYPE" in Dark Souls.</remarks>
	public enum ObjectActionSpecialQualifiedType : byte {
	}

	/// <summary></summary>
	/// <remarks>"OBJACT_CHR_SORB_TYPE" in Dark Souls.</remarks>
	public enum ObjectActionCharacterSOrbType : byte {
	}

	/// <summary></summary>
	/// <remarks>"OBJACT_EVENT_KICK_TIMING" in Dark Souls.</remarks>
	public enum ObjectActionEventKickTiming : byte {
	}

	/// <summary></summary>
	/// <remarks>"SKELETON_PARAM_KNEE_AXIS_DIR" in Dark Souls.</remarks>
	public enum SkeletonKneeAxisDirection : byte {
	}

	/// <summary></summary>
	/// <remarks>"SP_EFFECT_TYPE" in Dark Souls.</remarks>
	public enum SpecialEffectType : byte {
	}

	/// <summary></summary>
	/// <remarks>"SP_EFFECT_VFX_EFFECT_TYPE" in Dark Souls.</remarks>
	public enum SpecialEffectVfxEffectType : byte {
	}

	/// <summary></summary>
	/// <remarks>"SP_EFFECT_VFX_SOUL_PARAM_TYPE" in Dark Souls.</remarks>
	public enum SpecialEffectVfxSoulParameterType : byte {
	}

	/// <summary></summary>
	/// <remarks>"SP_EFFECT_VFX_PLAYCATEGORY" in Dark Souls.</remarks>
	public enum SpecialEffectVfxPlayCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"SP_EFFECT_USELIMIT_CATEGORY" in Dark Souls.</remarks>
	public enum SpecialEffectUseLimitCategory : byte {
	}
	/// <summary></summary>
	/// <remarks>"SP_EFFECT_SPCATEGORY" in Dark Souls.</remarks>
	public enum SpecialEffectSpCategory : ushort {
	}

	/// <summary></summary>
	/// <remarks>"SP_EFFECT_SAVE_CATEGORY" in Dark Souls.</remarks>
	public enum SpecialEffectSaveCategory : sbyte {
	}

	/// <summary></summary>
	/// <remarks>"SP_EFE_WEP_CHANGE_PARAM" in Dark Souls.</remarks>
	public enum SpecialEffectWeaponChange : byte {
	}

	/// <summary></summary>
	/// <remarks>"SP_EFFECT_MOVE_TYPE" in Dark Souls.</remarks>
	public enum SpecialEffectMoveType : byte {
	}

	/// <summary></summary>
	/// <remarks>"SP_EFFECT_THROW_CONDITION_TYPE" in Dark Souls.</remarks>
	public enum SpecialEffectThrowCondition : byte {
	}

	/// <summary></summary>
	/// <remarks>"SHOP_LINEUP_SHOPTYPE" in Dark Souls.</remarks>
	public enum StoreInventoryType : byte {
	}

	/// <summary></summary>
	/// <remarks>"SHOP_LINEUP_EQUIPTYPE" in Dark Souls.</remarks>
	public enum StoreInventoryEquipmentType : byte {
		Weapon = 0,
		Armor = 1,
		Ring = 2,
		Item = 3,
		Spell = 4,
	}

	/// <summary></summary>
	/// <remarks>"THROW_PAD_TYPE" in Dark Souls.</remarks>
	public enum ThrowPadType : byte {
	}

	/// <summary></summary>
	/// <remarks>"THROW_ENABLE_STATE" in Dark Souls.</remarks>
	public enum ThrowEnableState : byte {
	}

	/// <summary></summary>
	/// <remarks>"THROW_TYPE" in Dark Souls.</remarks>
	public enum ThrowType : byte {
	}

	/// <summary></summary>
	/// <remarks>"THROW_DMY_CHR_DIR_TYPE" in Dark Souls.</remarks>
	public enum ThrowDmyCharacterDirectionType : byte {
	}

	/// <summary></summary>
	/// <remarks>"WEP_BASE_CHANGE_CATEGORY" in Dark Souls.</remarks>
	public enum WeaponBaseChangeCategory : byte {
	}

	/// <summary></summary>
	/// <remarks>"WEAPON_CATEGORY" in Dark Souls.</remarks>
	public enum WeaponCategory : byte {
		Dagger = 0,

		/// <summary>Short swords, long swords, whips.</summary>
		Sword = 1,

		/// <summary>Rapiers, mail breakers, estocs.</summary>
		Rapier = 2,

		/// <summary>Scimitars, falchions, shoretels.</summary>
		Scimitar = 3,

		/// <summary>Hand axes, battle axes, cleavers.</summary>
		Axe = 4,

		/// <summary>Clubs, maces.</summary>
		Club = 5,

		/// <summary>Spears, tridents, partisans.</summary>
		Spear = 6,

		/// <summary>Halberds, Lucernes, Scythes</summary>
		Halberd = 7,

		/// <summary>Pyromancy flames, catalysts.</summary>
		Magic = 8,

		/// <summary>Fists, caestus, claws, dark hand.</summary>
		Fist = 9,

		Bow = 10,

		Crossbow = 11,

		Shield = 12,

		Arrow = 13,

		Bolt = 14,
	}

	/// <summary></summary>
	/// <remarks>"WEP_CORRECT_TYPE" in Dark Souls.</remarks>
	public enum WeaponCorrectType : byte {
	}

	/// <summary></summary>
	/// <remarks>"WEP_MATERIAL_ATK" in Dark Souls.</remarks>
	public enum WeaponMaterialAttack : byte {
	}

	/// <summary></summary>
	/// <remarks>"WEP_MATERIAL_DEF" in Dark Souls.</remarks>
	public enum WeaponMaterialDefend : byte {
	}

	/// <summary></summary>
	/// <remarks>"WEP_MATERIAL_DEF_SFX" in Dark Souls.</remarks>
	public enum WeaponMaterialDefendSound : byte {
	}

	/// <summary>This value times 100,000 indexes "AtkParam_Pc.param".</summary>
	/// <remarks>"WEPMOTION_CATEGORY" in Dark Souls.</remarks>
	public enum WeaponMotionCategory : byte {
		Dagger = 20,
		Sword = 23,

		/// <summary>Claymores, Man-serpent Greatswords, Flamberges, Black Knight Sword, Greatsword of Artorias, Server, Murakumo.</summary>
		Claymore = 25,

		/// <summary>Zweihander, Greatsword, Machete, Black Knight Greatsword.</summary>
		Zweihander = 26,

		/// <summary>Rapier, Mail Breaker, Estoc.</summary>
		Rapier = 27,

		/// <summary>Scimitar, Falchion, Shotel.</summary>
		Scimitar = 28,

		/// <summary>Uchigatana, Iaito.</summary>
		Uchigatana = 29,

		/// <summary>Hand axe, battle axe, butcher knife.</summary>
		Axe = 30,

		/// <summary>Crescent axe, great axe.</summary>
		GreatAxe = 32,

		/// <summary>Club, mace, morning star, hammer.</summary>
		Club = 33,

		/// <summary>Great club, grant, demon's great hammer, Smough's hammer.</summary>
		GreatClub = 35,

		/// <summary>Spear.</summary>
		Spear = 36,

		/// <summary>Fists, caestus, claw.</summary>
		Fists = 42,
	}
}
