using System;
using System.Collections;
using System.Collections.Generic;
using Evolvinary.Helper;
using Evolvinary.Launch;
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
            var cam = EvolvinaryMain.get().Camera;
            var entities = new List<Entity>();

            //top left culling
            var chunkStart = Vector2.Max(Vector2.Zero, cam.toWorldPos(Vector2.Zero));
            var chunkStartX = World.toChunkCoord(MathHelp.floor(chunkStart.X));
            var chunkStartY = World.toChunkCoord(MathHelp.floor(chunkStart.Y));
            //bottom right culling
            var chunkEnd = cam.toWorldPos(new Vector2(manager.getScreenWidth(), manager.getScreenHeight()));
            var chunkEndX = Math.Min(this.world.getChunkSizeX()-1, World.toChunkCoord(MathHelp.floor(chunkEnd.X)));
            var chunkEndY = Math.Min(this.world.getChunkSizeY()-1, World.toChunkCoord(MathHelp.floor(chunkEnd.Y)));

            for(var chunkX = chunkStartX; chunkX <= chunkEndX; chunkX++){
                for(var chunkY = chunkStartY; chunkY <= chunkEndY; chunkY++){
                    var chunk = this.world.getChunkFromChunkCoords(chunkX, chunkY);

                    for(var y = 0; y < Chunk.Size; y++){
                        for(var x = 0; x < Chunk.Size; x++){
                            var tile = chunk.getTile(x, y);

                            if(tile.Renderer != null){
                                var pos = new Vector2(chunkX * Chunk.Size+x, chunkY * Chunk.Size+y);
                                tile.Renderer.draw(tile, pos * Tile.Size, manager, time);
                            }
                        }
                    }

                    if(chunk.Entities.Count > 0){
                        entities.AddRange(chunk.Entities);
                    }
                }
            }

            if(entities.Count > 0){
                entities.Sort(EntityComparer.Instance);
                foreach(var entity in entities){
                    if(entity.CurrentRenderer != null){
                        entity.CurrentRenderer.draw(entity, entity.Pos * Tile.Size, manager, time);
                    }
                }
            }
        }

        public void Dispose(){
        }

        private class EntityComparer : IComparer<Entity>{
            public static readonly EntityComparer Instance = new EntityComparer();

            public int Compare(Entity first, Entity second){
                var firstBound = first.BoundingBox.offset(first.Pos);
                var secondBound = second.BoundingBox.offset(second.Pos);

                return Comparer.Default.Compare(firstBound.Y+firstBound.Height, secondBound.Y+secondBound.Height);
            }
        }
    }
}