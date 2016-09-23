using Evolvinary.Main.Guis.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Rendering.Renderers.Guis.Buttons{
    public class ButtonRendererStatic : ButtonRenderer{
        protected readonly Rectangle RenderRect;

        public ButtonRendererStatic(Button button, Rectangle renderRect) : base(button){
            this.RenderRect = renderRect;
        }

        public override void draw(RenderManager manager, GameTime time){
            var location = this.Button.Area.Location.ToVector2();
            var halfSize = this.Button.Area.Size.ToVector2() / 2;

            var scaleFactor = this.Button.isMouseOver() ? 1.25F : 1;
            var render = location-halfSize * scaleFactor+halfSize;

            manager.Batch.Draw(GuiRendererIngame.MenuTextures, render, this.RenderRect, Color.White, 0F, Vector2.Zero, scaleFactor, SpriteEffects.None, 0F);
        }
    }
}