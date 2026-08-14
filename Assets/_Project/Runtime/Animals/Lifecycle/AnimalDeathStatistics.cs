using System;
using ZooWorld.Animals.Definitions;

namespace ZooWorld.Animals.Lifecycle
{
    public class AnimalDeathStatistics
    {
        public int DeadPreyCount { get; private set; }
        public int DeadPredatorCount { get; private set; }
        public event Action Changed;

        public void Record(AnimalRole role)
        {
            switch (role)
            {
                case AnimalRole.Prey:
                    DeadPreyCount++;
                    break;
                case AnimalRole.Predator:
                    DeadPredatorCount++;
                    break;
            }

            Changed?.Invoke();
        }
    }
}
