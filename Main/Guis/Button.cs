using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class Button{
        private readonly ButtonRenderer defaultRenderer;

        private readonly Gui gui;
        public readonly Rectangle Area;
        public readonly int Id;

        public Button(int id, Gui gui, int posX, int posY, int sizeX, int sizeY, Rectangle renderRect){
            this.Id = id;
            this.gui = gui;
            this.Area = new Rectangle(posX, posY, sizeX, sizeY);
            this.defaultRenderer = new ButtonRenderer(this, renderRect);
        }

        public virtual void update(GameTime time){

        }

        public virtual ButtonRenderer getRenderer(){
            return this.defaultRenderer;
        }

        public bool isMouseOver(){
            return this.Area.Contains(this.gui.Input.getMousePos());
        }
    }
}