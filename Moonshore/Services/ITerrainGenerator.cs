using System.Drawing;

namespace Moonshore.Services
{
    public interface ITerrainGenerator
    {
        void StackTerrainInfo(
            string name,
            float maxPos,
            Color colour);

        TerrainInfo GetTerrainInfo(float value);

        TerrainInfo[] Generate(
            float[] noiseMap,
            int mapWidth,
            int mapHeight);
    }
}
