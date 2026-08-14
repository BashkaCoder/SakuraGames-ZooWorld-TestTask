using ZooWorld.Animals.Definitions;

namespace ZooWorld.Animals.Interaction
{
    public class PredatorDuelRule : IAnimalContactRule
    {
        private readonly IRandomSource _random;

        public PredatorDuelRule(IRandomSource random)
        {
            _random = random;
        }

        public bool TryResolve(IAnimalEncounterParticipant first, IAnimalEncounterParticipant second)
        {
            if (first.Role != AnimalRole.Predator || second.Role != AnimalRole.Predator)
            {
                return false;
            }

            var survivor = _random.NextBool() ? first : second;
            var defeated = ReferenceEquals(survivor, first) ? second : first;
            
            if (!defeated.TryDie())
            {
                return false;
            }

            survivor.ReportConsumption();
            return true;
        }
    }
}
