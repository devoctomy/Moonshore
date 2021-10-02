using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Moonshore.Services
{
    public class TerrainGenerator : ITerrainGenerator
    {
        private List<TerrainInfo> _terrain = new List<TerrainInfo>();

        public void StackTerrainInfo(
            string name,
            float maxPos,
            Color colour)
        {
            var minPos = _terrain.Count > 0 ? _terrain[_terrain.Count -1].MaxPos : 0;
            if(minPos < 1.0f)
            {
                _terrain.Add(new TerrainInfo
                {
                    Name = name,
                    MinPos = minPos,
                    MaxPos = maxPos,
                    Colour = colour
                });
            }
            else
            {
                throw new System.Exception("Cannot stack terrain info any higher.");
            }
        }

        public TerrainInfo GetTerrainInfo(float value)
        {
            var terrain = _terrain.SingleOrDefault(x =>
                value > x.MinPos && 
                value < x.MaxPos);
            return terrain;
        }

        public TerrainInfo[] Generate(
            float[] noiseMap,
            int mapWidth,
            int mapHeight)
        {
            var terrainMap = new TerrainInfo[mapWidth * mapHeight];
            var curPos = 0;
            for (var y = 0; y < mapHeight; y++)
            {
                for (var x = 0; x < mapWidth; x++)
                {
                    var curTerrain = GetTerrainInfo(noiseMap[curPos]);
                    if (curTerrain != null)
                    {
                        terrainMap[curPos] = curTerrain;
                    }

                    curPos += 1;
                }
            }

            return terrainMap;
        }
    }
}
