using System;
using Evolvinary.Main;
using Evolvinary.Main.Guis;
using Evolvinary.Main.Input;
using Evolvinary.Rendering;
using Microsoft.Xna.Framework;

namespace Evolvinary.Launch{
    public class EvolvinaryMain : Game{
        private static EvolvinaryMain instance;

        public RenderManager RenderManager;
        public Camera Camera;

        public Gui CurrentGui;

        public EvolvinaryMain(){
            this.Content.RootDirectory = "Content";
            this.RenderManager = new RenderManager(this);
        }

        protected override void LoadContent(){
            base.LoadContent();

            GameData.doBootstrap();
            this.RenderManager.loadContent();

            this.Camera = new Camera(0F, 0F, 1F);

            this.openGui(null);
        }

        protected override void Draw(GameTime time){
            base.Draw(time);

            this.RenderManager.draw(time);
        }

        protected override void Update(GameTime time){
            base.Update(time);

            InputProcessor.update(time, this);

            if(this.CurrentGui.doesGameGoOn()){
                GameData.WorldTest.update(time);
            }

            this.CurrentGui.update(time);
        }

        protected override void UnloadContent(){
            base.UnloadContent();

            this.RenderManager.Dispose();
            GameData.dispose();

            this.Content.Dispose();
            this.GraphicsDevice.Dispose();
        }

        public static EvolvinaryMain get(){
            return instance ?? (instance = new EvolvinaryMain());
        }

        public static t loadContent<t>(string path){
            return get().Content.Load<t>(path);
        }

        public void openGui(Gui gui){
            if(gui == null){
                gui = new GuiIngame(GameData.MainPlayer);
            }

            if(this.CurrentGui != null){
                this.CurrentGui.onClosed();
            }

            this.RenderManager.openGui(gui.getRenderer());

            this.CurrentGui = gui;
            this.CurrentGui.onOpened();
        }
    }
}