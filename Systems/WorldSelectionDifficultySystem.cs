﻿using System;
using System.Collections.Generic;
using System.Reflection;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Systems
{
    public class WorldSelectionDifficultySystem : ModSystem
    {
        public override void PostSetupContent()
        {
            // Load all of the World Difficulties to the World Difficulty list
            // The difficulty the world uses will be the latest applicable one, so these are added in order from easiest to hardest

            string revenge = CalamityUtils.GetTextValue("UI.Revengeance");
            string death = CalamityUtils.GetTextValue("UI.Death");
            string rev = CalamityUtils.GetTextValue("UI.Rev");
            string master = Language.GetTextValue("UI.Master");
            string legend = CalamityUtils.GetTextValue("UI.Legend");
            string plus = "+";

            WorldDifficulties.Add(new WorldDifficulty(revenge, GetRevengeance, new(211, 42, 42)));
            WorldDifficulties.Add(new WorldDifficulty(death, GetDeath, new(192, 64, 219)));
            WorldDifficulties.Add(new WorldDifficulty(rev + plus + master, GetMasterRevengeance, new(124, 153, 242)));
            WorldDifficulties.Add(new WorldDifficulty(death + plus + master, GetMasterDeath, new(255, 25, 255)));
            WorldDifficulties.Add(new WorldDifficulty(rev + plus + legend, GetLegendRevengeance, new(240, 128, 128)));
            WorldDifficulties.Add(new WorldDifficulty(death + plus + legend, GetLegendDeath, new(220, 255, 132)));
        }

        public override void SaveWorldHeader(TagCompound tag)
        {
            // Since CalamityWorld is static, and therefore invalid for TryGetHeaderData, make copies for Rev and Death
            tag["RevengeanceMode"] = CalamityWorld.revenge;
            tag["DeathMode"] = CalamityWorld.death;
        }

        public record WorldDifficulty(string name, Func<AWorldListItem, bool> function, Color color);

        public static List<WorldDifficulty> WorldDifficulties = new List<WorldDifficulty>();
        public static bool GetRevengeance(AWorldListItem item)
        {
            if (item.Data.TryGetHeaderData<WorldSelectionDifficultySystem>(out TagCompound tag))
            {
                if (tag.ContainsKey("RevengeanceMode") && tag.GetBool("RevengeanceMode"))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetDeath(AWorldListItem item)
        {
            if (item.Data.TryGetHeaderData<WorldSelectionDifficultySystem>(out TagCompound tag))
            {
                if (tag.ContainsKey("DeathMode") && tag.GetBool("DeathMode"))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetMasterRevengeance(AWorldListItem item)
        {
            // Grab data from the listed world
            FieldInfo worldDataField = typeof(UIWorldListItem).GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance);
            WorldFileData worldData = (WorldFileData)worldDataField.GetValue(item);

            int trueGameMode = worldData.GameMode;
            if (worldData.ForTheWorthy)
            {
                trueGameMode++;
            }

            if (item.Data.TryGetHeaderData<WorldSelectionDifficultySystem>(out TagCompound tag))
            {
                if (tag.ContainsKey("RevengeanceMode") && tag.GetBool("RevengeanceMode") && trueGameMode == 2)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetMasterDeath(AWorldListItem item)
        {
            // Grab data from the listed world
            FieldInfo worldDataField = typeof(UIWorldListItem).GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance);
            WorldFileData worldData = (WorldFileData)worldDataField.GetValue(item);

            int trueGameMode = worldData.GameMode;
            if (worldData.ForTheWorthy)
            {
                trueGameMode++;
            }

            if (item.Data.TryGetHeaderData<WorldSelectionDifficultySystem>(out TagCompound tag))
            {
                if (tag.ContainsKey("DeathMode") && tag.GetBool("DeathMode") && trueGameMode == 2)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetLegendRevengeance(AWorldListItem item)
        {
            // Grab data from the listed world
            FieldInfo worldDataField = typeof(UIWorldListItem).GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance);
            WorldFileData worldData = (WorldFileData)worldDataField.GetValue(item);

            int trueGameMode = worldData.GameMode;
            if (worldData.ForTheWorthy)
            {
                trueGameMode++;
            }

            if (item.Data.TryGetHeaderData<WorldSelectionDifficultySystem>(out TagCompound tag))
            {
                if (tag.ContainsKey("RevengeanceMode") && tag.GetBool("RevengeanceMode") && trueGameMode == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetLegendDeath(AWorldListItem item)
        {
            // Grab data from the listed world
            FieldInfo worldDataField = typeof(UIWorldListItem).GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance);
            WorldFileData worldData = (WorldFileData)worldDataField.GetValue(item);

            int trueGameMode = worldData.GameMode;
            if (worldData.ForTheWorthy)
            {
                trueGameMode++;
            }

            if (item.Data.TryGetHeaderData<WorldSelectionDifficultySystem>(out TagCompound tag))
            {
                if (tag.ContainsKey("DeathMode") && tag.GetBool("DeathMode") && trueGameMode == 3)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
