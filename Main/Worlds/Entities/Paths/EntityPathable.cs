using System;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class EntityPathable : EntityPlaceable{
        public Path Path;

        public override void update(GameTime time){
            base.update(time);

            if(this.Path != null){
                if(!this.Path.update(time)){
                    this.Path = null;
                }
            }
        }

        public float getSpeed(){
            return 0.03F;
        }
    }
}