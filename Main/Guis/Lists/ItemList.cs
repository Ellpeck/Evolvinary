using Evolvinary.Main.Items;

namespace Evolvinary.Main.Guis.Lists{
    public class ItemList : ScrollList{
        public ItemList(int startId, Gui gui, int posX, int posY, int sizeX, int sizeY, Inventory inventory) : base(gui, posX, posY, sizeX, sizeY){
            for(var i = 0; i < inventory.size(); i++){
                this.addComponent(new ListComponentItem(startId+i, gui, inventory, i));
            }
        }
    }
}