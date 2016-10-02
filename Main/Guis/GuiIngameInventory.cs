using Evolvinary.Launch;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Guis.Lists;
using Evolvinary.Rendering.Renderers.Guis;
using Evolvinary.Rendering.Renderers.Guis.Buttons;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiIngameInventory : GuiIngame{
        public ScrollList List;

        public GuiIngameInventory(PlayerData currentPlayer) : base(currentPlayer){
        }

        public override void onOpened(){
            base.onOpened();

            var height = getUnscaledHeight();
            this.List = new ScrollList(0, this, 65, height-200, 350, 200);
            this.List.addComponent(new ListComponentObjectPanel(-1, this, "This test", "This is a description hi hows it goin blah blah this needs to be longer than it currently is so that this actually works so yea"));
            this.List.addComponent(new ListComponentObjectPanel(-2, this, "Baguette", "Blah bla bla bla bla bla blah"));
            this.List.addComponent(new ListComponentObjectPanel(-3, this, "Fromage", "This is also a test"));
            this.List.addComponent(new ListComponentObjectPanel(-4, this, "Oui Oui", "I need a really long text to be able to fill this gap"));
            this.List.addComponent(new ListComponentObjectPanel(-5, this, "Out of ideas", "Yes really"));
            this.List.addComponent(new ListComponentObjectPanel(-6, this, "Silo", "This is a silo, it does siloy stuff"));
            this.List.addComponent(new ListComponentObjectPanel(-7, this, "Grass", "This is grass. It grows."));
            this.List.addComponent(new ListComponentObjectPanel(-8, this, "Indeed", "Yes, indeed!"));
            this.List.addComponent(new ListComponentObjectPanel(-9, this, "Forward", "Yo, this is actually going backwards but whatever"));
            this.List.addComponent(new ListComponentObjectPanel(-10, this, "Backwhat?", "I don't know either"));

            var invButton = this.ButtonList[1];
            invButton.setRenderer(new ButtonRendererStatic(invButton, new Rectangle(286, 0, 30, 30)));
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

            if(button.Id == 1){
                EvolvinaryMain.get().openGui(null);
            }
        }
    }
}