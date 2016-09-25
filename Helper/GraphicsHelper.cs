using Evolvinary.Launch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Helper{
    public class GraphicsHelper{

        public static Texture2D Graydient; //Yes this is a pun
        public static Texture2D TranslucentGray;

        public static void init(){
            var game = EvolvinaryMain.get();

            Graydient = new Texture2D(game.GraphicsDevice, 256, 128);
            var gradientData = new Color[Graydient.Width * Graydient.Height];
            for(var x = 0; x < Graydient.Width; x++){
                for(var y = 0; y < Graydient.Height; y++){
                    gradientData[x+y * Graydient.Width] = new Color(0F, 0F, 0F, 1F-x / ((float) Graydient.Width+50));
                }
            }
            Graydient.SetData(gradientData);

            TranslucentGray = new Texture2D(game.GraphicsDevice, game.RenderManager.getScreenWidth(), game.RenderManager.getScreenHeight());
            var translucentData = new Color[TranslucentGray.Width*TranslucentGray.Height];
            for(var i = 0; i < translucentData.Length; i++){
                translucentData[i] = new Color(0F, 0F, 0F, 0.5F);
            }
            TranslucentGray.SetData(translucentData);
        }

        public static void dispose(){
            Graydient.Dispose();
            TranslucentGray.Dispose();
        }
    }
}