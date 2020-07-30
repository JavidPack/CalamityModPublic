using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items
{
    public class ChargerTestItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charger Test Item");
            Tooltip.SetDefault("Charges all Draedon's Arnsenal weapons in your inventory\n" +
                               "If this makes it into a public release someone screwed up");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 42;
            item.rare = ItemRarityID.Red;
            item.Calamity().customRarity = CalamityRarity.DraedonRust;

            item.consumable = false;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override bool UseItem(Player player)
        {
            for (int i = 0; i < player.inventory.Length; i++)
            {
                if (player.inventory[i].type > ItemID.Count && player.inventory[i].Calamity().Chargeable)
                    player.inventory[i].Calamity().CurrentCharge = player.inventory[i].Calamity().ChargeMax;
            }
            return true;
        }

    }
}
