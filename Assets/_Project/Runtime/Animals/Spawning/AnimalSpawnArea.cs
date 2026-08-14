using UnityEngine;

namespace ZooWorld.Animals.Spawning
{
    public class AnimalSpawnArea : MonoBehaviour
    {
        private const float SpawnHeight = 0.55f;

        [field: SerializeField] public Vector2 Center { get; private set; }
        [field: SerializeField] public Vector2 Size { get; private set; }

        public Vector3 PickPosition(IRandomSource random)
        {
            var x = Center.x + ((random.Value01() - 0.5f) * Size.x);
            var z = Center.y + ((random.Value01() - 0.5f) * Size.y);
            return new Vector3(x, SpawnHeight, z);
        }
    }
}
