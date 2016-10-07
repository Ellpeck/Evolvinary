using Evolvinary.Main.Items;

namespace Evolvinary.Main{
    public class PlayerData{
        public Inventory Inventory = new Inventory(15);
        public int Money = 2000000;

        public PlayerData(){
            this.Inventory.add(new Stack(GameData.ItemGrass, 60));
            this.Inventory.add(new Stack(GameData.ItemSilo, 15));
            this.Inventory.add(new Stack(GameData.ItemCow, 10));
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