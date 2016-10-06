using System;
using Evolvinary.Launch;
using Evolvinary.Main;
using Evolvinary.Main.Guis;
using Evolvinary.Main.Guis.Lists;
using Evolvinary.Main.Input;
using Evolvinary.Main.Items;
using Evolvinary.Main.Worlds.Entities;
using Microsoft.Xna.Framework;

namespace Evolvinary.Rendering.Renderers.Guis{
    public class GuiRendererIngameInventory : GuiRendererIngame{
        private Stack cachedStack;

        public GuiRendererIngameInventory(Gui gui) : base(gui){
        }

        public override void draw(RenderManager manager, GameTime time){
            base.draw(manager, time);

            var gui = this.Gui as GuiIngameInventory;
            if(gui != null){
                var world = GameData.WorldTest;
                this.drawList(gui.List, manager, time);

                var stack = (gui.List.getSelectedComponent() as ListComponentItem)?.getStack();
                if(stack != this.cachedStack){
                    this.cachedStack = stack;

                    world.Renderer.PhantomEntity = stack != null ? stack.getHeldEntity() : null;
                }

                var phantom = world.Renderer.PhantomEntity as EntityPlaceable;
                if(phantom != null){
                    var pos = EvolvinaryMain.get().Camera.toWorldPos(InputProcessor.getMousePos().ToVector2());
                    phantom.Pos = pos;

                    var canPlace = phantom.canPlace(gui.CurrentPlayer, world, pos);
                    phantom.RenderColor = (canPlace ? Color.White : Color.Red) * 0.75F;
                }
            }
        }

        public override void onClosed(){
            GameData.WorldTest.Renderer.PhantomEntity = null;
        }
    }
}