using Evolvinary.Helper;
using Evolvinary.Main.Worlds.Tiles;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Worlds.Entities{
    public class EntitySilo : EntityPlaceable{
        private static readonly EntityRenderer Renderer = new StaticEntityRenderer(new Rectangle(432, 0, 80, 144));

        public EntitySilo(){
            this.attachRenderer(Renderer);
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