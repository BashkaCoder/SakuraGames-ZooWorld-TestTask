using UnityEngine;
using ZooWorld.World;

namespace ZooWorld.Animals.Locomotion
{
    [CreateAssetMenu(menuName = "Zoo World/Locomotion/Hop", fileName = "HopLocomotion")]
    public class HopLocomotionDefinition : LocomotionDefinition
    {
        [field: SerializeField] public float Interval { get; private set; }
        [field: SerializeField] public float Distance { get; private set; }

        public override IAnimalLocomotion Create(IRandomSource random, IWorldBounds worldBounds)
        {
            return new HopLocomotion(Interval, Distance, random, worldBounds);
        }
    }
}
