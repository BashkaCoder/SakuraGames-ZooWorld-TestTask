using UnityEngine;
using ZooWorld.World;

namespace ZooWorld.Animals.Locomotion
{
    [CreateAssetMenu(menuName = "Zoo World/Locomotion/Linear", fileName = "LinearLocomotion")]
    public class LinearLocomotionDefinition : LocomotionDefinition
    {
        [field: SerializeField] public float Speed { get; private set; }

        public override IAnimalLocomotion Create(IRandomSource random, IWorldBounds worldBounds)
        {
            return new LinearLocomotion(Speed, LocomotionDirection.RandomHorizontal(random), worldBounds);
        }
    }
}
