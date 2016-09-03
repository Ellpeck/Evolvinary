using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Main{
	public class GameMain : Game{
		public GraphicsDeviceManager Graphics;
		public SpriteBatch SpriteBatch;

		public GameMain(){
			this.Graphics = new GraphicsDeviceManager(this);
			this.Content.RootDirectory = "Content";

			this.Graphics.PreferredBackBufferWidth = 1280;
			this.Graphics.PreferredBackBufferHeight = 720;
		}

		protected override void LoadContent(){
			this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
		}

		protected override void Draw(GameTime gameTime){
			this.GraphicsDevice.Clear(Color.CornflowerBlue);
			base.Draw(gameTime);
		}
	}
}