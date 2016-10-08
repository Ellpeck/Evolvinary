using System;
using System.Collections;
using System.Collections.Generic;
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
            var entities = new List<Entity>();

            foreach(var chunk in this.world.getChunks().Values){
                for(var y = Chunk.Size-1; y >= 0; y--){
                    for(var x = 0; x < Chunk.Size; x++){
                        var cell = chunk.getCell(x, y);
                        if(cell?.Tile?.Renderer != null){
                            cell.Tile.Renderer.draw(cell, cell.Pos * Tile.Size, manager, time);
                        }
                    }
                }

                entities.AddRange(chunk.Entities);
            }

            entities.Sort(EntityComparer.Instance);
            foreach(var entity in entities){
                if(entity.CurrentRenderer != null){
                    entity.CurrentRenderer.draw(entity, entity.Pos * Tile.Size, manager, time);
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