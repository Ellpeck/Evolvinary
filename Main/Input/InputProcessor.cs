using Evolvinary.Launch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Evolvinary.Main.Input{
    public class InputProcessor{
        private readonly EvolvinaryMain game;

        public KeySetting Enter = new KeySetting(Keys.Enter);
        public KeySetting Escape = new KeySetting(Keys.Escape);

        public MouseSetting LeftMouse = new MouseSetting();
        public MouseSetting RightMouse = new MouseSetting();
        public MouseSetting MiddleMouse = new MouseSetting();

        public InputProcessor(EvolvinaryMain game){
            this.game = game;
        }

        public void update(GameTime time){
            this.Enter.update();
            this.Escape.update();

            var state = Mouse.GetState();
            this.LeftMouse.update(state.LeftButton);
            this.RightMouse.update(state.RightButton);
            this.MiddleMouse.update(state.MiddleButton);

            this.game.Camera.checkInputs();
        }

        public int getMouseWheel(){
            return Mouse.GetState().ScrollWheelValue;
        }

        public int getMouseX(){
            return Mouse.GetState().X;
        }

        public int getMouseY(){
            return Mouse.GetState().Y;
        }

        public Point getMousePos(){
            return Mouse.GetState().Position;
        }
    }
}