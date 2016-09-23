using Evolvinary.Helper;
using Evolvinary.Main.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis{
    public class GuiRendererIngameMenu : GuiRenderer{
        public GuiRendererIngameMenu(Gui gui) : base(gui){
        }

        public override void draw(RenderManager manager, GameTime time){
            var gui = this.Gui as GuiIngameMenu;
            if(gui != null){
                var amount = gui.ShouldClose ? 256-gui.FadeTime : gui.FadeTime;
                var size = new Point(amount, this.Gui.SizeY);
                var source = new Rectangle(0, 0, amount, 128);
                manager.Batch.Draw(GraphicsHelper.Graydient, new Rectangle(this.Gui.Pos.ToPoint(), size), source, Color.White);
            }

            base.draw(manager, time);

            if(this.Gui.ButtonList[0].isMouseOver()){
                drawHoveringOverlayAtMouse(manager.Batch, "Close Menu", Color.White);
            }
        }
    }
}