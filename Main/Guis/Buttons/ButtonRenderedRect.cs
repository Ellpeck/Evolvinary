using Evolvinary.Rendering.Renderers.Guis.Buttons;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis.Buttons{
    public class ButtonRenderedRect : Button{
        public ButtonRenderedRect(int id, Gui gui, int posX, int posY, int sizeX, int sizeY, Rectangle renderRect) : base(id, gui, posX, posY, sizeX, sizeY){
            this.setRenderer(new ButtonRendererStatic(this, renderRect));
        }
    }
}