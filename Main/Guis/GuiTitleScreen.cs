using Evolvinary.Rendering.Renderers.Guis;

namespace Evolvinary.Main.Guis{
    public class GuiTitleScreen : Gui{
        public GuiTitleScreen(int posX, int posY, int sizeX, int sizeY) : base(posX, posY, sizeX, sizeY){
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererTitleScreen(this);
        }
    }
}