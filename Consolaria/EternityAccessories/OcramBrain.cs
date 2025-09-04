using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Build.Evaluation;
using ssm.Content.Projectiles;

namespace ssm.Consolaria.EternityAccessories
{
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    public class OcramBrain : SoulsItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.EmodeConsolaria;
        }
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 32;
            Item.maxStack = 1;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OcramBrainEffect>(Item);
        }

        public class OcramBrainEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<OcramBrain>();
            public override bool ActiveSkill => true;

            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (player.GetModPlayer<CSEConsolariaPlayer>().OcramBrainCharge >= 300)
                {
                    player.GetModPlayer<CSEConsolariaPlayer>().OcramBrainCharge = 0;
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<OcramBrainProjectile>(), 50, 10f, player.whoAmI);
                }
            }
        }
    }
}