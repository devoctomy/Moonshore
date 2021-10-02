using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Moonshore.Services
{
    public class CavePlotter : IMapItemPlotter
    {
        private static Object _lock = new object();
        private static MapItemInfo _cave;

        public CavePlotter()
        {
            lock(_lock)
            {
                if (_cave == null)
                {
                    _cave = new MapItemInfo
                    {
                        Name = "cave",
                        Image = Image.FromFile("Resources\\cave.png")
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
            var random = new System.Random(seed);
            var allCavePositions = new List<Vector2>();
            var curPos = 0;
            for (var y = 0; y < mapHeight; y++)
            {
                for (var x = 0; x < mapWidth; x++)
                {
                    var curTerrain = terrainMap[curPos];
                    if (curTerrain != null)
                    {
                        var curSample = itemNoiseMap[curPos];
                        var curVector = new Vector2(x, y);
                        var nearbyCaves = allCavePositions.Where(x =>
                            Vector2.Distance(curVector, x) < 5f).ToList();

                        if(nearbyCaves.Count() > 0)
                        {
                            continue;
                        }

                        switch (curTerrain.Name)
                        {
                            case "grass":
                                {
                                    if (curSample >= 0.275f)
                                    {
                                        var rnd = random.NextDouble();
                                        if (rnd < 0.0001d)
                                        {
                                            itemMap[curPos] = _cave;
                                            allCavePositions.Add(curVector);
                                        }
                                    }

                                    break;
                                }

                            case "lowmountains":
                                {                                 
                                    if (curSample >= 0.275f)
                                    {
                                        var rnd = random.NextDouble();
                                        if (rnd < 0.0005d)
                                        {
                                            itemMap[curPos] = _cave;
                                            allCavePositions.Add(curVector);
                                        }
                                    }

                                    break;
                                }

                            case "highmountains":
                                {
                                    if (curSample >= 0.275f)
                                    {
                                        var rnd = random.NextDouble();
                                        if (rnd < 0.001d)
                                        {
                                            itemMap[curPos] = _cave;
                                            allCavePositions.Add(curVector);
                                        }
                                    }

                                    break;
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
