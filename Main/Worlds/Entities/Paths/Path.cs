using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Evolvinary.Helper;
using Evolvinary.Launch;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class Path{
        private static readonly Queue PathsForCalcing = new Queue();
        private static Thread pathCalcThread;

        private readonly bool doesLoop;
        private readonly bool continueTryingWhenFailed;
        private readonly EntityPathable entity;

        private int currentTarget;
        private readonly List<PathWaypoint> waypoints = new List<PathWaypoint>();

        public Path(EntityPathable entity, PathWaypoint goal, bool doesLoop, bool continueTryingWhenFailed) : this(entity, new[]{goal}, doesLoop, continueTryingWhenFailed){
        }

        public Path(EntityPathable entity, IEnumerable<PathWaypoint> waypoints, bool doesLoop, bool continueTryingWhenFailed){
            this.entity = entity;
            this.doesLoop = doesLoop;
            this.continueTryingWhenFailed = continueTryingWhenFailed;

            this.waypoints.AddRange(waypoints);

            PathsForCalcing.Enqueue(this);
            if(pathCalcThread == null || !pathCalcThread.IsAlive){
                pathCalcThread = new Thread(calcPaths);
                pathCalcThread.Start();
            }
        }

        private static void calcPaths(){
            while(PathsForCalcing.Count > 0){
                var path = PathsForCalcing.Dequeue() as Path;
                if(path != null){
                    path.calcAll();
                }
            }
        }

        public void calcAll(){
            var oldEnd = this.entity.Pos;
            foreach(var waypoint in this.waypoints){
                waypoint.calculate(oldEnd, this.entity.World);
                oldEnd = waypoint.Goal;
            }
        }

        public virtual bool update(GameTime time){
            if(this.currentTarget >= 0){
                var target = this.getCurrentWaypoint();
                if(target.IsCalced){
                    var nextGoal = target.getNextPos();
                    var speed = this.entity.getSpeed();
                    var distance = this.entity.Pos-nextGoal;

                    if(MathHelp.isCloseTo(this.entity.Pos, nextGoal, speed)){
                        if(!this.goOn(target, true)){
                            return false;
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
                this.goOn(target, false);
                return !this.continueTryingWhenFailed;
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

        public virtual bool shouldLoopNextTime(){
            return this.doesLoop;
        }

        public PathWaypoint getCurrentWaypoint(){
            return this.waypoints[this.currentTarget];
        }
    }
}