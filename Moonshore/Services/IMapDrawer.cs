using System.Collections.Generic;
using System.Drawing;

namespace Moonshore.Services
{
    public interface IMapDrawer
    {
        Bitmap Draw(
            TerrainInfo[] terrainMap,
            IEnumerable<MapItemInfo[]> mapItemLayers,
            int mapWidth,
            int mapHeight);
    }
}
