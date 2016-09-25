using System;
using System.Collections.Generic;
using Evolvinary.Helper;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class Path{
        private readonly bool doesLoop;
        private readonly EntityPathable entity;

        public int CurrentTarget;
        public List<PathWaypoint> Waypoints = new List<PathWaypoint>();

        public Path(EntityPathable entity, IEnumerable<PathWaypoint> waypoints, bool doesLoop){
            this.entity = entity;
            this.doesLoop = doesLoop;

            this.Waypoints.AddRange(waypoints);
        }

        public virtual bool update(GameTime time){
            if(this.CurrentTarget >= 0){
                var target = this.Waypoints[this.CurrentTarget];

                var speed = this.entity.getSpeed();
                var distance = this.entity.Pos-target.Pos;

                if(MathHelp.isCloseTo(this.entity.Pos, target.Pos, speed)){
                    this.CurrentTarget++;
                    if(this.CurrentTarget >= this.Waypoints.Count){
                        if(this.shouldLoopNextTime()){
                            this.CurrentTarget = 0;
                        }
                        else{
                            this.CurrentTarget = -1;
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
    }
}