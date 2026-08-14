using UnityEngine;

namespace ZooWorld.Animals.Definitions
{
    [CreateAssetMenu(menuName = "Zoo World/Animals/Animal Catalog", fileName = "AnimalCatalog")]
    public class AnimalCatalog : ScriptableObject
    {
        [field: SerializeField] public AnimalDefinition[] Definitions { get; private set; }

        public AnimalDefinition Pick(IRandomSource random)
        {
            var totalWeight = 0f;
            foreach (var definition in Definitions)
            {
                totalWeight += definition.SpawnWeight;
            }

            var selection = random.Value01() * totalWeight;
            foreach (var definition in Definitions)
            {
                selection -= definition.SpawnWeight;
                if (selection <= 0f)
                {
                    return definition;
                }
            }

            return Definitions[^1];
        }
    }
}
