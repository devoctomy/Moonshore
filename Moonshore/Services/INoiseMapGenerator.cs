namespace Moonshore.Services
{
    public interface INoiseMapGenerator
    {
        void SetParam(
            string name,
            object value);

        T GetParam<T>(string name);

        float[] Generate(
            int seed,
            int mapWidth,
            int mapHeight);
    }
}
