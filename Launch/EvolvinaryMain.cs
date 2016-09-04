using Evolvinary.Main;
using Evolvinary.Rendering;
using Microsoft.Xna.Framework;

namespace Evolvinary.Launch{
    public class EvolvinaryMain : Game{
        private static EvolvinaryMain instance;

        public RenderManager RenderManager;

        public EvolvinaryMain(){
            this.Content.RootDirectory = "Content";
            this.RenderManager = new RenderManager(this);
        }

        protected override void LoadContent(){
            GameData.doBootstrap();

            this.RenderManager.loadContent();
        }

        protected override void Draw(GameTime time){
            base.Draw(time);

            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.RenderManager.draw(time);
        }

        protected override void Update(GameTime time){
            base.Update(time);
            this.RenderManager.update(time);
        }

        public static EvolvinaryMain get(){
            return instance ?? (instance = new EvolvinaryMain());
        }
    }
}