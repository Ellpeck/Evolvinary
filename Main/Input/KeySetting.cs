using Microsoft.Xna.Framework.Input;

namespace Evolvinary.Main.Input{
    public class KeySetting{
        private readonly Keys keyToQuery;

        public bool IsDown;
        public bool PressedOnce;

        public KeySetting(Keys keyToQuery){
            this.keyToQuery = keyToQuery;
        }

        public void update(){
            var isDownNow = Keyboard.GetState().IsKeyDown(this.keyToQuery);

            this.PressedOnce = isDownNow && !this.IsDown;
            this.IsDown = isDownNow;
        }
    }
}