namespace Evolvinary.Main.Items{
    public class Inventory{
        private readonly Stack[] inv;

        public Inventory(int maxSize){
            this.inv = new Stack[maxSize];
        }

        public bool addExisting(Stack stack){
            var there = this.get(stack.Item);
            if(there != null){
                there.Amount += stack.Amount;
                return true;
            }
            return this.addNew(stack);
        }

        public bool addNew(Stack stack){
            for(var i = 0; i < this.inv.Length; i++){
                if(this.inv[i] == null){
                    this.inv[i] = stack;
                    return true;
                }
            }
            return false;
        }

        public Stack get(int place){
            if(place >= 0 && place < this.inv.Length){
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

        public void set(Stack stack, int place){
            if(place >= 0 && place < this.inv.Length){
                this.inv[place] = stack;
            }
        }

        public int size(){
            return this.inv.Length;
        }
    }
}