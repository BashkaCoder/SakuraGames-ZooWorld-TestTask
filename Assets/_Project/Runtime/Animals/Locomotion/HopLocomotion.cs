using UnityEngine;
using ZooWorld.World;

namespace ZooWorld.Animals.Locomotion
{
    public class HopLocomotion : IAnimalLocomotion
    {
        private readonly float _interval;
        private readonly float _distance;
        private readonly IRandomSource _random;
        private readonly IWorldBounds _worldBounds;
        private Vector3 _direction;
        private float _elapsed;

        public HopLocomotion(float interval, float distance, IRandomSource random, IWorldBounds worldBounds)
        {
            _interval = interval;
            _distance = distance;
            _random = random;
            _worldBounds = worldBounds;
            _direction = LocomotionDirection.RandomHorizontal(_random);
        }

        public void FixedTick(AnimalBody body, float fixedDeltaTime)
        {
            _elapsed += fixedDeltaTime;
            if (_elapsed < _interval)
            {
                return;
            }

            _elapsed = 0f;
            body.SetPlanarVelocity(Vector3.zero);
            _direction = ScreenReturnSteering.Redirect(body.Position, _direction, _worldBounds.Bounds);
            body.MoveBy(_direction * _distance);
            _direction = LocomotionDirection.RandomHorizontal(_random);
        }

        public void Redirect(Vector3 direction)
        {
            _direction = direction;
        }
    }
}
