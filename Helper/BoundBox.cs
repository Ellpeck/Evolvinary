using Microsoft.Xna.Framework;

namespace Evolvinary.Helper{
    public class BoundBox{
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public BoundBox(float x, float y, float width, float height){
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public BoundBox(Rectangle rect) : this(rect.X, rect.Y, rect.Width, rect.Height){
        }

        public bool contains(Vector2 value){
            return this.X <= value.X && value.X < this.X+this.Width && this.Y <= value.Y && value.Y < this.Y+this.Height;
        }
    }
}