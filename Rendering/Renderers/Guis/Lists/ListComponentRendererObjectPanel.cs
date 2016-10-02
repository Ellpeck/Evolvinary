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

        public override void draw(RenderManager manager, GameTime time){
            base.draw(manager, time);
            var font = manager.NormalFont;
            var moused = this.Component.isMouseOver();
            var area = this.Component.CurrentArea;
            var scale = moused ? 1.1F : 1F;

            var srcRect = new Rectangle(0, 0, area.Width, area.Height);
            GuiRenderer.drawRectWithScale(manager, GraphicsHelper.TranslucentGray, area, srcRect, scale);

            var heightThird = area.Height / 3;
            var upperRect = new Rectangle(area.X, area.Y, area.Width, heightThird);
            GuiRenderer.drawCenteredText(manager, this.title, scale*1.5F, upperRect, true);

            var lowerRect = new Rectangle(area.X, area.Y+heightThird+2, area.Width, heightThird*2);
            var descToLength = GuiRenderer.splitTextToLength(this.description, font, lowerRect.Width);
            foreach(var s in descToLength){
                GuiRenderer.drawCenteredText(manager, s, scale, lowerRect, false);
                lowerRect.Offset(0, font.LineSpacing);
            }
        }
    }
}