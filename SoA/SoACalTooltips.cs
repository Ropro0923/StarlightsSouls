﻿//using FargowiltasSouls.Content.Items.Accessories.Souls;
//using System.Collections.Generic;
//using Terraria.Localization;
//using Terraria;
//using Terraria.ModLoader;
//using ssm.Core;

//namespace ssm.SoA
//{
//    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Calamity.Name)]
//    public class SoACalTooltips : GlobalItem
//    {
//        static string ExpandedTooltipLoc(string line) => Language.GetTextValue($"Mods.ssm.ExpandedTooltips.{line}");
//        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
//        {
//            string key = "Mods.ssm.Items.AddedEffects.";

//            if (item.type == ModContent.ItemType<MasochistSoul>() && !item.social)
//            {
//                tooltips.Insert(9, new TooltipLine(Mod, "SoARampartDeities", Language.GetTextValue(key + "SoARampart")));
//            }
//        }
//    }
//}
