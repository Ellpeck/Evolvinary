using Evolvinary.Launch;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Input;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiIngame : Gui{
        public GuiIngame() : base(0, 0, getUnscaledWidth(), getUnscaledHeight()){
        }

        public override void onOpened(InputProcessor input){
            base.onOpened(input);

            var width = getUnscaledWidth();
            this.ButtonList.Add(new ButtonRenderedRect(0, this, width-32, 2, 30, 30, new Rectangle(256, 0, 30, 30)));
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererIngame(this);
        }

        public override void onActionPerformed(Button button){
            if(button.Id == 0){
                EvolvinaryMain.get().openGui(new GuiIngameMenu());
            }
        }

        public override bool allowCameraMovement(){
            return true;
        }
    }
}