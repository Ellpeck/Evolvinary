using Evolvinary.Main.Worlds.Entities.Paths;
using Evolvinary.Rendering.Renderers.Guis.Buttons;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis.Buttons{
    public class ButtonToggleMovement : ButtonRenderedRect{
        private static readonly Rectangle TextureAreaOn = new Rectangle(406, 0, 30, 30);
        private static readonly Rectangle TextureAreaOff = new Rectangle(436, 0, 30, 30);

        private readonly EntityPathable entity;

        public ButtonToggleMovement(int id, Gui gui, int posX, int posY, EntityPathable entity) : base(id, gui, posX, posY, 30, 30, TextureAreaOn){
            this.entity = entity;

            this.updateRenderer();
        }

        public override void onPressed(){
            base.onPressed();

            this.entity.MovementStopped = !this.entity.MovementStopped;
            this.updateRenderer();
        }

        private void updateRenderer(){
            var renderer = this.getRenderer() as ButtonRendererStatic;
            if(renderer != null){
                renderer.setRenderRect(this.entity.MovementStopped ? TextureAreaOff : TextureAreaOn);
            }
        }
    }
}