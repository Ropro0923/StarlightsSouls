using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Content.Items.Accessories.Souls;

namespace ssm.Consolaria.EternityAccessories
{
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]

    public partial class CSEConsolariaPlayer : ModPlayer
    {
        public bool ThanksgivingLeftovers;
        public bool LepusFoot;
        public int OcramBrainCharge;
        public override void ResetEffects()
        {
            if (Player.HasEffect<OcramBrain.OcramBrainEffect>())
            {
                if (OcramBrainCharge < 300)
                {
                    OcramBrainCharge++;
                }
            }
            else
            {
                OcramBrainCharge = 0;
            }
        }
        public override void UpdateDead()
        {
            OcramBrainCharge = 0;
        }
    }
}