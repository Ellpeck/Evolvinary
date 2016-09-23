using System;
using System.Collections.Generic;
using System.Linq;
using Evolvinary.Main.Worlds;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Main.Worlds.Tiles;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers{
    public class WorldRenderer : IDisposable{
        private readonly World world;

        public WorldRenderer(World world){
            this.world = world;
        }

        public void draw(RenderManager manager, GameTime time){
            var chunks = this.world.getChunks().Values.ToList();
            chunks.Sort(ChunkComparer.Instance);

            foreach(var chunk in chunks){
                for(var y = Chunk.Size-1; y >= 0; y--){
                    for(var x = 0; x < Chunk.Size; x++){
                        var cell = chunk.getCell(x, y);
                        if(cell?.Tile?.Renderer != null){
                            cell.Tile.Renderer.draw(cell, cell.Pos * Tile.Size, manager, time);
                        }
                    }
                }
            }

            foreach(var chunk in chunks){
                var entities = chunk.Entities.ToList();
                entities.Sort(EntityComparer.Instance);

                foreach(var entity in chunk.Entities){
                    if(entity.Renderer != null){
                        entity.Renderer.draw(entity, entity.Pos * Tile.Size, manager, time);
                    }
                }
            }
        }

        public void Dispose(){
        }

        private class ChunkComparer : IComparer<Chunk>{
            public static readonly ChunkComparer Instance = new ChunkComparer();

            public int Compare(Chunk x, Chunk y){
                if(x.PosY == y.PosY){
                    return 0;
                }
                return x.PosY < y.PosY ? 1 : -1;
            }
        }

        private class EntityComparer : IComparer<Entity>{
            public static readonly EntityComparer Instance = new EntityComparer();

            public int Compare(Entity x, Entity y){
                if(x.Pos.Y == y.Pos.Y){
                    return 0;
                }
                return x.Pos.Y < y.Pos.Y ? 1 : -1;
            }
        }
    }
}