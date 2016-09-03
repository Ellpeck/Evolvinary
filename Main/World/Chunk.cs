namespace Evolvinary.Main.World{
    public class Chunk{
        public static readonly int Size = 32;

        public World World;
        public int PosX;
        public int PosY;

        public Cell[][] Cells = new Cell[Size][];

        public Chunk(World world, int posX, int posY){
            this.World = world;
            this.PosX = posX;
            this.PosY = posY;
        }

        public void populate(){
            for(var x = 0; x < Size; x++){
                for(var y = 0; y < Size; y++){
                    this.Cells[x][y] = new Cell(this.World, this.PosX * Size+x, this.PosY * Size+y);
                }
            }
        }
    }
}