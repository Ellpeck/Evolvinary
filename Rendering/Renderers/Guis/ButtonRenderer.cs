using Evolvinary.Main.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis{
    public class ButtonRenderer{
        private readonly Button button;
        private readonly Rectangle renderRect;

        public ButtonRenderer(Button button, Rectangle renderRect){
            this.button = button;
            this.renderRect = renderRect;
        }

        public virtual void draw(RenderManager manager, GameTime time){
            manager.Batch.Draw(GuiRendererIngame.MenuTextures, this.button.Area, this.renderRect, Color.White);
        }
    }
}