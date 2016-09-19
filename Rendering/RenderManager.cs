using System;
using System.Collections.Generic;
using Evolvinary.Launch;
using Evolvinary.Main;
using Evolvinary.Rendering.Renderers.Entities;
using Evolvinary.Rendering.Renderers.Guis;
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

        public GuiRenderer CurrentGuiRenderer;

        public RenderManager(EvolvinaryMain game){
            this.Graphics = new GraphicsDeviceManager(game);
            this.game = game;

            this.Graphics.PreferredBackBufferWidth = 1280;
            this.Graphics.PreferredBackBufferHeight = 720;
            this.game.IsMouseVisible = true;
        }

        public void loadContent(){
            this.Batch = new SpriteBatch(this.game.GraphicsDevice);

            this.TileTexture = EvolvinaryMain.loadContent<Texture2D>("Textures/Tiles");
            this.StaticEntityTexture = EvolvinaryMain.loadContent<Texture2D>("Textures/Entities/Static");
        }

        public void draw(GameTime time){
            this.game.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.Batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, this.game.Camera.Transform);
            GameData.WorldTest.Renderer.draw(this, time);
            this.Batch.End();

            this.Batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            if(this.CurrentGuiRenderer != null){
                this.CurrentGuiRenderer.draw(this, time);
            }
            this.Batch.End();
        }

        public void Dispose(){
            foreach(var renderer in this.EntityRenderers){
                renderer.Dispose();
            }

            this.TileTexture.Dispose();
            this.Batch.Dispose();
        }

        public void openGui(GuiRenderer gui){
            if(this.CurrentGuiRenderer != null){
                this.CurrentGuiRenderer.onClosed();
            }

            this.CurrentGuiRenderer = gui;

            if(this.CurrentGuiRenderer != null){
                this.CurrentGuiRenderer.onOpened();
            }
        }

        public int getScreenWidth(){
            return this.Graphics.PreferredBackBufferWidth;
        }

        public int getScreenHeight(){
            return this.Graphics.PreferredBackBufferHeight;
        }
    }
}