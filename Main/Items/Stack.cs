using Evolvinary.Main.Worlds.Entities;

namespace Evolvinary.Main.Items{
    public class Stack{
        public Item Item;
        public int Amount;

        public Stack(Item item, int amount){
            this.Item = item;
            this.Amount = amount;
        }

        public EntityPlaceable getHeldEntity(){
            var holder = this.Item as ItemEntityHolder;
            return holder != null ? holder.createEntity() : null;
        }
    }
}