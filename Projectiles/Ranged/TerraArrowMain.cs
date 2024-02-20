﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using CalamityMod.Particles;

namespace CalamityMod.Projectiles.Ranged
{
    public class TerraArrowMain : ModProjectile, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Ranged";
        public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

        private bool initialized = false;
        private bool hitDirect = false;
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
            Projectile.extraUpdates = 8;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.Calamity().pointBlankShotDuration = CalamityGlobalProjectile.DefaultPointBlankDuration;
            Projectile.ArmorPenetration = 25;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.damage = (int)(Projectile.damage * 0.18f);
                Projectile.velocity *= 0.25f;
                LineParticle spark = new LineParticle(Projectile.Center + Projectile.velocity * 4, Projectile.velocity * 2.95f, false, 7, 2f, Color.LimeGreen);
                GeneralParticleHandler.SpawnParticle(spark);
            }
            Projectile.ai[0]++;

            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
        }

        public override void OnKill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 vel1 = (Projectile.velocity * 0.4f).RotatedBy(Main.rand.NextFloat(0.015f, 0.04f));
                Vector2 vel2 = (Projectile.velocity * 0.4f).RotatedBy(Main.rand.NextFloat(-0.015f, -0.04f));

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel1 * Main.rand.NextFloat(0.95f, 1.05f), ModContent.ProjectileType<TerraArrowSplit>(), (int)(Projectile.damage * 3), 0f, Projectile.owner, 0f, hitDirect ? 1f : 0f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel2 * Main.rand.NextFloat(0.95f, 1.05f), ModContent.ProjectileType<TerraArrowSplit>(), (int)(Projectile.damage * 3), 0f, Projectile.owner, 0f, hitDirect ? 1f : 0f);
                
                PointParticle spark = new PointParticle(Projectile.Center - Projectile.velocity + Projectile.velocity.RotatedBy(2.3f), Projectile.velocity.RotatedBy(2.3f) * 0.5f, false, 5, 1.1f, Color.LimeGreen);
                GeneralParticleHandler.SpawnParticle(spark);
                PointParticle spark2 = new PointParticle(Projectile.Center - Projectile.velocity + Projectile.velocity.RotatedBy(-2.3f), Projectile.velocity.RotatedBy(-2.3f) * 0.5f, false, 5, 1.1f, Color.LimeGreen);
                GeneralParticleHandler.SpawnParticle(spark2);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.timeLeft <= 4)
            {
                hitDirect = true;
                SoundEngine.PlaySound(SoundID.Item53 with { Pitch = 0.9f, Volume = 0.4f }, Projectile.Center);
                int Dusts = 8;
                float radians = MathHelper.TwoPi / Dusts;
                Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
                float rotRando = Main.rand.NextFloat(0.1f, 2.5f);
                for (int i = 0; i < Dusts; i++)
                {
                    Vector2 dustVelocity = spinningPoint.RotatedBy(radians * i).RotatedBy(0.5f * rotRando) * 6f;
                    Dust dust2 = Dust.NewDustPerfect(Projectile.Center, 264, dustVelocity);
                    dust2.noGravity = true;
                    dust2.scale = Main.rand.NextFloat(0.85f, 1.35f);
                    dust2.color = Main.rand.NextBool(3) ? Color.MediumAquamarine : Color.Lime;
                }
                Projectile.Kill();
            }
        }
    }
}
