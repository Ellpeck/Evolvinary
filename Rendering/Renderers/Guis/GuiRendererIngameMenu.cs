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
                var size = new Point(amount, this.Gui.Area.Width);
                var source = new Rectangle(0, 0, amount, 128);
                manager.Batch.Draw(GraphicsHelper.WhiteGradient, new Rectangle(this.Gui.Area.Location, size), source, Color.Black);
            }

            base.draw(manager, time);
        }
    }
}