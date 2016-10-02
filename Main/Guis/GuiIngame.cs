using System.Collections.Generic;
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

        private readonly Dictionary<Button, Entity> selectableEntities = new Dictionary<Button, Entity>();

        public GuiIngame(PlayerData currentPlayer) : base(currentPlayer, 0, 0, getUnscaledWidth(), getUnscaledHeight()){
        }

        public override void onOpened(){
            base.onOpened();
            var width = getUnscaledWidth();
            var height = getUnscaledHeight();

            this.ButtonList.Add(new ButtonRenderedRect(0, this, width-40, 10, 30, 30, new Rectangle(256, 0, 30, 30)));

            this.ButtonList.Add(new ButtonRenderedRect(1, this, 10, height-40, 30, 30, new Rectangle(376, 0, 30, 30)));
            this.ButtonList.Add(new ButtonRenderedRect(2, this, 10, height-80, 30, 30, new Rectangle(346, 0, 30, 30)));
            this.ButtonList.Add(new ButtonRenderedRect(3, this, 10, height-120, 30, 30, new Rectangle(316, 0, 30, 30)));
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererIngame(this);
        }

        public override void onActionPerformed(Button button){
            switch(button.Id){
                case 0:
                    EvolvinaryMain.get().openGui(new GuiIngameMenu(this.CurrentPlayer));
                    break;
                case 1:
                    EvolvinaryMain.get().openGui(new GuiIngameInventory(this.CurrentPlayer));
                    break;
                case 3:
                    EvolvinaryMain.get().openGui(new GuiMap(this.CurrentPlayer));
                    break;
                default:
                    if(this.selectableEntities.ContainsKey(button)){
                        var entity = this.selectableEntities[button];
                        this.SelectedEntity = entity;

                        foreach(var key in this.selectableEntities.Keys){
                            this.ButtonList.Remove(key);
                        }
                        this.selectableEntities.Clear();
                    }
                    break;
            }
        }

        public override void onKeyPress(KeySetting key){
            base.onKeyPress(key);

            if(key == InputProcessor.Escape){
                EvolvinaryMain.get().openGui(new GuiIngameMenu(this.CurrentPlayer));
            }
        }

        public override bool doesGameGoOn(){
            return true;
        }

        public override bool canMoveCamera(){
            return true;
        }

        public override void onMousePress(MouseSetting mouse){
            if(mouse == InputProcessor.LeftMouse){
                if(this.selectableEntities.Count <= 0){
                    var mousePos = EvolvinaryMain.get().Camera.toWorldPos(InputProcessor.getMousePos().ToVector2());

                    if(this.SelectedEntity != null){
                        if(InputProcessor.Shift.IsDown){
                            var pathable = this.SelectedEntity as EntityPathable;
                            if(pathable != null){
                                pathable.Path = new Path(pathable, new[]{new PathWaypoint(mousePos)}, false);
                                return;
                            }
                        }
                        else{
                            this.SelectedEntity = null;
                        }
                    }

                    var mouseRect = new BoundBox(mousePos.X-0.1F, mousePos.Y-0.1F, 0.2F, 0.2F);
                    var entities = GameData.WorldTest.getEntitiesInBound(mouseRect, null);

                    if(entities.Count > 0){
                        if(entities.Count > 1){
                            foreach(var entity in entities){
                                if(entity.canBeSelected()){
                                    var pos = EvolvinaryMain.get().Camera.toCameraPos(entity.Pos) / Scale;
                                    this.selectableEntities.Add(new ButtonTextOnly(this.selectableEntities.Count-102834, this, (int) pos.X, (int) pos.Y, 30, 10, entity.getDisplayName(), 1F), entity);
                                }
                            }

                            if(this.selectableEntities.Count > 0){
                                this.SelectedEntity = null;
                                this.ButtonList.AddRange(this.selectableEntities.Keys);
                                return;
                            }
                        }
                        else{
                            var entity = entities[0];
                            this.SelectedEntity = entity;
                            return;
                        }
                    }
                }
                else{
                    base.onMousePress(mouse);

                    foreach(var key in this.selectableEntities.Keys){
                        this.ButtonList.Remove(key);
                    }
                    this.selectableEntities.Clear();

                    return;
                }
            }

            base.onMousePress(mouse);
        }

        public override void update(GameTime time){
            base.update(time);

            if(this.selectableEntities.Count > 0){
                foreach(var entry in this.selectableEntities){
                    var pos = EvolvinaryMain.get().Camera.toCameraPos(entry.Value.Pos) / Scale;

                    var button = entry.Key;
                    button.Area.X = (int) pos.X;
                    button.Area.Y = (int) pos.Y;
                }
            }
        }
    }
}