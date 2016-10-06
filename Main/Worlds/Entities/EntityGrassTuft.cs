using Evolvinary.Helper;
using Evolvinary.Main.Worlds.Entities.Paths;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities{
    public class EntityGrassTuft : EntityPathable{
        private static readonly EntityRenderer[] Renderers ={
            new StaticEntityRenderer(new Rectangle(0, 0, 16, 16)).register(),
            new StaticEntityRenderer(new Rectangle(16, 0, 16, 16)).register(),
            new StaticEntityRenderer(new Rectangle(32, 0, 16, 16)).register()
        };

        private int currentStage;
        private int growthTime;

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
                            var amount = this.Rand.Next(3);
                            if(amount > 0){
                                for(var i = 0; i < amount; i++){
                                    var pos = new Vector2(this.Pos.X+((float) this.Rand.NextDouble() * 5-2.5F), this.Pos.Y+((float) this.Rand.NextDouble() * 5-2.5F));

                                    var entities = this.World.getEntitiesInBound(new BoundBox(pos.X-1F, pos.Y-1F, 2F, 2F), typeof(EntityGrassTuft), false);
                                    if(entities.Count <= 0){
                                        var newTuft = new EntityGrassTuft();
                                        newTuft.set(this.World, pos);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else{
                this.growthTime = 200+this.Rand.Next(400);
            }
        }

        public override string getDisplayName(){
            return "Grass";
        }
    }
}