using System.Collections.Generic;
using System.Linq;
using Evolvinary.Main.Worlds.Cells;
using Evolvinary.Main.Worlds.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Main.Worlds{
    public class Chunk{
        public static readonly int Size = 32;

        public World World;
        public int PosX;
        public int PosY;

        public readonly List<Entity> Entities = new List<Entity>();
        private readonly Cell[,] cells = new Cell[Size, Size];

        public Chunk(World world, int posX, int posY){
            this.World = world;
            this.PosX = posX;
            this.PosY = posY;
        }

        public Cell getCell(int x, int y){
            return this.cells[x, y];
        }

        public void setCell(Cell cell, int x, int y){
            this.cells[x, y] = cell;
        }

        public void update(GameTime time){
            for(var x = 0; x < Size; x++){
                for(var y = 0; y < Size; y++){
                    var cell = this.getCell(x, y);
                    if(cell != null){
                        cell.update(time);
                    }
                }
            }

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