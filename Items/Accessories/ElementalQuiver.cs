﻿using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace CalamityMod.Items.Accessories
{
    public class ElementalQuiver : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Elemental Quiver");
            Tooltip.SetDefault("15% increased ranged damage, 5% increased ranged critical strike chance and 20% reduced ammo usage\n" +
                "Grants a 20% chance to not consume arrows\n" +
                "Greatly increases all ranged projectile velocity");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = CalamityGlobalItem.Rarity14BuyPrice;
            Item.accessory = true;
            Item.Calamity().customRarity = CalamityRarity.DarkBlue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.magicQuiver = true;
            CalamityPlayer modPlayer = player.Calamity();
            modPlayer.artemisEmblem = true;
            modPlayer.rangedAmmoCost *= 0.8f;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.MagicQuiver).
                AddIngredient<ArtemisEmblem>().
                AddIngredient(ItemID.LunarBar, 8).
                AddIngredient<GalacticaSingularity>(4).
                AddIngredient<AscendantSpiritEssence>(4).
                AddTile<CosmicAnvil>().
                Register();
        }
    }
}
