using Evolvinary.Main.Items;
using Evolvinary.Rendering.Renderers.Guis.Lists;

namespace Evolvinary.Main.Guis.Lists{
    public class ListComponentItem : ListComponentObjectPanel{
        private readonly Inventory inventory;
        private readonly int index;

        public ListComponentItem(int id, Gui gui, Inventory inventory, int index) : base(id, gui, null, null){
            this.inventory = inventory;
            this.index = index;

            this.setRenderer(new ListComponentRendererItem(this));
        }

        public Stack getStack(){
            return this.inventory.get(this.index);
        }

        public void setStack(Stack stack){
            this.inventory.set(stack, this.index);
        }
    }
}