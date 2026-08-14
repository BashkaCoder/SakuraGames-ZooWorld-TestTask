using ZooWorld.Animals.Definitions;

namespace ZooWorld.Animals.Interaction
{
    public interface IAnimalEncounterParticipant
    {
        public AnimalRole Role { get; }
        public bool IsAlive { get; }
        public bool TryDie();
        public void ReportConsumption();
    }
}
