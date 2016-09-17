using System;
using Evolvinary.Launch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Evolvinary.Rendering{
    public class Camera{
        public Matrix Transform;
        private Vector2 pos;
        private float zoom;

        private int lastX;
        private int lastY;
        private int lastScroll;

        public Camera(float defaultX, float defaultY, float defaultZoom){
            this.pos = new Vector2(defaultX, defaultY);
            this.zoom = defaultZoom;

            this.reloadMatrix();
        }

        public void update(int screenWidth, int screenHeight){
            var state = Mouse.GetState();
            if(EvolvinaryMain.get().IsActive){
                var shouldReloadMatrix = false;

                if(this.lastX != state.X || this.lastY != state.Y){
                    if(state.RightButton == ButtonState.Pressed){
                        this.pos.X -= state.X-this.lastX;
                        this.pos.Y -= state.Y-this.lastY;
                        shouldReloadMatrix = true;
                    }
                    this.lastX = state.X;
                    this.lastY = state.Y;
                }

                if(this.lastScroll != state.ScrollWheelValue){
                    var zoomDiff = (state.ScrollWheelValue-this.lastScroll) / 1000F;
                    var newZoom = Math.Max(0.5F, Math.Min(10F, this.zoom+zoomDiff * this.zoom));
                    if(newZoom != this.zoom){
                        var zoomChange = 1-newZoom / this.zoom;
                        this.pos.X -= (this.pos.X+state.X) * zoomChange;
                        this.pos.Y -= (this.pos.Y+state.Y) * zoomChange;

                        this.zoom = newZoom;
                        shouldReloadMatrix = true;
                    }
                    this.lastScroll = state.ScrollWheelValue;
                }

                if(shouldReloadMatrix){
                    this.reloadMatrix();
                }
            }
        }

        private void reloadMatrix(){
            Console.WriteLine("REloading");
            this.Transform = Matrix.CreateScale(this.zoom) * Matrix.CreateTranslation((int) -this.pos.X, (int) -this.pos.Y, 0F);
        }
    }
}