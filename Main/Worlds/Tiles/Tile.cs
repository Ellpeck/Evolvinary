using Evolvinary.Rendering.Renderers;

namespace Evolvinary.Main.Worlds.Tiles{
    public class Tile{
        public static readonly int Size = 16;

        public string Name;
        public TileRenderer Renderer;

        public Tile(string name){
            this.Name = name;
        }

        public Tile setTextureCoords(int x, int y){
            this.Renderer = new TileRenderer(x, y);
            return this;
        }
    }
}