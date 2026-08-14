using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZooWorld.Animals.Definitions;

namespace ZooWorld.Animals.Spawning
{
    public class AnimalSpawner
    {
        private readonly IObjectResolver _resolver;

        public AnimalSpawner(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public void Spawn(AnimalDefinition definition, Vector3 position)
        {
            _resolver.Instantiate(definition.Prefab, position, Quaternion.identity).Configure(definition);
        }
    }
}
