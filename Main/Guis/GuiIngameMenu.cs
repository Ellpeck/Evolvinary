using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Input;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiIngameMenu : Gui{
        public int FadeTime;
        public bool ShouldClose;

        public GuiIngameMenu() : base(0, 0, getUnscaledWidth(), getUnscaledHeight()){
        }

        public override void onOpened(InputProcessor input){
            base.onOpened(input);

            var width = getUnscaledWidth();
            this.ButtonList.Add(new ButtonRenderedRect(0, this, width-32, 2, 30, 30, new Rectangle(286, 0, 30, 30)));
        }

        public override void update(GameTime time){
            base.update(time);

            if(this.FadeTime < 256){
                this.FadeTime+=MathHelp.ceil((256-this.FadeTime)/10D);

                if(this.FadeTime >= 256){
                    if(this.ShouldClose){
                        EvolvinaryMain.get().openGui(null);
                    }
                    else{

                    }
                }
            }
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererIngameMenu(this);
        }

        public override void onActionPerformed(Button button){
            if(button.Id == 0 && !this.ShouldClose){
                this.ShouldClose = true;
                this.FadeTime = 0;
            }
        }
    }
}