using System;
using Microsoft.Xna.Framework;

namespace Evolvinary.Helper{
    public class MathHelp{
        public static int floor(double value){
            var i = (int) value;
            return value < (double) i ? i-1 : i;
        }

        public static int ceil(double value){
            var i = (int) value;
            return value > (double) i ? i+1 : i;
        }

        public static bool isInbetween(double value, double first, double second){
            var lower = Math.Min(first, second);
            var upper = Math.Max(first, second);

            return value >= lower && value <= upper;
        }

        public static bool isCloseTo(Vector2 value, Vector2 other, double radius){
            return isInbetween(value.X, other.X-radius, other.X+radius) && isInbetween(value.Y, other.Y-radius, other.Y+radius);
        }
    }
}