using System;
using Evolvinary.Launch;
using Evolvinary.Main.Guis.Buttons;
using Evolvinary.Main.Input;
using Evolvinary.Main.Input.Setting;
using Evolvinary.Rendering.Renderers.Guis;
using Microsoft.Xna.Framework;

namespace Evolvinary.Main.Guis{
    public class GuiMap : Gui{
        public Color[,] MapData;

        public GuiMap(PlayerData currentPlayer) : base(currentPlayer, 0, 0, getUnscaledWidth(), getUnscaledHeight()){
        }

        public override GuiRenderer getRenderer(){
            return new GuiRendererMap(this);
        }

        public override void onOpened(){
            base.onOpened();

            var width = getUnscaledWidth();
            this.ButtonList.Add(new ButtonRenderedRect(0, this, width-40, 10, 30, 30, new Rectangle(286, 0, 30, 30)));

            var map = GameData.WorldTest;
            var sizeX = map.getWorldSizeX();
            var sizeY = map.getWorldSizeY();
            this.MapData = new Color[sizeX, sizeY];

            for(var x = 0; x < sizeX; x++){
                for(var y = 0; y < sizeY; y++){
                    var tile = map.getTile(x, y);
                    if(tile != null){
                        this.MapData[x, y] = tile.UniqueColor;
                    }
                }
            }
        }

        public override void onActionPerformed(Button button){
            if(button.Id == 0){
                EvolvinaryMain.get().openGui(new GuiIngame(this.CurrentPlayer));
            }
        }
    }
}