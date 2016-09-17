using System;
using Evolvinary.Main.Worlds.Cells;
using Evolvinary.Rendering.Renderers.Tiles;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Tiles{
    public class Tile : IDisposable{
        public static readonly int Size = 16;

        public string Name;
        public TileRenderer Renderer;
        public Color GenColor;

        public Tile(string name){
            this.Name = name;
        }

        public Tile setGenColorIndex(Color color){
            this.GenColor = color;
            return this;
        }

        public Tile setTextureCoords(int x, int y){
            this.Renderer = new TileRenderer(x, y);
            return this;
        }

        public Tile register(){
            GameData.TileRegistry.Add(this);
            return this;
        }

        public Cell makeCell(World world, Vector2 pos){
            return new Cell(this, world, pos);
        }

        public void Dispose(){
            this.Renderer.Dispose();
        }
    }
}