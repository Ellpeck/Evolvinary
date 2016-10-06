using System;
using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Input;
using Evolvinary.Main.Input.Setting;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiIngameMenu : Gui{
        public int FadeTime;
        public bool ShouldClose;
        private bool hasButtons;

        public GuiIngameMenu(PlayerData currentPlayer) : base(currentPlayer, 0, 0, getUnscaledWidth(), getUnscaledHeight()){
        }

        public override void onOpened(){
            base.onOpened();

            var width = getUnscaledWidth();
            this.ButtonList.Add(new ButtonRenderedRect(0, this, width-40, 10, 30, 30, new Rectangle(286, 0, 30, 30)));
        }

        public override void update(GameTime time){
            base.update(time);

            if(this.FadeTime < 256){
                this.FadeTime += MathHelp.ceil((256-this.FadeTime) / 10D);

                if(this.FadeTime >= 256){
                    if(this.ShouldClose){
                        EvolvinaryMain.get().openGui(null);
                    }
                    else{
                        this.ButtonList.Add(new ButtonTextOnly(1, this, 60, 30, 150, 20, "Save Game", 2F));
                        this.ButtonList.Add(new ButtonTextOnly(2, this, 60, 80, 150, 20, "Load Game", 2F));
                        this.ButtonList.Add(new ButtonTextOnly(3, this, 60, 130, 150, 20, "Start New Game", 2F));
                        this.ButtonList.Add(new ButtonTextOnly(4, this, 60, 180, 150, 20, "Options", 2F));
                        this.ButtonList.Add(new ButtonTextOnly(5, this, 60, 230, 150, 20, "To Title Screen", 2F));
                        this.ButtonList.Add(new ButtonTextOnly(6, this, 60, 280, 150, 20, "Quit Game", 2F));
                        this.hasButtons = true;
                    }
                }
            }
        }

        public override void onActionPerformed(Button button){
            switch(button.Id){
                case 0:
                    this.onTryClose();
                    break;
                case 6:
                    EvolvinaryMain.get().Exit();
                    break;
            }
        }

        public override void onTryClose(){
            if(!this.ShouldClose){
                this.ShouldClose = true;
                this.FadeTime = 0;

                if(this.hasButtons){
                    for(var i = 0; i < 6; i++){
                        this.ButtonList.RemoveAt(1);
                    }
                    this.hasButtons = false;
                }
            }
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererIngameMenu(this);
        }
    }
}