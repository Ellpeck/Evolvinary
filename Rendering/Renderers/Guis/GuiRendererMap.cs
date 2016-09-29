using Evolvinary.Helper;
using Evolvinary.Main.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis{
    public class GuiRendererMap : GuiRenderer{
        public GuiRendererMap(Gui gui) : base(gui){
        }

        public override bool shouldRenderWorld(){
            return false;
        }

        public override void draw(RenderManager manager, GameTime time){
            base.draw(manager, time);

            var gui = this.Gui as GuiMap;
            if(gui != null){
                const int tileSize = 5;
                var srcRect = new Rectangle(0, 0, tileSize, tileSize);
                for(var x = 0; x < gui.MapData.GetLength(0); x++){
                    for(var y = 0; y < gui.MapData.GetLength(1); y++){
                        manager.Batch.Draw(GraphicsHelper.SolidWhite, new Vector2(x * tileSize, y * tileSize), srcRect, gui.MapData[x, y]);
                    }
                }
            }
        }
    }
}