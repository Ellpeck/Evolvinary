using Microsoft.Xna.Framework;

namespace Evolvinary.Helper{
    public class BoundBox{
        public static readonly BoundBox Empty = new BoundBox(0, 0, 0, 0);

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

        public bool intersects(BoundBox box){
            return !(box.X > this.X+this.Width || box.X+box.Width < this.X || box.Y > this.Y+this.Height || box.Y+box.Height < this.Y);
        }

        public bool intersects(Rectangle rect){
            return !(rect.Left > this.X+this.Width || rect.Right < this.X || rect.Top > this.Y+this.Height || rect.Bottom < this.Y);
        }

        public BoundBox offset(Vector2 pos){
            return new BoundBox(this.X+pos.X, this.Y+pos.Y, this.Width, this.Height);
        }

        public Rectangle toRect(){
            return new Rectangle(MathHelp.floor(this.X), MathHelp.floor(this.Y), MathHelp.floor(this.Width), MathHelp.floor(this.Height));
        }
    }
}