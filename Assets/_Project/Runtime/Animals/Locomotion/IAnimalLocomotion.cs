using UnityEngine;

namespace ZooWorld.Animals.Locomotion
{
    public interface IAnimalLocomotion
    {
        public void FixedTick(AnimalBody body, float fixedDeltaTime);
        public void Redirect(Vector3 direction);
    }
}
