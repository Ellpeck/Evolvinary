using System;
using System.Collections.Generic;
using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Main.Worlds.Tiles;
using Evolvinary.Rendering.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Main.Worlds{
    public class World : IDisposable{
        private Chunk[,] chunks;

        public readonly WorldRenderer Renderer;
        public readonly Random SeededRand;
        public readonly string Name;

        public World(string name, int seed){
            this.Name = name;
            this.SeededRand = new Random(seed);
            this.Renderer = new WorldRenderer(this);

            this.loadChunks();
        }

        private void loadChunks(){
            var texture = EvolvinaryMain.loadContent<Texture2D>("Maps/"+this.Name);
            var data = new Color[texture.Width * texture.Height];
            texture.GetData(data);

            var chunkAmountX = toChunkCoord(texture.Width);
            var chunkAmountY = toChunkCoord(texture.Height);
            this.chunks = new Chunk[chunkAmountX,chunkAmountY];

            for(var x = 0; x < chunkAmountX; x++){
                for(var y = 0; y < chunkAmountY; y++){
                     this.chunks[x, y] = new Chunk(this, x, y);
                }
            }

            for(var x = 0; x < texture.Width; x++){
                for(var y = 0; y < texture.Height; y++){
                    var color = data[x+y * texture.Width];

                    var tile = GameData.getTileByColor(color);
                    this.setTile(x, y, tile);
                }
            }
        }

        public Tile getTile(int x, int y){
            var chunk = this.getChunkFromWorldCoords(x, y);
            return chunk != null ? chunk.getTile(x-toWorldCoord(chunk.PosX), y-toWorldCoord(chunk.PosY)) : null;
        }

        public void setTile(int x, int y, Tile tile){
            var chunk = this.getChunkFromWorldCoords(x, y);
            if(chunk != null){
                chunk.setTile(tile, x-toWorldCoord(chunk.PosX), y-toWorldCoord(chunk.PosY));
            }
        }

        public Chunk getChunkFromWorldCoords(int x, int y){
            return this.getChunkFromChunkCoords(toChunkCoord(x), toChunkCoord(y));
        }

        public static int toChunkCoord(int worldCoord){
            return MathHelp.floor((double) worldCoord / Chunk.Size);
        }

        public static int toWorldCoord(int chunkCoord){
            return chunkCoord * Chunk.Size;
        }

        public Chunk getChunkFromChunkCoords(int chunkX, int chunkY){
            return chunkX >= 0 && chunkY >= 0 && this.chunks.GetLength(0) > chunkX && this.chunks.GetLength(1) > chunkY ? this.chunks[chunkX, chunkY] : null;
        }

        public Chunk[,] getChunks(){
            return this.chunks;
        }

        public void Dispose(){
            this.Renderer.Dispose();
        }

        public void update(GameTime time){
            foreach(var chunk in this.chunks){
                chunk.update(time);
            }
        }

        public List<Chunk> getChunksInWorldArea(int topLeftX, int topLeftY, int bottomRightX, int bottomRightY){
            return this.getChunksInChunkArea(toChunkCoord(topLeftX), toChunkCoord(topLeftY), toChunkCoord(bottomRightX), toChunkCoord(bottomRightY));
        }

        public List<Chunk> getChunksInChunkArea(int topLeftX, int topLeftY, int bottomRightX, int bottomRightY){
            var containedChunks = new List<Chunk>();

            for(var x = topLeftX; x <= bottomRightX; x++){
                for(var y = topLeftY; y <= bottomRightY; y++){
                    var chunk = this.getChunkFromChunkCoords(x, y);
                    if(chunk != null){
                        containedChunks.Add(chunk);
                    }
                }
            }

            return containedChunks;
        }

        public Entity getClosestEntityToPointInBound(Vector2 point, BoundBox bound, Type type, bool select){
            var entities = this.getEntitiesInBound(bound, type, select);

            var minDistance = double.MaxValue;
            Entity picked = null;

            foreach(var entity in entities){
                var currDistance = Vector2.DistanceSquared(entity.Pos, point);
                if(currDistance <= minDistance){
                    minDistance = currDistance;
                    picked = entity;
                }
            }

            return picked;
        }

        public Entity getFirstEntityOnPoint(Vector2 point, Type type, bool select){
            var entities = this.getEntitiesOnPoint(point, type, select);
            return entities.Count > 0 ? entities[0] : null;
        }

        public List<Entity> getEntitiesOnPoint(Vector2 point, Type type, bool select){
            return this.getEntitiesInBound(new BoundBox(point.X-0.01F, point.Y-0.01F, 0.02F, 0.02F), type, select);
        }

        public bool isWalkableExcept(int x, int y, Entity except){
            var cell = this.getTile(x, y);
            if(cell == null || !cell.isWalkable()){
                return false;
            }

            var entities = this.getEntitiesInBound(new BoundBox(x, y, 1, 1), null, false);
            foreach(var entity in entities){
                if(entity != except){
                    if(!entity.isWalkable()){
                        return false;
                    }
                }
            }

            return true;
        }

        public static List<Vector2> getAdjacentCoords(int x, int y, bool diagonals){
            var cells = new List<Vector2>();

            if(diagonals){
                cells.Add(new Vector2(x+1, y+1));
                cells.Add(new Vector2(x-1, y+1));
                cells.Add(new Vector2(x+1, y-1));
                cells.Add(new Vector2(x-1, y-1));
            }

            cells.Add(new Vector2(x, y+1));
            cells.Add(new Vector2(x+1, y));
            cells.Add(new Vector2(x, y-1));
            cells.Add(new Vector2(x-1, y));

            return cells;
        }

        public List<Entity> getEntitiesInBound(BoundBox rect, Type type, bool select){
            var entities = new List<Entity>();

            var containedChunks = this.getChunksInChunkArea(toChunkCoord(MathHelp.ceil(rect.X))-1, toChunkCoord(MathHelp.ceil(rect.Y))-1, toChunkCoord(MathHelp.ceil(rect.X+rect.Width))+1, toChunkCoord(MathHelp.ceil(rect.Y+rect.Height))+1);
            foreach(var chunk in containedChunks){
                for(var i = 0; i < chunk.Entities.Count; i++){
                    var entity = chunk.Entities[i];
                    if(entity != null){
                        if((type == null || entity.GetType() == type) && (select ? entity.MouseSelectBox : entity.BoundingBox).offset(entity.Pos).intersects(rect)){
                            entities.Add(entity);
                        }
                    }
                }
            }

            return entities;
        }

        public int getChunkSizeX(){
            return this.chunks.GetLength(0);
        }

        public int getChunkSizeY(){
            return this.chunks.GetLength(1);
        }

        public int getWorldSizeX(){
            return this.getChunkSizeX() * Chunk.Size;
        }

        public int getWorldSizeY(){
            return this.getChunkSizeY() * Chunk.Size;
        }
    }
}