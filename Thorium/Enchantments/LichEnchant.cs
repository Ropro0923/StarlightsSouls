using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.Painting;
using ssm.Core;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.SummonItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.BossLich;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.PlagueDoctorEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LichEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            //plague effect
            if (player.AddEffect<PlagueEffect>(Item))
            {
                thoriumPlayer.setPlague = true;
            }
            //lich effect
            if (player.AddEffect<LichEffect>(Item))
            {
                thoriumPlayer.setLich = true;
            }

            ModContent.Find<ModItem>(this.thorium.Name, "Phylactery").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LichCowl>());
            recipe.AddIngredient(ModContent.ItemType<LichCarapace>());
            recipe.AddIngredient(ModContent.ItemType<LichTalon>());
            recipe.AddIngredient(ModContent.ItemType<PlagueDoctorEnchant>());
            recipe.AddIngredient(ModContent.ItemType<Phylactery>());
            recipe.AddIngredient(ModContent.ItemType<SoulCleaver>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class LichEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LichEnchant>();
        }
    }
}
