using Evolvinary.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis{
    public class GuiRenderer{

        public Gui Gui;

        public GuiRenderer(Gui gui){
            this.Gui = gui;
        }

        public virtual void draw(GameTime time){

        }

        public virtual void onOpened(){

        }

        public virtual void onClosed(){

        }
    }
}