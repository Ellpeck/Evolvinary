using System;
using System.Collections.Generic;
using Evolvinary.Helper;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class PathWaypoint{
        public readonly Vector2 Goal;
        private readonly Vector2 goalRounded;

        private readonly Action<PathWaypoint, bool> callback;

        private readonly List<SubWaypoint> openList = new List<SubWaypoint>();
        private readonly List<SubWaypoint> closedList = new List<SubWaypoint>();

        private readonly List<Vector2> pointsToGoTo = new List<Vector2>();
        private int currentPointAt;

        public bool IsCurrentlyCalcing;
        public bool IsCalced;
        public bool Failed;

        public PathWaypoint(Vector2 goal) : this(goal, null){
        }

        public PathWaypoint(Vector2 goal, Action<PathWaypoint, bool> callback){
            this.Goal = goal;
            this.goalRounded = new Vector2(MathHelp.floor(this.Goal.X), MathHelp.floor(this.Goal.Y));

            this.callback = callback;
        }

        public PathWaypoint(Entity entity) : this(entity.Pos){
        }

        public PathWaypoint(float x, float y) : this(new Vector2(x, y)){
        }

        public bool onGoalReached(bool success){
            this.currentPointAt--;

            if(this.currentPointAt < 0){
                if(this.callback != null){
                    this.callback.Invoke(this, success);
                }

                return false;
            }

            return true;
        }

        public void updateCalcing(Vector2 startingPoint, Entity entity, Path path){
            if(!this.IsCalced && !this.Failed){
                if(!this.IsCurrentlyCalcing){
                    var start = new SubWaypoint(startingPoint, null, this.Goal);
                    this.openList.Add(start);

                    this.IsCurrentlyCalcing = true;
                }

                if(this.openList.Count > 0){
                    var lowestF = this.getLowestFFromOpenList();
                    if(lowestF != null){
                        this.openList.Remove(lowestF);
                        this.closedList.Add(lowestF);

                        //Path is complete
                        if(lowestF.Pos == this.goalRounded){
                            this.savePath(lowestF);
                            this.IsCalced = true;
                            this.IsCurrentlyCalcing = false;
                            return;
                        }

                        this.addToOpenList(lowestF, entity);
                    }
                }
                else{
                    this.Failed = true;
                    this.IsCurrentlyCalcing = false;
                }
            }
        }

        private void savePath(SubWaypoint end){
            var point = end;
            while(point != null){
                if(point.Parent != null){
                    point.Pos = Vector2.Add(point.Pos, new Vector2(0.5F, 0.5F));
                }

                this.pointsToGoTo.Add(point.Pos);
                point = point.Parent;
            }

            this.currentPointAt = this.pointsToGoTo.Count-1;
        }

        private void addToOpenList(SubWaypoint parent, Entity entity){
            var adjacent = World.getAdjacentCoords(MathHelp.floor(parent.Pos.X), MathHelp.floor(parent.Pos.Y), false);

            foreach(var coord in adjacent){
                if(entity.World.isWalkableExcept(MathHelp.floor(coord.X), MathHelp.floor(coord.Y), entity)){
                    var subWaypoint = new SubWaypoint(coord, parent, this.Goal);

                    if(getContainedSubWaypoint(this.closedList, subWaypoint) == null){
                        var contained = getContainedSubWaypoint(this.openList, subWaypoint);
                        if(contained == null){
                            this.openList.Add(subWaypoint);
                        }
                        else{
                            if(parent.CostFromStart+10 < contained.CostFromStart){
                                contained.Parent = parent;
                                contained.calculateCosts(this.Goal);
                            }
                        }
                    }
                }
            }
        }

        private static SubWaypoint getContainedSubWaypoint(IEnumerable<SubWaypoint> list, SubWaypoint waypoint){
            foreach(var point in list){
                if(point.Pos == waypoint.Pos){
                    return point;
                }
            }
            return null;
        }

        private SubWaypoint getLowestFFromOpenList(){
            SubWaypoint lowestWaypoint = null;
            var lowestF = int.MaxValue;

            foreach(var waypoint in this.openList){
                var f = waypoint.CostFromStart+waypoint.CostToEnd;
                if(f <= lowestF){
                    lowestF = f;
                    lowestWaypoint = waypoint;
                }
            }

            return lowestWaypoint;
        }

        public Vector2 getNextPos(){
            return this.pointsToGoTo[this.currentPointAt];
        }

        public override string ToString(){
            return "PathWaypoint@"+this.Goal+", Calcing? "+this.IsCurrentlyCalcing+", Failed? "+this.Failed+", Success? "+this.IsCalced;
        }
    }
}