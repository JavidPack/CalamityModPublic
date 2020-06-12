using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials
{
    public class BloodlettingEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodletting Essence");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 8));
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(silver: 1);
            item.rare = 1;
        }
    }
}
