using System;
using System.Collections.Generic;
using System.Linq;
using Evolvinary.Helper;
using Evolvinary.Launch;
using Evolvinary.Main.Guis;
using Evolvinary.Main.Guis.Lists;
using Evolvinary.Main.Input;
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

        protected void drawList(ScrollList list, RenderManager manager, GameTime time){
            var renderer = list.getRenderer();
            if(renderer != null){
                renderer.draw(manager, time);
            }
        }

        public virtual void onOpened(){
        }

        public virtual void onClosed(){
        }

        public virtual bool shouldRenderWorld(){
            return true;
        }

        public static void drawHoveringOverlayAtMouse(SpriteBatch batch, string text, Color color){
            drawHoveringOverlayAtMouse(batch, text, color, 0);
        }

        public static void drawHoveringOverlayAtMouse(SpriteBatch batch, string text, Color color, int length){
            drawHoveringOverlay(batch, text, (int) (InputProcessor.getMouseX() / Gui.Scale+5), (int) (InputProcessor.getMouseY() / Gui.Scale+5), color, length, false);
        }

        public static void drawHoveringOverlay(SpriteBatch batch, string text, int x, int y, Color color, int length, bool canGoOffScreen){
            var font = EvolvinaryMain.get().RenderManager.NormalFont;
            var lineHeight = font.LineSpacing;

            var split = splitTextToLength(text, font, length);

            var longestLength = 0;
            foreach(var s in split){
                var l = font.MeasureString(s).X;
                if(longestLength < l){
                    longestLength = (int) l;
                }
            }

            if(!canGoOffScreen){
                var diffX = x+longestLength-Gui.getUnscaledWidth();
                if(diffX > 0){
                    x -= diffX;
                }
                x = Math.Max(4, x);
                y = Math.Max(Math.Min(y, Gui.getUnscaledHeight()-split.Length * lineHeight-4), 2);
            }

            batch.Draw(GraphicsHelper.TranslucentWhite, new Vector2(x-4, y-2), new Rectangle(0, 0, longestLength+4, split.Length * lineHeight+6), Color.Black);

            var height = 0;
            foreach(var s in split){
                batch.DrawString(font, s, new Vector2(x, y+height), color);
                height += lineHeight;
            }
        }

        public static string[] splitTextToLength(string text, SpriteFont font, int length){
            if(length > 0){
                var result = new List<string>();
                var words = text.Split(' ');
                var accumulated = "";

                foreach(var word in words){
                    if(font.MeasureString(accumulated+word).X >= length){
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

            return new[]{text+" "};
        }

        public static void drawCenteredText(RenderManager manager, string text, float scale, Rectangle area, bool centerY, Color color){
            var font = manager.NormalFont;

            var pos = area.Location;
            var x = pos.X+area.Width / 2-font.MeasureString(text).X * scale / 2;
            var y = centerY ? pos.Y+area.Height / 2-font.LineSpacing * scale / 2 : pos.Y;

            manager.Batch.DrawString(font, text, new Vector2(x, y), color, 0F, Vector2.Zero, scale, SpriteEffects.None, 0F);
        }

        public static void drawRectWithScale(RenderManager manager, Texture2D texture, Rectangle area, Rectangle source, float scale, Color color){
            var location = area.Location.ToVector2();
            var halfSize = area.Size.ToVector2() / 2;

            var render = location-halfSize * scale+halfSize;

            manager.Batch.Draw(texture, render, source, color, 0F, Vector2.Zero, scale, SpriteEffects.None, 0F);
        }
    }
}