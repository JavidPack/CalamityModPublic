﻿using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Cooldowns;
using CalamityMod.Projectiles.Magic;
using Terraria.Audio;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static CalamityMod.CalamityUtils;
using static Terraria.ModLoader.ModContent;
using static Microsoft.Xna.Framework.Input.Keys;
using System;
using CalamityMod.Items.BaseItems;
using System.Collections.Generic;
using CalamityMod.Particles;

namespace CalamityMod.Items.Armor.Wulfrum
{
    #region Armor pieces

    //Datsuzei / Moonstone armor from starlight river was a nice jumping off point for this kind of set.

    [AutoloadEquip(EquipType.Head)]
    [LegacyName("WulfrumHelmet")]
    [LegacyName("WulfrumHeadSummon")]
    public class WulfrumHat : ModItem, IExtendedHat
    {
        #region big hat
        public string ExtensionTexture => "CalamityMod/Items/Armor/Wulfrum/WulfrumHat_HeadExtension";
        public Vector2 ExtensionSpriteOffset(PlayerDrawSet drawInfo) => -Vector2.UnitY * 2f;
        public string EquipSlotName(Player drawPlayer) => drawPlayer.Male ? Name : "WulfrumHatFemale";
        #endregion

        public static readonly SoundStyle SetActivationSound = new("CalamityMod/Sounds/Custom/AbilitySounds/WulfrumBastionActivate");
        public static readonly SoundStyle SetBreakSound = new("CalamityMod/Sounds/Custom/AbilitySounds/WulfrumBastionBreak");
        public static readonly SoundStyle SetBreakSoundSafe = new("CalamityMod/Sounds/Custom/AbilitySounds/WulfrumBastionBreakSafely");

        public static int BastionBuildTime = (int)(0.25f * 60);
        public static int BastionTime = 30 * 60;
        public static int TimeLostPerHit = 2 * 60;
        public static int BastionCooldown = 20 * 60;


        internal static Item DummyCannon = new Item(); //Used for the attack swap. Basically we force the player to hold a fake item.

