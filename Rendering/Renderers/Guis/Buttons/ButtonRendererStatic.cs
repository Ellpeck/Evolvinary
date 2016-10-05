using Evolvinary.Main.Guis.Buttons;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis.Buttons{
    public class ButtonRendererStatic : ButtonRenderer{
        protected readonly Rectangle RenderRect;

        public ButtonRendererStatic(Button button, Rectangle renderRect) : base(button){
            this.RenderRect = renderRect;
        }

        public override void draw(RenderManager manager, GameTime time){
            GuiRenderer.drawRectWithScale(manager, GuiRendererIngame.MenuTextures, this.Button.Area, this.RenderRect, this.Button.isMouseOver() ? 1.25F : 1F, Color.White);
        }
    }
}