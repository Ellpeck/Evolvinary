using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities.Paths{
    public class EntityPathable : EntityPlaceable{
        public static readonly List<Path> PathsForCalcing = new List<Path>();
        private static Thread pathCalcThread;

        private Path path;

        public override void update(GameTime time){
            base.update(time);

            if(this.path != null){
                if(!this.path.update(time)){
                    this.path = null;
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

        private static void calcPaths(){
            while(PathsForCalcing.Count > 0){
                var path = PathsForCalcing[0];
                if(path.calcAll()){
                    PathsForCalcing.Remove(path);
                }

                Thread.Sleep(1);
            }
        }
    }
}