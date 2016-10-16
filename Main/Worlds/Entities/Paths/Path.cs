using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Evolvinary.Helper;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class Path{
        private readonly bool doesLoop;
        private readonly bool continueTryingWhenFailed;
        private readonly EntityPathable entity;

        private int currentTarget;
        private readonly List<PathWaypoint> waypoints = new List<PathWaypoint>();

        private int calcAt;

        public Path(EntityPathable entity, PathWaypoint goal, bool doesLoop, bool continueTryingWhenFailed) : this(entity, new[]{goal}, doesLoop, continueTryingWhenFailed){
        }

        public Path(EntityPathable entity, IEnumerable<PathWaypoint> waypoints, bool doesLoop, bool continueTryingWhenFailed){
            this.entity = entity;
            this.doesLoop = doesLoop;
            this.continueTryingWhenFailed = continueTryingWhenFailed;

            this.waypoints.AddRange(waypoints);
        }

        public bool calcAll(){
            var currWaypoint = this.waypoints[this.calcAt];
            var lastWaypoint = this.calcAt <= 0 ? this.entity.Pos : this.waypoints[this.calcAt-1].Goal;

            currWaypoint.updateCalcing(lastWaypoint, this.entity, this);

            if(currWaypoint.IsCalced || currWaypoint.Failed){
                this.calcAt++;
                return this.calcAt >= this.waypoints.Count;
            }
            return false;
        }

        public bool update(GameTime time){
            if(this.currentTarget >= 0){
                var speed = this.entity.getSpeed();

                var target = this.getCurrentWaypoint();
                if(target.IsCalced){
                    if(MathHelp.isCloseTo(this.entity.Pos, target.getNextPos(), speed)){
                        if(!this.goOn(target, true)){
                            return false;
                        }
                    }
                }

                //In case the waypoint changed from the goOn method called above
                target = this.getCurrentWaypoint();
                if(target.IsCalced){
                    var nextGoal = target.getNextPos();
                    var distance = this.entity.Pos-nextGoal;

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

                if(target.Failed){
                    this.goOn(target, false);
                    return !this.continueTryingWhenFailed;
                }

                return true;
            }
            return false;
        }

        private bool goOn(PathWaypoint target, bool success){
            if(!target.onGoalReached(success)){
                this.currentTarget++;
                if(this.currentTarget >= this.waypoints.Count){
                    if(this.shouldLoopNextTime()){
                        this.currentTarget = 0;
                    }
                    else{
                        this.currentTarget = -1;
                        return false;
                    }
                }
            }
            return true;
        }

        public bool shouldLoopNextTime(){
            return this.doesLoop;
        }

        public PathWaypoint getCurrentWaypoint(){
            return this.waypoints[this.currentTarget];
        }
    }
}