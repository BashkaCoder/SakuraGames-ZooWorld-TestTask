using UnityEngine;
using VContainer;
using ZooWorld.Animals.Definitions;
using ZooWorld.Animals.Interaction;
using ZooWorld.Animals.Lifecycle;

namespace ZooWorld.Animals
{
    public class Animal : MonoBehaviour, IAnimalEncounterParticipant
    {
        [SerializeField] private AnimalBody _animalBody;
        [SerializeField] private TastyFeedback _tastyFeedback;

        private AnimalDeathStatistics _statistics;
        private AnimalEncounterResolver _encounterResolver;
        private AnimalLifetime _lifetime;

        public AnimalRole Role { get; private set; }

        public bool IsAlive => _lifetime.IsAlive;
        public event System.Action ConsumedAnotherAnimal;

        [Inject]
        public void Construct(AnimalDeathStatistics statistics, AnimalEncounterResolver encounterResolver)
        {
            _statistics = statistics;
            _encounterResolver = encounterResolver;
        }

        public void Configure(AnimalDefinition definition)
        {
            Role = definition.Role;
            _lifetime = new AnimalLifetime(Role, _statistics);
            _animalBody.Configure(this, definition.Locomotion);
            _tastyFeedback.Bind(this);
        }

        public bool TryDie()
        {
            if (!_lifetime.TryDie())
            {
                return false;
            }

            Destroy(gameObject);
            return true;
        }

        public void ReportConsumption()
        {
            ConsumedAnotherAnimal?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsAlive)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out Animal other))
            {
                _encounterResolver.Resolve(this, other);
                return;
            }

            _animalBody.RedirectFromEnvironment(collision);
        }
    }
}
