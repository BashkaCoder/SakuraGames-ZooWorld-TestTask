using ZooWorld.Animals.Definitions;

namespace ZooWorld.Animals.Lifecycle
{
    public class AnimalLifetime
    {
        private readonly AnimalRole _role;
        private readonly AnimalDeathStatistics _statistics;

        public bool IsAlive { get; private set; } = true;

        public AnimalLifetime(AnimalRole role, AnimalDeathStatistics statistics)
        {
            _role = role;
            _statistics = statistics;
        }

        public bool TryDie()
        {
            if (!IsAlive)
            {
                return false;
            }

            IsAlive = false;
            _statistics.Record(_role);
            return true;
        }
    }
}
