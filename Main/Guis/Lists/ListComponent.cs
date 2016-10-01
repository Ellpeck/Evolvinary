using Evolvinary.Main.Input;
using Evolvinary.Rendering.Renderers.Guis.Lists;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis.Lists{
    public class ListComponent{
        private ListComponentRenderer renderer;

        protected readonly Gui Gui;
        public Rectangle CurrentArea;
        public readonly int Height;
        public bool IsVisible;

        public ListComponent(Gui gui, int height){
            this.Gui = gui;
            this.Height = height;
        }

        public bool isMouseOver(){
            return this.CurrentArea.Contains(InputProcessor.getMousePos().ToVector2() / Gui.Scale);
        }

        public virtual void update(GameTime time){
        }

        public void setRenderer(ListComponentRenderer renderer){
            this.renderer = renderer;
        }

        public ListComponentRenderer getRenderer(){
            return this.renderer;
        }

        public virtual void onClicked(){

        }
    }
}