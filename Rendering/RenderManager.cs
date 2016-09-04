using Evolvinary.Launch;
using Evolvinary.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        }

        public void loadContent(){
            this.Batch = new SpriteBatch(this.game.GraphicsDevice);
            this.Camera = new Camera(0F, 0F, 1F);

            this.TileTexture = this.game.Content.Load<Texture2D>("Tiles");
        }

        public void draw(GameTime time){
            this.Batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, this.Camera.Transform);
            GameData.WorldTest.Renderer.draw(this, time);
            this.Batch.End();
        }

        public void update(GameTime time){
            if(Keyboard.GetState().IsKeyDown(Keys.W)){
                this.Camera.Pos.Y--;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A)){
                this.Camera.Pos.X--;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S)){
                this.Camera.Pos.Y++;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D)){
                this.Camera.Pos.X++;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Q)){
                this.Camera.Zoom+=0.1F;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.E)){
                this.Camera.Zoom-=0.1F;
            }

            this.Camera.update();
        }
    }
}