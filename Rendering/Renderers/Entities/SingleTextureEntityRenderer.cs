using Evolvinary.Launch;
using Evolvinary.Main.Worlds.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Rendering.Renderers.Entities{
    public class SingleTextureEntityRenderer : EntityRenderer{
        private readonly Texture2D texture;

        public SingleTextureEntityRenderer(string texturePath){
            this.texture = EvolvinaryMain.loadContent<Texture2D>("Textures/Entities/"+texturePath);
        }

        public override void draw(Entity entity, Vector2 pos, RenderManager manager, GameTime time){
            manager.Batch.Draw(this.texture, pos, Color.White);
        }

        public override void Dispose(){
            this.texture.Dispose();
        }
    }
}