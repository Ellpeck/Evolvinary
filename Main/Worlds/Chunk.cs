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

        public void populate(Texture2D generator){
            if(generator.Width == Size && generator.Height == Size){
                var colors = new Color[Size * Size];
                generator.GetData(colors);

                for(var x = 0; x < Size; x++){
                    for(var y = 0; y < Size; y++){
                        var color = colors[x+y * Size];
                        var tile = GameData.getTileByColor(color);
                        if(tile != null){
                            this.setCell(tile.makeCell(this.World, new Vector2(World.toWorldCoord(this.PosX)+x, World.toWorldCoord(this.PosY)+y)), x, y);
                        }
                    }
                }
            }
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

            foreach(var entity in this.Entities.ToList()){
                entity.update(time);
            }
        }
    }
}