using Evolvinary.Launch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Helper{
    public class GraphicsHelper{

        public static Texture2D WhiteGradient;
        public static Texture2D TranslucentWhite;
        public static Texture2D SolidWhite;

        public static void init(){
            var game = EvolvinaryMain.get();

            WhiteGradient = new Texture2D(game.GraphicsDevice, 256, 128);
            var gradientData = new Color[WhiteGradient.Width * WhiteGradient.Height];
            for(var x = 0; x < WhiteGradient.Width; x++){
                for(var y = 0; y < WhiteGradient.Height; y++){
                    gradientData[x+y * WhiteGradient.Width] = new Color(1F, 1F, 1F, 1F-x / ((float) WhiteGradient.Width+50));
                }
            }
            WhiteGradient.SetData(gradientData);

            TranslucentWhite = new Texture2D(game.GraphicsDevice, game.RenderManager.getScreenWidth(), game.RenderManager.getScreenHeight());
            var translucentData = new Color[TranslucentWhite.Width*TranslucentWhite.Height];
            for(var i = 0; i < translucentData.Length; i++){
                translucentData[i] = new Color(1F, 1F, 1F, 0.5F);
            }
            TranslucentWhite.SetData(translucentData);

            SolidWhite = new Texture2D(game.GraphicsDevice, game.RenderManager.getScreenWidth(), game.RenderManager.getScreenHeight());
            var whiteData = new Color[SolidWhite.Width*SolidWhite.Height];
            for(var i = 0; i < whiteData.Length; i++){
                whiteData[i] = new Color(1F, 1F, 1F, 1F);
            }
            SolidWhite.SetData(whiteData);
        }

        public static void dispose(){
            WhiteGradient.Dispose();
            TranslucentWhite.Dispose();
            SolidWhite.Dispose();
        }
    }
}