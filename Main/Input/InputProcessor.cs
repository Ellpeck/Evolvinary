﻿using System.Collections.Generic;
using Evolvinary.Launch;
using Evolvinary.Main.Guis;
using Evolvinary.Main.Input.Setting;
using Evolvinary.Main.Worlds.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Evolvinary.Main.Input{
    public class InputProcessor{
        public static readonly List<InputSetting> KeyBindings = new List<InputSetting>();

        public static readonly InputSetting Shift = new KeySetting(Keys.LeftShift).register();
        public static readonly InputSetting Enter = new KeySetting(Keys.Enter).register();
        public static readonly InputSetting Escape = new KeySetting(Keys.Escape).register();

        public static readonly InputSetting LeftMouse = new MouseSetting(0).register();
        public static readonly InputSetting RightMouse = new MouseSetting(1).register();
        public static readonly InputSetting MiddleMouse = new MouseSetting(2).register();

        public static void update(GameTime time, EvolvinaryMain game){
            foreach(var bind in KeyBindings){
                bind.update();
            }

            game.Camera.checkInputs();

            if(MiddleMouse.PressedOnce && game.CurrentGui.allowCameraMovement()){
                var pos = game.Camera.toWorldPos(getMousePos().ToVector2());

                var silo = new EntitySilo();
                silo.place(GameData.MainPlayer, 1000, GameData.WorldTest, pos);
            }
        }

        public static int getMouseWheel(){
            return Mouse.GetState().ScrollWheelValue;
        }

        public static int getMouseX(){
            return Mouse.GetState().X;
        }

        public static int getMouseY(){
            return Mouse.GetState().Y;
        }

        public static Point getMousePos(){
            return Mouse.GetState().Position;
        }

        public static Entity getSelectedEntity(){
            var gui = EvolvinaryMain.get().CurrentGui as GuiIngame;
            return gui != null ? gui.SelectedEntity : null;
        }
    }
}