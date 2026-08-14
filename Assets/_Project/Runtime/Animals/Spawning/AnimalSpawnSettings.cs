using UnityEngine;

namespace ZooWorld.Animals.Spawning
{
    [CreateAssetMenu(menuName = "Zoo World/Animals/Animal Spawn Settings", fileName = "AnimalSpawnSettings")]
    public class AnimalSpawnSettings : ScriptableObject
    {
        [field: SerializeField] public float MinDelay { get; private set; }
        [field: SerializeField] public float MaxDelay { get; private set; }
    }
}
