using System;
using System.Drawing;

namespace Moonshore.Services
{
    public class FaunaPlotter : IMapItemPlotter
    {
        private static Object _lock = new object();
        private static MapItemInfo _shortGrass;
        private static MapItemInfo _longGrass;
        private static MapItemInfo _tree;

        public FaunaPlotter()
        {
            lock(_lock)
            {
                if (_shortGrass == null)
                {
                    _shortGrass = new MapItemInfo
                    {
                        Name = "shortgrass",
                        Image = Image.FromFile("Resources\\shortgrass.png")
                    };
                }

                if (_longGrass == null)
                {
                    _longGrass = new MapItemInfo
                    {
                        Name = "longgrass",
                        Image = Image.FromFile("Resources\\longgrass.png")
                    };
                }

                if (_tree == null)
                {
                    _tree = new MapItemInfo
                    {
                        Name = "tree",
                        Image = Image.FromFile("Resources\\tree.png")
                    };
                }
            }
        }

        public MapItemInfo[] Plot(
            int seed,
            TerrainInfo[] terrainMap,
            float[] itemNoiseMap,
            int mapWidth,
            int mapHeight)
        {
            var itemMap = new MapItemInfo[mapWidth * mapHeight];
            var curPos = 0;
            for (var y = 0; y < mapHeight; y++)
            {
                for (var x = 0; x < mapWidth; x++)
                {
                    var curTerrain = terrainMap[curPos];
                    if (curTerrain != null)
                    {
                        if(curTerrain.Name == "grass")
                        {
                            var curSample = itemNoiseMap[curPos];
                            if(curSample >= 0.275f)
                            {
                                if (curSample < 0.4f)
                                {
                                    itemMap[curPos] = _shortGrass;
                                }
                                else if (curSample < 0.5f)
                                {
                                    itemMap[curPos] = _longGrass;
                                }
                                else if(curSample < 0.6f)
                                {
                                    itemMap[curPos] = _tree;
                                }
                            }
                        }
                    }

                    curPos += 1;
                }
            }

            return itemMap;
        }
    }
}
