﻿using Evolvinary.Launch;
using Evolvinary.Main.Input;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiIngameMenu : Gui{
        public GuiIngameMenu() : base(0, 0, EvolvinaryMain.get().RenderManager.getScreenWidth(), EvolvinaryMain.get().RenderManager.getScreenHeight()){
        }

        public override void onOpened(InputProcessor input){
            base.onOpened(input);

            var width = EvolvinaryMain.get().RenderManager.getScreenWidth();
            this.ButtonList.Add(new Button(0, this, width-65, 5, 60, 60, new Rectangle(286, 0, 30, 30)));
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererIngameMenu(this);
        }

        public override void onActionPerformed(Button button){
            if(button.Id == 0){
                EvolvinaryMain.get().openGui(null);
            }
        }
    }
}