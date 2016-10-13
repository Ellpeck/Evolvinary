using Evolvinary.Helper;
using Evolvinary.Main.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis.Selection{
    public class GuiRendererSelection : GuiRenderer{
        public GuiRendererSelection(Gui gui) : base(gui){
        }

        public override void draw(RenderManager manager, GameTime time){
            base.draw(manager, time);

            var source = new Rectangle(0, 0, this.Gui.Area.Width, this.Gui.Area.Height);
            manager.Batch.Draw(GraphicsHelper.TranslucentWhite, this.Gui.Area, source, Color.Black);
        }
    }
}