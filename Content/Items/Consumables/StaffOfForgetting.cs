using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using SoulysMod.Content.Players;

namespace SoulysMod.Content.Items.Consumables
{
    public class StaffOfForgetting : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip", "Use to forget all conjured summons."));
        }


        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 10);
        }

        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<ConjurorPlayer>() is ConjurorPlayer conjuror)
            {
                conjuror.unlockedSummons.Clear();
                Main.NewText("Your conjured knowledge has been purged.", Color.MediumPurple);
                SoundEngine.PlaySound(SoundID.Item84, player.position);
                CombatText.NewText(player.Hitbox, Color.Red, "Mind blanked");
            }

            return true;
        }
        public override string Texture => "SoulysMod/Assets/Textures/Items/Consumables/StaffOfForgetting";

    }
}

