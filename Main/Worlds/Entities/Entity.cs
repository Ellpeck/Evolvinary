using System;
using Evolvinary.Helper;
using Evolvinary.Main.Guis;
using Evolvinary.Main.Guis.Selection;
using Evolvinary.Main.Worlds.Entities.Paths;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities{
    public class Entity{
        protected Random Rand = new Random();

        public World World;
        public Vector2 Pos;

        public BoundBox BoundingBox;
        public BoundBox MouseSelectBox;

        public EntityRenderer CurrentRenderer;
        public Color RenderColor = Color.White;
        public float RenderScale = 1F;

        public bool Dead;

        public Entity(){
            this.BoundingBox = this.getBoundBox();
            this.MouseSelectBox = this.getMouseSelectBox();
        }

        public Entity attachRenderer(EntityRenderer renderer){
            this.CurrentRenderer = renderer;
            return this;
        }

        public virtual BoundBox getBoundBox(){
            return BoundBox.Empty;
        }

        public virtual BoundBox getMouseSelectBox(){
            return this.getBoundBox();
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

        public void set(World world, Vector2 pos){
            this.setWorld(world);
            this.setPosition(pos);
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

        public virtual string getDisplayName(){
            return "missingno";
        }

        public virtual bool onDied(){
            return true;
        }

        public virtual bool isWalkable(){
            return true;
        }

        public virtual GuiSelection onSelected(GuiIngame gui){
            return null;
        }
    }
}