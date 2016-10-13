using System;
using System.Collections.Generic;
using System.Linq;
using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Input;
using Evolvinary.Main.Input.Setting;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public abstract class Gui{
        public static readonly float Scale = 1.6F;
        public static readonly Matrix ScaleMatrix = Matrix.CreateScale(Scale);

        public Rectangle Area;
        public PlayerData CurrentPlayer;

        public List<Button> ButtonList = new List<Button>();

        public Gui(PlayerData currentPlayer, int posX, int posY, int sizeX, int sizeY){
            this.CurrentPlayer = currentPlayer;
            this.Area = new Rectangle(posX, posY, sizeX, sizeY);
        }

        public virtual void update(GameTime time){
            for(var i = 0; i < this.ButtonList.Count; i++){
                var button = this.ButtonList[i];
                button.update(time);
            }

            foreach(var bind in InputProcessor.KeyBindings){
                if(bind.PressedOnce){
                    var key = bind as KeySetting;
                    if(key != null){
                        this.onKeyPress(key);
                    }
                    else{
                        this.onMousePress((MouseSetting) bind);
                    }
                }
            }
        }

        public virtual void setPosition(Point newLoc){
            var diff = this.Area.Location-newLoc;

            if(diff != Point.Zero){
                this.Area.Location = newLoc;

                foreach(var button in this.ButtonList){
                    button.Area.Location -= diff;
                }
            }
        }

        public virtual void onKeyPress(KeySetting key){
            if(key == InputProcessor.Escape){
                this.onTryClose();
            }
        }

        public virtual bool onMousePress(MouseSetting mouse){
            if(mouse == InputProcessor.LeftMouse){
                for(var i = 0; i < this.ButtonList.Count; i++){
                    var button = this.ButtonList[i];
                    if(button.isMouseOver()){
                        button.onPressed();
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual void onActionPerformed(Button button){
        }

        public abstract GuiRenderer getRenderer();

        public virtual void onOpened(){
            this.ButtonList.Clear();
        }

        public virtual void onClosed(){
        }

        public virtual bool doesGameGoOn(){
            return false;
        }

        public virtual bool canMoveCamera(){
            return false;
        }

        public static int getUnscaledWidth(){
            return MathHelp.ceil(EvolvinaryMain.get().RenderManager.getScreenWidth() / Scale);
        }

        public static int getUnscaledHeight(){
            return MathHelp.ceil(EvolvinaryMain.get().RenderManager.getScreenHeight() / Scale);
        }

        public virtual void onTryClose(){
            EvolvinaryMain.get().openGui(null);
        }

        public bool isMouseOver(){
            return this.Area.Contains(InputProcessor.getMousePos().ToVector2() / Scale);
        }
    }
}