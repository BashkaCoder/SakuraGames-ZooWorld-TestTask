using UnityEngine;
using ZooWorld.World;

namespace ZooWorld.Animals.Locomotion
{
    public abstract class LocomotionDefinition : ScriptableObject
    {
        public abstract IAnimalLocomotion Create(IRandomSource random, IWorldBounds worldBounds);
    }
}
