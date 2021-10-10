using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moonshore.Services
{
    public class JsonMapSerialiser : IMapSerialiser
    {
        public void Save(
            string fileName,
            TerrainInfo[] terrainMap,
            IEnumerable<MapItemInfo[]> mapItemLayers,
            int mapWidth,
            int mapHeight)
        {
            var json = new JObject();

            var info = new JObject();
            info.Add("Width", new JValue(mapWidth));
            info.Add("Height", new JValue(mapWidth));
            json.Add("Info", info);

            var terrainTypes = new JArray();
            var distinctTypes = terrainMap.Distinct().ToList();
            foreach(var curType in distinctTypes)
            {
                if(curType == null)
                {
                    continue;
                }    

                var terrainType = new JObject();
                terrainType.Add("Index", new JValue(distinctTypes.IndexOf(curType)));
                terrainType.Add("Name", new JValue(curType.Name));
                terrainType.Add("Colour", new JValue($"{curType.Colour.R},{curType.Colour.G},{curType.Colour.B}"));
                terrainTypes.Add(terrainType);
            }
            json.Add("TerrainTypes", terrainTypes);

            var map = new JArray();
            var curPos = 0;
            for (var y = 0; y < mapHeight; y++)
            {
                var curRow = new StringBuilder();
                for (var x = 0; x < mapWidth; x++)
                {
                    var curTerrain = terrainMap[curPos];
                    curRow.Append(distinctTypes.IndexOf(curTerrain));
                    curRow.Append(",");

                    curPos += 1;
                }

                curRow.Length -= 1;
                map.Add(new JValue(curRow.ToString()));
            }

            json.Add("Map", map);
            var jsonRaw = json.ToString(Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(
                fileName,
                jsonRaw);
        }
    }
}
