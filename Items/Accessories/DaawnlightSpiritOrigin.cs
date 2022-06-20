﻿using CalamityMod.Buffs.Pets;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories
{
    public class DaawnlightSpiritOrigin : ModItem
    {
        // "Despite the seemingly insane numbers here, I think this item might actually be underpowered"
        // hindsight: the item was not underpowered. Ozzatron 05NOV2021
        //
        // Regular crits are intentionally weak; bullseyes should be doing all the work
        private const float BullseyeCritRatio = 3.5f; // Bullseye crits deal x3.5 damage instead of x2.
        private const float StoredCritConversionRatio = 0.01f; // Add +1% more damage to crits for every 1% critical chance the player would have had.
        private const float MinUseTimeForSlowBonus = 11f;
        private const float MaxSlowBonusUseTime = 72f;
        private const float MaxSlowWeaponBonus = 0.33f; // Up to +33% more damage to crits for slower weapons.

        // These were very carefully calculated, please don't change them.
        internal const float RegularEnemyBullseyeRadius = 8f;
        internal const float BossBullseyeRadius = 18f;

        internal static float GetDamageMultiplier(Player p, CalamityPlayer mp, bool hitBullseye)
        {
            float baseCritMult = 2f; // In vanilla Terraria, crits do +100% damage.
            if (hitBullseye)
                baseCritMult = BullseyeCritRatio; // Replace a "regular crit" with a "bullseye crit".

            // Factor in the critical strike chance the player isn't getting to use.
            float convertedCritBonus = StoredCritConversionRatio * mp.spiritOriginConvertedCrit;

            float useTimeInterpolant = Utils.GetLerpValue(MinUseTimeForSlowBonus, MaxSlowBonusUseTime, p.ActiveItem().useTime, true);
            float slowWeaponBonus = MathHelper.Lerp(0f, MaxSlowWeaponBonus, useTimeInterpolant);
            return baseCritMult * (1f + convertedCritBonus + slowWeaponBonus);
        }

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Daawnlight Spirit Origin");
            Tooltip.SetDefault("All nearby enemies and bosses are marked with bullseyes\n" +
                "Ranged attacks that strike a bullseye always critically strike and deal massive damage\n" +
                "When a bullseye is struck, it vanishes and a new one appears elsewhere\n" +
                "Explosions or large projectiles cannot strike bullseyes\n" +
                "Converts all ranged critical strike chance boosts into extra critical strike damage\n" +
                "All ranged weapons will deal even more critical strike damage the slower they are\n" +
                "Summons a heroic spirit from another world if accessory visibility is enabled\n" +
                "The heroic spirit is also summoned when this accessory is placed in vanity slots\n" +
                "'A strand of a lost cosmos remains, waiting for its master'");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 38;
            Item.accessory = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = CalamityGlobalItem.Rarity11BuyPrice;
            Item.Calamity().donorItem = true;
        }

        // The pet is purely visual and does not affect the functionality of the item.
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Calamity().spiritOrigin = true;

            // If visibility is disabled, despawn the pet.
            if (hideVisual)
            {
                if (player.FindBuffIndex(ModContent.BuffType<ArcherofLunamoon>()) != -1)
                    player.ClearBuff(ModContent.BuffType<ArcherofLunamoon>());
            }
            // If visibility is enabled, spawn the pet.
            else if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<ArcherofLunamoon>()) == -1)
                    player.AddBuff(ModContent.BuffType<ArcherofLunamoon>(), 18000, true);
            }
        }

        public override void UpdateVanity(Player player)
        {
            // Summon anime girl if it's in vanity slot as the pet is purely vanity
            // It's possible for other "pet" items like Fungal Clump or HotE to summon a passive version of their "pets" with some tweaks though
            player.Calamity().spiritOriginVanity = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<ArcherofLunamoon>()) == -1)
                    player.AddBuff(ModContent.BuffType<ArcherofLunamoon>(), 18000, true);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<DeadshotBrooch>().
                AddIngredient(ItemID.LunarBar, 10).
                AddIngredient<MysteriousCircuitry>(15).
                AddIngredient<DubiousPlating>(15).
                AddIngredient<GalacticaSingularity>(4).
                AddTile(TileID.LunarCraftingStation).
                Register();
        }
    }
}
