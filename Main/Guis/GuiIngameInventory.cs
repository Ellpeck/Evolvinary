using System;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Guis.Lists;
using Evolvinary.Rendering.Renderers.Guis;
using Evolvinary.Rendering.Renderers.Guis.Lists;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiIngameInventory : GuiIngame{

        public ScrollList List;

        public GuiIngameInventory(PlayerData currentPlayer) : base(currentPlayer){
        }

        public override void onOpened(){
            base.onOpened();

            this.List = new ScrollList(0, this, 10, 10, 50, 80);
            this.List.addComponent(new ListComponentButton(this, 20, new ButtonTextOnly(-1, this, "Blah", 1F)));
            this.List.addComponent(new ListComponentButton(this, 20, new ButtonTextOnly(-2, this, "Test", 1.5F)));
            this.List.addComponent(new ListComponentButton(this, 20, new ButtonTextOnly(-3, this, "My face", 1F)));
            this.List.addComponent(new ListComponentButton(this, 20, new ButtonTextOnly(-4, this, "Dickbutt", 0.75F)));
            this.List.addComponent(new ListComponentButton(this, 20, new ButtonTextOnly(-5, this, "Hello", 1F)));
            this.List.addComponent(new ListComponentButton(this, 20, new ButtonTextOnly(-6, this, "Money", 1F)));
        }

        public override void update(GameTime time){
            base.update(time);
            this.List.update(time);
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererIngameInventory(this);
        }

        public override bool doesGameGoOn(){
            return false;
        }

        public override bool canMoveCamera(){
            return !this.List.isMouseOver();
        }

        public override void onActionPerformed(Button button){
            base.onActionPerformed(button);

            Console.WriteLine(button.Id);
        }
    }
}