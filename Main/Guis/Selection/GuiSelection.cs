using Evolvinary.Launch;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Rendering.Renderers.Guis;
using Evolvinary.Rendering.Renderers.Guis.Selection;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis.Selection{
    public abstract class GuiSelection : Gui{
        public Entity Entity;

        public GuiSelection(PlayerData currentPlayer, int sizeX, int sizeY, Entity entity) : base(currentPlayer, 0, 0, sizeX, sizeY){
            this.Entity = entity;
            this.updatePos();
        }

        public override void update(GameTime time){
            base.update(time);
            this.updatePos();
        }

        private void updatePos(){
            this.setPosition((EvolvinaryMain.get().Camera.toCameraPos(this.Entity.Pos) / Scale).ToPoint());
        }

        public virtual bool canSelectEntities(){
            return !this.isMouseOver();
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererSelection(this);
        }

        public override bool canMoveCamera(){
            return true;
        }

        public override bool doesGameGoOn(){
            return true;
        }
    }
}