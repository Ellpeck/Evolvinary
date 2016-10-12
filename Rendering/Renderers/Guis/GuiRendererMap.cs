using System;
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

                var endX = Math.Min(gui.MapData.GetLength(0), Gui.getUnscaledWidth() / tileSize);
                var endY = Math.Min(gui.MapData.GetLength(1), Gui.getUnscaledHeight() / tileSize);

                for(var x = 0; x < endX; x++){
                    for(var y = 0; y < endY; y++){
                        var pos = new Vector2(x * tileSize, y * tileSize);
                        manager.Batch.Draw(GraphicsHelper.SolidWhite, pos, srcRect, gui.MapData[x, y]);
                    }
                }
            }
        }
    }
}