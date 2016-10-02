using System;
using System.Collections.Generic;
using System.Linq;
using Evolvinary.Main.Input;
using Evolvinary.Rendering.Renderers.Guis.Lists;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis.Lists{
    public class ScrollList{
        public readonly List<ListComponent> Components = new List<ListComponent>();

        private ListRenderer renderer;
        protected readonly Gui Gui;
        public Rectangle Area;
        public readonly int Id;

        private int scrollOffset;

        public ScrollList(int id, Gui gui, int posX, int posY, int sizeX, int sizeY){
            this.Id = id;
            this.Gui = gui;
            this.Area = new Rectangle(posX, posY, sizeX, sizeY);

            this.setRenderer(new ListRenderer(this));
        }

        public void addComponent(ListComponent component){
            this.Components.Add(component);
            this.redefineAreas();
        }

        public void removeComponent(ListComponent component){
            this.Components.Remove(component);
            this.redefineAreas();
        }

        private void redefineAreas(){
            var totalHeight = this.scrollOffset;

            foreach(var component in this.Components){
                component.redefineArea(new Rectangle(this.Area.X, this.Area.Y+totalHeight, this.Area.Width, component.Height));
                component.IsVisible = totalHeight >= 0 && totalHeight < this.Area.Height;

                totalHeight += component.Height+4;
            }
        }

        public bool isMouseOver(){
            return this.Area.Contains(InputProcessor.getMousePos().ToVector2() / Gui.Scale);
        }

        public void update(GameTime time){
            var moused = this.isMouseOver();

            foreach(var component in this.Components.ToList()){
                component.update(time);

                if(moused && component.IsVisible && component.isMouseOver() && InputProcessor.LeftMouse.PressedOnce){
                    component.onClicked();
                }
            }

            if(moused){
                var delta = InputProcessor.getScrollDelta();
                if(delta != 0){
                    var oldOffset = this.scrollOffset;

                    var totalHeight = -this.Area.Height;
                    foreach(var component in this.Components){
                        totalHeight += component.Height+4;
                    }
                    this.scrollOffset = Math.Max(-totalHeight, Math.Min(0, this.scrollOffset+delta / 10));

                    if(this.scrollOffset != oldOffset){
                        this.redefineAreas();
                    }
                }
            }
        }

        public ListComponent getHoveredComponent(){
            if(this.isMouseOver()){
                foreach(var component in this.Components){
                    if(component.IsVisible && component.isMouseOver()){
                        return component;
                    }
                }
            }
            return null;
        }

        public void setRenderer(ListRenderer renderer){
            this.renderer = renderer;
        }

        public ListRenderer getRenderer(){
            return this.renderer;
        }
    }
}