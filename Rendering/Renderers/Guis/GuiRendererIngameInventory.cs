using Evolvinary.Main.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis{
    public class GuiRendererIngameInventory : GuiRendererIngame{

        public GuiRendererIngameInventory(Gui gui) : base(gui){
        }

        public override void draw(RenderManager manager, GameTime time){
            base.draw(manager, time);

            var gui = this.Gui as GuiIngameInventory;
            if(gui != null){
                this.drawList(gui.List, manager, time);
            }
        }
    }
}