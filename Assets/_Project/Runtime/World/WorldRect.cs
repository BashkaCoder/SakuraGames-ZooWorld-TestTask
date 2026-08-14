using UnityEngine;

namespace ZooWorld.World
{
    public readonly struct WorldRect
    {
        public Vector2 Min { get; }
        public Vector2 Max { get; }

        public WorldRect(Vector2 center, Vector2 size)
        {
            var halfSize = size * 0.5f;
            Min = center - halfSize;
            Max = center + halfSize;
        }
    }
}
