﻿using Terraria.DataStructures;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged
{
    public class TelluricGlare : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Telluric Glare");
            Tooltip.SetDefault("Fires volleys of four colossal radiant arrows which can pass through walls");
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 176;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 54;
            Item.height = 92;
            Item.useTime = 5;
            Item.useAnimation = 20;
            Item.reuseDelay = 23;
            Item.knockBack = 7.5f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;

            Item.value = CalamityGlobalItem.Rarity12BuyPrice;
            Item.rare = ItemRarityID.Purple;
            Item.Calamity().customRarity = CalamityRarity.Turquoise;

            Item.UseSound = SoundID.Item102;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TelluricGlareArrow>();
            Item.shootSpeed = 18f;
            Item.useAmmo = AmmoID.Arrow;
            Item.Calamity().canFirePointBlankShots = true;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-14f, 0f);

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Always fires Radiant Arrows regardless of ammo used
            type = Item.shoot;

            // The arrow appears from a random location "on the bow".
            // They are also moved backwards so that they have some time to build up past positions. This helps make them not appear out of thin air.

            Vector2 offset = Vector2.Normalize(velocity.RotatedBy(MathHelper.PiOver2));
            position += offset * Main.rand.NextFloat(-19f, 19f);
            position -= 3f * velocity;
            return true;
        }
    }
}
