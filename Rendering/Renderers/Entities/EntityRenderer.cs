using System;
using Evolvinary.Launch;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Entities{
    public class EntityRenderer : IDisposable{
        public virtual void draw(Entity entity, Vector2 pos, RenderManager manager, GameTime time){
        }

        public virtual void Dispose(){
        }

        public EntityRenderer register(){
            EvolvinaryMain.get().RenderManager.EntityRenderers.Add(this);
            return this;
        }

        public virtual void drawOverlay(Entity entity, Vector2 posOnScreen, Vector2 mousePos, RenderManager manager, GameTime time){
            GuiRenderer.drawHoveringOverlay(manager.Batch, "My coordinates in the world are "+entity.Pos+"!", (int) posOnScreen.X, (int) posOnScreen.Y, Color.White, 450, true);
        }
    }
}