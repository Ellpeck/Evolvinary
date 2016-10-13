using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Main.Worlds.Entities.Paths;

namespace Evolvinary.Main.Guis.Selection{
    public class GuiSelectionCow : GuiSelection{
        public GuiSelectionCow(PlayerData currentPlayer, Entity entity) : base(currentPlayer, 100, 50, entity){
        }

        public override void onOpened(){
            base.onOpened();

            var entity = this.Entity as EntityPathable;
            if(entity != null){
                this.ButtonList.Add(new ButtonToggleMovement(0, this, this.Area.X+10, this.Area.Y+10, entity));
            }
        }
    }
}