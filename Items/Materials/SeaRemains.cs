﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials
{
    [LegacyName("VictideBar")]
    public class SeaRemains : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            // DisplayName.SetDefault("Sea Remains");
			ItemID.Sets.SortingPriorityMaterials[Type] = 60; // Meteorite
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(silver: 6);
            Item.rare = ItemRarityID.Green;
        }
        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<PearlShard>(2).
                AddIngredient(ItemID.Coral, 2).
                AddIngredient(ItemID.Starfish, 2).
                AddIngredient(ItemID.Seashell, 2).
                AddTile(TileID.Furnaces).
                Register();
        }
    }
}
