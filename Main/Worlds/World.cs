using System;
using System.Collections.Generic;
using System.IO;
using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Worlds.Cells;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Rendering.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Evolvinary.Main.Worlds{
    public class World : IDisposable{
        private readonly Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();

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
            var folderName = "Maps/"+this.Name;
            var dir = new DirectoryInfo(EvolvinaryMain.get().Content.RootDirectory+"/"+folderName);
            if(dir.Exists){
                var files = dir.GetFiles();
                foreach(var file in files){
                    var name = Path.GetFileNameWithoutExtension(file.Name);
                    var nums = name.Split(',');
                    var chunkX = int.Parse(nums[0]);
                    var chunkY = int.Parse(nums[1]);

                    var chunk = new Chunk(this, chunkX, chunkY);

                    var texture = EvolvinaryMain.loadContent<Texture2D>(folderName+"/"+name);
                    chunk.populate(texture);
                    texture.Dispose();

                    this.chunks.Add(new Vector2(chunkX, chunkY), chunk);
                }
            }
        }

        public Cell getCell(int x, int y){
            var chunk = this.getChunkFromWorldCoords(x, y);
            return chunk != null ? chunk.getCell(x-toWorldCoord(chunk.PosX), y-toWorldCoord(chunk.PosY)) : null;
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
            var pos = new Vector2(chunkX, chunkY);
            return this.chunks.ContainsKey(pos) ? this.chunks[pos] : null;
        }

        public Dictionary<Vector2, Chunk> getChunks(){
            return this.chunks;
        }

        public void Dispose(){
            this.Renderer.Dispose();
        }

        public void update(GameTime time){
            foreach(var chunk in this.chunks.Values){
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

        public List<Entity> getEntitiesInBound(BoundBox rect, Type type){
            var entities = new List<Entity>();

            var containedChunks = this.getChunksInChunkArea(toChunkCoord(MathHelp.ceil(rect.X))-1, toChunkCoord(MathHelp.ceil(rect.Y))-1, toChunkCoord(MathHelp.ceil(rect.X+rect.Width))+1, toChunkCoord(MathHelp.ceil(rect.Y+rect.Height))+1);
            foreach(var chunk in containedChunks){
                foreach(var entity in chunk.Entities){
                    if((type == null || entity.GetType() == type) && entity.BoundingBox.offset(entity.Pos).intersects(rect)){
                        entities.Add(entity);
                    }
                }
            }

            return entities;
        }

        public int getChunkSizeX(){
            var highest = 0;
            foreach(var chunk in this.chunks.Values){
                if(chunk.PosX > highest){
                    highest = chunk.PosX;
                }
            }
            return highest+1;
        }

        public int getChunkSizeY(){
            var highest = 0;
            foreach(var chunk in this.chunks.Values){
                if(chunk.PosY > highest){
                    highest = chunk.PosY;
                }
            }
            return highest+1;
        }

        public int getWorldSizeX(){
            return this.getChunkSizeX() * Chunk.Size;
        }

        public int getWorldSizeY(){
            return this.getChunkSizeY() * Chunk.Size;
        }
    }
}