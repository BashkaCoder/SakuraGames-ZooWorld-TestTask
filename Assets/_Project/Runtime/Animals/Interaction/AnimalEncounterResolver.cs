using System.Collections.Generic;

namespace ZooWorld.Animals.Interaction
{
    public class AnimalEncounterResolver
    {
        private readonly IReadOnlyList<IAnimalContactRule> _rules;

        public AnimalEncounterResolver(IReadOnlyList<IAnimalContactRule> rules)
        {
            _rules = rules;
        }

        public bool Resolve(IAnimalEncounterParticipant first, IAnimalEncounterParticipant second)
        {
            if (!first.IsAlive || !second.IsAlive)
            {
                return false;
            }

            foreach (var rule in _rules)
            {
                if (rule.TryResolve(first, second))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
