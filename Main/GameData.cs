using System.Collections.Generic;
using Evolvinary.Main.Worlds;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Main.Worlds.Tiles;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main{
    public class GameData{
        public static List<Tile> TileRegistry = new List<Tile>();

        public static Tile TileDirt;
        public static Tile TileRock;

        public static World WorldTest;

        public static void doBootstrap(){
            TileDirt = new Tile("dirt").setTextureCoords(0, 0).setGenColorIndex(new Color(81, 45, 31)).register();
            TileRock = new Tile("rock").setTextureCoords(1, 0).setGenColorIndex(Color.White).register();

            WorldTest = new World("Test", 78123);
        }

        public static Tile getTileByColor(Color color){
            foreach(var tile in TileRegistry){
                if(tile.GenColor == color){
                    return tile;
                }
            }
            return null;
        }

        public static void dispose(){
            WorldTest.Dispose();

            foreach(var tile in TileRegistry){
                tile.Dispose();
            }
        }
    }
}