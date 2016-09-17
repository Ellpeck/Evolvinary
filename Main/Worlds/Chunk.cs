using System;
using Evolvinary.Main.Worlds.Cells;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds{
    public class Chunk{
        public static readonly int Size = 32;

        public World World;
        public int PosX;
        public int PosY;

        public Cell[,] Cells = new Cell[Size, Size];

        public Chunk(World world, int posX, int posY){
            this.World = world;
            this.PosX = posX;
            this.PosY = posY;
        }

        public void populate(){
            for(var x = 0; x < Size; x++){
                for(var y = 0; y < Size; y++){
                    var tileToUse = this.World.SeededRand.NextDouble() >= 0.8 ? GameData.TileRock : GameData.TileDirt;
                    this.Cells[x, y] = tileToUse.makeCell(this.World, new Vector2(this.PosX * Size+x, this.PosY * Size+y));
                }
            }
        }

        public Cell getCell(int x, int y){
            return this.Cells[x, y];
        }

        public void setCell(Cell cell, int x, int y){
            this.Cells[x, y] = cell;
        }
    }
}