using Evolvinary.Main.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis{
    public class GuiRenderer{

        public Gui Gui;

        public GuiRenderer(Gui gui){
            this.Gui = gui;
        }

        public virtual void draw(RenderManager manager, GameTime time){
            foreach(var button in Gui.ButtonList){
                var renderer = button.getRenderer();
                if(renderer != null){
                    renderer.draw(manager, time);
                }
            }
        }

        public virtual void onOpened(){

        }

        public virtual void onClosed(){

        }
    }
}