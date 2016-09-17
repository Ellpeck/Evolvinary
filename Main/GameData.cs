using Evolvinary.Main.Worlds;
using Evolvinary.Main.Worlds.Tiles;

namespace Evolvinary.Main{
    public class GameData{
        public static Tile TileDirt;
        public static Tile TileRock;

        public static World WorldTest;

        public static void doBootstrap(){
            TileDirt = new Tile("dirt").setTextureCoords(0, 0);
            TileRock = new Tile("rock").setTextureCoords(1, 0);

            WorldTest = new World(12, 12, 78123);
        }
    }
}