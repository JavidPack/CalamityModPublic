﻿using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Fishing.SunkenSeaCatches;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions
{
    public class SoaringPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
            // DisplayName.SetDefault("Soaring Potion");
            /* Tooltip.SetDefault("Increases flight time and horizontal flight speed by 10%\n" +
                "Restores a fraction of your wing flight time after a true melee strike\n" +
                "The amount of flight time restored scales with your melee stats and weapon swing speed"); */
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
            Item.buffType = ModContent.BuffType<Soaring>();
            Item.buffTime = CalamityUtils.SecondsToFrames(360f);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.BottledWater).
                AddIngredient<SunkenSailfish>().
                AddIngredient(ItemID.SoulofFlight).
                AddTile(TileID.AlchemyTable).
				AddConsumeItemCallback(Recipe.ConsumptionRules.Alchemy).
                Register();

            CreateRecipe().
                AddIngredient(ItemID.BottledWater).
                AddIngredient<BloodOrb>(30).
                AddIngredient(ItemID.SoulofFlight).
                AddTile(TileID.AlchemyTable).
                Register();
        }
    }
}
