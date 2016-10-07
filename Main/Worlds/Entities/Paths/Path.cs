using System;
using System.Collections.Generic;
using Evolvinary.Helper;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class Path{
        private readonly Action<PathWaypoint> callback;
        private readonly bool doesLoop;
        private readonly EntityPathable entity;

        private int currentTarget;
        private readonly List<PathWaypoint> waypoints = new List<PathWaypoint>();

        public Path(EntityPathable entity, IEnumerable<PathWaypoint> waypoints, bool doesLoop) : this(entity, waypoints, doesLoop, null){
        }

        public Path(EntityPathable entity, PathWaypoint goal, bool doesLoop) : this(entity, new[]{goal}, doesLoop){
        }

        public Path(EntityPathable entity, PathWaypoint goal, bool doesLoop, Action<PathWaypoint> callback) : this(entity, new[]{goal}, doesLoop, callback){
        }

        public Path(EntityPathable entity, IEnumerable<PathWaypoint> waypoints, bool doesLoop, Action<PathWaypoint> callback){
            this.entity = entity;
            this.doesLoop = doesLoop;
            this.callback = callback;

            this.waypoints.AddRange(waypoints);
        }

        public virtual bool update(GameTime time){
            if(this.currentTarget >= 0){
                var target = this.getCurrentWaypoint();

                var speed = this.entity.getSpeed();
                var distance = this.entity.Pos-target.Pos;

                if(MathHelp.isCloseTo(this.entity.Pos, target.Pos, speed)){
                    this.currentTarget++;
                    if(this.currentTarget >= this.waypoints.Count){
                        if(this.shouldLoopNextTime()){
                            this.currentTarget = 0;
                        }
                        else{
                            if(this.callback != null){
                                this.callback.Invoke(target);
                            }

                            this.currentTarget = -1;
                            return false;
                        }
                    }
                }
                else{
                    var moveX = 0F;
                    if(!MathHelp.isInbetween(distance.X, -speed, speed)){
                        moveX = distance.X < 0 ? speed : -speed;
                    }

                    var moveY = 0F;
                    if(!MathHelp.isInbetween(distance.Y, -speed, speed)){
                        moveY = distance.Y < 0 ? speed : -speed;
                    }

                    this.entity.move(moveX, moveY);
                }
                return true;
            }
            return false;
        }

        public virtual bool shouldLoopNextTime(){
            return this.doesLoop;
        }

        public PathWaypoint getCurrentWaypoint(){
            return this.waypoints[this.currentTarget];
        }
    }
}