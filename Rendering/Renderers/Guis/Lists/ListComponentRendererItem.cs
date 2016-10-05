using Evolvinary.Main.Guis.Lists;

namespace Evolvinary.Rendering.Renderers.Guis.Lists{
    public class ListComponentRendererItem : ListComponentRendererObjectPanel{
        public ListComponentRendererItem(ListComponent component) : base(component, null, null){
        }

        public override string getDescription(){
            var component = this.Component as ListComponentItem;
            if(component != null){
                var stack = component.getStack();
                if(stack?.Item != null){
                    return stack.Item.getDescription();
                }
            }
            return null;
        }

        public override string getTitle(){
            var component = this.Component as ListComponentItem;
            if(component != null){
                var stack = component.getStack();
                if(stack?.Item != null){
                    return stack.Item.getTitle()+" x"+stack.Amount;
                }
            }
            return null;
        }
    }
}