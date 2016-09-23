using Microsoft.Xna.Framework.Input;

namespace Evolvinary.Main.Input.Setting{
    public class MouseSetting : InputSetting{

        private readonly int type;

        public MouseSetting(int type){
            this.type = type;
        }

        public override void update(){
            var mouse = Mouse.GetState();
            var state = this.type == 0 ? mouse.LeftButton : (this.type == 1 ? mouse.RightButton : mouse.MiddleButton);

            var isDownNow = state == ButtonState.Pressed;

            this.PressedOnce = isDownNow && !this.IsDown;
            this.IsDown = isDownNow;
        }
    }
}