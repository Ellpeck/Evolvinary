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
        public Vector2 Pos;
        public int SizeX;
        public int SizeY;
        public PlayerData CurrentPlayer;

        public List<Button> ButtonList = new List<Button>();

        public Gui(PlayerData currentPlayer, int posX, int posY, int sizeX, int sizeY){
            this.CurrentPlayer = currentPlayer;
            this.Pos = new Vector2(posX, posY);
            this.SizeX = sizeX;
            this.SizeY = sizeY;
        }

        public virtual void update(GameTime time){
            foreach(var button in this.ButtonList.ToList()){
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

        public virtual void onKeyPress(KeySetting key){
        }

        public virtual bool onMousePress(MouseSetting mouse){
            if(mouse == InputProcessor.LeftMouse){
                foreach(var button in this.ButtonList.ToList()){
                    if(button.isMouseOver()){
                        this.onActionPerformed(button);
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
            return MathHelp.floor(EvolvinaryMain.get().RenderManager.getScreenWidth() / Scale);
        }

        public static int getUnscaledHeight(){
            return MathHelp.floor(EvolvinaryMain.get().RenderManager.getScreenHeight() / Scale);
        }
    }
}