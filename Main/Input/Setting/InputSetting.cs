
namespace Evolvinary.Main.Input.Setting{
    public class InputSetting{

        public bool IsDown;
        public bool PressedOnce;

        public virtual void update(){

        }

        public virtual InputSetting register(){
            InputProcessor.KeyBindings.Add(this);
            return this;
        }
    }
}