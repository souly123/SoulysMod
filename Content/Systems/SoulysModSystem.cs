using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SoulysMod.Content.UI;
using Terraria;

namespace SoulysMod
{
    public class SoulysModSystem : ModSystem
    {
        internal ConjurorSelectorUI conjurorUI;
        private UserInterface userInterface;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                conjurorUI = new ConjurorSelectorUI();
                conjurorUI.Activate();
                userInterface = new UserInterface();
                userInterface.SetState(conjurorUI);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            userInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
            if (index != -1)
            {
                layers.Insert(index + 1, new LegacyGameInterfaceLayer(
                    "SoulysMod: Conjuror Selector UI",
                    () =>
                    {
                        userInterface?.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }
    }
}
