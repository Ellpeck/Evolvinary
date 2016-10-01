using Evolvinary.Rendering.Renderers.Guis.Buttons;

namespace Evolvinary.Main.Guis.Buttons{
    public class ButtonTextOnly : Button{
        public readonly string DisplayText;

        public ButtonTextOnly(int id, Gui gui, string text, float scale) : this(id, gui, 0, 0, 0, 0, text, scale){
        }

        public ButtonTextOnly(int id, Gui gui, int posX, int posY, int width, int height, string text, float scale) : base(id, gui, posX, posY, width, height){
            this.DisplayText = text;

            this.setRenderer(new ButtonRendererText(this, scale));
        }
    }
}