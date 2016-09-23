using Evolvinary.Main.Input;
using Evolvinary.Rendering.Renderers.Guis.Buttons;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis.Buttons{
    public class Button{
        private ButtonRenderer renderer;

        private readonly Gui gui;
        public readonly Rectangle Area;
        public readonly int Id;

        public Button(int id, Gui gui, int posX, int posY, int sizeX, int sizeY){
            this.Id = id;
            this.gui = gui;
            this.Area = new Rectangle(posX, posY, sizeX, sizeY);
        }

        public virtual void update(GameTime time){
        }

        public void setRenderer(ButtonRenderer renderer){
            this.renderer = renderer;
        }

        public ButtonRenderer getRenderer(){
            return this.renderer;
        }

        public bool isMouseOver(){
            return this.Area.Contains(InputProcessor.getMousePos().ToVector2()/Gui.Scale);
        }
    }
}