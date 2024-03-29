﻿using System;
using Evolvinary.Launch;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Guis.Lists;
using Evolvinary.Main.Input;
using Evolvinary.Main.Input.Setting;
using Evolvinary.Main.Worlds.Entities;
using Evolvinary.Rendering.Renderers.Guis;
using Evolvinary.Rendering.Renderers.Guis.Buttons;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiIngameInventory : GuiIngame{
        public ScrollList List;

        public GuiIngameInventory(PlayerData currentPlayer) : base(currentPlayer){
        }

        public override void onOpened(){
            base.onOpened();

            var height = getUnscaledHeight();
            this.List = new ItemList(-4823, this, 65, height-250, 350, 250, this.CurrentPlayer.Inventory);

            var renderer = this.ButtonList[1].getRenderer() as ButtonRendererStatic;
            if(renderer != null){
                renderer.setRenderRect(new Rectangle(286, 0, 30, 30));
            }
        }

        public override void update(GameTime time){
            base.update(time);
            this.List.update(time);
        }

        public override void onTryClose(){
            if(this.List.getSelectedComponent() == null){
                this.openIngame();
            }
            else{
                this.List.unselectAllExcept(null);
            }
        }

        public override bool canSelectEntities(){
            return this.List.getSelectedComponent() == null && base.canSelectEntities();
        }

        public override bool onMousePress(MouseSetting mouse){
            if(!base.onMousePress(mouse)){
                if(mouse == InputProcessor.LeftMouse && this.canMoveCamera()){
                    var selected = this.List.getSelectedComponent() as ListComponentItem;
                    if(selected != null){
                        var stack = selected.getStack();
                        if(stack != null){
                            var entity = stack.getHeldEntity();
                            if(entity != null){
                                var pos = EvolvinaryMain.get().Camera.toWorldPos(InputProcessor.getMousePos().ToVector2());

                                if(entity.canPlace(GameData.MainPlayer, GameData.WorldTest, pos)){
                                    entity.place(GameData.MainPlayer, GameData.WorldTest, pos);

                                    stack.Amount--;
                                    if(stack.Amount <= 0){
                                        selected.removeStack();
                                        this.List.removeComponent(selected);
                                    }

                                    if(!InputProcessor.Shift.IsDown){
                                        this.List.unselectAllExcept(null);
                                    }

                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }

            this.List.unselectAllExcept(null);
            return true;
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererIngameInventory(this);
        }

        public override bool canMoveCamera(){
            return !this.List.isMouseOver() && base.canMoveCamera();
        }

        private void openIngame(){
            var ingame = new GuiIngame(this.CurrentPlayer);
            EvolvinaryMain.get().openGui(ingame);
            ingame.setSelectedEntity(this.SelectedEntity);
        }

        public override void onActionPerformed(Button button){
            if(button.Id == 1){
                this.openIngame();
            }
            else{
                base.onActionPerformed(button);
            }
        }
    }
}