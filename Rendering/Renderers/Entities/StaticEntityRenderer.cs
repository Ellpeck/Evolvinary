using Evolvinary.Main.Worlds.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Entities{
    public class StaticEntityRenderer : EntityRenderer{

        private readonly Rectangle textureRect;

        public StaticEntityRenderer(Rectangle textureRect){
            this.textureRect = textureRect;
        }

        public override void draw(Entity entity, Vector2 pos, RenderManager manager, GameTime time){
            manager.Batch.Draw(manager.StaticEntityTexture, pos, this.textureRect, Color.White);
        }
    }
}