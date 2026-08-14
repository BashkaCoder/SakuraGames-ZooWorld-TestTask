using UnityEngine;
using ZooWorld.World;

namespace ZooWorld.Animals.Locomotion
{
    public class LinearLocomotion : IAnimalLocomotion
    {
        private readonly float _speed;
        private readonly IWorldBounds _worldBounds;
        private Vector3 _direction;

        public LinearLocomotion(float speed, Vector3 direction, IWorldBounds worldBounds)
        {
            _speed = speed;
            _direction = direction;
            _worldBounds = worldBounds;
        }

        public void FixedTick(AnimalBody body, float fixedDeltaTime)
        {
            _direction = ScreenReturnSteering.Redirect(body.Position, _direction, _worldBounds.Bounds);
            body.SetPlanarVelocity(_direction * _speed);
        }

        public void Redirect(Vector3 direction)
        {
            _direction = direction;
        }
    }
}
