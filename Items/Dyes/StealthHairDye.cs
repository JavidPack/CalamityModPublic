using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes
{
    public class StealthHairDye : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stealth Hair Dye");
        }

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 26;
			item.maxStack = 99;
			item.value = Item.buyPrice(gold: 5);
			item.rare = 2;
			item.UseSound = SoundID.Item3;
			item.useStyle = 2;
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.consumable = true;
		}
	}
}