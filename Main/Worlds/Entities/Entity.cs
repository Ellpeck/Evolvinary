using System;
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
            var oldChunk = this.World.getChunkFromWorldCoords((int) this.Pos.X, (int) this.Pos.Y);
            var newChunk = this.World.getChunkFromWorldCoords((int) newPos.X, (int) newPos.Y);

            if(force || oldChunk != newChunk){
                if(oldChunk != null){
                    oldChunk.Entities.Remove(this);
                }

                newChunk.Entities.Add(this);
            }
        }

        public virtual void update(GameTime time){

        }
    }
}