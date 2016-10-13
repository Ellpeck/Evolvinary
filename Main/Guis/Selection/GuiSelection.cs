using Evolvinary.Launch;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Rendering.Renderers.Guis;
using Evolvinary.Rendering.Renderers.Guis.Selection;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis.Selection{
    public abstract class GuiSelection : Gui{
        public Entity Entity;

        public GuiSelection(PlayerData currentPlayer, int sizeX, int sizeY) : base(currentPlayer, 0, 0, sizeX, sizeY){
        }

        public override void update(GameTime time){
            base.update(time);
            this.Pos = EvolvinaryMain.get().Camera.toCameraPos(this.Entity.Pos) / Scale;
        }

        public virtual bool canSelectEntities(){
            return true;
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