using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class PathWaypoint{
        public readonly Vector2 Pos;

        public PathWaypoint(Vector2 pos){
            this.Pos = pos;
        }

        public PathWaypoint(float x, float y) : this(new Vector2(x, y)){
        }
    }
}