using System;
using System.Collections.Generic;
using Evolvinary.Launch;
using Evolvinary.Main;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Rendering{
    public class RenderManager : IDisposable{
        private readonly EvolvinaryMain game;
        public readonly GraphicsDeviceManager Graphics;
        public SpriteBatch Batch;

        public Texture2D TileTexture;
        public Texture2D StaticEntityTexture;
        public List<EntityRenderer> EntityRenderers = new List<EntityRenderer>();

        public RenderManager(EvolvinaryMain game){
            this.Graphics = new GraphicsDeviceManager(game);
            this.game = game;

            this.Graphics.PreferredBackBufferWidth = 1280;
            this.Graphics.PreferredBackBufferHeight = 720;
            this.game.IsMouseVisible = true;
        }

        public void loadContent(){
            this.Batch = new SpriteBatch(this.game.GraphicsDevice);

            this.TileTexture = this.game.Content.Load<Texture2D>("Textures/Tiles");
            this.StaticEntityTexture = this.game.Content.Load<Texture2D>("Textures/Entities/Static");
        }

        public void draw(GameTime time){
            this.game.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.Batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, this.game.Camera.Transform);
            GameData.WorldTest.Renderer.draw(this, time);
            this.Batch.End();
        }

        public void Dispose(){
            foreach(var renderer in this.EntityRenderers){
                renderer.Dispose();
            }

            this.TileTexture.Dispose();
            this.Batch.Dispose();
        }
    }
}