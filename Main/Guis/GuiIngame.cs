using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Input;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Main.Worlds.Tiles;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiIngame : Gui{
        public Entity SelectedEntity;

        public GuiIngame() : base(0, 0, getUnscaledWidth(), getUnscaledHeight()){
        }

        public override void onOpened(InputProcessor input){
            base.onOpened(input);

            var width = getUnscaledWidth();
            this.ButtonList.Add(new ButtonRenderedRect(0, this, width-32, 2, 30, 30, new Rectangle(256, 0, 30, 30)));
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererIngame(this);
        }

        public override void onActionPerformed(Button button){
            if(button.Id == 0){
                EvolvinaryMain.get().openGui(new GuiIngameMenu());
            }
        }

        public override bool allowCameraMovement(){
            return true;
        }

        public override void update(GameTime time){
            base.update(time);

            if(this.Input.LeftMouse.PressedOnce){
                var mousePos = EvolvinaryMain.get().Camera.toWorldPos(this.Input.getMousePos().ToVector2());
                var mouseRect = new BoundBox(mousePos.X-0.5F, mousePos.Y-0.5F, 1F, 1F);
                var entities = GameData.WorldTest.getEntitiesInBound(mouseRect, null);

                if(entities.Count > 0){
                    foreach(var entity in entities){
                        if(entity.canBeSelected()){
                            this.SelectedEntity = entity;
                            break;
                        }
                    }
                }
                else{
                    this.SelectedEntity = null;
                }
            }
        }
    }
}