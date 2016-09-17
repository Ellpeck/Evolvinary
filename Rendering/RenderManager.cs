using Evolvinary.Launch;
using Evolvinary.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Rendering{
    public class RenderManager{
        private readonly EvolvinaryMain game;
        public readonly GraphicsDeviceManager Graphics;
        public SpriteBatch Batch;
        public Camera Camera;

        public Texture2D TileTexture;

        public RenderManager(EvolvinaryMain game){
            this.Graphics = new GraphicsDeviceManager(game);
            this.game = game;

            this.Graphics.PreferredBackBufferWidth = 1280;
            this.Graphics.PreferredBackBufferHeight = 720;
            this.game.IsMouseVisible = true;
        }

        public void loadContent(){
            this.Batch = new SpriteBatch(this.game.GraphicsDevice);
            this.Camera = new Camera(0F, 0F, 1F);

            this.TileTexture = this.game.Content.Load<Texture2D>("Tiles");
        }

        public void draw(GameTime time){
            this.game.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.Batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, this.Camera.Transform);
            GameData.WorldTest.Renderer.draw(this, time);
            this.Batch.End();
        }

        public void update(GameTime time){
            this.Camera.update(this.Graphics.PreferredBackBufferWidth, this.Graphics.PreferredBackBufferHeight);
        }
    }
}