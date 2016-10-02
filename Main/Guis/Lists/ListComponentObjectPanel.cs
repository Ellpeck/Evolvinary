using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Rendering.Renderers.Guis.Lists;

namespace Evolvinary.Main.Guis.Lists{
    public class ListComponentObjectPanel : ListComponentButton{
        public ListComponentObjectPanel(int id, Gui gui, string title, string description) : base(gui, 70, new Button(id, gui, 0, 0, 0, 0)){
            this.setRenderer(new ListComponentRendererObjectPanel(this, title, description));
        }
    }
}