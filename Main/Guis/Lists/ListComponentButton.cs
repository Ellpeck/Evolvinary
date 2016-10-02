using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Rendering.Renderers.Guis.Lists;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis.Lists{
    public class ListComponentButton : ListComponent{
        public readonly Button Button;

        public ListComponentButton(Gui gui, int height, Button button) : base(gui, height){
            this.Button = button;

            this.setRenderer(new ListComponentRendererButton(this));
        }

        public override void update(GameTime time){
            base.update(time);
            this.Button.update(time);
        }

        public override void redefineArea(Rectangle area){
            base.redefineArea(area);
            this.Button.Area = area;
        }

        public override void onClicked(){
            base.onClicked();
            this.Gui.onActionPerformed(this.Button);
        }
    }
}