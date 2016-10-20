using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class EntityPathable : EntityPlaceable{
        public static readonly List<Path> PathsForCalcing = new List<Path>();
        private static Thread pathCalcThread;

        private Path path;
        public bool MovementStopped;

        public override void update(GameTime time){
            base.update(time);

            if(!this.MovementStopped){
                this.updatePath(time);
            }
            else{
                this.setPath(null);
            }
        }

        protected virtual void updatePath(GameTime time){
            if(this.path != null){
                if(!this.path.update(time)){
                    this.setPath(null);
                }
            }
        }

        public void setPath(Path path){
            if(this.path != null){
                if(PathsForCalcing.Contains(this.path)){
                    pathCalcThread.Abort();
                    PathsForCalcing.Remove(this.path);
                }
            }

            this.path = path;

            if(this.path != null){
                PathsForCalcing.Add(path);
                if(pathCalcThread == null || !pathCalcThread.IsAlive){
                    pathCalcThread = new Thread(calcPaths);
                    pathCalcThread.IsBackground = true;
                    pathCalcThread.Start();
                }
            }
        }

        public Path getPath(){
            return this.path;
        }

        public float getSpeed(){
            return 0.03F;
        }

        public override bool canSelect(){
            return true;
        }

        private static void calcPaths(){
            var iterationsForOne = 0;
            var pauseCounter = 0;
            while(PathsForCalcing.Count > 0){
                pauseCounter++;

                iterationsForOne++;
                var failed = iterationsForOne >= 30000;

                var path = PathsForCalcing[0];
                if(path.calcAll() || failed){
                    PathsForCalcing.Remove(path);
                    iterationsForOne = 0;

                    if(failed){
                        Console.WriteLine("Couldn't calculate Path for "+path+" with entity "+path.Entity+"!");
                    }
                }

                if(pauseCounter >= 30){
                    Thread.Sleep(10);
                    pauseCounter = 0;
                }
            }
        }
    }
}