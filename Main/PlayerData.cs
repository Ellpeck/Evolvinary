using Evolvinary.Main.Items;

namespace Evolvinary.Main{
    public class PlayerData{
        public Inventory Inventory = new Inventory(15);
        public int Money = 20000;

        public PlayerData(){
            this.Inventory.addNew(new Stack(GameData.ItemGrass, 10));
            this.Inventory.addExisting(new Stack(GameData.ItemGrass, 15));
            this.Inventory.addExisting(new Stack(GameData.ItemSilo, 15));
        }

        public bool requestMoney(int amount, bool extract){
            if(this.Money >= amount){
                if(extract){
                    this.Money -= amount;
                }
                return true;
            }
            return false;
        }
    }
}