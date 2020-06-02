using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.Calamitas;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Tiles;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.AstralDesert;
using CalamityMod.Tiles.AstralSnow;
using CalamityMod.Tiles.Crags;
using CalamityMod.Tiles.Ores;
using CalamityMod.Tiles.SunkenSea;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace CalamityMod.World
{
	public class CalamityWorld : ModWorld
    {
        #region Vars
        public static int DoGSecondStageCountdown = 0;
        public static bool dragonScalesBought = false;
        private const int saveVersion = 0;
        public static int ArmoredDiggerSpawnCooldown = 0;

        //Boss Rush
        public static bool bossRushActive = false; //Whether Boss Rush is active or not
        public static bool deactivateStupidFuckingBullshit = false; //Force Boss Rush to inactive
        public static int bossRushStage = 0; //Boss Rush Stage
        public static int bossRushSpawnCountdown = 180; //Delay before another Boss Rush boss can spawn

        //Death Mode natural boss spawns
        public static int bossSpawnCountdown = 0; //Death Mode natural boss spawn countdown
        public static int bossType = 0; //Death Mode natural boss spawn type
		public static int deathBossSpawnCooldown = 0; //Cooldown between Death Mode natural boss spawns

        //Modes
        public static bool demonMode = false; //Spawn rate boost
        public static bool onionMode = false; //Extra accessory from Moon Lord
        public static bool revenge = false; //Revengeance Mode
        public static bool death = false; //Death Mode
        public static bool defiled = false; //Defiled Mode
        public static bool armageddon = false; //Armageddon Mode
        public static bool ironHeart = false; //Iron Heart Mode

		// New Temple Altar
		public static int newAltarX = 0;
		public static int newAltarY = 0;

        //Evil Islands
        public static int fehX = 0;
        public static int fehY = 0;

        //Brimstone Crag
        public static int fuhX = 0;
        public static int fuhY = 0;
        public static int calamityTiles = 0;

        //Abyss & Sulphur
        public static int numAbyssIslands = 0;
        public static int[] AbyssIslandX = new int[20];
        public static int[] AbyssIslandY = new int[20];
        public static int[] AbyssItemArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static int sulphurTiles = 0;
        public static int abyssTiles = 0;
        public static bool abyssSide = false;
        public static int abyssChasmBottom = 0;
        public static bool rainingAcid;
        public static int acidRainPoints = 0;
        public static int acidRainExtraDrawTime = 0;
        public static bool triedToSummonOldDuke = false;
        public static bool startAcidicDownpour = false;
        public static bool forcedRainAlready = false;
        public static bool forcedDownpourWithTear = false;
        public static int forceRainTimer = 0;
        public static float AcidRainCompletionRatio
        {
            get
            {
                return MathHelper.Clamp(acidRainPoints / (float)AcidRainEvent.NeededEnemyKills, 0f, 1f);
            }
        }

        //Astral
        public static int astralTiles = 0;

        //Sunken Sea
        public static int sunkenSeaTiles = 0;
        public static Rectangle SunkenSeaLocation = Rectangle.Empty;

        //Shrines
        public static int[] SChestX = new int[10];
        public static int[] SChestY = new int[10];
        public static bool roxShrinePlaced = false;

		//Spawned NPCs
        public static bool spawnedBandit = false;
        public static bool spawnedCirrus = false;

        #region Downed Bools
        public static bool downedBossAny = false; //Any boss
        public static bool downedDesertScourge = false;
        public static bool downedCrabulon = false;
        public static bool downedHiveMind = false;
        public static bool downedPerforator = false;
        public static bool downedSlimeGod = false;
        public static bool spawnedHardBoss = false; //Hardmode boss spawned
        public static bool downedCryogen = false;
        public static bool downedAquaticScourge = false;
        public static bool downedBrimstoneElemental = false;
        public static bool downedCalamitas = false;
        public static bool downedLeviathan = false;
        public static bool downedAstrageldon = false;
        public static bool downedStarGod = false;
        public static bool downedPlaguebringer = false;
        public static bool downedScavenger = false;
        public static bool downedBoomerDuke = false;
        public static bool downedGuardians = false;
        public static bool downedProvidence = false;
        public static bool downedSentinel1 = false; // Ceaseless Void
        public static bool downedSentinel2 = false; // Storm Weaver
        public static bool downedSentinel3 = false; // Signus, Envoy of the Devourer
        public static bool downedPolterghast = false;
        public static bool downedDoG = false;
        public static bool downedBumble = false;
        public static bool buffedEclipse = false;
        public static bool downedBuffedMothron = false;
        public static bool downedYharon = false;
        public static bool downedSCal = false;
        public static bool downedGSS = false;
        public static bool downedCLAM = false;
        public static bool downedBetsy = false; //Betsy

        public static bool downedEoCAcidRain = false;
        public static bool downedAquaticScourgeAcidRain = false;
        #endregion

        #endregion

        #region Initialize
        public override void Initialize()
        {
            if (CalamityConfig.Instance.NerfExpertPillars)
            {
                NPC.LunarShieldPowerExpert = 100;
            }
            CalamityGlobalNPC.holyBoss = -1;
            CalamityGlobalNPC.doughnutBoss = -1;
            CalamityGlobalNPC.voidBoss = -1;
            CalamityGlobalNPC.energyFlame = -1;
            CalamityGlobalNPC.hiveMind = -1;
            CalamityGlobalNPC.scavenger = -1;

			for (int i = 0; i < CalamityGlobalNPC.bobbitWormBottom.Length; i++)
				CalamityGlobalNPC.bobbitWormBottom[i] = -1;

            CalamityGlobalNPC.DoGHead = -1;
            CalamityGlobalNPC.SCal = -1;
            CalamityGlobalNPC.ghostBoss = -1;
            CalamityGlobalNPC.laserEye = -1;
            CalamityGlobalNPC.fireEye = -1;
            CalamityGlobalNPC.brimstoneElemental = -1;
			CalamityGlobalNPC.signus = -1;
            bossRushStage = 0;
            DoGSecondStageCountdown = 0;
            ArmoredDiggerSpawnCooldown = 0;
            bossRushActive = false;
            bossRushSpawnCountdown = 180;
            bossSpawnCountdown = 0;
			deathBossSpawnCooldown = 0;
            bossType = 0;
			newAltarX = 0;
			newAltarY = 0;
            abyssChasmBottom = 0;
            abyssSide = false;
			spawnedBandit = false;
			spawnedCirrus = false;
            downedDesertScourge = false;
            downedAquaticScourge = false;
            downedHiveMind = false;
            downedPerforator = false;
            downedSlimeGod = false;
            downedCryogen = false;
            downedBrimstoneElemental = false;
            downedCalamitas = false;
            downedLeviathan = false;
            downedDoG = false;
            downedPlaguebringer = false;
            downedScavenger = false;
            downedGuardians = false;
            downedProvidence = false;
            downedSentinel1 = false;
            downedSentinel2 = false;
            downedSentinel3 = false;
            downedYharon = false;
            buffedEclipse = false;
            downedSCal = false;
            downedCLAM = false;
            downedBumble = false;
            downedCrabulon = false;
            downedBetsy = false;
            downedBossAny = false;
            spawnedHardBoss = false;
            demonMode = false;
            onionMode = false;
            revenge = false;
            downedStarGod = false;
            downedAstrageldon = false;
            downedPolterghast = false;
            downedGSS = false;
            downedBuffedMothron = false;
            downedBoomerDuke = false;
            death = false;
            defiled = false;
            armageddon = false;
            ironHeart = false;
            dragonScalesBought = false;
            rainingAcid = false;
            downedEoCAcidRain = false;
            downedAquaticScourgeAcidRain = false;
			forceRainTimer = 0;
        }
        #endregion

        #region Save
        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedDesertScourge)
                downed.Add("desertScourge");
            if (downedAquaticScourge)
                downed.Add("aquaticScourge");
            if (downedHiveMind)
                downed.Add("hiveMind");
            if (downedPerforator)
                downed.Add("perforator");
            if (downedSlimeGod)
                downed.Add("slimeGod");
            if (downedCryogen)
                downed.Add("cryogen");
            if (downedBrimstoneElemental)
                downed.Add("brimstoneElemental");
            if (downedCalamitas)
                downed.Add("calamitas");
            if (downedLeviathan)
                downed.Add("leviathan");
            if (downedDoG)
                downed.Add("devourerOfGods");
            if (downedPlaguebringer)
                downed.Add("plaguebringerGoliath");
            if (downedGuardians)
                downed.Add("guardians");
            if (downedProvidence)
                downed.Add("providence");
            if (downedSentinel1)
                downed.Add("ceaselessVoid");
            if (downedSentinel2)
                downed.Add("stormWeaver");
            if (downedSentinel3)
                downed.Add("signus");
            if (downedYharon)
                downed.Add("yharon");
            if (buffedEclipse)
                downed.Add("eclipse");
            if (downedSCal)
                downed.Add("supremeCalamitas");
            if (downedBumble)
                downed.Add("bumblebirb");
            if (downedCrabulon)
                downed.Add("crabulon");
            if (downedBetsy)
                downed.Add("betsy");
            if (downedScavenger)
                downed.Add("scavenger");
            if (downedBossAny)
                downed.Add("anyBoss");
            if (demonMode)
                downed.Add("demonMode");
            if (onionMode)
                downed.Add("onionMode");
            if (revenge)
                downed.Add("revenge");
            if (downedStarGod)
                downed.Add("starGod");
            if (downedAstrageldon)
                downed.Add("astrageldon");
            if (spawnedHardBoss)
                downed.Add("hardBoss");
            if (downedPolterghast)
                downed.Add("polterghast");
            if (downedGSS)
                downed.Add("greatSandShark");
            if (downedBuffedMothron)
                downed.Add("moth");
            if (downedBoomerDuke)
                downed.Add("oldDuke");
            if (death)
                downed.Add("death");
            if (defiled)
                downed.Add("defiled");
            if (armageddon)
                downed.Add("armageddon");
            if (ironHeart)
                downed.Add("ironHeart");
            if (abyssSide)
                downed.Add("abyssSide");
            if (bossRushActive)
                downed.Add("bossRushActive");
            if (downedCLAM)
                downed.Add("clam");
            if (dragonScalesBought)
                downed.Add("scales");
            if (rainingAcid)
                downed.Add("acidRain");
            if (spawnedBandit)
                downed.Add("bandit");
            if (spawnedCirrus)
                downed.Add("drunkPrincess");
            if (downedEoCAcidRain)
                downed.Add("eocRain");
            if (downedAquaticScourgeAcidRain)
                downed.Add("hmRain");
            if (triedToSummonOldDuke)
                downed.Add("spawnedBoomer");
			if (startAcidicDownpour)
				downed.Add("startDownpour");
			if (forcedRainAlready)
				downed.Add("forcedRain");
            if (forcedDownpourWithTear)
                downed.Add("forcedTear");

            return new TagCompound
            {
                {
                    "downed", downed
                },
                {
                    "abyssChasmBottom", abyssChasmBottom
                },
                {
                    "acidRainPoints", acidRainPoints
                }
            };
        }
        #endregion

        #region Load
        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedDesertScourge = downed.Contains("desertScourge");
            downedAquaticScourge = downed.Contains("aquaticScourge");
            downedHiveMind = downed.Contains("hiveMind");
            downedPerforator = downed.Contains("perforator");
            downedSlimeGod = downed.Contains("slimeGod");
            downedCryogen = downed.Contains("cryogen");
            downedBrimstoneElemental = downed.Contains("brimstoneElemental");
            downedCalamitas = downed.Contains("calamitas");
            downedLeviathan = downed.Contains("leviathan");
            downedDoG = downed.Contains("devourerOfGods");
            downedPlaguebringer = downed.Contains("plaguebringerGoliath");
            downedGuardians = downed.Contains("guardians");
            downedProvidence = downed.Contains("providence");
            downedSentinel1 = downed.Contains("ceaselessVoid");
            downedSentinel2 = downed.Contains("stormWeaver");
            downedSentinel3 = downed.Contains("signus");
            downedYharon = downed.Contains("yharon");
            buffedEclipse = downed.Contains("eclipse");
            downedSCal = downed.Contains("supremeCalamitas");
            downedBumble = downed.Contains("bumblebirb");
            downedCrabulon = downed.Contains("crabulon");
            downedBetsy = downed.Contains("betsy");
            downedScavenger = downed.Contains("scavenger");
            downedBossAny = downed.Contains("anyBoss");
            demonMode = downed.Contains("demonMode");
            onionMode = downed.Contains("onionMode");
            revenge = downed.Contains("revenge");
            downedStarGod = downed.Contains("starGod");
            downedAstrageldon = downed.Contains("astrageldon");
            spawnedHardBoss = downed.Contains("hardBoss");
            downedPolterghast = downed.Contains("polterghast");
            downedGSS = downed.Contains("greatSandShark");
            downedBuffedMothron = downed.Contains("moth");
            downedBoomerDuke = downed.Contains("oldDuke");
            death = downed.Contains("death");
            defiled = downed.Contains("defiled");
            armageddon = downed.Contains("armageddon");
            ironHeart = downed.Contains("ironHeart");
            abyssSide = downed.Contains("abyssSide");
            bossRushActive = downed.Contains("bossRushActive");
            downedCLAM = downed.Contains("clam");
            dragonScalesBought = downed.Contains("scales");
            rainingAcid = downed.Contains("acidRain");
            spawnedBandit = downed.Contains("bandit");
            spawnedCirrus = downed.Contains("drunkPrincess");
            downedEoCAcidRain = downed.Contains("eocRain");
            downedAquaticScourgeAcidRain = downed.Contains("hmRain");
            triedToSummonOldDuke = downed.Contains("spawnedBoomer");
			startAcidicDownpour = downed.Contains("startDownpour");
			forcedRainAlready = downed.Contains("forcedRain");
            forcedDownpourWithTear = downed.Contains("forcedTear");

            abyssChasmBottom = tag.GetInt("abyssChasmBottom");
            acidRainPoints = tag.GetInt("acidRainPoints");
        }
        #endregion

        #region LoadLegacy
        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            abyssChasmBottom = reader.ReadInt32();
            acidRainPoints = reader.ReadInt32();

            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedDesertScourge = flags[0];
                downedHiveMind = flags[1];
                downedPerforator = flags[2];
                downedSlimeGod = flags[3];
                downedCryogen = flags[4];
                downedBrimstoneElemental = flags[5];
                downedCalamitas = flags[6];
                downedLeviathan = flags[7];

                BitsByte flags2 = reader.ReadByte();
                downedDoG = flags2[0];
                downedPlaguebringer = flags2[1];
                downedGuardians = flags2[2];
                downedProvidence = flags2[3];
                downedSentinel1 = flags2[4];
                downedSentinel2 = flags2[5];
                downedSentinel3 = flags2[6];
                downedYharon = flags2[7];

                // Explicitly discard the now-unused vanilla boss booleans
                BitsByte flags3 = reader.ReadByte();
                downedSCal = flags3[0];
                downedBumble = flags3[1];
                downedCrabulon = flags3[2];
                downedBetsy = flags3[3];
                downedScavenger = flags3[4];
                _ = flags3[5];
                _ = flags3[6];
                _ = flags3[7];

                BitsByte flags4 = reader.ReadByte();
                _ = flags4[0];
                _ = flags4[1];
                _ = flags4[2];
                _ = flags4[3];
                downedBossAny = flags4[4];
                demonMode = flags4[5];
                onionMode = flags4[6];
                revenge = flags4[7];

                BitsByte flags5 = reader.ReadByte();
                downedStarGod = flags5[0];
                spawnedBandit = flags5[1];
                spawnedCirrus = flags5[2];
                startAcidicDownpour = flags5[3];
                spawnedHardBoss = flags5[4];
                downedPolterghast = flags5[5];
                death = flags5[6];
                downedGSS = flags5[7];

                BitsByte flags6 = reader.ReadByte();
                abyssSide = flags6[0];
                downedAquaticScourge = flags6[1];
                downedAstrageldon = flags6[2];
                buffedEclipse = flags6[3];
                armageddon = flags6[4];
                defiled = flags6[5];
                downedBuffedMothron = flags6[6];
                ironHeart = flags6[7];

                BitsByte flags7 = reader.ReadByte();
                bossRushActive = flags7[0];
                downedBoomerDuke = flags7[1];
                downedCLAM = flags7[2];
                dragonScalesBought = flags7[3];
                rainingAcid = flags7[4];
                downedEoCAcidRain = flags7[5];
                downedAquaticScourgeAcidRain = flags7[6];
                triedToSummonOldDuke = flags7[7];

                BitsByte flags8 = reader.ReadByte();
                forcedRainAlready = flags8[0];
                forcedDownpourWithTear = flags8[1];
                _ = flags8[2];
                _ = flags8[3];
                _ = flags8[4];
                _ = flags8[5];
                _ = flags8[6];
                _ = flags8[7];
            }
            else
            {
                ModContent.GetInstance<CalamityMod>().Logger.Error("Unknown loadVersion: " + loadVersion);
            }
        }
        #endregion

        #region NetSend
        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedDesertScourge;
            flags[1] = downedHiveMind;
            flags[2] = downedPerforator;
            flags[3] = downedSlimeGod;
            flags[4] = downedCryogen;
            flags[5] = downedBrimstoneElemental;
            flags[6] = downedCalamitas;
            flags[7] = downedLeviathan;

            BitsByte flags2 = new BitsByte();
            flags2[0] = downedDoG;
            flags2[1] = downedPlaguebringer;
            flags2[2] = downedGuardians;
            flags2[3] = downedProvidence;
            flags2[4] = downedSentinel1;
            flags2[5] = downedSentinel2;
            flags2[6] = downedSentinel3;
            flags2[7] = downedYharon;

            // Don't write meaningful values for the now-unused vanilla boss booleans
            BitsByte flags3 = new BitsByte();
            flags3[0] = downedSCal;
            flags3[1] = downedBumble;
            flags3[2] = downedCrabulon;
            flags3[3] = downedBetsy;
            flags3[4] = downedScavenger;
            flags3[5] = false;
            flags3[6] = false;
            flags3[7] = false;

            BitsByte flags4 = new BitsByte();
            flags4[0] = false;
            flags4[1] = false;
            flags4[2] = false;
            flags4[3] = false;
            flags4[4] = downedBossAny;
            flags4[5] = demonMode;
            flags4[6] = onionMode;
            flags4[7] = revenge;

            BitsByte flags5 = new BitsByte();
            flags5[0] = downedStarGod;
            flags5[1] = spawnedBandit;
            flags5[2] = spawnedCirrus;
            flags5[3] = startAcidicDownpour;
            flags5[4] = spawnedHardBoss;
            flags5[5] = downedPolterghast;
            flags5[6] = death;
            flags5[7] = downedGSS;

            BitsByte flags6 = new BitsByte();
            flags6[0] = abyssSide;
            flags6[1] = downedAquaticScourge;
            flags6[2] = downedAstrageldon;
            flags6[3] = buffedEclipse;
            flags6[4] = armageddon;
            flags6[5] = defiled;
            flags6[6] = downedBuffedMothron;
            flags6[7] = ironHeart;

            BitsByte flags7 = new BitsByte();
            flags7[0] = bossRushActive;
            flags7[1] = downedBoomerDuke;
            flags7[2] = downedCLAM;
            flags7[3] = dragonScalesBought;
            flags7[4] = rainingAcid;
            flags7[5] = downedEoCAcidRain;
            flags7[6] = downedAquaticScourgeAcidRain;
            flags7[7] = triedToSummonOldDuke;

            BitsByte flags8 = new BitsByte();
            flags8[0] = forcedRainAlready;
            flags8[1] = forcedDownpourWithTear;
            flags8[2] = false;
            flags8[3] = false;
            flags8[4] = false;
            flags8[5] = false;
            flags8[6] = false;
            flags8[7] = false;

            writer.Write(flags);
            writer.Write(flags2);
            writer.Write(flags3);
            writer.Write(flags4);
            writer.Write(flags5);
            writer.Write(flags6);
            writer.Write(flags7);
            writer.Write(flags8);
            writer.Write(abyssChasmBottom);
            writer.Write(acidRainPoints);
        }
        #endregion

        #region NetReceive
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedDesertScourge = flags[0];
            downedHiveMind = flags[1];
            downedPerforator = flags[2];
            downedSlimeGod = flags[3];
            downedCryogen = flags[4];
            downedBrimstoneElemental = flags[5];
            downedCalamitas = flags[6];
            downedLeviathan = flags[7];

            BitsByte flags2 = reader.ReadByte();
            downedDoG = flags2[0];
            downedPlaguebringer = flags2[1];
            downedGuardians = flags2[2];
            downedProvidence = flags2[3];
            downedSentinel1 = flags2[4];
            downedSentinel2 = flags2[5];
            downedSentinel3 = flags2[6];
            downedYharon = flags2[7];

            // Explicitly discard the now-unused vanilla boss booleans
            BitsByte flags3 = reader.ReadByte();
            downedSCal = flags3[0];
            downedBumble = flags3[1];
            downedCrabulon = flags3[2];
            downedBetsy = flags3[3];
            downedScavenger = flags3[4];
            _ = flags3[5];
            _ = flags3[6];
            _ = flags3[7];

            BitsByte flags4 = reader.ReadByte();
            _ = flags4[0];
            _ = flags4[1];
            _ = flags4[2];
            _ = flags4[3];
            downedBossAny = flags4[4];
            demonMode = flags4[5];
            onionMode = flags4[6];
            revenge = flags4[7];

            BitsByte flags5 = reader.ReadByte();
            downedStarGod = flags5[0];
            spawnedBandit = flags5[1];
            spawnedCirrus = flags5[2];
            startAcidicDownpour = flags5[3];
            spawnedHardBoss = flags5[4];
            downedPolterghast = flags5[5];
            death = flags5[6];
            downedGSS = flags5[7];

            BitsByte flags6 = reader.ReadByte();
            abyssSide = flags6[0];
            downedAquaticScourge = flags6[1];
            downedAstrageldon = flags6[2];
            buffedEclipse = flags6[3];
            armageddon = flags6[4];
            defiled = flags6[5];
            downedBuffedMothron = flags6[6];
            ironHeart = flags6[7];

            BitsByte flags7 = reader.ReadByte();
            bossRushActive = flags7[0];
            downedBoomerDuke = flags7[1];
            downedCLAM = flags7[2];
            dragonScalesBought = flags7[3];
            rainingAcid = flags7[4];
            downedEoCAcidRain = flags7[5];
            downedAquaticScourgeAcidRain = flags7[6];
            triedToSummonOldDuke = flags7[7];

            BitsByte flags8 = reader.ReadByte();
            forcedRainAlready = flags8[0];
            forcedDownpourWithTear = flags8[1];
            _ = flags8[2];
            _ = flags8[3];
            _ = flags8[4];
            _ = flags8[5];
            _ = flags8[6];
            _ = flags8[7];

            abyssChasmBottom = reader.ReadInt32();
            acidRainPoints = reader.ReadInt32();
        }
        #endregion

        #region Tiles
        public override void ResetNearbyTileEffects()
        {
            calamityTiles = 0;
            astralTiles = 0;
            sunkenSeaTiles = 0;
            sulphurTiles = 0;
            abyssTiles = 0;
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            calamityTiles = tileCounts[ModContent.TileType<CharredOre>()] + tileCounts[ModContent.TileType<BrimstoneSlag>()];
            sunkenSeaTiles = tileCounts[ModContent.TileType<EutrophicSand>()] + tileCounts[ModContent.TileType<Navystone>()] + tileCounts[ModContent.TileType<SeaPrism>()];
            abyssTiles = tileCounts[ModContent.TileType<AbyssGravel>()] + tileCounts[ModContent.TileType<Voidstone>()];
            sulphurTiles = tileCounts[ModContent.TileType<SulphurousSand>()] + tileCounts[ModContent.TileType<SulphurousSandNoWater>()] + tileCounts[ModContent.TileType<SulphurousSandstone>()] + tileCounts[ModContent.TileType<HardenedSulphurousSandstone>()];

            #region Astral Stuff
            int astralDesertTiles = tileCounts[ModContent.TileType<AstralSand>()] + tileCounts[ModContent.TileType<AstralSandstone>()] + tileCounts[ModContent.TileType<HardenedAstralSand>()] + tileCounts[ModContent.TileType<AstralFossil>()];
            int astralSnowTiles = tileCounts[ModContent.TileType<AstralIce>()] + tileCounts[ModContent.TileType<AstralSnow>()];

            Main.sandTiles += astralDesertTiles;
            Main.snowTiles += astralSnowTiles;

            astralTiles = astralDesertTiles + astralSnowTiles + tileCounts[ModContent.TileType<AstralDirt>()] + tileCounts[ModContent.TileType<AstralStone>()] + tileCounts[ModContent.TileType<AstralGrass>()] + tileCounts[ModContent.TileType<AstralOre>()] + tileCounts[ModContent.TileType<AstralSilt>()] + tileCounts[ModContent.TileType<AstralClay>()];
            #endregion
        }
        #endregion

        #region PreWorldGen
        public override void PreWorldGen()
        {
            numAbyssIslands = 0;
            roxShrinePlaced = false;
        }
        #endregion

        #region ModifyWorldGenTasks
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("IceTomb", delegate (GenerationProgress progress)
                {
                    progress.Message = "Ice Tomb";
                    SmallBiomes.PlaceIceTomb();
                }));

                tasks.Insert(ShiniesIndex + 2, new PassLegacy("EvilIsland", delegate (GenerationProgress progress)
                {
                    progress.Message = "Evil Island";
                    SmallBiomes.PlaceEvilIsland();
                }));
            }

            int DungeonChestIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Dungeon"));
            if (DungeonChestIndex != -1)
            {
                tasks.Insert(DungeonChestIndex + 1, new PassLegacy("Calamity Mod: Biome Chests", WorldGenerationMethods.GenerateBiomeChests));
            }

            int WaterFromSandIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Remove Water From Sand"));
            if (WaterFromSandIndex != -1)
            {
                tasks.Insert(WaterFromSandIndex + 1, new PassLegacy("SunkenSea", delegate (GenerationProgress progress)
                {
                    progress.Message = "Making the world more wet";
                    SunkenSea.Place(new Point(WorldGen.UndergroundDesertLocation.Left, WorldGen.UndergroundDesertLocation.Bottom));
                }));
            }

			int JungleTempleIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Temple"));
			tasks[JungleTempleIndex] = new PassLegacy("Jungle Temple", delegate (GenerationProgress progress)
			{
				progress.Message = "Building the jungle temple (Calamity)";
				WorldGenerationMethods.NewJungleTemple();
			});

            int JungleTempleIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Temple"));
			tasks[JungleTempleIndex2] = new PassLegacy("Temple", delegate (GenerationProgress progress)
			{
				progress.Message = "Building the jungle temple (Calamity)";
				Main.tileSolid[162] = false;
				Main.tileSolid[226] = true;
				WorldGenerationMethods.NewJungleTemplePart2();
				Main.tileSolid[232] = false;
			});

			int LihzahrdAltarIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Lihzahrd Altars"));
			tasks[LihzahrdAltarIndex] = new PassLegacy("Lihzahrd Altars", delegate (GenerationProgress progress)
			{
				progress.Message = "Placing a Lihzahrd altar (Calamity)";
				WorldGenerationMethods.NewJungleTempleLihzahrdAltar();
			});

            int FinalIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            if (FinalIndex != -1)
            {
                //Not touching this yet because the Crags will be reworked in the future
                #region BrimstoneCrag
                tasks.Insert(FinalIndex + 1, new PassLegacy("BrimstoneCrag", delegate (GenerationProgress progress)
                {
                    progress.Message = "Brimstone Crag";

                    int x = Main.maxTilesX;

                    int xUnderworldGen = WorldGen.genRand.Next((int)((double)x * 0.1), (int)((double)x * 0.15));
                    int yUnderworldGen = Main.maxTilesY - 100;

                    fuhX = xUnderworldGen;
                    fuhY = yUnderworldGen;

                    WorldGenerationMethods.UnderworldIsland(xUnderworldGen, yUnderworldGen, 180, 201, 120, 136);
                    WorldGenerationMethods.UnderworldIsland(xUnderworldGen - 50, yUnderworldGen - 30, 100, 111, 60, 71);
                    WorldGenerationMethods.UnderworldIsland(xUnderworldGen + 50, yUnderworldGen - 30, 100, 111, 60, 71);

                    WorldGenerationMethods.ChasmGenerator(fuhX - 110, fuhY - 10, WorldGen.genRand.Next(150) + 150);
                    WorldGenerationMethods.ChasmGenerator(fuhX + 110, fuhY - 10, WorldGen.genRand.Next(150) + 150);

                    WorldGenerationMethods.UnderworldIsland(xUnderworldGen - 150, yUnderworldGen - 30, 60, 66, 35, 41);
                    WorldGenerationMethods.UnderworldIsland(xUnderworldGen + 150, yUnderworldGen - 30, 60, 66, 35, 41);
                    WorldGenerationMethods.UnderworldIsland(xUnderworldGen - 180, yUnderworldGen - 20, 60, 66, 35, 41);
                    WorldGenerationMethods.UnderworldIsland(xUnderworldGen + 180, yUnderworldGen - 20, 60, 66, 35, 41);

                    WorldGenerationMethods.UnderworldIslandHouse(fuhX, fuhY + 30, 1323);
                    WorldGenerationMethods.UnderworldIslandHouse(fuhX - 22, fuhY + 15, 1322);
                    WorldGenerationMethods.UnderworldIslandHouse(fuhX + 22, fuhY + 15, 535);
                    WorldGenerationMethods.UnderworldIslandHouse(fuhX - 50, fuhY - 30, 112);
                    WorldGenerationMethods.UnderworldIslandHouse(fuhX + 50, fuhY - 30, 906);
                    WorldGenerationMethods.UnderworldIslandHouse(fuhX - 150, fuhY - 30, 218);
                    WorldGenerationMethods.UnderworldIslandHouse(fuhX + 150, fuhY - 30, 3019);
                    WorldGenerationMethods.UnderworldIslandHouse(fuhX - 180, fuhY - 20, 274);
                    WorldGenerationMethods.UnderworldIslandHouse(fuhX + 180, fuhY - 20, 220);
                }));
                #endregion

                int SulphurIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
                if (SulphurIndex != -1)
                {
                    tasks.Insert(SulphurIndex + 1, new PassLegacy("Sulphur", delegate (GenerationProgress progress)
                    {
                        progress.Message = "Sulphur Sea";
                        Abyss.PlaceSulphurSea();
                    }));
                }

                tasks.Insert(FinalIndex + 2, new PassLegacy("SpecialShrines", delegate (GenerationProgress progress)
                {
                    progress.Message = "Special Shrines";
                    SmallBiomes.PlaceShrines();
                }));

                tasks.Insert(FinalIndex + 3, new PassLegacy("Abyss", delegate (GenerationProgress progress)
                {
                    progress.Message = "The Abyss";
                    Abyss.PlaceAbyss();
                }));

                tasks.Insert(FinalIndex + 4, new PassLegacy("Sulphur2", delegate (GenerationProgress progress)
                {
                    progress.Message = "Finishing Sulphur Sea";
                    Abyss.FinishGeneratingSulphurSea();
                }));

                tasks.Insert(FinalIndex + 5, new PassLegacy("IWannaRock", delegate (GenerationProgress progress)
                {
                    progress.Message = "I Wanna Rock";
                    WorldGenerationMethods.PlaceRoxShrine();
                }));
            }

            tasks.Add(new PassLegacy("Planetoid Test", WorldGenerationMethods.Planetoids));
        }

        // An Astral Meteor always falls at the beginning of Hardmode.
        public override void ModifyHardmodeTasks(List<GenPass> tasks)
        {
            // Yes, this internal identifier is misspelled in vanilla.
            int announceIndex = tasks.FindIndex(match => match.Name == "Hardmode Announcment");

            // Insert the Astral biome generation right before the final hardmode announcement.
            tasks.Insert(announceIndex, new PassLegacy("AstralMeteor", delegate (GenerationProgress progress)
            {
                WorldGenerationMethods.PlaceAstralMeteor();
            }));
        }
        #endregion

        #region PostUpdate
        public override void PostUpdate()
        {
            // Sunken Sea Location...duh
            SunkenSeaLocation = new Rectangle(WorldGen.UndergroundDesertLocation.Left, WorldGen.UndergroundDesertLocation.Bottom,
                        WorldGen.UndergroundDesertLocation.Width, WorldGen.UndergroundDesertLocation.Height / 2);

            // Player variable, always finds the closest player relative to the center of the map
            int closestPlayer = Player.FindClosest(new Vector2(Main.maxTilesX / 2, (float)Main.worldSurface / 2f) * 16f, 0, 0);
			Player player = Main.player[closestPlayer];
			CalamityPlayer modPlayer = player.Calamity();

            // Force boss rush to off
            if (!deactivateStupidFuckingBullshit)
            {
                deactivateStupidFuckingBullshit = true;
                bossRushActive = false;
                CalamityMod.UpdateServerBoolean();
            }

            // Attempt to start the acid rain at the 4:29AM
			bool moreRain = !downedEoCAcidRain || (!downedAquaticScourgeAcidRain && downedAquaticScourge) || (!downedBoomerDuke && downedPolterghast);
            if (Main.time == 32399 && !Main.dayTime && Main.rand.NextBool(moreRain ? 3 : 300) && !Main.LocalPlayer.Calamity().noStupidNaturalARSpawns)
            {
                AcidRainEvent.TryStartEvent();
                CalamityMod.UpdateServerBoolean();
            }
			if (NPC.downedBoss1 && !downedEoCAcidRain && !forcedRainAlready)
			{
				for (int playerIndex = 0; playerIndex < Main.maxPlayers; playerIndex++)
				{
					if (Main.player[playerIndex].active)
					{
						if (Main.player[playerIndex].Calamity().ZoneSulphur)
						{
							forcedRainAlready = true;
							AcidRainEvent.TryStartEvent();
							CalamityMod.UpdateServerBoolean();
						}
					}
				}
			}
			if (forceRainTimer == 1)
			{
				AcidRainEvent.TryStartEvent();
				CalamityMod.UpdateServerBoolean();
			}
			if (forceRainTimer > 0)
				forceRainTimer--;

            if (rainingAcid)
            {
                if (!startAcidicDownpour)
                {
                    int sulphSeaWidth = Abyss.BiomeWidth;
                    for (int playerIndex = 0; playerIndex < Main.maxPlayers; playerIndex++)
                    {
                        // A variable named "player" already exists above, which retrieves the player who is closest to the map center.
                        Player player2 = Main.player[playerIndex];
                        if (player2.active)
                        {
                            // An artificial biome can be made, and therefore, the event could be started by an artificial biome.
                            // While fighting the event in an artificial biome is not bad, having it be started by a patch of Sulphurous Sand
                            // would definitely be strange.
                            // Because of this, this code is executed based on if the player is in the sea (based on tile count) AND position relative to the naturally generated sea.
                            if (((player2.Center.X <= (sulphSeaWidth + 60f) * 16f && abyssSide) ||
                                (player2.Center.X >= (Main.maxTilesX - (sulphSeaWidth + 60f)) * 16f && !abyssSide)) &&
                                player2.Calamity().ZoneSulphur)
                            {
                                startAcidicDownpour = true;
                                CalamityMod.UpdateServerBoolean();
                                break;
                            }
                        }
                    }
                }

                // Makes rain pour at its maximum intensity (but only after an idiot meanders into the Sulphurous Sea)
                // You'll never catch me, Fabs, Not when I shift into MAXIMUM OVERDRIVE!!
                if ((startAcidicDownpour || forcedDownpourWithTear) && !NPC.AnyNPCs(ModContent.NPCType<OldDuke>()))
                {
					Main.raining = true;
					Main.cloudBGActive = 1f;
					Main.numCloudsTemp = Main.cloudLimit;
					Main.numClouds = Main.numCloudsTemp;
					Main.windSpeedTemp = 0.72f;
					Main.windSpeedSet = Main.windSpeedTemp;
					Main.weatherCounter = 600;
					Main.maxRaining = 0.89f;
				}

                // Summon Old Duke tornado post-Polter as needed
                if (downedPolterghast && acidRainPoints == 2)
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<OldDuke>()) &&
                    CalamityUtils.CountProjectiles(ModContent.ProjectileType<OverlyDramaticDukeSummoner>()) <= 0)
                    {
                        if (triedToSummonOldDuke)
                        {
                            acidRainPoints = 0;
                            triedToSummonOldDuke = false;
                            AcidRainEvent.UpdateInvasion(false);
                        }
                        else
                        {
                            int playerClosestToAbyss = Player.FindClosest(new Vector2(abyssSide ? 0 : Main.maxTilesX * 16, (int)Main.worldSurface), 0, 0);
                            Player closestToAbyss = Main.player[playerClosestToAbyss];
                            if (Main.netMode != NetmodeID.MultiplayerClient && Math.Abs(closestToAbyss.Center.X - (abyssSide ? 0 : Main.maxTilesX * 16)) <= 12000f)
                            {
                                Projectile.NewProjectile(closestToAbyss.Center + Vector2.UnitY * 160f, Vector2.Zero,
                                    ModContent.ProjectileType<OverlyDramaticDukeSummoner>(), 120, 8f, Main.myPlayer);
                            }
                        }
                    }
                }
            }
			else
			{
				startAcidicDownpour = false;
			}

			// Lumenyl crystal, tenebris spread and sea prism crystal spawn rates
			int l = 0;
			float mult2 = 1.5E-05f * Main.worldRate;
			while (l < Main.maxTilesX * Main.maxTilesY * mult2)
			{
				int x = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
				int y = WorldGen.genRand.Next((int)Main.worldSurface - 1, Main.maxTilesY - 20);

				int y2 = y - 1;
				if (y2 < 10)
					y2 = 10;

				if (Main.tile[x, y] != null)
				{
					if (Main.tile[x, y].nactive())
					{
						if (Main.tile[x, y].liquid <= 32)
						{
							if (Main.tile[x, y].type == TileID.JungleGrass)
							{
								if (Main.tile[x, y2].liquid == 0)
								{
									// Plantera Bulbs pre-mech
									if (WorldGen.genRand.Next(1500) == 0)
									{
										if (Main.hardMode && (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3))
										{
											bool placeBulb = true;
											int minDistanceFromOtherBulbs = 150;
											for (int i = x - minDistanceFromOtherBulbs; i < x + minDistanceFromOtherBulbs; i += 2)
											{
												for (int j = y - minDistanceFromOtherBulbs; j < y + minDistanceFromOtherBulbs; j += 2)
												{
													if (i > 1 && i < Main.maxTilesX - 2 && j > 1 && j < Main.maxTilesY - 2 && Main.tile[i, j].active() && Main.tile[i, j].type == TileID.PlanteraBulb)
													{
														placeBulb = false;
														break;
													}
												}
											}

											if (placeBulb)
											{
												WorldGen.PlaceJunglePlant(x, y2, TileID.PlanteraBulb, 0, 0);
												WorldGen.SquareTileFrame(x, y2);
												WorldGen.SquareTileFrame(x + 2, y2);
												WorldGen.SquareTileFrame(x - 1, y2);
												if (Main.tile[x, y2].type == TileID.PlanteraBulb && Main.netMode == NetmodeID.Server)
												{
													NetMessage.SendTileSquare(-1, x, y2, 5);
												}
											}
										}
									}

									// Life Fruit pre-mech
									int random = Main.expertMode ? 30 : 40;
									if (WorldGen.genRand.Next(random) == 0)
									{
										if (Main.hardMode && !NPC.downedMechBossAny)
										{
											bool placeFruit = true;
											int minDistanceFromOtherFruit = Main.expertMode ? 50 : 60;
											for (int i = x - minDistanceFromOtherFruit; i < x + minDistanceFromOtherFruit; i += 2)
											{
												for (int j = y - minDistanceFromOtherFruit; j < y + minDistanceFromOtherFruit; j += 2)
												{
													if (i > 1 && i < Main.maxTilesX - 2 && j > 1 && j < Main.maxTilesY - 2 && Main.tile[i, j].active() && Main.tile[i, j].type == TileID.LifeFruit)
													{
														placeFruit = false;
														break;
													}
												}
											}

											if (placeFruit)
											{
												WorldGen.PlaceJunglePlant(x, y2, TileID.LifeFruit, WorldGen.genRand.Next(3), 0);
												WorldGen.SquareTileFrame(x, y2);
												WorldGen.SquareTileFrame(x + 1, y2 + 1);
												if (Main.tile[x, y2].type == TileID.LifeFruit && Main.netMode == NetmodeID.Server)
												{
													NetMessage.SendTileSquare(-1, x, y2, 4);
												}
											}
										}
									}
								}
							}
						}

						int tileType = Main.tile[x, y].type;
						bool tenebris = tileType == ModContent.TileType<Tenebris>() && downedCalamitas;

						if (CalamityGlobalTile.GrowthTiles.Contains(tileType) || tenebris)
						{
							int growthChance = tenebris ? 4 : 2;
							if (tileType == ModContent.TileType<Navystone>())
								growthChance *= 5;

							if (Main.rand.NextBool(growthChance))
							{
								switch (WorldGen.genRand.Next(4))
								{
									case 0:
										x++;
										break;
									case 1:
										x--;
										break;
									case 2:
										y++;
										break;
									case 3:
										y--;
										break;
									default:
										break;
								}

								if (Main.tile[x, y] != null)
								{
									Tile tile = Main.tile[x, y];
									bool growTile = tenebris ? (tile.active() && tile.type == ModContent.TileType<PlantyMush>()) : (!tile.active() && tile.liquid >= 128);
									bool meetsAdditionalGrowConditions = tile.slope() == 0 && !tile.halfBrick() && !tile.lava();
									if (growTile && meetsAdditionalGrowConditions)
									{
										int tileType2 = ModContent.TileType<SeaPrismCrystals>();

										if (tileType == ModContent.TileType<Voidstone>())
											tileType2 = ModContent.TileType<LumenylCrystals>();

										if (CanPlaceBasedOnProximity(x, y, tileType2) || tenebris)
										{
											tile.type = tenebris ? (ushort)tileType : (ushort)tileType2;

											if (!tenebris)
											{
												tile.active(true);
												if (Main.tile[x, y + 1].active() && Main.tileSolid[Main.tile[x, y + 1].type] && Main.tile[x, y + 1].slope() == 0 && !Main.tile[x, y + 1].halfBrick())
												{
													tile.frameY = 0;
												}
												else if (Main.tile[x, y - 1].active() && Main.tileSolid[Main.tile[x, y - 1].type] && Main.tile[x, y - 1].slope() == 0 && !Main.tile[x, y - 1].halfBrick())
												{
													tile.frameY = 18;
												}
												else if (Main.tile[x + 1, y].active() && Main.tileSolid[Main.tile[x + 1, y].type] && Main.tile[x + 1, y].slope() == 0 && !Main.tile[x + 1, y].halfBrick())
												{
													tile.frameY = 36;
												}
												else if (Main.tile[x - 1, y].active() && Main.tileSolid[Main.tile[x - 1, y].type] && Main.tile[x - 1, y].slope() == 0 && !Main.tile[x - 1, y].halfBrick())
												{
													tile.frameY = 54;
												}
												tile.frameX = (short)(WorldGen.genRand.Next(18) * 18);
											}

											WorldGen.SquareTileFrame(x, y, true);

											if (Main.netMode == 2)
												NetMessage.SendTileSquare(-1, x, y, 1, TileChangeType.None);
										}
									}
								}
							}
						}
					}
				}
				l++;
			}

			// Boss Rush shit
			if (bossRushActive)
            {
                // Prevent Moon Lord from spawning naturally
                if (NPC.MoonLordCountdown > 0)
                {
                    NPC.MoonLordCountdown = 0;
                }

                // Do boss rush countdown and shit if no boss is alive
                if (!CalamityPlayer.areThereAnyDamnBosses)
                {
                    // Stage text
                    if (bossRushSpawnCountdown > 0)
                    {
                        bossRushSpawnCountdown--;
                        if (bossRushSpawnCountdown == 180)
                        {
                            // After Fishron is dead
                            if (bossRushStage == 28)
                            {
                                string key = "Mods.CalamityMod.BossRushTierThreeEndText2";
                                Color messageColor = Color.LightCoral;
                                if (Main.netMode == NetmodeID.SinglePlayer)
                                {
                                    Main.NewText(Language.GetTextValue(key), messageColor);
                                }
                                else if (Main.netMode == NetmodeID.Server)
                                {
                                    NetMessage.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                                }
                            }

                            // After Providence is dead
                            else if (bossRushStage == 36)
                            {
                                string key = "Mods.CalamityMod.BossRushTierFourEndText2";
                                Color messageColor = Color.LightCoral;
                                if (Main.netMode == NetmodeID.SinglePlayer)
                                {
                                    Main.NewText(Language.GetTextValue(key), messageColor);
                                }
                                else if (Main.netMode == NetmodeID.Server)
                                {
                                    NetMessage.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                                }
                            }
                        }
                    }

                    // Cooldown and boss spawn
                    if (bossRushSpawnCountdown <= 0)
                    {
                        // Cooldown before next boss spawns
                        bossRushSpawnCountdown = 60;

                        // Increase cooldown post-Fishron
                        if (bossRushStage >= 27)
                        {
                            bossRushSpawnCountdown += 300;
                        }

                        // Change cooldown based on stage
                        switch (bossRushStage)
                        {
                            // When Destroyer or Cultist dies, increase time to show text
                            case 9:
                            case 18:
                                bossRushSpawnCountdown = 300;
                                break;

                            // When Signus dies, increase time to give players a moment to get in a good spot for Ravager
                            case 25:
                                bossRushSpawnCountdown = 360;
                                break;

                            // When Calamitas Clone dies, increase time to give players a moment to relax
                            case 32:
                                bossRushSpawnCountdown = 420;
                                break;
                            default:
                                break;
                        }

                        // Post-Wall of Flesh teleport back to spawn
                        if (bossRushStage == 13)
                        {
                            for (int playerIndex = 0; playerIndex < Main.maxPlayers; playerIndex++)
                            {
                                if (Main.player[playerIndex].active)
                                {
                                    Player player2 = Main.player[playerIndex];
                                    player2.Spawn();
                                }
                            }
                        }

                        // Remove Providence debuff for next boss fight
                        else if (bossRushStage == 36)
                        {
                            for (int playerIndex = 0; playerIndex < Main.maxPlayers; playerIndex++)
                            {
                                if (Main.player[playerIndex].active)
                                {
                                    Player player2 = Main.player[playerIndex];
                                    if (player2.FindBuffIndex(ModContent.BuffType<ExtremeGravity>()) > -1)
                                    {
                                        player2.ClearBuff(ModContent.BuffType<ExtremeGravity>());
                                    }
                                }
                            }
                        }

                        // Spawn bosses
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Main.PlaySound(SoundID.Roar, player.position, 0);
                            switch (bossRushStage)
                            {
                                case 0:
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.QueenBee);
                                    break;
                                case 1:
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.BrainofCthulhu);
                                    break;
                                case 2:
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.KingSlime);
                                    break;
                                case 3:
                                    ChangeTime(false);
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.EyeofCthulhu);
                                    break;
                                case 4:
                                    ChangeTime(false);
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.SkeletronPrime);
                                    break;
                                case 5:
                                    ChangeTime(true);
                                    NPC.NewNPC((int)(player.position.X + (float)Main.rand.Next(-100, 101)),
                                        (int)(player.position.Y - 400f),
                                        NPCID.Golem, 0, 0f, 0f, 0f, 0f, 255);
                                    break;
                                case 6:
                                    ChangeTime(true);
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<ProfanedGuardianBoss>());
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<ProfanedGuardianBoss2>());
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<ProfanedGuardianBoss3>());
                                    break;
                                case 7:
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.EaterofWorldsHead);
                                    break;
                                case 8:
                                    ChangeTime(false);
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<AstrumAureus>());
                                    break;
                                case 9:
                                    ChangeTime(false);
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.TheDestroyer);
                                    break;
                                case 10:
                                    ChangeTime(false);
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.Spazmatism);
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.Retinazer);
                                    break;
                                case 11:
                                    ChangeTime(true);
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<Bumblefuck>());
                                    break;
                                case 12:
                                    NPC.SpawnWOF(player.position);
                                    break;
                                case 13:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<HiveMind>());
                                    break;
                                case 14:
                                    ChangeTime(false);
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.SkeletronHead);
                                    break;
                                case 15:
                                    ChangeTime(true);
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<StormWeaverHead>());
                                    break;
                                case 16:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<AquaticScourgeHead>());
                                    break;
                                case 17:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<DesertScourgeHead>());
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<DesertScourgeHeadSmall>());
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<DesertScourgeHeadSmall>());
                                    break;
                                case 18:
                                    int num1302 = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 400, NPCID.CultistBoss, 0, 0f, 0f, 0f, 0f, 255);
                                    Main.npc[num1302].direction = Main.npc[num1302].spriteDirection = Math.Sign(player.Center.X - (float)player.Center.X - 90f);
                                    break;
                                case 19:
                                    for (int doom = 0; doom < Main.maxNPCs; doom++)
                                    {
                                        bool isPillar = Main.npc[doom].type == NPCID.LunarTowerStardust || Main.npc[doom].type == NPCID.LunarTowerVortex ||
                                                        Main.npc[doom].type == NPCID.LunarTowerNebula || Main.npc[doom].type == NPCID.LunarTowerSolar;
                                        if (Main.npc[doom].active && isPillar)
                                        {
                                            Main.npc[doom].active = false;
                                            Main.npc[doom].netUpdate = true;
                                        }
                                    }
                                    NPC.NewNPC((int)(player.position.X + (float)Main.rand.Next(-100, 101)), (int)(player.position.Y - 400f), ModContent.NPCType<CrabulonIdle>(), 0, 0f, 0f, 0f, 0f, 255);
                                    break;
                                case 20:
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.Plantera);
                                    break;
                                case 21:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<CeaselessVoid>());
                                    break;
                                case 22:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<PerforatorHive>());
                                    break;
                                case 23:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<Cryogen>());
                                    break;
                                case 24:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<BrimstoneElemental>());
                                    break;
                                case 25:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<Signus>());
                                    break;
                                case 26:
                                    NPC.NewNPC((int)(player.position.X + (float)Main.rand.Next(-100, 101)), (int)(player.position.Y - 400f), ModContent.NPCType<RavagerBody>(), 0, 0f, 0f, 0f, 0f, 255);
                                    break;
                                case 27:
                                    NPC.NewNPC((int)(player.position.X + (float)Main.rand.Next(-100, 101)), (int)(player.position.Y - 400f), NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, 255);
                                    break;
                                case 28:
                                    NPC.SpawnOnPlayer(closestPlayer, NPCID.MoonLordCore);
                                    break;
                                case 29:
                                    ChangeTime(false);
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<AstrumDeusHeadSpectral>());
                                    break;
                                case 30:
                                    ChangeTime(true);
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<Polterghast>());
                                    break;
                                case 31:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<PlaguebringerGoliath>());
                                    break;
                                case 32:
                                    ChangeTime(false);
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<Calamitas>());
                                    break;
                                case 33:
                                    ChangeTime(true);
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<Siren>());
                                    break;
                                case 34:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<SlimeGod>());
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<SlimeGodRun>());
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<SlimeGodCore>());
                                    break;
                                case 35:
                                    ChangeTime(true);
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<Providence>());
                                    break;
                                case 36:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<SupremeCalamitas>());
                                    break;
                                case 37:
                                    ChangeTime(true);
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<Yharon>());
                                    break;
                                case 38:
                                    NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<DevourerofGodsHeadS>());
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                bossRushSpawnCountdown = 180;
                if (bossRushStage != 0)
                {
                    bossRushStage = 0;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var netMessage = mod.GetPacket();
                        netMessage.Write((byte)CalamityModMessageType.BossRushStage);
                        netMessage.Write(bossRushStage);
                        netMessage.Send();
                    }
                }
            }

            if (DoGSecondStageCountdown > 0)
            {
                DoGSecondStageCountdown--;
                if (Main.netMode == NetmodeID.Server)
                {
                    var netMessage = mod.GetPacket();
                    netMessage.Write((byte)CalamityModMessageType.DoGCountdownSync);
                    netMessage.Write(DoGSecondStageCountdown);
                    netMessage.Send();
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (DoGSecondStageCountdown == 21540)
                    {
                        NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<CeaselessVoid>());
                    }
                    if (DoGSecondStageCountdown == 14340)
                    {
                        NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<StormWeaverHead>());
                    }
                    if (DoGSecondStageCountdown == 7140)
                    {
                        NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<Signus>());
                    }
                    if (DoGSecondStageCountdown <= 60)
                    {
                        if (!NPC.AnyNPCs(ModContent.NPCType<DevourerofGodsHeadS>()))
                        {
                            NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<DevourerofGodsHeadS>());
                            string key = "Mods.CalamityMod.EdgyBossText10";
                            Color messageColor = Color.Cyan;
                            if (Main.netMode == NetmodeID.SinglePlayer)
                            {
                                Main.NewText(Language.GetTextValue(key), messageColor);
                            }
                            else if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                            }
                        }
                    }
                }
            }

            if (player.ZoneDungeon && !NPC.downedBoss3 && !player.dead)
            {
                if (!NPC.AnyNPCs(NPCID.DungeonGuardian) && Main.netMode != NetmodeID.MultiplayerClient)
                    NPC.SpawnOnPlayer(closestPlayer, NPCID.DungeonGuardian); //your hell is as vast as my bonergrin, pray your life ends quickly
            }

            if (player.ZoneRockLayerHeight &&
                !player.ZoneUnderworldHeight &&
                !player.ZoneDungeon &&
                !player.ZoneJungle &&
                !modPlayer.ZoneSunkenSea &&
                !CalamityPlayer.areThereAnyDamnBosses)
            {
                if (NPC.downedPlantBoss &&
                    !modPlayer.ZoneAbyss &&
                    player.townNPCs < 3f)
                {
                    double spawnRate = 100000D;

                    if (revenge)
                        spawnRate *= 0.85D;

                    if (demonMode)
                        spawnRate *= 0.75D;

                    if (death && Main.bloodMoon)
                        spawnRate *= 0.2D;
                    if (modPlayer.zerg)
                        spawnRate *= 0.5D;
                    if (modPlayer.chaosCandle)
                        spawnRate *= 0.6D;
                    if (player.enemySpawns)
                        spawnRate *= 0.7D;
                    if (Main.waterCandles > 0)
                        spawnRate *= 0.8D;

                    if (modPlayer.bossZen || DoGSecondStageCountdown > 0)
                        spawnRate *= 50D;
                    if (modPlayer.zen || (CalamityConfig.Instance.DisableExpertTownSpawns && player.townNPCs > 1f && Main.expertMode))
                        spawnRate *= 2D;
                    if (modPlayer.tranquilityCandle)
                        spawnRate *= 1.67D;
                    if (player.calmed)
                        spawnRate *= 1.43D;
                    if (Main.peaceCandles > 0)
                        spawnRate *= 1.25D;

                    int chance = (int)spawnRate;
                    if (Main.rand.Next(chance) == 0)
                    {
                        if (!NPC.AnyNPCs(ModContent.NPCType<ArmoredDiggerHead>()) && Main.netMode != NetmodeID.MultiplayerClient && 
						ArmoredDiggerSpawnCooldown <= 0)
						{
                            NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<ArmoredDiggerHead>());
							ArmoredDiggerSpawnCooldown = 3600;
						}
                    }
                }
            }
			if (ArmoredDiggerSpawnCooldown > 0)
				ArmoredDiggerSpawnCooldown--;

            if (Main.dayTime && Main.hardMode)
            {
                if (player.townNPCs >= 2f)
                {
                    if (Main.rand.NextBool(2000))
                    {
                        int steamGril = NPC.FindFirstNPC(NPCID.Steampunker);
                        if (steamGril == -1 && Main.netMode != NetmodeID.MultiplayerClient)
                            NPC.SpawnOnPlayer(closestPlayer, NPCID.Steampunker); //Steampunker has awoken!
                    }
                }
            }

            if (modPlayer.ZoneAbyss)
            {
                if (player.chaosState)
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<EidolonWyrmHeadHuge>()) && Main.netMode != NetmodeID.MultiplayerClient)
                        NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<EidolonWyrmHeadHuge>());
                }
            }

            // DO NOT REMOVE -- this code is being kept for April Fools 2020
            if (Main.rand.NextBool(100000000) && DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
            {
                string key = "Mods.CalamityMod.AprilFools";
                Color messageColor = Color.Crimson;
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(Language.GetTextValue(key), messageColor);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                }
            }

            if (death && !CalamityPlayer.areThereAnyDamnBosses && !Main.snowMoon && !Main.pumpkinMoon && !DD2Event.Ongoing && player.statLifeMax2 >= 300) //does not occur while a boss is alive or during certain events
            {
                if (bossSpawnCountdown <= 0 && deathBossSpawnCooldown <= 0) // Check for countdown and cooldown being 0
                {
                    if (Main.rand.NextBool(50000))
                    {
						// Only set countdown and boss type if conditions are met
						// Night time bosses set message only before 11pm. Day time bosses only before 2pm.
                        if (!NPC.downedBoss1 && bossType == 0)
                            if (!Main.dayTime && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && Main.time < 12600)
                            {
                                BossText();
                                bossType = NPCID.EyeofCthulhu;
                                bossSpawnCountdown = 3600; // 1 minute
                            }

                        if (!NPC.downedBoss2 && bossType == 0)
                            if (player.ZoneCorrupt)
                            {
                                BossText();
                                bossType = NPCID.EaterofWorldsHead;
                                bossSpawnCountdown = 3600;
                            }

                        if (!NPC.downedBoss2 && bossType == 0)
                            if (player.ZoneCrimson)
                            {
                                BossText();
                                bossType = NPCID.BrainofCthulhu;
                                bossSpawnCountdown = 3600;
                            }

                        if (!NPC.downedQueenBee && bossType == 0)
                            if (player.ZoneJungle && (player.ZoneOverworldHeight || player.ZoneSkyHeight))
                            {
                                BossText();
                                bossType = NPCID.QueenBee;
                                bossSpawnCountdown = 3600;
                            }

                        if (!downedDesertScourge && bossType == 0)
                            if (player.ZoneDesert)
                            {
                                BossText();
                                bossType = ModContent.NPCType<DesertScourgeHead>();
                                bossSpawnCountdown = 3600;
                            }

                        if (!downedPerforator && bossType == 0)
                            if (player.ZoneCrimson)
                            {
                                BossText();
                                bossType = ModContent.NPCType<PerforatorHive>();
                                bossSpawnCountdown = 3600;
                            }

                        if (!downedHiveMind && bossType == 0)
                            if (player.ZoneCorrupt)
                            {
                                BossText();
                                bossType = ModContent.NPCType<HiveMind>();
                                bossSpawnCountdown = 3600;
                            }

                        if (!downedCrabulon && bossType == 0)
                            if (player.ZoneGlowshroom)
                            {
                                BossText();
                                bossType = ModContent.NPCType<CrabulonIdle>();
                                bossSpawnCountdown = 3600;
                            }

                        if (Main.hardMode)
                        {
                            if (!NPC.downedMechBoss1 && bossType == 0)
                                if (!Main.dayTime && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && Main.time < 12600)
                                {
                                    BossText();
                                    bossType = NPCID.TheDestroyer;
                                    bossSpawnCountdown = 3600;
                                }

                            if (!NPC.downedMechBoss2 && bossType == 0)
                                if (!Main.dayTime && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && Main.time < 12600)
                                {
                                    BossText();
                                    bossType = NPCID.Spazmatism;
                                    bossSpawnCountdown = 3600;
                                }

                            if (!NPC.downedMechBoss3 && bossType == 0)
                                if (!Main.dayTime && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && Main.time < 12600)
                                {
                                    BossText();
                                    bossType = NPCID.SkeletronPrime;
                                    bossSpawnCountdown = 3600;
                                }

                            if (!NPC.downedPlantBoss && bossType == 0)
                                if (player.ZoneJungle && !player.ZoneOverworldHeight && !player.ZoneSkyHeight)
                                {
                                    BossText();
                                    bossType = NPCID.Plantera;
                                    bossSpawnCountdown = 3600;
                                }

                            if (!NPC.downedFishron && bossType == 0)
                                if (player.ZoneBeach && !modPlayer.ZoneSulphur)
                                {
                                    BossText();
                                    bossType = NPCID.DukeFishron;
                                    bossSpawnCountdown = 3600;
                                }

                            if (!downedCryogen && bossType == 0)
                                if (player.ZoneSnow && player.ZoneOverworldHeight)
                                {
                                    BossText();
                                    bossType = ModContent.NPCType<Cryogen>();
                                    bossSpawnCountdown = 3600;
                                }

                            if (!downedCalamitas && bossType == 0)
                                if (!Main.dayTime && player.ZoneOverworldHeight && Main.time < 12600)
                                {
                                    BossText();
                                    bossType = ModContent.NPCType<Calamitas>();
                                    bossSpawnCountdown = 3600;
                                }

                            if (!downedAstrageldon && bossType == 0)
                                if (modPlayer.ZoneAstral &&
                                    !Main.dayTime && player.ZoneOverworldHeight)
                                {
                                    BossText();
                                    bossType = ModContent.NPCType<AstrumAureus>();
                                    bossSpawnCountdown = 3600;
                                }

                            if (!downedPlaguebringer && bossType == 0)
                                if (player.ZoneJungle && NPC.downedGolemBoss && player.ZoneOverworldHeight)
                                {
                                    BossText();
                                    bossType = ModContent.NPCType<PlaguebringerGoliath>();
                                    bossSpawnCountdown = 3600;
                                }

                            if (NPC.downedMoonlord)
                            {
                                if (!downedGuardians && bossType == 0)
                                    if (Main.dayTime && (player.ZoneUnderworldHeight ||
                                        (player.ZoneHoly && player.ZoneOverworldHeight)) && Main.time < 34200) //only before 2pm
                                    {
                                        BossText();
                                        bossType = ModContent.NPCType<ProfanedGuardianBoss>();
                                        bossSpawnCountdown = 3600;
                                    }

                                if (!downedBumble && bossType == 0)
                                    if (player.ZoneJungle && player.ZoneOverworldHeight)
                                    {
                                        BossText();
                                        bossType = ModContent.NPCType<Bumblefuck>();
                                        bossSpawnCountdown = 3600;
                                    }
                            }
                        }
                        if (Main.netMode == NetmodeID.Server)
                        {
                            var netMessage = mod.GetPacket();
                            netMessage.Write((byte)CalamityModMessageType.BossSpawnCountdownSync);
                            netMessage.Write(bossSpawnCountdown);
                            netMessage.Send();
                            var netMessage2 = mod.GetPacket();
                            netMessage2.Write((byte)CalamityModMessageType.BossTypeSync);
                            netMessage2.Write(bossType);
                            netMessage2.Send();
                        }
                    }
                }
                else
                {
					if (bossSpawnCountdown > 0)
					{
						bossSpawnCountdown--;
						if (Main.netMode == NetmodeID.Server)
						{
							var netMessage = mod.GetPacket();
							netMessage.Write((byte)CalamityModMessageType.BossSpawnCountdownSync);
							netMessage.Write(bossSpawnCountdown);
							netMessage.Send();
						}
					}

                    if (bossSpawnCountdown <= 0 && deathBossSpawnCooldown <= 0) // Check both cooldowns again here to avoid infinite message possibilities
                    {
                        bool canSpawn = true;
                        switch (bossType)
                        {
                            case NPCID.EyeofCthulhu:
                                if (Main.dayTime || (!player.ZoneOverworldHeight && !player.ZoneSkyHeight) || Main.time > 16200)
                                    canSpawn = false;
                                break;
                            case NPCID.EaterofWorldsHead:
                                if (!player.ZoneCorrupt)
                                    canSpawn = false;
                                break;
                            case NPCID.BrainofCthulhu:
                                if (!player.ZoneCrimson)
                                    canSpawn = false;
                                break;
                            case NPCID.QueenBee:
                                if (!player.ZoneJungle || (!player.ZoneOverworldHeight && !player.ZoneSkyHeight))
                                    canSpawn = false;
                                break;
                            case NPCID.TheDestroyer:
                                if (Main.dayTime || (!player.ZoneOverworldHeight && !player.ZoneSkyHeight) || Main.time > 16200)
                                    canSpawn = false;
                                break;
                            case NPCID.Spazmatism:
                                if (Main.dayTime || (!player.ZoneOverworldHeight && !player.ZoneSkyHeight) || Main.time > 16200)
                                    canSpawn = false;
                                break;
                            case NPCID.SkeletronPrime:
                                if (Main.dayTime || (!player.ZoneOverworldHeight && !player.ZoneSkyHeight) || Main.time > 16200)
                                    canSpawn = false;
                                break;
                            case NPCID.Plantera:
                                if (!player.ZoneJungle || player.ZoneOverworldHeight || player.ZoneSkyHeight)
                                    canSpawn = false;
                                break;
                            case NPCID.DukeFishron:
                                if (!player.ZoneBeach)
                                    canSpawn = false;
                                break;
                        }

                        if (bossType == ModContent.NPCType<DesertScourgeHead>())
                        {
                            if (!player.ZoneDesert)
                                canSpawn = false;
                        }
                        else if (bossType == ModContent.NPCType<PerforatorHive>())
                        {
                            if (!player.ZoneCrimson)
                                canSpawn = false;
                        }
                        else if (bossType == ModContent.NPCType<HiveMind>())
                        {
                            if (!player.ZoneCorrupt)
                                canSpawn = false;
                        }
                        else if (bossType == ModContent.NPCType<CrabulonIdle>())
                        {
                            if (!player.ZoneGlowshroom)
                                canSpawn = false;
                        }
                        else if (bossType == ModContent.NPCType<Cryogen>())
                        {
                            if (!player.ZoneSnow || !player.ZoneOverworldHeight)
                                canSpawn = false;
                        }
                        else if (bossType == ModContent.NPCType<Calamitas>())
                        {
                            if (Main.dayTime || !player.ZoneOverworldHeight || Main.time > 16200)
                                canSpawn = false;
                        }
                        else if (bossType == ModContent.NPCType<AstrumAureus>())
                        {
                            if (!modPlayer.ZoneAstral ||
                                    Main.dayTime || !player.ZoneOverworldHeight)
                                canSpawn = false;
                        }
                        else if (bossType == ModContent.NPCType<PlaguebringerGoliath>())
                        {
                            if (!player.ZoneJungle || !player.ZoneOverworldHeight)
                                canSpawn = false;
                        }
                        else if (bossType == ModContent.NPCType<ProfanedGuardianBoss>())
                        {
                            if (!Main.dayTime || (!player.ZoneUnderworldHeight &&
                                        (!player.ZoneHoly || !player.ZoneOverworldHeight)) || Main.time > 37800)
                                canSpawn = false;
                        }
                        else if (bossType == ModContent.NPCType<Bumblefuck>())
                        {
                            if (!player.ZoneJungle || !player.ZoneOverworldHeight)
                                canSpawn = false;
                        }

                        if (canSpawn)
                        {
							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								if (bossType == NPCID.Spazmatism)
									NPC.SpawnOnPlayer(closestPlayer, NPCID.Retinazer);
								else if (bossType == ModContent.NPCType<ProfanedGuardianBoss>())
								{
									NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<ProfanedGuardianBoss2>());
									NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<ProfanedGuardianBoss3>());
								}
								else if (bossType == ModContent.NPCType<DesertScourgeHead>())
								{
									NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<DesertScourgeHeadSmall>());
									NPC.SpawnOnPlayer(closestPlayer, ModContent.NPCType<DesertScourgeHeadSmall>());
								}
								if (bossType == NPCID.DukeFishron)
								{
									NPC.NewNPC((int)player.Center.X - 300, (int)player.Center.Y - 300, bossType);
								}
								else
								{
									NPC.SpawnOnPlayer(closestPlayer, bossType);
								}
							}

							deathBossSpawnCooldown = 86400; // 24 minutes (1 full Terraria day)
							if (Main.netMode == NetmodeID.Server)
							{
								var netMessage = mod.GetPacket();
								netMessage.Write((byte)CalamityModMessageType.DeathBossSpawnCountdownSync);
								netMessage.Write(deathBossSpawnCooldown);
								netMessage.Send();
							}
						}

                        bossType = 0;
                        if (Main.netMode == NetmodeID.Server)
                        {
                            var netMessage = mod.GetPacket();
                            netMessage.Write((byte)CalamityModMessageType.BossTypeSync);
                            netMessage.Write(bossType);
                            netMessage.Send();
                        }
                    }

					// IMPORTANT! Decrement this cooldown AFTER everything else to avoid infinite possibilities
					if (deathBossSpawnCooldown > 0)
					{
						deathBossSpawnCooldown--;
						if (Main.netMode == NetmodeID.Server)
						{
							var netMessage = mod.GetPacket();
							netMessage.Write((byte)CalamityModMessageType.DeathBossSpawnCountdownSync);
							netMessage.Write(deathBossSpawnCooldown);
							netMessage.Send();
						}
					}
				}
            }

            if (!downedDesertScourge && Main.netMode != NetmodeID.MultiplayerClient && !Main.hardMode)
                CalamityUtils.StopSandstorm();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                CultistRitual.delay -= Main.dayRate * 10;
                if (CultistRitual.delay < 0)
                {
                    CultistRitual.delay = 0;
                }
                CultistRitual.recheck -= Main.dayRate * 10;
                if (CultistRitual.recheck < 0)
                {
                    CultistRitual.recheck = 0;
                }
            }
        }
		#endregion

		#region Check Placement Proximity
		private bool CanPlaceBasedOnProximity(int x, int y, int tileType)
		{
			if (tileType == ModContent.TileType<LumenylCrystals>() && !downedCalamitas)
				return false;

			int minDistanceFromOtherTiles = 6;
			int sameTilesNearby = 0;
			for (int i = x - minDistanceFromOtherTiles; i < x + minDistanceFromOtherTiles; i++)
			{
				for (int j = y - minDistanceFromOtherTiles; j < y + minDistanceFromOtherTiles; j++)
				{
					if (Main.tile[i, j].active() && Main.tile[i, j].type == tileType)
					{
						sameTilesNearby++;
						if (sameTilesNearby > 1)
							return false;
					}
				}
			}

			return true;
		}
		#endregion

		#region ChangeTime
		public static void ChangeTime(bool day)
        {
            Main.time = 0.0;
            Main.dayTime = day;
            CalamityMod.UpdateServerBoolean();
        }
        #endregion

        #region BossText
        public void BossText()
        {
            string key = "Mods.CalamityMod.BossSpawnText";
            Color messageColor = Color.Crimson;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(Language.GetTextValue(key), messageColor);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
            }
        }
        #endregion
    }
}
