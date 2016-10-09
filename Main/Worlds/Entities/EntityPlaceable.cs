using Evolvinary.Helper;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities{
    public class EntityPlaceable : Entity{
        public PlayerData PlacerPlayer;

        public virtual void place(PlayerData placerPlayer, World world, Vector2 pos){
            if(placerPlayer.requestMoney(this.getPlacePrice(), true)){
                this.PlacerPlayer = placerPlayer;
                this.set(world, pos);
            }
        }

        public virtual bool canPlace(PlayerData placerPlayer, World world, Vector2 pos){
            if(placerPlayer.requestMoney(this.getPlacePrice(), false)){
                var box = this.BoundingBox.offset(pos);

                var entities = world.getEntitiesInBound(box, null, false);
                foreach(var entity in entities){
                    if(!entity.isWalkable()){
                        return false;
                    }
                }

                for(var x = 0; x < box.Width; x++){
                    for(var y = 0; y < box.Height; y++){
                        var cell = world.getCell(MathHelp.floor(box.X+x), MathHelp.floor(box.Y+y));
                        if(cell == null || !cell.isWalkable()){
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public virtual int getPlacePrice(){
            return 1000;
        }
    }
}