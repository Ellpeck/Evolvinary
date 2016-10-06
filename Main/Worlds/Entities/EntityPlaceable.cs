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
                return world.getEntitiesInBound(this.BoundingBox.offset(pos), null, false).Count <= 0;
            }
            return false;
        }

        public virtual int getPlacePrice(){
            return 1000;
        }
    }
}