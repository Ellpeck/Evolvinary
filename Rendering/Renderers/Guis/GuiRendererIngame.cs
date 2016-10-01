using Evolvinary.Launch;
using Evolvinary.Main.Guis;
using Evolvinary.Main.Guis.Lists;
using Evolvinary.Main.Input;
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
                var entity = gui.SelectedEntity;
                if(entity?.Renderer != null){
                    entity.Renderer.drawOverlay(entity, EvolvinaryMain.get().Camera.toCameraPos(entity.Pos) / Gui.Scale, InputProcessor.getMousePos().ToVector2(), manager, time);
                }

                drawHoveringOverlay(manager.Batch, gui.CurrentPlayer.MoneyCounter+" Moneys", 0, 0, Color.White, 0, false);
            }
        }

        protected void drawList(ScrollList list, RenderManager manager, GameTime time){
            var renderer = list.getRenderer();
            if(renderer != null){
                renderer.draw(manager, time);
            }
        }
    }
}