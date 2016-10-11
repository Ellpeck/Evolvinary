using System;
using Evolvinary.Rendering.Renderers.Tiles;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Tiles{
    public class Tile : IDisposable{
        public static readonly int Size = 16;

        public string Name;
        public TileRenderer Renderer;
        public Color UniqueColor;
        public bool IsWalkable;

        public Tile(string name){
            this.Name = name;
        }

        public Tile setUniqueColor(Color color){
            this.UniqueColor = color;
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

        public Tile setWalkable(){
            this.IsWalkable = true;
            return this;
        }

        public void Dispose(){
            this.Renderer.Dispose();
        }

        public virtual bool isWalkable(){
            return this.IsWalkable;
        }
    }
}