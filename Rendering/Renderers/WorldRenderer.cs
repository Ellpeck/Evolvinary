using System;
using Evolvinary.Launch;
using Evolvinary.Main.Worlds;
using Evolvinary.Main.Worlds.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Evolvinary.Rendering.Renderers{
    public class WorldRenderer : IDisposable{
        private readonly World world;

        public WorldRenderer(World world){
            this.world = world;
        }

        public void draw(RenderManager manager, GameTime time){
            foreach(var chunk in this.world.getChunks().Values){
                for(var x = 0; x < Chunk.Size; x++){
                    for(var y = 0; y < Chunk.Size; y++){
                        var cell = chunk.getCell(x, y);
                        if(cell?.Tile?.Renderer != null){
                            cell.Tile.Renderer.draw(cell, cell.Pos*Tile.Size, manager, time);
                        }
                    }
                }

                foreach(var entity in chunk.Entities){
                    if(entity.Renderer != null){
                        entity.Renderer.draw(entity, entity.Pos*Tile.Size, manager, time);
                    }
                }
            }
        }

        public void Dispose(){

        }
    }
}