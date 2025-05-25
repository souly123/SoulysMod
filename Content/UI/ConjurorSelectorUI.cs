using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using SoulysMod.Content.Players;
using Microsoft.Xna.Framework.Input.Touch;

namespace SoulysMod.Content.UI
{
    public class ConjurorSelectorUI : UIState
    {
        private UIImage itemIcon;

        public override void OnInitialize()
        {
            // DraggableUIPanel
            var panel = new DraggableUIPanel();
            panel.Width.Set(50f, 0f);
            panel.Height.Set(50f, 0f);
            panel.Left.Set(500f, 0f);
            panel.Top.Set(30f, 0f);

            // Custom Texture
            
            Texture2D texture = ModContent.Request<Texture2D>("SoulysMod/Assets/Textures/UI/ConjurorSelector").Value;
            
            itemIcon = new UIImage(texture);
            itemIcon.Width.Set(40f, 0f);
            itemIcon.Height.Set(40f, 0f);
            itemIcon.HAlign = 0.5f;
            itemIcon.VAlign = 0.5f;
        
            panel.Append(itemIcon);
            Append(panel);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Player player = Main.LocalPlayer;
            if (player.GetModPlayer<ConjurorPlayer>() is ConjurorPlayer conjuror &&
                conjuror.unlockedSummons.TryGetValue(conjuror.selectedSummonID, out var info))
            {
                itemIcon.SetImage(TextureAssets.Item[info.ItemID]);
            }
            else
            {
                itemIcon.SetImage(ModContent.Request<Texture2D>("SoulysMod/Assets/Textures/UI/ConjurorSelector").Value);
            }
        }
    }
}

