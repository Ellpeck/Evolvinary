using Evolvinary.Helper;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities{
    public class EntityGrassTuft : EntityPlaceable{
        private static readonly EntityRenderer[] Renderers ={
            new StaticEntityRenderer(new Rectangle(0, 0, 16, 16)),
            new StaticEntityRenderer(new Rectangle(16, 0, 16, 16)),
            new StaticEntityRenderer(new Rectangle(32, 0, 16, 16))
        };

        private int currentStage;
        private int growthTime;
        private int deathCounter;

        public EntityGrassTuft(){
            this.attachRenderer(Renderers[0]);
        }

        public override BoundBox getBoundBox(){
            return new BoundBox(-0.3F, 0F, 0.6F, 0.3F);
        }

        public override BoundBox getMouseSelectBox(){
            return new BoundBox(-0.3F, -0.3F, 0.6F, 0.6F);
        }

        public override void update(GameTime time){
            base.update(time);

            if(this.growthTime > 0){
                this.growthTime--;

                if(this.growthTime <= 0){
                    if(this.Rand.NextDouble() >= 0.25F){
                        if(this.currentStage < 2){
                            this.currentStage++;
                            this.attachRenderer(Renderers[this.currentStage]);
                        }
                        else{
                            if(this.deathCounter < 5){
                                var amount = this.Rand.Next(3);
                                if(amount > 0){
                                    for(var i = 0; i < amount; i++){
                                        var pos = new Vector2(this.Pos.X+((float) this.Rand.NextDouble() * 5-2.5F), this.Pos.Y+((float) this.Rand.NextDouble() * 5-2.5F));

                                        if(this.World.isWalkableExcept(MathHelp.floor(pos.X), MathHelp.floor(pos.Y), null)){
                                            var entities = this.World.getEntitiesInBound(new BoundBox(pos.X-0.5F, pos.Y-0.5F, 1F, 1F), typeof(EntityGrassTuft), false);
                                            if(entities.Count <= 0){
                                                var newTuft = new EntityGrassTuft();
                                                newTuft.set(this.World, pos);
                                            }
                                        }
                                    }
                                }

                                this.deathCounter++;
                            }
                        }
                    }
                }
            }
            else{
                this.growthTime = 400+this.Rand.Next(400);
            }
        }

        public override string getDisplayName(){
            return "Grass";
        }
    }
}