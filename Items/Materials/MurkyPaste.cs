﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityMod.Items.Materials
{
    public class MurkyPaste : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
            // DisplayName.SetDefault("Murky Paste");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(silver: 2);
            Item.rare = ItemRarityID.Blue;
        }
    }
}