        public static bool PowerModeEngaged(Player player, out CooldownInstance cd)
        {
            cd = null;
            bool hasWulfrumBastionCD = player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out cd);
            return (hasWulfrumBastionCD && cd.timeLeft > BastionCooldown);
        }

        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                EquipLoader.AddEquipTexture(Mod, "CalamityMod/Items/Armor/Wulfrum/WulfrumHat_FemaleHead", EquipType.Head, name : "WulfrumHatFemale");
            }

            On.Terraria.Player.KeyDoubleTap += ActivateSetBonus;
            On.Terraria.Main.DrawPendingMouseText += SpoofMouseItem;
        }

        public override void Unload()
        {
            On.Terraria.Player.KeyDoubleTap -= ActivateSetBonus;
            On.Terraria.Main.DrawPendingMouseText -= SpoofMouseItem;


            DummyCannon.TurnToAir();
            DummyCannon = null;
        }

        private void ActivateSetBonus(On.Terraria.Player.orig_KeyDoubleTap orig, Player player, int keyDir)
        {
            BastionBuildTime = (int)(0.55f * 60);

            if (keyDir == 0 && HasArmorSet(player))
            {
                //Only activate if no cooldown & available scrap.
                if (!player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out CooldownInstance cd) && player.HasItem(ModContent.ItemType<WulfrumMetalScrap>()))
                {
                    player.ConsumeItem(ModContent.ItemType<WulfrumMetalScrap>());
                    //I Thiiiinnnk there's no need to add mp syncing packets sicne cooldowns get auto synced right.
                    player.AddCooldown(WulfrumBastion.ID, BastionCooldown + BastionTime);
                    //Though do i need to sync that or is the player inventory auto synced?
                    DummyCannon.SetDefaults(ItemType<WulfrumFusionCannon>());
                }
            }

            orig(player, keyDir);
        }

        //Replaces the tooltip of the armor set with the fusion cannon if the player holds shift
        private void SpoofMouseItem(On.Terraria.Main.orig_DrawPendingMouseText orig)
        {
            var player = Main.LocalPlayer;

            if (DummyCannon.IsAir && !Main.gameMenu)
                DummyCannon.SetDefaults(ItemType<WulfrumFusionCannon>());

            if (IsPartOfSet(Main.HoverItem) && HasArmorSet(player) && Main.keyState.IsKeyDown(LeftShift))
            {
                Main.HoverItem = DummyCannon.Clone();
                Main.hoverItemName = DummyCannon.Name;
            }

            orig();
        }


        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Wulfrum Hat & Goggles");
            Tooltip.SetDefault("10% increased minion damage\n"+
                "Comes equipped with hair extensions" );
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 0, 75, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) =>  body.type == ItemType<WulfrumJacket>() && legs.type == ItemType<WulfrumOveralls>();
        public static bool HasArmorSet(Player player) => player.armor[0].type == ItemType<WulfrumHat>() && player.armor[1].type == ItemType<WulfrumJacket>() && player.armor[2].type == ItemType<WulfrumOveralls>();
        public bool IsPartOfSet(Item item) => item.type == ItemType<WulfrumHat>() ||
                item.type == ItemType<WulfrumJacket>() ||
                item.type == ItemType<WulfrumOveralls>();

        public override void UpdateArmorSet(Player player)
        {
            WulfrumArmorPlayer armorPlayer = player.GetModPlayer<WulfrumArmorPlayer>();
            WulfrumTransformationPlayer transformationPlayer = player.GetModPlayer<WulfrumTransformationPlayer>();

            armorPlayer.wulfrumSet = true;

            player.setBonus = "+3 defense and +1 max minion"; //The cooler part of the set bonus happens in modifytooltips because i can't recolor it otherwise. Madge
            player.statDefense += 3;
            player.maxMinions++;
            if (PowerModeEngaged(player, out var cd))
            {
                if (cd.timeLeft == BastionCooldown + BastionTime)
                {
                    ActivationEffects(player);
                }

                //Can't account for previous fullbody transformations but at this point, whatever.
                Item headItem = player.armor[10] ?? player.armor[0];
                bool hatVisible = !transformationPlayer.transformationActive && headItem.type == ItemType<WulfrumHat>();

                //Spawn the hat
                if (cd.timeLeft == BastionCooldown + BastionTime - (int)(BastionBuildTime * 0.9f) && hatVisible)
                {
                    Particle leftoverHat = new WulfrumHatParticle(player, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver4) * Main.rand.NextFloat(3f, 7f), 25);
                    GeneralParticleHandler.SpawnParticle(leftoverHat);
                }

                if (cd.timeLeft < BastionCooldown + BastionTime - BastionBuildTime)
                    player.GetModPlayer<WulfrumTransformationPlayer>().transformationActive = true;
                else if (cd.timeLeft <= BastionCooldown + BastionTime - (int)(BastionBuildTime * 0.9f))
                    player.GetModPlayer<WulfrumTransformationPlayer>().forceHelmetOn = true;


                player.moveSpeed *= 0.8f;
                player.statDefense += 15;
                //Drop the player's held item if they were holding something before
                if (!(Main.mouseItem.type == DummyCannon.type) && !Main.mouseItem.IsAir)
                    Main.LocalPlayer.QuickSpawnClonedItem(null, Main.mouseItem, Main.mouseItem.stack);
                
                //Slot 58 is the "fake" slot thats used for the item the player is holding in their mouse.
                Main.mouseItem = DummyCannon;
                player.inventory[58] = DummyCannon;
                player.selectedItem = 58;
            }

            else
            {
                //Clear the player's hand
                if (Main.mouseItem.type == ItemType<WulfrumFusionCannon>())
                    Main.mouseItem = new Item();

                DummyCannon.TurnToAir();
            }
        }

        public void ActivationEffects(Player player)
        {
            SoundEngine.PlaySound(SetActivationSound);

            //Do'nt do the effect ifthe player is already using the wulfrum vanity lol.
            bool transformedAlready = player.GetModPlayer<WulfrumTransformationPlayer>().transformationActive;

            if (!transformedAlready)
            {
                for (int i = 0; i < 5; i++)
                {
                    Particle part = new WulfrumBastionPartsParticle(player, i, BastionBuildTime + 2);
                    GeneralParticleHandler.SpawnParticle(part);
                }
            }

            //Do spawn the cannon always though
            Particle gun = new WulfrumBastionPartsParticle(player, 5, BastionBuildTime + 2);

            if (transformedAlready)
            {
                (gun as WulfrumBastionPartsParticle).TimeOffset = 0;
                (gun as WulfrumBastionPartsParticle).AnimationTime = BastionBuildTime + 2;
            }
            GeneralParticleHandler.SpawnParticle(gun);
        }

        public static void ModifySetTooltips(ModItem item, List<TooltipLine> tooltips)
        {
            if (HasArmorSet(Main.LocalPlayer))
            {
                int setBonusIndex = tooltips.FindIndex(x => x.Name == "SetBonus" && x.Mod == "Terraria");

                if (setBonusIndex != -1)
                {
                    TooltipLine setBonus1 = new TooltipLine(item.Mod, "CalamityMod:SetBonus1", "Wulfrum Bastion - Double tap DOWN to equip a heavy wulfrum armor");
                    setBonus1.OverrideColor = Color.Lerp(new Color(194, 255, 67), new Color(112, 244, 244), 0.5f + 0.5f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f));
                    tooltips.Insert(setBonusIndex + 1, setBonus1);

                    TooltipLine setBonus2 = new TooltipLine(item.Mod, "CalamityMod:SetBonus2", "While the power armor is active, you can only use the integrated fusion cannon, but get increased defensive stats");
                    setBonus2.OverrideColor = new Color(110, 192, 93);
                    tooltips.Insert(setBonusIndex + 2, setBonus2);

                    TooltipLine setBonus3 = new TooltipLine(item.Mod, "CalamityMod:SetBonus3", "Calling down the armor consumes one piece of wulfrum scrap, and the armor will lose durability faster when hit");
                    setBonus3.OverrideColor = new Color(110, 192, 93);
                    tooltips.Insert(setBonusIndex + 3, setBonus3);
                }

                TooltipLine itemDisplay = new TooltipLine(item.Mod, "CalamityMod:ArmorItemDisplay", "Hold SHIFT to see the stats of the fusion cannon");
                itemDisplay.OverrideColor = new Color(190, 190, 190);
                tooltips.Add(itemDisplay);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips) => ModifySetTooltips(this, tooltips);

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<SummonDamageClass>() += 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<WulfrumMetalScrap>(5).
                AddIngredient<EnergyCore>().
                AddTile(TileID.Anvils).
                Register();
        }
    }

    [AutoloadEquip(EquipType.Body)]
    [LegacyName("WulfrumArmor")]
    public class WulfrumJacket : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Wulfrum Jacket");
            Tooltip.SetDefault("3% increased critical strike chance");

            if (Main.netMode != NetmodeID.Server)
            {
                var equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
                ArmorIDs.Body.Sets.HidesArms[equipSlot] = true;
                ArmorIDs.Body.Sets.HidesTopSkin[equipSlot] = true;
            }
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips) => WulfrumHat.ModifySetTooltips(this, tooltips);

        public override void UpdateEquip(Player player) => player.GetCritChance<GenericDamageClass>() += 3;

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<WulfrumMetalScrap>(12).
                AddIngredient<EnergyCore>().
                AddTile(TileID.Anvils).
                Register();
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    [LegacyName("WulfrumLeggings")]
    public class WulfrumOveralls : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Wulfrum Overalls");
            Tooltip.SetDefault("Movement speed increased by 5%");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 1;

            if (Main.netMode != NetmodeID.Server)
            {
                var equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
                ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlot] = true;
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips) => WulfrumHat.ModifySetTooltips(this, tooltips);
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<WulfrumMetalScrap>(8).
                AddIngredient<EnergyCore>().
                AddTile(TileID.Anvils).
                Register();
        }
    }
    #endregion

    public class WulfrumFusionCannon : HeldOnlyItem, IHideFrontArm
    {

        public static readonly SoundStyle ShootSound = new("CalamityMod/Sounds/Item/WulfrumProthesisShoot") { PitchVariance = 0.1f, Volume = 0.55f };
        public static readonly SoundStyle HitSound = new("CalamityMod/Sounds/Item/WulfrumProthesisHit") { PitchVariance = 0.1f, Volume = 0.75f, MaxInstances = 3 };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Experimental Wulfrum Fusion Array");
            Tooltip.SetDefault("Fires quick bursts of medium-range pellets\n" +
                "[c/878787:\"Who needs whips when you can simply become the summon yourself?\"]");
            //Imaging hiding actually important/interesting/funny lore in there... :drool: that would be so dark souls
        }
        public override string Texture => "CalamityMod/Items/Armor/Wulfrum/WulfrumFusionCannon";

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Summon;
            Item.width = 34;
            Item.height = 42;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = ShootSound;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<WulfrumBolt>();
            Item.shootSpeed = 18f;
            Item.holdStyle = 16; //Custom hold style

            Item.useAnimation = 10;
            Item.useTime = 4;
            Item.reuseDelay = 17;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var name = tooltips.FirstOrDefault(x => x.Name == "ItemName" && x.Mod == "Terraria");
            name.OverrideColor = Color.Lerp(new Color(194, 255, 67), new Color(112, 244, 244), 0.5f + 0.5f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f));
        }

        public override void HoldItem(Player player)
        {
            player.Calamity().mouseWorldListener = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (!WulfrumHat.HasArmorSet(player))
                {
                    Item.TurnToAir();
                    Main.mouseItem = new Item();
                }
            }

            Item.noUseGraphic = false;

            if (!player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out var cd) || cd.timeLeft > WulfrumHat.BastionCooldown + WulfrumHat.BastionTime - WulfrumHat.BastionBuildTime)
                Item.noUseGraphic = true;

        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.PiOver4 * 0.1f);
        }

        public override bool CanUseItem(Player player)
        {
            return player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out var cd) && cd.timeLeft < WulfrumHat.BastionCooldown + WulfrumHat.BastionTime - WulfrumHat.BastionBuildTime;
        }

        public void SetItemInHand(Player player, Rectangle heldItemFrame)
        {
            //Make the player face where they're aiming.
            if (player.Calamity().mouseWorld.X > player.Center.X)
            {
                player.ChangeDir(1);
            }
            else
            {
                player.ChangeDir(-1);
            }

            if (!player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out var cd) || cd.timeLeft > WulfrumHat.BastionCooldown + WulfrumHat.BastionTime - WulfrumHat.BastionBuildTime)
                return;

            float animProgress = 1 - player.itemAnimation / (float)player.itemAnimationMax;

            //Default
            Vector2 itemPosition = player.MountedCenter + new Vector2(-2f * player.direction, -1f * player.gravDir);
            float itemRotation = (player.Calamity().mouseWorld - itemPosition).ToRotation();

            //Adjust for animation

            if (animProgress < 0.7f)
                itemPosition -= itemRotation.ToRotationVector2() * (1 - (float)Math.Pow(1 - (0.7f - animProgress) / 0.7f, 4)) * 4f;

            if (animProgress < 0.4f)
                itemRotation += -0.45f * (float)Math.Pow((0.4f - animProgress) / 0.4f, 2) * player.direction * player.gravDir;

            //Shakezzz
            if (player.itemTime == 1 && Main.projectile.Any(n => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<WulfrumManaDrain>()))
            {
                itemPosition += Main.rand.NextVector2Circular(2f, 2f);
            }


            Vector2 itemSize = new Vector2(38, 18);
            Vector2 itemOrigin = new Vector2(-12, 0);
            CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin, true);
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame) => SetItemInHand(player, heldItemFrame);
        public override void UseStyle(Player player, Rectangle heldItemFrame) => SetItemInHand(player, heldItemFrame);
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) => false;
    }



    public class WulfrumArmorPlayer : ModPlayer
    {
        public static int BastionShootDamage = 10;
        public static float BastionShootSpeed = 18f;
        public static int BastionShootTime = 10;

        public bool wulfrumSet = false;
        
        public override void ResetEffects()
        {
            wulfrumSet = false;
        }

        public override void UpdateDead()
        {
            wulfrumSet = false;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (WulfrumHat.PowerModeEngaged(Player, out var cd) && Main.netMode != NetmodeID.Server)
            {
                SetBonusEndEffect(true);
                Player.GetModPlayer<WulfrumTransformationPlayer>().transformationActive = false;
            }
        }

        public override void PostUpdate()
        {
            if (wulfrumSet && Player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out var cd) && cd.timeLeft == WulfrumHat.BastionCooldown)
            {
                SetBonusEndEffect(false);
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (WulfrumHat.PowerModeEngaged(Player, out var cd))
            {
                cd.timeLeft -= WulfrumHat.TimeLostPerHit;
                if (cd.timeLeft < WulfrumHat.BastionCooldown)
                {
                    cd.timeLeft = WulfrumHat.BastionCooldown - 1;
                    if (Main.netMode != NetmodeID.Server)
                        SetBonusEndEffect(true);
                }
            }
        }

        public void SetBonusEndEffect(bool violent)
        {
            SoundStyle breakSound = WulfrumHat.SetBreakSoundSafe;
            float goreSpeed = 3f;
            int goreCount = 4;
            int goreIncrement = 2;

            if (violent)
            {
                breakSound = WulfrumHat.SetBreakSound;
                goreSpeed = 9f;
                goreCount = 9;
                goreIncrement = 1;
            }

            SoundEngine.PlaySound(breakSound, Player.Center);
            //Only spawn the cannon gore if the player already has the vanity on.
            if (Player.GetModPlayer<WulfrumTransformationPlayer>().vanityEquipped)
            {
                Vector2 shrapnelVelocity = Main.rand.NextVector2Circular(goreSpeed, goreSpeed);
                Gore.NewGore(Player.GetSource_Death(), Player.Center, shrapnelVelocity, Mod.Find<ModGore>("WulfrumPowerSuit1").Type);
            }

            else
            {
                int j = 1;

                for (int i = 1; i < goreCount; i++)
                {
                    Vector2 shrapnelVelocity = Main.rand.NextVector2Circular(goreSpeed, goreSpeed);
                    string goreType = "WulfrumPowerSuit" + j.ToString();
                    Gore.NewGore(Player.GetSource_Death(), Player.Center, shrapnelVelocity, Mod.Find<ModGore>(goreType).Type);

                    j += Main.rand.Next(1, goreIncrement);
                }
            }
        }


        //This shouldn't ever be possible since the power mode prevents you from using or moving items around
        public override void PostUpdateMiscEffects()
        {
            if (!wulfrumSet && WulfrumHat.PowerModeEngaged(Player, out var cd))
            {
                cd.timeLeft = WulfrumHat.BastionCooldown;
            }
        }

        public override void FrameEffects()
        {
            //Give the braids variant to w*men
            if (!Player.Male && Player.head == EquipLoader.GetEquipSlot(Mod, "WulfrumHat", EquipType.Head))
                Player.head = EquipLoader.GetEquipSlot(Mod, "WulfrumHatFemale", EquipType.Head);
        }
    }
}
