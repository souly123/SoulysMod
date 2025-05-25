using Microsoft.Xna.Framework;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;


namespace SoulysMod.Content.UI
{
    public class DraggableUIPanel : UIPanel
    {
        private Vector2 offset;
        private bool dragging;

        public DraggableUIPanel()
        {
            Width.Set(100f, 0f);
            Height.Set(100f, 0f);
            BackgroundColor = new Color(0, 0, 0, 0);
            BorderColor = new Color(0, 0, 0, 0);
            Left.Set(600f, 0f); // X position in pixels
            Top.Set(400f, 0f);  // Y position in pixels


            // Hook into left mouse events
            OnLeftMouseDown += DragStart;
            OnLeftMouseUp += DragEnd;
        }

        private void DragStart(UIMouseEvent evt, UIElement listeningElement)
        {
            offset = evt.MousePosition - new Vector2(Left.Pixels, Top.Pixels);
            dragging = true;
        }

        private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
        {
            dragging = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();
            }
        }
    }
}
