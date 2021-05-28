using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace CalamityMod.Projectiles.Melee
{
    public class EonBeamV2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beam");
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 300;
            aiType = ProjectileID.EnchantedBeam;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
			if (projectile.ai[0] == 1f) //True Ark of the Ancients
				projectile.penetrate = 2;

            Lighting.AddLight(projectile.Center, (255 - projectile.alpha) * 0.3f / 255f, (255 - projectile.alpha) * 0.4f / 255f, (255 - projectile.alpha) * 1f / 255f);
            if (projectile.localAI[1] > 7f)
            {
                int num308 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 66, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 150, new Color(53, Main.DiscoG, 255), 1.2f);
                Main.dust[num308].velocity *= 0.1f;
                Main.dust[num308].noGravity = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(53, Main.DiscoG, 255, projectile.alpha);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			if (projectile.timeLeft > 295)
				return false;

			Texture2D tex = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.rotation, tex.Size() / 2f, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 7; k++)
            {
                int num308 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 66, 0f, 0f, 150, new Color(53, Main.DiscoG, 255), 1.2f);
                Main.dust[num308].noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (projectile.ai[0] != 1f) //excludes True Ark of the Ancients
			{
				target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
				target.AddBuff(BuffID.Frostburn, 120);
				target.AddBuff(ModContent.BuffType<Plague>(), 120);
				target.AddBuff(ModContent.BuffType<HolyFlames>(), 120);
			}
        }
    }
}
