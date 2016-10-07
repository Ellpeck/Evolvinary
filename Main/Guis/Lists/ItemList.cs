using Evolvinary.Main.Items;

namespace Evolvinary.Main.Guis.Lists{
    public class ItemList : ScrollList{
        private readonly int startId;
        private readonly Inventory inventory;

        public ItemList(int startId, Gui gui, int posX, int posY, int sizeX, int sizeY, Inventory inventory) : base(gui, posX, posY, sizeX, sizeY){
            this.startId = startId;
            this.inventory = inventory;

            this.redefineAreas();
        }

        public override void redefineAreas(){
            this.Components.Clear();
            for(var i = 0; i < this.inventory.size(); i++){
                this.Components.Add(new ListComponentItem(startId+i, this.Gui, inventory, i));
            }

            base.redefineAreas();
        }
    }
}