using Evolvinary.Helper;
using Evolvinary.Main.Guis.Lists;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis.Lists{
    public class ListRenderer{
        protected readonly ScrollList List;

        public ListRenderer(ScrollList list){
            this.List = list;
        }

        public virtual void draw(RenderManager manager, GameTime time){
            var area = this.List.Area;
            var srcRect = new Rectangle(0, 0, area.Width, area.Height);
            var drawArea = new Rectangle(area.X-5, area.Y-5, area.Width+10, area.Height+10);
            manager.Batch.Draw(GraphicsHelper.TranslucentWhite, drawArea, srcRect, Color.Black);

            foreach(var component in this.List.Components){
                if(component.IsVisible){
                    var renderer = component.getRenderer();
                    if(renderer != null){
                        renderer.draw(manager, time);
                    }
                }
            }
        }
    }
}