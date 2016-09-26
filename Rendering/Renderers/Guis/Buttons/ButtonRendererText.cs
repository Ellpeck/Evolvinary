using Evolvinary.Main.Guis.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Rendering.Renderers.Guis.Buttons{
    public class ButtonRendererText : ButtonRenderer{
        private readonly float scale;

        public ButtonRendererText(Button button, float scale) : base(button){
            this.scale = scale;
        }

        public override void draw(RenderManager manager, GameTime time){
            base.draw(manager, time);

            var button = this.Button as ButtonTextOnly;
            if(button != null){
                var font = manager.NormalFont;
                var text = button.DisplayText;
                var scaleFactor = button.isMouseOver() ? this.scale+0.5F : this.scale;

                var pos = button.Area.Location;
                var x = pos.X+button.Area.Width / 2-font.MeasureString(text).X * scaleFactor / 2;
                var y = pos.Y+button.Area.Height / 2-font.LineSpacing * scaleFactor / 2;

                manager.Batch.DrawString(font, text, new Vector2(x, y), Color.White, 0F, Vector2.Zero, scaleFactor, SpriteEffects.None, 0F);
            }
        }
    }
}