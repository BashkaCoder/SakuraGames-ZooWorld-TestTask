using UnityEngine;
using ZooWorld.Animals.Locomotion;

namespace ZooWorld.Animals.Definitions
{
    [CreateAssetMenu(menuName = "Zoo World/Animals/Animal Definition", fileName = "AnimalDefinition")]
    public class AnimalDefinition : ScriptableObject
    {
        [field: SerializeField] public Animal Prefab { get; private set; }
        [field: SerializeField] public AnimalRole Role { get; private set; }
        [field: SerializeField] public LocomotionDefinition Locomotion { get; private set; }
        [field: SerializeField] public float SpawnWeight { get; private set; }
    }
}
