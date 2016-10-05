using Evolvinary.Main.Guis.Buttons;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis.Buttons{
    public class ButtonRendererText : ButtonRenderer{
        private readonly float scale;

        public ButtonRendererText(Button button, float scale) : base(button){
            this.scale = scale;
        }

        public override void draw(RenderManager manager, GameTime time){
            base.draw(manager, time);

            var button = this.Button as ButtonTextOnly;
            if(button != null){
                GuiRenderer.drawCenteredText(manager, button.DisplayText, button.isMouseOver() ? this.scale+0.5F : this.scale, button.Area, true, Color.White);
            }
        }
    }
}