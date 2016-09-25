using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities{
    public class EntityPlaceable : Entity{
        public PlayerData PlacerPlayer;

        public virtual bool place(PlayerData placerPlayer, int cost, World world, Vector2 pos){
            if(placerPlayer.requestMoney(cost, true)){
                this.PlacerPlayer = placerPlayer;
                this.set(world, pos);

                return true;
            }
            return false;
        }
    }
}