using Evolvinary.Main.Items;

namespace Evolvinary.Main{
    public class PlayerData{
        public Inventory Inventory = new Inventory(15);

        public PlayerData(){
            this.Inventory.add(new Stack(GameData.ItemGrass, 6000));
            this.Inventory.add(new Stack(GameData.ItemSilo, 15));
            this.Inventory.add(new Stack(GameData.ItemCow, 10));
        }
    }
}