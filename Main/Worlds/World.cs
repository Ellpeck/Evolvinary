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

        public List<Chunk> getChunksInBound(BoundBox rect){
            var containedChunks = new List<Chunk>();

            var topLeft = this.getChunkFromWorldCoords(MathHelp.ceil(rect.X), MathHelp.ceil(rect.Y));
            var bottomRight = this.getChunkFromWorldCoords(MathHelp.ceil(rect.X+rect.Width), MathHelp.ceil(rect.Y+rect.Height));

            if(topLeft != null || bottomRight != null){
                if(topLeft == null){
                    topLeft = bottomRight;
                }
                else if(bottomRight == null){
                    bottomRight = topLeft;
                }

                var diffX = bottomRight.PosX-topLeft.PosX;
                var diffY = bottomRight.PosY-topLeft.PosY;

                for(var x = 0; x <= diffX; x++){
                    for(var y = 0; y <= diffY; y++){
                        var theX = topLeft.PosX+x;
                        var theY = topLeft.PosY+y;

                        var chunk = this.getChunkFromChunkCoords(theX, theY);
                        if(chunk != null){
                            containedChunks.Add(chunk);
                        }
                    }
                }
            }

            return containedChunks;
        }

        public List<Entity> getEntitiesInBound(BoundBox rect, Type type){
            var entities = new List<Entity>();

            var containedChunks = this.getChunksInBound(rect);
            foreach(var chunk in containedChunks){
                foreach(var entity in chunk.Entities){
                    if((type == null || entity.GetType() == type) && entity.BoundingBox.offset(entity.Pos).intersects(rect)){
                        entities.Add(entity);
                    }
                }
            }

            return entities;
        }
    }
}