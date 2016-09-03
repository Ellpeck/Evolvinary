namespace Evolvinary.Main.World{
    public class Cell{
        public World World;
        public int PosX;
        public int PosY;

        public Cell(World world, int posX, int posY){
            this.World = world;
            this.PosX = posX;
            this.PosY = posY;
        }
    }
}