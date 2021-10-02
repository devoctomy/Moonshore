using System.Numerics;

namespace Moonshore.Services
{
    public interface INoiseGenerator
    {
        float Generate(float x);
        float Generate(float x, float y);
        float Generate(Vector2 coord);
    }
}
