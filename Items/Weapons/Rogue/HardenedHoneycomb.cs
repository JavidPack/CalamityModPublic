﻿using Terraria.DataStructures;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue
{
    public class HardenedHoneycomb : RogueWeapon
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hardened Honeycomb");
            // Tooltip.SetDefault(@"Fires a honeycomb that shatters into fragments
//Grants the honey buff to players it touches
//Stealth strikes can bounce off walls and enemies");
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.damage = 20;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.useAnimation = Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 300;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<Honeycomb>();
            Item.shootSpeed = 10f;
            Item.DamageType = RogueDamageClass.Instance;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.Calamity().StealthStrikeAvailable()) //setting the stealth strike
            {
                int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                if (stealth.WithinBounds(Main.maxProjectiles))
                {
                    Main.projectile[stealth].Calamity().stealthStrike = true;
                    Main.projectile[stealth].penetrate = 3;
                }
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100).
                AddIngredient(ItemID.Hive).
                AddIngredient(ItemID.CrispyHoneyBlock).
                AddIngredient(ItemID.BeeWax).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}
