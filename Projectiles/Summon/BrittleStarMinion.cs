﻿using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon
{
    public class BrittleStarMinion : ModProjectile
    {
        public Player Owner => Main.player[Projectile.owner];
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brittle Star");
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 28;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.minion = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override void AI()
        {
            Owner.AddBuff(ModContent.BuffType<BrittleStar>(), 1);
            if (Projectile.type == ModContent.ProjectileType<BrittleStarMinion>())
            {
                if (Owner.dead)
                {
                    Owner.Calamity().bStar = false;
                }
                if (Owner.Calamity().bStar)
                {
                    Projectile.timeLeft = 2;
                }
            }

            Projectile.rotation += Projectile.velocity.X * 0.04f; // Spins faster the faster he moves in the X-axis.

            Projectile.ChargingMinionAI(1200f, 1500f, 2200f, 150f, 0, 24f, 15f, 4f, new Vector2(0f, -60f), 12f, 12f, false, false, 1);
        }
    }
}
