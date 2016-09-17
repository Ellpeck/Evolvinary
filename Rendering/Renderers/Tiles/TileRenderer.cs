using System;
using Evolvinary.Main.Worlds.Cells;
using Evolvinary.Main.Worlds.Tiles;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Tiles{
    public class TileRenderer : IDisposable{

        private readonly Rectangle textureRect;

        public TileRenderer(int textureX, int textureY){
            this.textureRect = new Rectangle(textureX*Tile.Size, textureY*Tile.Size, Tile.Size, Tile.Size);
        }

        public void draw(Cell cell, Vector2 pos, RenderManager manager, GameTime time){
            manager.Batch.Draw(manager.TileTexture, pos, this.textureRect, Color.White);
        }

        public void Dispose(){

        }
    }
}