using Evolvinary.Main;
using Evolvinary.Rendering;
using Microsoft.Xna.Framework;

namespace Evolvinary.Launch{
    public class EvolvinaryMain : Game{
        private static EvolvinaryMain instance;

        public RenderManager RenderManager;
        public Camera Camera;

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

            GameData.WorldTest.update(time);

            this.Camera.update(this.GraphicsDevice.DisplayMode.Width, this.GraphicsDevice.DisplayMode.Height);
        }

        public static EvolvinaryMain get(){
            return instance ?? (instance = new EvolvinaryMain());
        }

        protected override void UnloadContent(){
            base.UnloadContent();

            this.RenderManager.Dispose();
            GameData.dispose();

            this.Content.Dispose();
            this.GraphicsDevice.Dispose();
        }
    }
}