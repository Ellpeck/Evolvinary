using System.Collections.Generic;

namespace Evolvinary.Main.Items{
    public class Inventory{
        private readonly List<Stack> inv = new List<Stack>();
        private readonly int maxSize;

        public Inventory(int maxSize){
            this.maxSize = maxSize;
        }

        public bool add(Stack stack){
            var there = this.get(stack.Item);
            if(there != null){
                there.Amount += stack.Amount;
                return true;
            }
            return this.addNew(stack);
        }

        public bool addNew(Stack stack){
            if(this.size() < this.maxSize){
                this.inv.Add(stack);
                return true;
            }
            return false;
        }

        public Stack get(int place){
            if(place >= 0 && place < this.inv.Count){
                return this.inv[place];
            }
            return null;
        }

        public Stack get(Item item){
            foreach(var stack in this.inv){
                if(stack != null && stack.Item == item){
                    return stack;
                }
            }
            return null;
        }

        public void remove(Stack stack){
            if(this.inv.Contains(stack)){
                this.inv.Remove(stack);
            }
        }

        public int size(){
            return this.inv.Count;
        }
    }
}