using System;
using System.Collections.Generic;
using System.IO;
using Evolvinary.Launch;
using Evolvinary.Main.Worlds.Cells;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Rendering.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Evolvinary.Main.Worlds{
    public class World : IDisposable{
        private readonly Dictionary<string, Texture2D> chunkGenerators = new Dictionary<string, Texture2D>();
        private readonly Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();

        public readonly WorldRenderer Renderer;
        public readonly Random SeededRand;
        public readonly string Name;

        public World(string name, int defaultSizeX, int defaultSizeY, int seed){
            this.Name = name;
            this.SeededRand = new Random(seed);
            this.Renderer = new WorldRenderer(this);

            this.loadChunkGenFiles();

            for(var x = 0; x < defaultSizeX; x++){
                for(var y = 0; y < defaultSizeY; y++){
                    this.getChunkFromChunkCoords(x, y);
                }
            }
        }

        private void loadChunkGenFiles(){
            var content = EvolvinaryMain.get().Content;

            var folderName = "Maps/"+this.Name;
            var dir = new DirectoryInfo(content.RootDirectory+"/"+folderName);
            if(dir.Exists){
                var files = dir.GetFiles();
                foreach(var file in files){
                    var name = Path.GetFileNameWithoutExtension(file.Name);
                    var texture = content.Load<Texture2D>(folderName+"/"+name);
                    this.chunkGenerators.Add(name, texture);
                }
            }
        }

        public Cell getCell(int x, int y){
            var chunk = this.getChunkFromWorldCoords(x, y);
            return chunk.getCell(x / Chunk.Size, y / Chunk.Size);
        }

        public Chunk getChunkFromWorldCoords(int x, int y){
            return this.getChunkFromChunkCoords(x / Chunk.Size, y / Chunk.Size);
        }

        public Chunk getChunkFromChunkCoords(int chunkX, int chunkY){
            var pos = new Vector2(chunkX, chunkY);

            if(!this.chunks.ContainsKey(pos)){
                var genKey = chunkX+","+chunkY;

                if(this.chunkGenerators.ContainsKey(genKey)){
                    var chunk = new Chunk(this, chunkX, chunkY);
                    chunk.populate(this.chunkGenerators[genKey]);
                    this.chunks.Add(pos, chunk);

                    return chunk;
                }

                return null;
            }

            return this.chunks[pos];
        }

        public Dictionary<Vector2, Chunk> getChunks(){
            return this.chunks;
        }

        public void Dispose(){
            foreach(var generator in this.chunkGenerators.Values){
                generator.Dispose();
            }

            this.Renderer.Dispose();
        }

        public void update(GameTime time){
            foreach(var chunk in this.chunks.Values){
                chunk.update(time);
            }

            var state = Mouse.GetState();
            if(state.LeftButton == ButtonState.Pressed){
                var cursorInWorld = EvolvinaryMain.get().Camera.toWorldPos(state.Position.ToVector2());
                var tuft = new EntityGrassTuft(this, 0);
                tuft.setPosition(cursorInWorld);
            }
        }

        public List<Chunk> getChunksContainedInRect(Rectangle rect){
            var containedChunks = new List<Chunk>();

            var topLeft = this.getChunkFromWorldCoords(rect.X, rect.Y);
            var bottomRight = this.getChunkFromWorldCoords(rect.X+rect.Width, rect.Y+rect.Height);

            if(topLeft != null && bottomRight != null){
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

        public List<Entity> getEntitiesWithinRect(Rectangle rect, Type type){
            var entities = new List<Entity>();

            var containedChunks = this.getChunksContainedInRect(rect);
            foreach(var chunk in containedChunks){
                var switchedRect = new Rectangle(rect.X-chunk.PosX * Chunk.Size, rect.Y-chunk.PosY * Chunk.Size, rect.Width, rect.Height);
                foreach(var entity in chunk.Entities){
                    if((type == null || entity.GetType() == type) && switchedRect.Contains(entity.Pos)){
                        entities.Add(entity);
                    }
                }
            }

            return entities;
        }
    }
}