using Evolvinary.Main.Guis.Buttons;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis.Buttons{
    public class ButtonRenderer{
        protected readonly Button Button;

        public ButtonRenderer(Button button){
            this.Button = button;
        }

        public virtual void draw(RenderManager manager, GameTime time){
        }
    }
}