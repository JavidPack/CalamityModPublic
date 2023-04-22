﻿using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions
{
    public class ZenPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
            // DisplayName.SetDefault("Zen Potion");
            // Tooltip.SetDefault("Vastly decreases enemy spawn rate");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.useTurn = true;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<Zen>();
            Item.buffTime = CalamityUtils.SecondsToFrames(900f);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.BottledWater).
                AddIngredient<PurifiedGel>(2).
                AddIngredient(ItemID.Daybloom, 3).
                AddTile(TileID.AlchemyTable).
				AddConsumeItemCallback(Recipe.ConsumptionRules.Alchemy).
                Register();

            CreateRecipe().
                AddIngredient(ItemID.BottledWater).
                AddIngredient<BloodOrb>(20).
                AddIngredient<PurifiedGel>(2).
                AddTile(TileID.AlchemyTable).
                Register();
        }
    }
}
