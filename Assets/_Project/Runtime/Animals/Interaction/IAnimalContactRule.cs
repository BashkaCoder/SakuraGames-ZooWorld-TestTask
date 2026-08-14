namespace ZooWorld.Animals.Interaction
{
    public interface IAnimalContactRule
    {
        public bool TryResolve(IAnimalEncounterParticipant first, IAnimalEncounterParticipant second);
    }
}
