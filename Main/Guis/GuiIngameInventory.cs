using Evolvinary.Rendering.Renderers.Guis;

namespace Evolvinary.Main.Guis{
    public class GuiIngameInventory : GuiIngame{
        public GuiIngameInventory(PlayerData currentPlayer) : base(currentPlayer){
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererIngameInventory(this);
        }
    }
}