using Evolvinary.Helper;
using Evolvinary.Main.Worlds.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Rendering.Renderers.Entities{
    public class AnimatedSpriteEntityRenderer : EntityRenderer{
        private double elapsedTime;
        private int currFrame;

        private readonly int frameCount;
        private readonly double frameTime;

        private readonly Texture2D texture;
        private readonly Rectangle[] leftAreas;
        private readonly Rectangle[] rightAreas;
        private readonly Rectangle[] upAreas;
        private readonly Rectangle[] downAreas;

        public AnimatedSpriteEntityRenderer(Texture2D texture, int spriteSizeX, int spriteSizeY, int frameCount, double frameTime){
            this.texture = texture;
            this.frameTime = frameTime;
            this.frameCount = frameCount;

            this.leftAreas = new Rectangle[this.frameCount];
            this.rightAreas = new Rectangle[this.frameCount];
            this.upAreas = new Rectangle[this.frameCount];
            this.downAreas = new Rectangle[this.frameCount];

            for(var i = 0; i < this.frameCount; i++){
                this.leftAreas[i] = new Rectangle(spriteSizeX * i, 0, spriteSizeX, spriteSizeY);
                this.rightAreas[i] = new Rectangle(spriteSizeX * i, spriteSizeY, spriteSizeX, spriteSizeY);
                this.upAreas[i] = new Rectangle(spriteSizeX * i, spriteSizeY * 2, spriteSizeX, spriteSizeY);
                this.downAreas[i] = new Rectangle(spriteSizeX * i, spriteSizeY * 3, spriteSizeX, spriteSizeY);
            }
        }

        public override void draw(Entity entity, Vector2 pos, RenderManager manager, GameTime time){
            this.update(time, entity);

            var area = this.getCurrArea(entity.Facing);
            var renderPos = new Vector2(pos.X-area.Width / 2, pos.Y-area.Height / 2);
            manager.Batch.Draw(this.texture, renderPos, area, Color.White);
        }

        private void update(GameTime time, Entity entity){
            if(entity.IsMoving){
                this.elapsedTime += time.ElapsedGameTime.TotalSeconds;

                if(this.elapsedTime >= this.frameTime){
                    this.currFrame++;
                    if(this.currFrame >= this.frameCount){
                        this.currFrame = 0;
                    }

                    this.elapsedTime -= this.frameTime;
                }
            }
            else{
                this.currFrame = 0;
            }
        }

        private Rectangle getCurrArea(Direction dir){
            Rectangle[] areas;
            switch(dir){
                case Direction.Up:
                    areas = this.upAreas;
                    break;
                case Direction.Down:
                    areas = this.downAreas;
                    break;
                case Direction.Left:
                    areas = this.leftAreas;
                    break;
                default:
                    areas = this.rightAreas;
                    break;
            }

            return areas[this.currFrame];
        }
    }
}