using UnityEngine;
using VContainer;
using ZooWorld.Animals.Definitions;
using ZooWorld.Animals.Locomotion;
using ZooWorld.World;

namespace ZooWorld.Animals
{
    [RequireComponent(typeof(Rigidbody))]
    public class AnimalBody : MonoBehaviour
    {
        [SerializeField] private Rigidbody _body;

        private IRandomSource _random;
        private IWorldBounds _worldBounds;
        private IAnimalLocomotion _locomotion;
        private Animal _animal;

        public Vector3 Position => _body.position;

        [Inject]
        public void Construct(IRandomSource random, IWorldBounds worldBounds)
        {
            _random = random;
            _worldBounds = worldBounds;
        }

        public void Configure(Animal owner, LocomotionDefinition locomotionDefinition)
        {
            _animal = owner;
            _locomotion = locomotionDefinition.Create(_random, _worldBounds);
        }

        public void SetPlanarVelocity(Vector3 velocity)
        {
            _body.linearVelocity = new Vector3(velocity.x, 0f, velocity.z);
        }

        public void MoveBy(Vector3 displacement)
        {
            _body.MovePosition(_body.position + new Vector3(displacement.x, 0f, displacement.z));
        }

        public void RedirectFromEnvironment(Collision collision)
        {
            var contact = collision.GetContact(0);
            var direction = contact.normal;
            if (Vector3.Dot(direction, _body.position - contact.point) < 0f)
            {
                direction = -direction;
            }

            direction.y = 0f;
            if (direction.sqrMagnitude < Mathf.Epsilon)
            {
                return;
            }

            _locomotion.Redirect(direction.normalized);
        }

        private void FixedUpdate()
        {
            if (!_animal.IsAlive)
            {
                return;
            }

            _locomotion.FixedTick(this, Time.fixedDeltaTime);
        }
    }
}
