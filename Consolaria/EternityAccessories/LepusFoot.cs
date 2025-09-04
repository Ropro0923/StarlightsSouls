using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Content.Items;

namespace ssm.Consolaria.EternityAccessories
{
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    public class LepusFoot : SoulsItem
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
            player.GetModPlayer<CSEConsolariaPlayer>().LepusFoot = true;
        }
    }
}
