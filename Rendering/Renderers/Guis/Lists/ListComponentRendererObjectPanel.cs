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
            var scale = this.Component.isMouseOver() ? 1.1F : 1F;
            var textColor = this.Component.IsSelected ? Color.Black : Color.White;
            var heightThird = area.Height / 3;

            var srcRect = new Rectangle(0, 0, area.Width, area.Height);
            GuiRenderer.drawRectWithScale(manager, GraphicsHelper.TranslucentWhite, area, srcRect, scale, this.Component.IsSelected ? Color.CornflowerBlue : Color.Black);

            var theTitle = this.getTitle();
            if(theTitle != null){
                var upperRect = new Rectangle(area.X, area.Y, area.Width, heightThird);
                GuiRenderer.drawCenteredText(manager, theTitle, scale * 1.5F, upperRect, true, textColor);
            }

            var theDesc = this.getDescription();
            if(theDesc != null){
                var lowerRect = new Rectangle(area.X, area.Y+heightThird+2, area.Width, heightThird * 2);
                var descToLength = GuiRenderer.splitTextToLength(theDesc, font, lowerRect.Width);
                foreach(var s in descToLength){
                    GuiRenderer.drawCenteredText(manager, s, scale, lowerRect, false, textColor);
                    lowerRect.Offset(0, font.LineSpacing);
                }
            }
        }
    }
}