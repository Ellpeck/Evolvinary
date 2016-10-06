using Evolvinary.Helper;
using Evolvinary.Main.Worlds.Tiles;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities{
    public class EntitySilo : EntityPlaceable{
        public EntitySilo(){
            this.attachRenderer(new StaticEntityRenderer(new Rectangle(432, 0, 80, 144)).register());
        }

        public override BoundBox getMouseSelectBox(){
            return new BoundBox(-40 / (float) Tile.Size, -72 / (float) Tile.Size, 80 / (float) Tile.Size, 144 / (float) Tile.Size);
        }

        public override BoundBox getBoundBox(){
            return new BoundBox(-40 / (float) Tile.Size, 0, 80 / (float) Tile.Size, 72 / (float) Tile.Size);
        }

        public override string getDisplayName(){
            return "Silo";
        }
    }
}