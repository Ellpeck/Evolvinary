using Microsoft.Xna.Framework.Input;

namespace Evolvinary.Main.Input{
    public class MouseSetting{
        public bool IsDown;
        public bool PressedOnce;

        public void update(ButtonState state){
            var isDownNow = state == ButtonState.Pressed;

            this.PressedOnce = isDownNow && !this.IsDown;
            this.IsDown = isDownNow;
        }
    }
}