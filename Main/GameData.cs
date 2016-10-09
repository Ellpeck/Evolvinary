using System.Collections.Generic;
using Evolvinary.Main.Items;
using Evolvinary.Main.Worlds;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Main.Worlds.Tiles;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main{
    public class GameData{
        public static List<Tile> TileRegistry = new List<Tile>();

        public static Tile TileDirt;
        public static Tile TileRock;
        public static Tile TileGrass;
        public static Tile TileTest;

        public static Item ItemSilo;
        public static Item ItemGrass;
        public static Item ItemCow;

        public static World WorldTest;

        public static PlayerData MainPlayer;

        public static void doBootstrap(){
            TileDirt = new Tile("dirt").setTextureCoords(0, 0).setUniqueColor(new Color(81, 45, 31)).setWalkable().register();
            TileRock = new Tile("rock").setTextureCoords(1, 0).setUniqueColor(Color.White).register();
            TileGrass = new Tile("grass").setTextureCoords(2, 0).setUniqueColor(new Color(41, 107, 23)).setWalkable().register();
            TileTest = new Tile("test").setTextureCoords(15, 15).setUniqueColor(Color.Magenta).setWalkable().register();

            ItemSilo = new ItemEntityHolder("Silo", "Can be placed on your farm, stores grain and other resources", typeof(EntitySilo));
            ItemGrass = new ItemEntityHolder("Grass", "Can be placed on your farm, is eaten by animals like cows and pigs.", typeof(EntityGrassTuft));
            ItemCow = new ItemEntityHolder("Cow", "Can be placed in a grassy area. Moos and eats up all the grass.", typeof(EntityCow));

            WorldTest = new World("Test", 78123);

            MainPlayer = new PlayerData();
        }

        public static Tile getTileByColor(Color color){
            foreach(var tile in TileRegistry){
                if(tile.UniqueColor == color){
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