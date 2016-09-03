using System;

namespace Evolvinary.Main{
    public class Program{
        [STAThread]
        //ReSharper disable once InconsistentNaming
        public static void Main(){
            var game = new GameMain();
            game.Run();
        }
    }
}