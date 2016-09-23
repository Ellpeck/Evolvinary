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

            var gui = this.Gui as GuiIngame;
            if(gui != null){
                if(gui.ButtonList[0].isMouseOver()){
                    drawHoveringOverlayAtMouse(manager.Batch, "View Menu", Color.White);
                }

                var entity = gui.SelectedEntity;
                if(entity?.Renderer != null){
                    entity.Renderer.drawOverlay(entity, EvolvinaryMain.get().Camera.toCameraPos(entity.Pos)/Gui.Scale, this.Gui.Input.getMousePos().ToVector2(), manager, time);
                }
            }
        }
    }
}