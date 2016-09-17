using System;
using Evolvinary.Launch;
using Evolvinary.Main.Worlds.Entities;
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
    }
}