﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing
{
    public class FishofNight : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish of Night");
            Tooltip.SetDefault("Right click to extract souls");
            SacrificeTotal = 10;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 34;
            Item.height = 34;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 1);
        }

        public override bool CanRightClick() => true;
        public override void ModifyItemLoot(ItemLoot itemLoot) => itemLoot.Add(ItemID.SoulofNight, 1, 2, 5);
    }
}
