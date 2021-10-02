using System.Collections.Generic;
using System.Numerics;

namespace Moonshore.Services
{
    /// <summary>
    /// Perlin noise map generator
    /// Modified slightly to be instance based with parameters for setting
    /// generator values.
    /// 
    /// Modified original implementation by Code2DTutorials
    /// https://github.com/Venom0us/Code2DTutorials
    /// </summary>
    public class PerlinNoiseMapGenerator : INoiseMapGenerator
    {
        private readonly Dictionary<string, object> _params = new Dictionary<string, object>();
        private readonly PerlinNoiseGenerator _perlinNoiseGenerator = new PerlinNoiseGenerator();

        public PerlinNoiseMapGenerator()
        {
            SetParam("octaves", 5);
            SetParam("scale", 25f);
            SetParam("offset", new Vector2(0,0));
            SetParam("persistance", 0.286f);
            SetParam("lacunarity", 2.9f);
        }

        public void SetParam(
            string name,
            object value)
        {
            _params[name] = value;
        }

        public T GetParam<T>(string name)
        {
            return (T)_params[name];
        }    

        public float[] Generate(
            int seed,
            int mapWidth,
            int mapHeight)
        {
            float[] noiseMap = new float[mapWidth * mapHeight];
            var random = new System.Random(seed);

            if (GetParam<int>("octaves") < 1)
            {
                SetParam("octaves", 1);
            }

            Vector2[] octaveOffsets = new Vector2[GetParam<int>("octaves")];
            for (int i = 0; i < GetParam<int>("octaves"); i++)
            {
                float offsetX = random.Next(-100000, 100000) + GetParam<Vector2>("offset").X;
                float offsetY = random.Next(-100000, 100000) + GetParam<Vector2>("offset").Y;
                octaveOffsets[i] = new Vector2(offsetX, offsetY);
            }

            if (GetParam<float>("scale") <= 0f)
            {
                SetParam("scale", 0.0001f);
            }

            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;
            float halfWidth = mapWidth / 2f;
            float halfHeight = mapHeight / 2f;

            for (int x = 0, y; x < mapWidth; x++)
            {
                for (y = 0; y < mapHeight; y++)
                {
                    float amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;
                    for (int i = 0; i < GetParam<int>("octaves"); i++)
                    {
                        float sampleX = (x - halfWidth) / GetParam<float>("scale") * frequency + octaveOffsets[i].X;
                        float sampleY = (y - halfHeight) / GetParam<float>("scale") * frequency + octaveOffsets[i].Y;
                        float perlinValue = _perlinNoiseGenerator.Generate(sampleX, sampleY) * 2 - 1;

                        noiseHeight += perlinValue * amplitude;
                        amplitude *= GetParam<float>("persistance");
                        frequency *= GetParam<float>("lacunarity");
                    }

                    if (noiseHeight > maxNoiseHeight)
                        maxNoiseHeight = noiseHeight;
                    else if (noiseHeight < minNoiseHeight)
                        minNoiseHeight = noiseHeight;

                    noiseMap[y * mapWidth + x] = noiseHeight;
                }
            }

            for (int x = 0, y; x < mapWidth; x++)
            {
                for (y = 0; y < mapHeight; y++)
                {
                    noiseMap[y * mapWidth + x] = InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[y * mapWidth + x]);
                }
            }
            return noiseMap;
        }

        private static float Clamp01(float value)
        {
            if (value < 0F)
                return 0F;
            else if (value > 1F)
                return 1F;
            else
                return value;
        }

        private static float InverseLerp(float a, float b, float value)
        {
            if (a != b)
                return Clamp01((value - a) / (b - a));
            else
                return 0.0f;
        }
    }
}
