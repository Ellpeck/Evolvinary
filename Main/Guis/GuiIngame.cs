using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Input;
using Evolvinary.Main.Input.Setting;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Main.Worlds.Entities.Paths;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiIngame : Gui{
        public Entity SelectedEntity;
        public PlayerData CurrentPlayer;

        public GuiIngame(PlayerData currentPlayer) : base(0, 0, getUnscaledWidth(), getUnscaledHeight()){
            this.CurrentPlayer = currentPlayer;
        }

        public override void onOpened(){
            base.onOpened();

            var width = getUnscaledWidth();
            this.ButtonList.Add(new ButtonRenderedRect(0, this, width-40, 10, 30, 30, new Rectangle(256, 0, 30, 30)));
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererIngame(this);
        }

        public override void onActionPerformed(Button button){
            if(button.Id == 0){
                EvolvinaryMain.get().openGui(new GuiIngameMenu());
            }
        }

        public override void onKeyPress(KeySetting key){
            base.onKeyPress(key);

            if(key == InputProcessor.Escape){
                EvolvinaryMain.get().openGui(new GuiIngameMenu());
            }
        }

        public override bool allowCameraMovement(){
            return true;
        }

        public override void update(GameTime time){
            base.update(time);

            if(InputProcessor.LeftMouse.PressedOnce){
                var mousePos = EvolvinaryMain.get().Camera.toWorldPos(InputProcessor.getMousePos().ToVector2());
                var mouseRect = new BoundBox(mousePos.X-0.1F, mousePos.Y-0.1F, 0.2F, 0.2F);
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
                    if(InputProcessor.Shift.IsDown){
                        var pathable = this.SelectedEntity as EntityPathable;
                        if(pathable != null){
                            pathable.Path = new Path(pathable, new[]{new PathWaypoint(mousePos)}, false);
                        }
                    }
                    else{
                        this.SelectedEntity = null;
                    }
                }
            }
        }
    }
}