using System;
using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Guis;
using Evolvinary.Main.Guis.Selection;
using Evolvinary.Main.Worlds.Entities.Paths;
using Evolvinary.Main.Worlds.Tiles;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Main.Worlds.Entities{
    public class EntityCow : EntityPathable{
        private int eatCooldown;

        private static readonly Texture2D Sprite = EvolvinaryMain.loadContent<Texture2D>("Textures/Entities/Cow");

        public EntityCow(){
            this.attachRenderer(new AnimatedSpriteEntityRenderer(Sprite, 32, 28, 4, 0.2));
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

        protected override void updatePath(GameTime time){
            base.updatePath(time);

            if(this.eatCooldown <= 0){
                if(this.getPath() == null){
                    var grass = this.World.getClosestEntityToPointInBound(this.Pos, new BoundBox(-5, -5, 10, 10).offset(this.Pos), typeof(EntityGrassTuft), false);
                    if(grass != null){
                        this.setPath(new Path(this, new[]{new PathWaypoint(grass.Pos, this.onGrassSearchReached)}, false, false));
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

        public override GuiSelection onSelected(GuiIngame gui){
            return new GuiSelectionCow(gui.CurrentPlayer, this);
        }
    }
}