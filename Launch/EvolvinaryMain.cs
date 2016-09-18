using Evolvinary.Guis;
using Evolvinary.Main;
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
        }

        protected override void Draw(GameTime time){
            base.Draw(time);

            this.RenderManager.draw(time);
        }

        protected override void Update(GameTime time){
            base.Update(time);
            var mode = this.GraphicsDevice.DisplayMode;
            this.Camera.update(mode.Width, mode.Height);

            GameData.WorldTest.update(time);
            if(this.CurrentGui != null){
                this.CurrentGui.update(time);
            }
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

        public void openGui(Gui gui){
            if(this.CurrentGui != null){
                this.CurrentGui.onClosed();
            }

            this.RenderManager.openGui(gui != null ? gui.getRenderer() : null);
            this.CurrentGui = gui;

            if(this.CurrentGui != null){
                this.CurrentGui.onOpened();
            }
        }
    }
}