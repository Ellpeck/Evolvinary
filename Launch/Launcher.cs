using System;

namespace Evolvinary.Launch{
    public class Launcher{
        [STAThread]
        //ReSharper disable once InconsistentNaming
        public static void Main(){
            EvolvinaryMain.get().Run();
        }
    }
}