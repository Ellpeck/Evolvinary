using System;
using System.Collections.Generic;
using Evolvinary.Main.Worlds.Cells;
using Evolvinary.Rendering.Renderers;

namespace Evolvinary.Main.Worlds{
    public class World{
        private readonly Dictionary<long, Chunk> chunks = new Dictionary<long, Chunk>();

        public readonly WorldRenderer Renderer;
        public readonly Random SeededRand;

        public World(int defaultSizeX, int defaultSizeY, int seed){
            this.Renderer = new WorldRenderer(this);
            this.SeededRand = new Random(seed);

            for(var x = 0; x < defaultSizeX; x++){
                for(var y = 0; y < defaultSizeY; y++){
                    this.getChunkFromChunkCoords(x, y);
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
            var id = getIdForChunk(chunkX, chunkY);

            if(!this.chunks.ContainsKey(id)){
                var chunk = new Chunk(this, chunkX, chunkY);
                chunk.populate();
                this.chunks.Add(id, chunk);
                return chunk;
            }
            return this.chunks[id];
        }

        public static long getIdForChunk(int chunkX, int chunkY){
            return chunkX * 239+chunkY * 83;
        }

        public Dictionary<long, Chunk> getChunks(){
            return this.chunks;
        }
    }
}