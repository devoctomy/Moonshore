using Moonshore.Services;
using System;
using System.Drawing;
using System.Linq;

namespace Moonshore
{
    class Program
    {
        static void Main(string[] args)
        {
            var seed = 100;
            var noiseMapGenerator = new PerlinNoiseMapGenerator();
            noiseMapGenerator.SetParam("persistance", 0.8f);
            noiseMapGenerator.SetParam("lacunarity", 0.3f);
            noiseMapGenerator.SetParam("scale", 90f);
            var mapWidth = 1500;
            var mapHeight = 1500;
            Console.WriteLine("Generating noise map...");
            var noiseMap = noiseMapGenerator.Generate(
                seed,
                mapWidth,
                mapHeight);

            Console.WriteLine("Generating world map...");
            var terrainGenerator = new TerrainGenerator();
            terrainGenerator.StackTerrainInfo(
                "water",
                0.40f,
                Color.Blue);
            terrainGenerator.StackTerrainInfo(
                "sand",
                0.5f,
                Color.Yellow);
            terrainGenerator.StackTerrainInfo(
                "grass",
                0.65f,
                Color.Green);
            terrainGenerator.StackTerrainInfo(
                "hillside",
                0.7f,
                Color.Brown);
            terrainGenerator.StackTerrainInfo(
                "lowmountains",
                0.9f,
                Color.DarkGray);
            terrainGenerator.StackTerrainInfo(
                "highmountains",
                0.95f,
                Color.LightGray);
            terrainGenerator.StackTerrainInfo(
                "peaks",
                1f,
                Color.White);
            var terrainMap = terrainGenerator.Generate(
                noiseMap,
                mapWidth,
                mapHeight);

            Console.WriteLine("Plot map items...");
            var treePlotter = new FaunaPlotter();
            var treeNoiseMap = noiseMapGenerator.Generate(
                50,
                mapWidth,
                mapHeight);
            var treeLayer = treePlotter.Plot(
                seed,
                terrainMap,
                treeNoiseMap,
                mapWidth,
                mapHeight);

            var cavePlotter = new CavePlotter();
            var caveNoiseMap = noiseMapGenerator.Generate(
                50,
                mapWidth,
                mapHeight);
            var caveLayer = cavePlotter.Plot(
                seed,
                terrainMap,
                caveNoiseMap,
                mapWidth,
                mapHeight);


            Console.WriteLine("Drawing map...");
            var mapDrawer = new MapDrawer();
            var mapBitmap = mapDrawer.Draw(
                terrainMap,
                new MapItemInfo[][] { treeLayer, caveLayer },
                mapWidth,
                mapHeight);
            mapBitmap.Save("c:\\temp\\map.bmp");

            Console.WriteLine("Serialising to json map file...");
            var mapSerialiser = new JsonMapSerialiser();
            mapSerialiser.Save(
                "c:\\temp\\map.json",
                terrainMap,
                new MapItemInfo[][] { treeLayer, caveLayer },
                mapWidth,
                mapHeight);
        }
    }
}
