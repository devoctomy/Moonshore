using System.Collections.Generic;
using System.Drawing;

namespace Moonshore.Services
{
    public class MapDrawer : IMapDrawer
    {
        public int TerrainUnitSize { get; set; } = 8;

        public Bitmap Draw(
            TerrainInfo[] terrainMap,
            IEnumerable<MapItemInfo[]> mapItemLayers,
            int mapWidth,
            int mapHeight)
        {
            var bitmap = new Bitmap(
                mapWidth * TerrainUnitSize,
                mapHeight * TerrainUnitSize);
            var graphics = Graphics.FromImage(bitmap);
            var curPos = 0;
            for (var y = 0; y < mapHeight; y++)
            {
                for (var x = 0; x < mapWidth; x++)
                {
                    var curTerrain = terrainMap[curPos];
                    if (curTerrain != null)
                    {
                        using var brush = new SolidBrush(curTerrain.Colour);
                        graphics.FillRectangle(
                            brush,
                            new RectangleF(
                                x * TerrainUnitSize,
                                y * TerrainUnitSize,
                                TerrainUnitSize,
                                TerrainUnitSize));

                        foreach(var curItemLayer in mapItemLayers)
                        {
                            var curItem = curItemLayer[curPos];
                            if(curItem != null)
                            {
                                graphics.DrawImageUnscaled(
                                    curItem.Image,
                                    x * TerrainUnitSize,
                                    y * TerrainUnitSize);
                            }
                        }    
                    }

                    curPos += 1;
                }
            }

            return bitmap;
        }
    }
}
