﻿using System;
using System.Collections.Generic;
using System.Linq;
using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Guis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolvinary.Rendering.Renderers.Guis{
    public class GuiRenderer{
        public Gui Gui;

        public GuiRenderer(Gui gui){
            this.Gui = gui;
        }

        public virtual void draw(RenderManager manager, GameTime time){
            foreach(var button in Gui.ButtonList){
                var renderer = button.getRenderer();
                if(renderer != null){
                    renderer.draw(manager, time);
                }
            }
        }

        public virtual void onOpened(){
        }

        public virtual void onClosed(){
        }

        public static void drawHoveringOverlayAtMouse(SpriteBatch batch, string text, Color color, int length){
            var input = EvolvinaryMain.get().Inputs;
            drawHoveringOverlay(batch, text, (int) (input.getMouseX() / Gui.Scale)+5, (int) (input.getMouseY() / Gui.Scale)+5, color, length);
        }

        public static void drawHoveringOverlay(SpriteBatch batch, string text, int x, int y, Color color, int length){
            var font = EvolvinaryMain.get().RenderManager.NormalFont;
            const int lineHeight = 12;

            var split = splitTextToLength(text, font, length);

            var diffX = x+length / Gui.Scale-Gui.getUnscaledWidth()+2;
            if(diffX > 0){
                x -= (int) diffX;
            }
            x = Math.Max(2, x);
            y = Math.Max(Math.Min(y, Gui.getUnscaledHeight()-split.Length * lineHeight-6), 2);

            batch.Draw(GraphicsHelper.TranslucentGray, new Vector2(x-4, y-2), new Rectangle(0, 0, (int) (length/Gui.Scale+6), split.Length * lineHeight+8), Color.White);

            var height = 0;
            foreach(var s in split){
                batch.DrawString(font, s, new Vector2(x, y+height), color);
                height += lineHeight;
            }
        }

        public static string[] splitTextToLength(string text, SpriteFont font, int length){
            var result = new List<string>();
            var words = text.Split(' ');
            var accumulated = "";

            foreach(var word in words){
                if(font.MeasureString(accumulated+word).X >= length / Gui.Scale){
                    result.Add(accumulated);
                    accumulated = word+" ";
                }
                else{
                    accumulated += word+" ";
                }
            }

            result.Add(accumulated);

            return result.ToArray();
        }
    }
}