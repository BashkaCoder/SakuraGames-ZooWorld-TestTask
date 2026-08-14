using ZooWorld.Animals.Definitions;

namespace ZooWorld.Animals.Interaction
{
    public class PredatorConsumesPreyRule : IAnimalContactRule
    {
        public bool TryResolve(IAnimalEncounterParticipant first, IAnimalEncounterParticipant second)
        {
            if (first.Role == AnimalRole.Predator && second.Role == AnimalRole.Prey)
            {
                return TryConsume(first, second);
            }

            if (second.Role == AnimalRole.Predator && first.Role == AnimalRole.Prey)
            {
                return TryConsume(second, first);
            }

            return false;
        }

        private static bool TryConsume(IAnimalEncounterParticipant predator, IAnimalEncounterParticipant prey)
        {
            if (!prey.TryDie())
            {
                return false;
            }

            predator.ReportConsumption();
            return true;
        }
    }
}
