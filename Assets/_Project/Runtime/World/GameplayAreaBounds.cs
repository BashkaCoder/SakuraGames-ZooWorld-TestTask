using UnityEngine;

namespace ZooWorld.World
{
    public class GameplayAreaBounds : MonoBehaviour, IWorldBounds
    {
        [field: SerializeField] public Vector2 Center { get; private set; }
        [field: SerializeField] public Vector2 Size { get; private set; }

        public WorldRect Bounds => new(Center, Size);
    }
}
