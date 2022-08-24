﻿using Terraria.DataStructures;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace CalamityMod.Items.Weapons.Ranged
{
    public class HeavenlyGale : ModItem
    {
        public const int ShootDelay = 32;

        public const int ArrowsPerBurst = 9;

        public const int ArrowShootRate = 4;

        public const int ArrowShootTime = ArrowsPerBurst * ArrowShootRate;

        public const int MaxChargeTime = 300;

        public const float ArrowTargetingRange = 1100f;

        public static readonly SoundStyle FireSound = new("CalamityMod/Sounds/Item/HeavenlyGaleFire");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavenly Gale");
            Tooltip.SetDefault("Remake this tooltip");
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 654;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 44;
            Item.height = 58;
            Item.useTime = 42;
            Item.useAnimation = 42;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Arrow;
            Item.Calamity().customRarity = CalamityRarity.Violet;
            Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
            Item.Calamity().canFirePointBlankShots = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo spawnSource, Vector2 position, Vector2 velocity, int type, int damage, float knockback) => false;

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<PlanetaryAnnihilation>().
                AddIngredient<Alluvion>().
                AddIngredient<AstrealDefeat>().
                AddIngredient<ClockworkBow>().
                AddIngredient<Galeforce>(). //Why is this here
                AddIngredient<TheBallista>().
                AddIngredient<MiracleMatter>().
                AddTile<DraedonsForge>().
                Register();
        }
    }
}
