﻿using System;
using System.Collections.Generic;
using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Guis.Selection;
using Evolvinary.Main.Input;
using Evolvinary.Main.Input.Setting;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Main.Worlds.Entities.Paths;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiIngame : Gui{
        public Entity SelectedEntity;
        public GuiSelection SelectionGui;

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

        public void setSelectedEntity(Entity entity){
            this.SelectedEntity = entity;
            this.openSubGui(this.SelectedEntity == null ? null : this.SelectedEntity.onSelected(this));
        }

        public override void onActionPerformed(Button button){
            switch(button.Id){
                case 0:
                    EvolvinaryMain.get().openGui(new GuiIngameMenu(this.CurrentPlayer));
                    break;
                case 1:
                    var inv = new GuiIngameInventory(this.CurrentPlayer);
                    EvolvinaryMain.get().openGui(inv);
                    inv.setSelectedEntity(this.SelectedEntity);
                    break;
                case 3:
                    EvolvinaryMain.get().openGui(new GuiMap(this.CurrentPlayer));
                    break;
                case 2:
                    break;
                default:
                    this.setSelectedEntity(null);

                    if(this.selectableEntities.ContainsKey(button)){
                        var entity = this.selectableEntities[button];
                        this.setSelectedEntity(entity);
                    }

                    foreach(var key in this.selectableEntities.Keys){
                        this.ButtonList.Remove(key);
                    }
                    this.selectableEntities.Clear();

                    break;
            }
        }

        private void openSubGui(GuiSelection gui){
            if(this.SelectionGui != null){
                this.SelectionGui.onClosed();
            }

            var renderer = EvolvinaryMain.get().RenderManager.CurrentGuiRenderer as GuiRendererIngame;
            if(renderer != null){
                renderer.openSubGui(gui == null ? null : gui.getRenderer());
            }

            this.SelectionGui = gui;

            if(this.SelectionGui != null){
                this.SelectionGui.onOpened();
            }
        }

        public override bool doesGameGoOn(){
            return this.SelectionGui == null || this.SelectionGui.doesGameGoOn();
        }

        public override bool canMoveCamera(){
            return this.SelectionGui == null || this.SelectionGui.canMoveCamera();
        }

        public virtual bool canSelectEntities(){
            return this.SelectionGui == null || this.SelectionGui.canSelectEntities();
        }

        public override void onTryClose(){
            EvolvinaryMain.get().openGui(new GuiIngameMenu(this.CurrentPlayer));
        }

        public override bool onMousePress(MouseSetting mouse){
            if(!base.onMousePress(mouse)){
                if(this.canSelectEntities()){
                    if(mouse == InputProcessor.LeftMouse){
                        if(this.selectableEntities.Count <= 0){
                            var mousePos = EvolvinaryMain.get().Camera.toWorldPos(InputProcessor.getMousePos().ToVector2());

                            var toReturn = false;
                            if(this.SelectedEntity != null){
                                if(InputProcessor.Shift.IsDown){
                                    var pathable = this.SelectedEntity as EntityPathable;
                                    if(pathable != null){
                                        pathable.setPath(pathable.World.isWalkableExcept(MathHelp.floor(mousePos.X), MathHelp.floor(mousePos.Y), null) ? new Path(pathable, new[]{new PathWaypoint(mousePos)}, false, false) : null);
                                        toReturn = true;
                                    }
                                }
                                else{
                                    this.setSelectedEntity(null);
                                    toReturn = true;
                                }
                            }

                            var entities = GameData.WorldTest.getEntitiesOnPoint(mousePos, null, true);

                            if(entities.Count > 0){
                                if(entities.Count > 1){
                                    foreach(var entity in entities){
                                        if(entity.canSelect() && entity.MouseSelectBox != BoundBox.Empty){
                                            var pos = EvolvinaryMain.get().Camera.toCameraPos(entity.Pos) / Scale;
                                            this.selectableEntities.Add(new ButtonTextOnly(this.selectableEntities.Count-102834, this, (int) pos.X, (int) pos.Y, 30, 10, entity.getDisplayName(), 1F), entity);
                                        }
                                    }

                                    if(this.selectableEntities.Count > 0){
                                        this.setSelectedEntity(null);
                                        this.ButtonList.AddRange(this.selectableEntities.Keys);

                                        toReturn = true;
                                    }
                                }
                                else{
                                    var entity = entities[0];
                                    if(entity.canSelect()){
                                        this.setSelectedEntity(entity);

                                        toReturn = true;
                                    }
                                }
                            }

                            return toReturn;
                        }

                        foreach(var key in this.selectableEntities.Keys){
                            this.ButtonList.Remove(key);
                        }
                        this.selectableEntities.Clear();

                        return true;
                    }
                }
                return false;
            }
            return true;
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

            if(this.SelectionGui != null){
                this.SelectionGui.update(time);
            }
        }
    }
}