using System.Collections.Generic;

namespace Evolvinary.Main.World{
	public class World{
		private readonly Dictionary<int, Chunk> chunks = new Dictionary<int, Chunk>();

		public World(int defaultSizeX, int defaultSizeY){
		}

		public Chunk getChunkFromWorldCoords(int x, int y){
			return this.getChunkFromChunkCoords(x / Chunk.Size, y / Chunk.Size);
		}

		public Chunk getChunkFromChunkCoords(int chunkX, int chunkY){
			var id = getIdForChunk(chunkX, chunkY);

			var chunk = this.chunks[id];
			if(chunk == null){
				chunk = new Chunk(this, chunkX, chunkY);
				chunk.populate();
				this.chunks.Add(id, chunk);
			}
			return chunk;
		}

		public static int getIdForChunk(int chunkX, int chunkY){
			return chunkX * 13+chunkY * 8;
		}
	}
}