using System.Collections.Generic;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Main.Worlds.Tiles;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds{
    public class Chunk{
        public static readonly int Size = 16;

        public World World;
        public int PosX;
        public int PosY;

        public readonly List<Entity> Entities = new List<Entity>();
        private readonly Tile[,] tiles = new Tile[Size, Size];

        public Chunk(World world, int posX, int posY){
            this.World = world;
            this.PosX = posX;
            this.PosY = posY;
        }

        public Tile getTile(int x, int y){
            return this.tiles[x, y];
        }

        public void setTile(Tile tile, int x, int y){
            this.tiles[x, y] = tile;
        }

        public void update(GameTime time){
            if(this.Entities.Count > 0){
                for(var i = 0; i < this.Entities.Count; i++){
                    var entity = this.Entities[i];
                    entity.update(time);
                    if(entity.Dead){
                        if(entity.onDied()){
                            this.Entities.Remove(entity);
                        }
                        else{
                            entity.Dead = false;
                        }
                    }
                }
            }
        }
    }
}