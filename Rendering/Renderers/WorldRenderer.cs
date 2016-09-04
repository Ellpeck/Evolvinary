using Evolvinary.Main.Worlds;
using Evolvinary.Main.Worlds.Tiles;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers{
    public class WorldRenderer{
        private readonly World world;

        public WorldRenderer(World world){
            this.world = world;
        }

        public void draw(RenderManager manager, GameTime time){
            foreach(var chunk in this.world.getChunks().Values){
                var chunkX = chunk.PosX * Chunk.Size * Tile.Size;
                var chunkY = chunk.PosY * Chunk.Size * Tile.Size;
                for(var x = 0; x < Chunk.Size; x++){
                    for(var y = 0; y < Chunk.Size; y++){
                        var cell = chunk.getCell(x, y);
                        if(cell?.Tile?.Renderer != null){
                            var renderPos = new Vector2(chunkX+x * Tile.Size, chunkY+y * Tile.Size);
                            cell.Tile.Renderer.draw(cell, renderPos, manager, time);
                        }
                    }
                }
            }
        }
    }
}