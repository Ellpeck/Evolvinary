using System;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities{
    public class EntityGrassTuft : Entity{
        private static readonly EntityRenderer[] Renderers = {
            new StaticEntityRenderer(new Rectangle(0, 0, 16, 16)).register(),
            new StaticEntityRenderer(new Rectangle(16, 0, 16, 16)).register(),
            new StaticEntityRenderer(new Rectangle(32, 0, 16, 16)).register()
        };

        private int currentStage;
        private int growthTime;

        public EntityGrassTuft(World world, int initialStage) : base(world){
            this.currentStage = initialStage;
            this.attachRenderer(Renderers[0]);
        }

        public override void update(GameTime time){
            base.update(time);

            this.growthTime++;
            if(this.growthTime >= 200){
                this.growthTime = 0;

                if(this.Rand.NextDouble() >= 0.25F){
                    if(this.currentStage < 2){
                        this.currentStage++;
                        this.attachRenderer(Renderers[this.currentStage]);
                    }
                    else{
                        var amount = this.Rand.Next(3);
                        if(amount > 0){
                            for(var i = 0; i < amount; i++){
                                var pos = new Vector2(this.Pos.X+((float) this.Rand.NextDouble() * 4-2), this.Pos.Y+((float) this.Rand.NextDouble() * 4-2));

                                var entities = this.World.getEntitiesWithinRect(new Rectangle((int) (pos.X-1), (int) (pos.Y-1), 2, 2), typeof(EntityGrassTuft));
                                if(entities.Count <= 0){
                                    var newTuft = new EntityGrassTuft(this.World, 0);
                                    newTuft.setPosition(pos);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}