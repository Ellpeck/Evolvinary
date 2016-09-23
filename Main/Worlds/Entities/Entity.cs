using System;
using Evolvinary.Helper;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities{
    public class Entity{
        protected Random Rand = new Random();

        public World World;
        public Vector2 Pos;
        public EntityRenderer Renderer;

        public Entity(World world){
            this.setWorld(world);
        }

        public Entity attachRenderer(EntityRenderer renderer){
            this.Renderer = renderer;
            return this;
        }

        public void move(Vector2 move){
            this.setPosition(this.Pos+move);
        }

        public void move(float x, float y){
            this.move(new Vector2(x, y));
        }

        public void setPosition(Vector2 pos){
            this.switchChunk(pos, false);
            this.Pos = pos;
        }

        public void setWorld(World world){
            this.World = world;
            this.switchChunk(this.Pos, true);
        }

        private void switchChunk(Vector2 newPos, bool force){
            var oldChunk = this.World.getChunkFromWorldCoords(MathHelp.floor(this.Pos.X), MathHelp.floor(this.Pos.Y));
            var newChunk = this.World.getChunkFromWorldCoords(MathHelp.floor(newPos.X), MathHelp.floor(newPos.Y));

            if(force || oldChunk != newChunk){
                if(oldChunk != null){
                    oldChunk.Entities.Remove(this);
                }
                if(newChunk != null){
                    newChunk.Entities.Add(this);
                }
            }
        }

        public virtual void update(GameTime time){
        }

        public virtual bool canBeSelected(){
            return true;
        }
    }
}