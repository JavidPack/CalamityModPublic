﻿using CalamityMod.Tiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityMod.Items.Placeables.Banners
{
    [LegacyName("CatfishBanner")]
    public class ToxicatfishBanner : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            // Tooltip.SetDefault("{$CommonItemTooltip.BannerBonus}Toxicatfish");
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.createTile = ModContent.TileType<MonsterBanner>();
            Item.placeStyle = 4;
        }
    }
}
