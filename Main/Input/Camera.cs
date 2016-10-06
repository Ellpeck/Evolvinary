using System;
using Evolvinary.Launch;
using Evolvinary.Main.Worlds.Tiles;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Input{
    public class Camera{
        public Matrix Transform;
        private Vector2 pos;
        public float Zoom;

        private int lastX;
        private int lastY;

        public Camera(float defaultX, float defaultY, float defaultZoom){
            this.pos = new Vector2(defaultX, defaultY);
            this.Zoom = defaultZoom;

            this.reloadMatrix();
        }

        public void checkInputs(){
            var game = EvolvinaryMain.get();
            if(game.IsActive && game.CurrentGui.canMoveCamera()){
                var shouldReloadMatrix = false;

                var mouseX = InputProcessor.getMouseX();
                var mouseY = InputProcessor.getMouseY();

                if(this.lastX != mouseX || this.lastY != mouseY){
                    if(InputProcessor.RightMouse.IsDown){
                        this.pos.X -= mouseX-this.lastX;
                        this.pos.Y -= mouseY-this.lastY;
                        shouldReloadMatrix = true;
                    }
                    this.lastX = mouseX;
                    this.lastY = mouseY;
                }

                var delta = InputProcessor.getScrollDelta();
                var zoomDiff = delta / 1000F;
                var newZoom = Math.Max(0.5F, Math.Min(10F, this.Zoom+zoomDiff * this.Zoom));
                if(newZoom != this.Zoom){
                    var zoomChange = 1-newZoom / this.Zoom;
                    this.pos.X -= (this.pos.X+mouseX) * zoomChange;
                    this.pos.Y -= (this.pos.Y+mouseY) * zoomChange;

                    this.Zoom = newZoom;
                    shouldReloadMatrix = true;
                }

                if(shouldReloadMatrix){
                    this.reloadMatrix();
                }
            }
        }

        private void reloadMatrix(){
            this.Transform = Matrix.CreateScale(this.Zoom) * Matrix.CreateTranslation((int) -this.pos.X, (int) -this.pos.Y, 0F);
        }

        public Vector2 toWorldPos(Vector2 pos){
            return Vector2.Transform(pos, Matrix.Invert(this.Transform)) / Tile.Size;
        }

        public Vector2 toCameraPos(Vector2 pos){
            return Vector2.Transform(pos * Tile.Size, this.Transform);
        }
    }
}