﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace CalamityMod
{
	[Label("Configs")]
	public class Configs : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;
		public override void OnLoaded() => CalamityMod.CalamityConfig = this;

		[Header("Expert Changes")]

		[DefaultValue(true)]
		[Label("Expert Debuff Duration Reduction")]
		[Tooltip("Debuffs in Expert Mode are no longer doubled")]
		public bool ExpertDebuffDurationReduction { get; set; }

		[DefaultValue(true)]
		[Label("Expert Chilled Water Removal")]
		[Tooltip("Water in the Ice and Snow biome in Expert Mode rapidly drain your breath rather than slowing your movement")]
		public bool ExpertChilledWaterRemoval { get; set; }

		[DefaultValue(true)]
		[Label("Expert Pillar Enemy Kill Count Reduction")]
		[Tooltip("Only 100 enemies are required to destroy the shields of the Celestial Pillars in Expert Mode")]
		public bool ExpertPillarEnemyKillCountReduction { get; set; }

		[DefaultValue(false)]
		[Label("Expert Enemy Spawns in Villages Reduction")]
		[Tooltip("Enemy spawns are drastically reduced in the presence of town NPCs in Expert Mode")]
		public bool DisableExpertEnemySpawnsNearHouse { get; set; }

		[Header("Revengeance Changes")]

		[DefaultValue(true)]
		[Label("Adrenaline and Rage")]
		[Tooltip("Enables the Adrenaline and Rage mechanics")]
		public bool AdrenalineAndRage { get; set; }

		[DefaultValue(typeof(Vector2), "-900, -1820")]
		[Range(-1920f, 0f)]
		[Label("Rage Meter Position")]
		[Tooltip("Changes the position of the Rage Meter")]
		public Vector2 RageMeterPos { get; set; }

		[DefaultValue(typeof(Vector2), "-750, -1820")]
		[Range(-1920f, 0f)]
		[Label("Adrenaline Meter Position")]
		[Tooltip("Changes the position of the Adrenaline Meter")]
		public Vector2 AdrenalineMeterPos { get; set; }

		[DefaultValue(false)]
		[Label("Revengeance and Death Thorium Boss buff")]
		[Tooltip("Buffs the health of Thorium bosses if Revengeance or Death is enabled")]
		public bool RevengeanceAndDeathThoriumBossBuff { get; set; }

		[Header("Boss Rush Curses")]

		[DefaultValue(false)]
		[Label("Boss Rush Accessory Curse")]
		[Tooltip("Accessories are limited to a maximum of five while the Boss Rush is active")]
		public bool BossRushAccessoryCurse { get; set; }

		[DefaultValue(false)]
		[Label("Boss Rush Health Curse")]
		[Tooltip("Life regeneration is disabled while the Boss Rush is active")]
		public bool BossRushHealthCurse { get; set; }

		[DefaultValue(false)]
		[Label("Boss Rush Dash Curse")]
		[Tooltip("Dashes are disabled while the Boss Rush is active")]
		public bool BossRushDashCurse { get; set; }

		[DefaultValue(false)]
		[Label("Boss Rush Xeroc Curse")]
		[Tooltip("All bosses are permanently enraged while the Boss Rush is active")]
		public bool BossRushXerocCurse { get; set; }

		[DefaultValue(false)]
		[Label("Boss Rush Immunity Frame Curse")]
		[Tooltip("Getting hit more than once in the span of five seconds will instantly kill you if the Boss Rush is active")]
		public bool BossRushImmunityFrameCurse { get; set; }

		[Header("Other")]

		[DefaultValue(true)]
		[Label("Proficiency")]
		[Tooltip("Enables the Proficiency stat that rewards the player based on the attack type they use")]
		public bool ProficiencyEnabled { get; set; }

		[DefaultValue(true)]
		[Label("Lethal Lava")]
		[Tooltip("Increases the severity of lava with a new debuff to punish those who stay in lava for too long")]
		public bool LethalLava { get; set; }

		[DefaultValue(false)]
		[Label("Mining Speed Boost")]
		[Tooltip("Boosts the player's mining speed by 75%")]
		public bool MiningSpeedBoost { get; set; }

		[Label("Boss Health Percentage Boost")]
		[Tooltip("Boosts the health of bosses to a maximum of +900% health")]
		[Range(0, 900)]
		[DefaultValue(0)]
		public int BossHealthPercentageBoost { get; set; }

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		{
			return true;
		}
	}
}