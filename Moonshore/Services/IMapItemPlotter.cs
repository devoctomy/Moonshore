namespace Moonshore.Services
{
    public interface IMapItemPlotter
    {
        MapItemInfo[] Plot(
            int seed,
            TerrainInfo[] terrainMap,
            float[] itemNoiseMap,
            int mapWidth,
            int mapHeight);
    }
}
