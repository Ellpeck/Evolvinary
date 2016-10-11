using System;
using System.Collections;
using System.Collections.Generic;
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

            foreach(var chunk in this.world.getChunks().Values){
                for(var y = Chunk.Size-1; y >= 0; y--){
                    for(var x = 0; x < Chunk.Size; x++){
                        var tile = chunk.getTile(x, y);

                        if(tile.Renderer != null){
                            var pos = new Vector2(chunk.PosX*Chunk.Size+x, chunk.PosY*Chunk.Size+y);
                            var topLeftCam = cam.toCameraPos(pos);
                            var bottomRightCam = cam.toCameraPos(Vector2.Add(pos, Vector2.One));

                            if(bottomRightCam.X >= 0 && bottomRightCam.Y >= 0){
                                if(topLeftCam.X < manager.getScreenWidth() && topLeftCam.Y < manager.getScreenHeight()){
                                    tile.Renderer.draw(tile, pos * Tile.Size, manager, time);
                                }
                            }
                        }
                    }
                }

                if(chunk.Entities.Count > 0){
                    entities.AddRange(chunk.Entities);
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