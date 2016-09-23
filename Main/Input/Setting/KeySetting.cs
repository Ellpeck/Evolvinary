using Microsoft.Xna.Framework.Input;

namespace Evolvinary.Main.Input.Setting{
    public class KeySetting : InputSetting{
        private readonly Keys keyToQuery;

        public KeySetting(Keys keyToQuery){
            this.keyToQuery = keyToQuery;
        }

        public override void update(){
            var isDownNow = Keyboard.GetState().IsKeyDown(this.keyToQuery);

            this.PressedOnce = isDownNow && !this.IsDown;
            this.IsDown = isDownNow;
        }
    }
}