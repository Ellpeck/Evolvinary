using System;
using Evolvinary.Helper;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class SubWaypoint{
        public Vector2 Pos;
        public SubWaypoint Parent;

        public int CostFromStart; //The G cost
        public int CostToEnd; //The H cost

        public SubWaypoint(Vector2 pos, SubWaypoint parent, Vector2 endPos){
            this.Pos = pos;
            this.Parent = parent;

            this.calculateCosts(endPos);
        }

        public void calculateCosts(Vector2 endPos){
            this.CostFromStart = this.Parent == null ? 0 : this.Parent.CostFromStart+10;

            var costToEndX = Math.Abs(endPos.X-this.Pos.X);
            var costToEndY = Math.Abs(endPos.Y-this.Pos.Y);
            this.CostToEnd = MathHelp.floor((costToEndX+costToEndY) * 10);
        }
    }
}