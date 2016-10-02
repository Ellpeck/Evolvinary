using Evolvinary.Main.Guis.Lists;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis.Lists{
    public class ListComponentRenderer{
        protected readonly ListComponent Component;

        public ListComponentRenderer(ListComponent component){
            this.Component = component;
        }

        public virtual void draw(RenderManager manager, GameTime time){
        }

        public virtual void drawUpper(RenderManager manager, GameTime time){
        }
    }
}