using Evolvinary.Main.Guis.Lists;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis.Lists{
    public class ListComponentRendererButton : ListComponentRenderer{
        public ListComponentRendererButton(ListComponent component) : base(component){
        }

        public override void draw(RenderManager manager, GameTime time){
            base.draw(manager, time);

            var component = this.Component as ListComponentButton;
            if(component != null){
                var renderer = component.Button.getRenderer();
                if(renderer != null){
                    renderer.draw(manager, time);
                }
            }
        }
    }
}