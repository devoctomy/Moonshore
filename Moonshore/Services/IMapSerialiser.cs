using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshore.Services
{
    public interface IMapSerialiser
    {
        public void Save(
            string fileName,
            TerrainInfo[] terrainMap,
            IEnumerable<MapItemInfo[]> mapItemLayers,
            int mapWidth,
            int mapHeight);
    }
}
