using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Guis{
    public abstract class Gui{
        public int PosXOnScreen;
        public int PosYOnScreen;

        public virtual void update(GameTime time){

        }

        public abstract GuiRenderer getRenderer();

        public virtual void onOpened(){

        }

        public virtual void onClosed(){

        }
    }
}