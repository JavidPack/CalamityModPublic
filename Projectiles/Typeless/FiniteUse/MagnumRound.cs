using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless.FiniteUse
{
    public class MagnumRound : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magnum Round");
        }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.light = 0.5f;
            projectile.alpha = 255;
            projectile.extraUpdates = 10;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = 1;
            aiType = 242;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.Organic())
            {
                damage += target.lifeMax / 25; //400 + 80 = 480 + (100000 / 25 = 4000) = 4480, if crit = 5600 = 5.6% of boss HP
            }
            if (damage > target.lifeMax / 15 && CalamityPlayer.areThereAnyDamnBosses)
                damage = target.lifeMax / 15;
            if (crit)
            {
                damage = (int)(damage * 1.25);
                knockback *= 1.25f;
            }
        }
    }
}
