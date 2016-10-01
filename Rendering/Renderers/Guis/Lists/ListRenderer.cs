using Evolvinary.Main.Guis.Lists;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis.Lists{
    public class ListRenderer{
        protected readonly ScrollList List;

        public ListRenderer(ScrollList list){
            this.List = list;
        }

        public virtual void draw(RenderManager manager, GameTime time){
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