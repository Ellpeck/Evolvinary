using Evolvinary.Main.Worlds.Tiles;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Cells{
    public class Cell{
        public Tile Tile;
        public World World;
        public Vector2 Pos;

        public Cell(Tile tile, World world, Vector2 pos){
            this.World = world;
            this.Pos = pos;
            this.Tile = tile;
        }

        public virtual void update(GameTime time){

        }

        public virtual bool isWalkable(){
            return this.Tile == GameData.TileDirt || this.Tile == GameData.TileTest;
        }
    }
}