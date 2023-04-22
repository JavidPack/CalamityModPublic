using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Plates
{
    public class Elumplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elumplate");
            // Tooltip.SetDefault("It resonates with otherworldly energy.");
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.createTile = ModContent.TileType<Tiles.Plates.Elumplate>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 13;
            Item.height = 10;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(silver: 3);
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            CreateRecipe(25).
                AddIngredient(ItemID.Obsidian, 25).
                AddIngredient<EssenceofEleum>().
                AddTile(TileID.Hellforge).
                Register();
            CreateRecipe().
                AddIngredient<ElumplateWall>(4).
                AddTile(TileID.WorkBenches).
                Register();
        }
    }
}
