using System;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class PathWaypoint{
        //TODO Add a SubWaypoint system and implement the A* pathing system per waypoint (instead of per entire path!)
        public readonly Vector2 Pos;
        private readonly Action<PathWaypoint> callback;

        public PathWaypoint(Vector2 pos) : this(pos, null){
        }

        public PathWaypoint(Vector2 pos, Action<PathWaypoint> callback){
            this.Pos = pos;
            this.callback = callback;
        }

        public PathWaypoint(Entity entity) : this(entity.Pos){
        }

        public PathWaypoint(float x, float y) : this(new Vector2(x, y)){
        }

        public void onReached(){
            if(this.callback != null){
                this.callback.Invoke(this);
            }
        }
    }
}