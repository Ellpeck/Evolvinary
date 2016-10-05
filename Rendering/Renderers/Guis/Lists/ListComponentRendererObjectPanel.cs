using Evolvinary.Helper;
using Evolvinary.Main.Guis.Lists;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis.Lists{
    public class ListComponentRendererObjectPanel : ListComponentRendererButton{
        private readonly string title;
        private readonly string description;

        public ListComponentRendererObjectPanel(ListComponent component, string title, string description) : base(component){
            this.title = title;
            this.description = description;
        }

        public virtual string getTitle(){
            return this.title;
        }

        public virtual string getDescription(){
            return this.description;
        }

        public override void draw(RenderManager manager, GameTime time){
            base.draw(manager, time);
            var font = manager.NormalFont;
            var area = this.Component.CurrentArea;
            var heightThird = area.Height / 3;

            var srcRect = new Rectangle(0, 0, area.Width, area.Height);
            manager.Batch.Draw(GraphicsHelper.TranslucentWhite, area, srcRect, this.Component.isMouseOver() ? Color.DarkSlateGray : Color.Black);

            if(this.Component.IsSelected){
                var selectRect = new Rectangle(area.X, area.Y, 15, area.Height);
                manager.Batch.Draw(GraphicsHelper.TranslucentWhite, selectRect, srcRect, Color.DarkGreen);
            }

            var theTitle = this.getTitle();
            if(theTitle != null){
                var upperRect = new Rectangle(area.X, area.Y, area.Width, heightThird);
                GuiRenderer.drawCenteredText(manager, theTitle, 1.5F, upperRect, true, Color.White);
            }

            var theDesc = this.getDescription();
            if(theDesc != null){
                var lowerRect = new Rectangle(area.X+20, area.Y+heightThird+2, area.Width-40, heightThird * 2);
                var descToLength = GuiRenderer.splitTextToLength(theDesc, font, lowerRect.Width);
                foreach(var s in descToLength){
                    GuiRenderer.drawCenteredText(manager, s, 1F, lowerRect, false, Color.White);
                    lowerRect.Offset(0, font.LineSpacing);
                }
            }
        }
    }
}