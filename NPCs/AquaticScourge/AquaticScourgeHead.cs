﻿using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Placeables;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Armor.Vanity;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using System;

namespace CalamityMod.NPCs.AquaticScourge
{
    [AutoloadBossHead]
    public class AquaticScourgeHead : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Scourge");
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 16f;
            NPC.GetNPCDamage();
            NPC.Calamity().canBreakPlayerDefense = true;
            NPC.width = 90;
            NPC.height = 90;
            NPC.defense = 10;
            NPC.DR_NERD(0.05f);
            NPC.aiStyle = -1;
            AIType = -1;
            NPC.LifeMaxNERB(77000, 92000, 1000000);
            double HPBoost = CalamityConfig.Instance.BossHealthBoost * 0.01;
            NPC.lifeMax += (int)(NPC.lifeMax * HPBoost);
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 40, 0, 0);
            NPC.behindTiles = true;
            NPC.chaseable = false;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;

            if (CalamityWorld.malice || BossRushEvent.BossRushActive)
                NPC.scale = 1.25f;
            else if (CalamityWorld.death)
                NPC.scale = 1.2f;
            else if (CalamityWorld.revenge)
                NPC.scale = 1.15f;
            else if (Main.expertMode)
                NPC.scale = 1.1f;

            NPC.Calamity().VulnerableToHeat = false;
            NPC.Calamity().VulnerableToSickness = false;
            NPC.Calamity().VulnerableToElectricity = true;
            NPC.Calamity().VulnerableToWater = false;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(NPC.chaseable);
            writer.Write(NPC.localAI[1]);
            writer.Write(NPC.localAI[2]);
            writer.Write(NPC.localAI[3]);
            for (int i = 0; i < 4; i++)
                writer.Write(NPC.Calamity().newAI[i]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.chaseable = reader.ReadBoolean();
            NPC.localAI[1] = reader.ReadSingle();
            NPC.localAI[2] = reader.ReadSingle();
            NPC.localAI[3] = reader.ReadSingle();
            for (int i = 0; i < 4; i++)
                NPC.Calamity().newAI[i] = reader.ReadSingle();
        }

        public override void AI()
        {
            if (NPC.justHit || NPC.life <= NPC.lifeMax * 0.999 || BossRushEvent.BossRushActive)
                Music = CalamityMod.Instance.GetMusicFromMusicMod("AquaticScourge") ?? MusicID.Boss2;

            CalamityAI.AquaticScourgeAI(NPC, Mod, true);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            Texture2D texture2D15 = TextureAssets.Npc[NPC.type].Value;
            Vector2 vector11 = new Vector2(TextureAssets.Npc[NPC.type].Value.Width / 2, TextureAssets.Npc[NPC.type].Value.Height / 2);

            Vector2 vector43 = NPC.Center - screenPos;
            vector43 -= new Vector2(texture2D15.Width, texture2D15.Height) * NPC.scale / 2f;
            vector43 += vector11 * NPC.scale + new Vector2(0f, NPC.gfxOffY);
            Color color = NPC.GetAlpha(drawColor);

            if (CalamityWorld.revenge || BossRushEvent.BossRushActive)
            {
                if (NPC.Calamity().newAI[3] > 480f)
                    color = Color.Lerp(color, Color.SandyBrown, MathHelper.Clamp((NPC.Calamity().newAI[3] - 480f) / 180f, 0f, 1f));
                else if (NPC.localAI[3] > 0f)
                    color = Color.Lerp(color, Color.SandyBrown, MathHelper.Clamp(NPC.localAI[3] / 90f, 0f, 1f));
            }

            spriteBatch.Draw(texture2D15, vector43, NPC.frame, color, NPC.rotation, vector11, NPC.scale, spriteEffects, 0f);

            return false;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            Rectangle targetHitbox = target.Hitbox;

            float dist1 = Vector2.Distance(NPC.Center, targetHitbox.TopLeft());
            float dist2 = Vector2.Distance(NPC.Center, targetHitbox.TopRight());
            float dist3 = Vector2.Distance(NPC.Center, targetHitbox.BottomLeft());
            float dist4 = Vector2.Distance(NPC.Center, targetHitbox.BottomRight());

            float minDist = dist1;
            if (dist2 < minDist)
                minDist = dist2;
            if (dist3 < minDist)
                minDist = dist3;
            if (dist4 < minDist)
                minDist = dist4;

            return minDist <= 50f;
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
                return NPC.Calamity().newAI[0] == 1f;

            return null;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.Calamity().disableNaturalScourgeSpawns)
                return 0f;

            if (spawnInfo.PlayerSafe)
                return 0f;

            if (spawnInfo.Player.Calamity().ZoneSulphur && spawnInfo.Water)
            {
                if (!NPC.AnyNPCs(ModContent.NPCType<AquaticScourgeHead>()))
                    return 0.01f;
            }

            return 0f;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<SulphurousSand>();
        }

        public override bool SpecialOnKill()
        {
            int closestSegmentID = DropHelper.FindClosestWormSegment(NPC,
                ModContent.NPCType<AquaticScourgeHead>(),
                ModContent.NPCType<AquaticScourgeBody>(),
                ModContent.NPCType<AquaticScourgeBodyAlt>(),
                ModContent.NPCType<AquaticScourgeTail>());
            NPC.position = Main.npc[closestSegmentID].position;
            return true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Boss bag
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AquaticScourgeBag>()));

            // Normal drops: Everything that would otherwise be in the bag
            var normalOnly = npcLoot.DefineNormalOnlyDropSet();
            {
                // Weapons
                int[] weapons = new int[] 
                {
                    ModContent.ItemType<SubmarineShocker>(),
                    ModContent.ItemType<Barinautical>(),
                    ModContent.ItemType<Downpour>(),
                    ModContent.ItemType<DeepseaStaff>(),
                    ModContent.ItemType<ScourgeoftheSeas>(),
                    ModContent.ItemType<CorrosiveSpine>()
                };
                normalOnly.Add(ItemDropRule.OneFromOptions(DropHelper.NormalWeaponDropRateInt, weapons));

                // Vanity
                normalOnly.Add(ModContent.ItemType<AquaticScourgeMask>(), 7);

                // Equipment
                normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<AquaticEmblem>()));
                normalOnly.Add(ModContent.ItemType<DeepDiver>(), 10);
                normalOnly.Add(ModContent.ItemType<SeasSearing>(), 10);

                // Fishing
                normalOnly.Add(ModContent.ItemType<BleachedAnglingKit>());
            }

            npcLoot.Add(DropHelper.PerPlayer(ItemID.GreaterHealingPotion, 1, 8, 14));
            npcLoot.Add(ModContent.ItemType<AquaticScourgeTrophy>(), 10);
            npcLoot.AddLore(() => !Main.expertMode, ModContent.ItemType<KnowledgeAquaticScourge>());
            npcLoot.AddLore(() => !Main.expertMode, ModContent.ItemType<KnowledgeSulphurSea>());            
        }

        public override void OnKill()
        {
            CalamityGlobalNPC.SetNewBossJustDowned(NPC);

            CalamityGlobalNPC.SetNewShopVariable(new int[] { ModContent.NPCType<SEAHOE>() }, DownedBossSystem.downedAquaticScourge);

            // If Aquatic Scourge has not yet been killed, notify players of buffed Acid Rain
            if (!DownedBossSystem.downedAquaticScourge)
            {
                if (!Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active)
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/MaulerRoar"), (int)Main.player[Main.myPlayer].position.X, (int)Main.player[Main.myPlayer].position.Y);

                string sulfSeaBoostKey = "Mods.CalamityMod.WetWormBossText";
                Color sulfSeaBoostColor = AcidRainEvent.TextColor;

                CalamityUtils.DisplayLocalizedText(sulfSeaBoostKey, sulfSeaBoostColor);
                //set a timer for acid rain to start after 10 seconds
                CalamityWorld.forceRainTimer = 601;
            }

            // Mark Aquatic Scourge as dead
            DownedBossSystem.downedAquaticScourge = true;
            CalamityNetcode.SyncWorld();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * NPC.GetExpertDamageMultiplier());
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hitDirection, -1f, 0, default, 1f);

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 15; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hitDirection, -1f, 0, default, 1f);

                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.position, NPC.velocity, Mod.Find<ModGore>("ASHead").Type, NPC.scale);
                }
            }
        }

        public override bool CheckActive()
        {
            if (NPC.Calamity().newAI[0] == 1f && !Main.player[NPC.target].dead && NPC.Calamity().newAI[1] != 1f)
                return false;

            return true;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(ModContent.BuffType<Irradiated>(), 360, true);
        }
    }
}
