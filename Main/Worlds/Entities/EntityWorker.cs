using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Worlds.Tiles;
using Evolvinary.Rendering.Renderers.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Main.Worlds.Entities{
    public class EntityWorker : EntityHuman{

        private static readonly Texture2D[] Sprites = {
            EvolvinaryMain.loadContent<Texture2D>("Textures/Entities/Worker1")
        };

        public EntityWorker(){
            this.attachRenderer(new AnimatedSpriteEntityRenderer(Sprites[this.Rand.Next(Sprites.Length)], 16, 32, 4, 0.2));
        }

        public override BoundBox getMouseSelectBox(){
            return new BoundBox(-8 / (float) Tile.Size, -16 / (float) Tile.Size, 16 / (float) Tile.Size, 32 / (float) Tile.Size);
        }

        public override BoundBox getBoundBox(){
            return this.getMouseSelectBox();
        }

        public override string getDisplayName(){
            return "Worker";
        }
    }
}