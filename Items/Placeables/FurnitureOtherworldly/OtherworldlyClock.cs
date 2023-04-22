﻿using CalamityMod.Items.Placeables.FurnitureCosmilite;
using Terraria.ID;
using Terraria.ModLoader;
namespace CalamityMod.Items.Placeables.FurnitureOtherworldly
{
    [LegacyName("OccultClock")]
    public class OtherworldlyClock : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.SetNameOverride("Otherworldly Clock");
            Item.width = 28;
            Item.height = 20;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.FurnitureOtherworldly.OtherworldlyClock>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<OtherworldlyStone>(), 10).AddIngredient(ModContent.ItemType<CosmiliteBrick>(), 3).AddIngredient(ItemID.Glass, 6).AddTile(TileID.LunarCraftingStation).Register();
        }
    }
}
