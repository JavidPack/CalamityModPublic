using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ReaverHelm : ModItem
    {
		//Defense and DR Helm
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaver Helm");
            Tooltip.SetDefault("10% increased damage reduction but 30% decreased damage");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 7;
            item.defense = 40; //58
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ReaverScaleMail>() && legs.type == ModContent.ItemType<ReaverCuisses>();
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            CalamityPlayer modPlayer = player.Calamity();
            player.thorns += 0.33f;
            player.moveSpeed -= 0.2f;
            modPlayer.reaverBlast = true;
            player.setBonus = "20% decreased movement speed\n" +
			"Enemy damage is reflected and summons a thorn spike\n" +
			"Reaver Rage activates when you are damaged";
			//Reaver Rage provides 30% damage to offset the helm "bonus", 5 def, and 5% melee speed.
        }

        public override void UpdateEquip(Player player)
        {
			player.allDamage -= 0.3f;
			player.endurance += 0.1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<DraedonBar>(), 8);
			recipe.AddIngredient(ItemID.JungleSpores, 6);
			recipe.AddIngredient(ModContent.ItemType<EssenceofCinder>());
			recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
