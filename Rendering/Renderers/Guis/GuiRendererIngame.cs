using Evolvinary.Launch;
using Evolvinary.Main.Guis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Rendering.Renderers.Guis{
    public class GuiRendererIngame : GuiRenderer{
        public static Texture2D MenuTextures = EvolvinaryMain.loadContent<Texture2D>("Textures/Guis/Menu");

        public GuiRendererIngame(Gui gui) : base(gui){
        }

        public override void draw(RenderManager manager, GameTime time){
            base.draw(manager, time);
        }
    }
}