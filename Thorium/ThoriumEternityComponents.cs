using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.Items.Accessories;
using ssm.Core;
using ssm.Thorium.Toggles;
using Terraria;
using Terraria.ModLoader;
using static ssm.Thorium.Enchantments.GeodeEnchant;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumEternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
			if (item.type == ModContent.ItemType<ColossusSoul>() || item.type == ModContent.ItemType<DimensionSoul>() || item.type == ModContent.ItemType<EternitySoul>() || item.type == ModContent.ItemType<StargateSoul>())
			{
				if (player.AddEffect<BlastShieldEffect>(item))
				{
				    ModContent.Find<ModItem>(ModCompatibility.Thorium.Name, "BlastShield").UpdateAccessory(player, hideVisual);
				}
			}

            if (item.type == ModContent.ItemType<WorldShaperSoul>() || item.type == ModContent.ItemType<DimensionSoul>() || item.type == ModContent.ItemType<EternitySoul>() || item.type == ModContent.ItemType<StargateSoul>())
            {
                player.AddEffect<GeodeEffect>(item);
            }
        }
    }
}