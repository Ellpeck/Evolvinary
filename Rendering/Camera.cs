using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering{
    public class Camera{

        public Matrix Transform;
        public Vector2 Pos;
        public float Zoom;

        public Camera(float defaultX, float defaultY, float defaultZoom){
            this.Pos = new Vector2(defaultX, defaultY);
            this.Zoom = defaultZoom;
        }

        public void update(){
            this.Transform = Matrix.CreateScale(this.Zoom) * Matrix.CreateTranslation(-this.Pos.X, -this.Pos.Y, 0F);
        }
    }
}