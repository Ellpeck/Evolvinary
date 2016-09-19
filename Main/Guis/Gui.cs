using System;
using System.Collections.Generic;
using Evolvinary.Main.Input;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public abstract class Gui{
        public Vector2 Pos;
        public int SizeX;
        public int SizeY;
        public InputProcessor Input;

        public List<Button> ButtonList = new List<Button>();

        public Gui(int posX, int posY, int sizeX, int sizeY){
            this.Pos = new Vector2(posX, posY);
            this.SizeX = sizeX;
            this.SizeY = sizeY;
        }

        public virtual void update(GameTime time){
            foreach(var button in this.ButtonList){
                button.update(time);

                if(button.isMouseOver() && this.Input.LeftMouse.PressedOnce){
                    this.onActionPerformed(button);
                }
            }
        }

        public virtual void onActionPerformed(Button button){

        }

        public abstract GuiRenderer getRenderer();

        public virtual void onOpened(InputProcessor input){
            this.Input = input;

            this.ButtonList.Clear();
        }

        public virtual void onClosed(){

        }

        public virtual bool allowCameraMovement(){
            return false;
        }
    }
}