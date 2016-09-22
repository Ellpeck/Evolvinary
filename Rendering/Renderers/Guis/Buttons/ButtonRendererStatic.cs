using Evolvinary.Main.Guis.Buttons;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis.Buttons{
    public class ButtonRendererStatic : ButtonRenderer{
        protected readonly Rectangle RenderRect;
        public ButtonRendererStatic(Button button, Rectangle renderRect) : base(button){
            this.RenderRect = renderRect;
        }

        public override void draw(RenderManager manager, GameTime time){
            manager.Batch.Draw(GuiRendererIngame.MenuTextures, this.Button.Area.Location.ToVector2(), this.RenderRect, Color.White);
        }
    }
}