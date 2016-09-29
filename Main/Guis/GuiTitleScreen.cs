using Evolvinary.Rendering.Renderers.Guis;

namespace Evolvinary.Main.Guis{
    public class GuiTitleScreen : Gui{
        public GuiTitleScreen(PlayerData currentPlayer, int posX, int posY, int sizeX, int sizeY) : base(currentPlayer, posX, posY, sizeX, sizeY){
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererTitleScreen(this);
        }
    }
}