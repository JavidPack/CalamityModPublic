﻿using Terraria.DataStructures;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged
{
    public class MagnomalyCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magnomaly Cannon");
            Tooltip.SetDefault("Launches a powerful exo rocket to nuke anything and everything\n" +
                "Rockets are surrounded by an invisible damaging aura and split into damaging beams on hit\n" +
                "66% chance to not consume rockets");
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 357;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 84;
            Item.height = 30;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 9.5f;
            Item.UseSound = SoundID.Item11;
            Item.value = Item.buyPrice(2, 50, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<MagnomalyRocket>();
            Item.shootSpeed = 15f;
            Item.useAmmo = AmmoID.Rocket;
            Item.Calamity().customRarity = CalamityRarity.Violet;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, -10);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<MagnomalyRocket>(), damage, knockback, player.whoAmI, 0f, 0f);
            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) < 66)
                return false;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<ThePack>().
                AddIngredient<ScorchedEarth>().
                AddIngredient<AethersWhisper>().
                AddIngredient(ItemID.ElectrosphereLauncher).
                AddIngredient<MiracleMatter>().
                AddTile<DraedonsForge>().
                Register();
        }
    }
}
