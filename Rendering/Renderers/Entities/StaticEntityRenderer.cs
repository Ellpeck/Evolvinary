using Evolvinary.Main.Worlds.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Entities{
    public class StaticEntityRenderer : EntityRenderer{

        private readonly Rectangle textureRect;

        public StaticEntityRenderer(Rectangle textureRect){
            this.textureRect = textureRect;
        }

        public override void draw(Entity entity, Vector2 pos, RenderManager manager, GameTime time){
            var renderPos = new Vector2(pos.X-this.textureRect.Width/2, pos.Y-this.textureRect.Height/2);
            manager.Batch.Draw(manager.StaticEntityTexture, renderPos, this.textureRect, Color.White);
        }
    }
}