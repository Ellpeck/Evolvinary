using System;
using Evolvinary.Helper;
using Evolvinary.Main.Worlds.Entities.Paths;
using Evolvinary.Main.Worlds.Tiles;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities{
    public class EntityCow : EntityPathable{
        private int eatCooldown;

        private static readonly EntityRenderer Renderer = new StaticEntityRenderer(new Rectangle(48, 0, 48, 32));

        public EntityCow(){
            this.attachRenderer(Renderer);
        }

        public override BoundBox getMouseSelectBox(){
            return new BoundBox(-24 / (float) Tile.Size, -16 / (float) Tile.Size, 48 / (float) Tile.Size, 32 / (float) Tile.Size);
        }

        public override BoundBox getBoundBox(){
            return this.getMouseSelectBox();
        }

        public override string getDisplayName(){
            return "Cow";
        }

        public override void update(GameTime time){
            base.update(time);

            if(this.eatCooldown <= 0){
                if(this.Path == null){
                    var grass = this.World.getClosestEntityToPointInBound(this.Pos, new BoundBox(-5, -5, 10, 10).offset(this.Pos), typeof(EntityGrassTuft), false);
                    if(grass != null){
                        this.Path = new Path(this, new[]{new PathWaypoint(grass.Pos, this.onGrassSearchReached)}, false, false);
                    }
                }
            }
            else{
                this.eatCooldown--;
            }
        }

        private void onGrassSearchReached(PathWaypoint lastWaypoint, bool reached){
            if(reached){
                var grass = this.World.getFirstEntityOnPoint(lastWaypoint.Goal, typeof(EntityGrassTuft), false);
                if(grass != null){
                    grass.Dead = true;
                }
            }
            this.eatCooldown = 200+this.Rand.Next(200);
        }

        public override bool isWalkable(){
            return false;
        }
    }
}