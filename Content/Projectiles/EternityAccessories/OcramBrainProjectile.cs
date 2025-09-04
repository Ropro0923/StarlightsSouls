using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm;

namespace ssm.Content.Projectiles
{
    public class OcramBrainProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Acorn; 
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 600;
            Projectile.damage = 330;
            Projectile.DamageType = DamageClass.Generic;
        }

        public override void AI()
        {
            CSEUtils.HomeInOnNPC(Projectile, false, 60f, 1f, 2f);
        }
    }
}
