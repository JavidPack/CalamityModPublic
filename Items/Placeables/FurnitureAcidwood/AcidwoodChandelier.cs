using CalamityMod.Tiles.FurnitureAcidwood;
using Terraria.ID;
using Terraria.ModLoader;
namespace CalamityMod.Items.Placeables.FurnitureAcidwood
{
    public class AcidwoodChandelier : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.SetNameOverride("Acidwood Chandelier");
            Item.width = 28;
            Item.height = 20;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<AcidwoodChandelierTile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Acidwood>(), 4).AddIngredient(ItemID.Torch, 4).AddIngredient(ItemID.Chain).AddTile(TileID.Anvils).Register();
        }
    }
}
