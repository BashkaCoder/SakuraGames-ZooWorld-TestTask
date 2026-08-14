using VContainer;
using VContainer.Unity;
using UnityEngine;
using ZooWorld.Animals;
using ZooWorld.Animals.Definitions;
using ZooWorld.Animals.Interaction;
using ZooWorld.Animals.Lifecycle;
using ZooWorld.Animals.Spawning;
using ZooWorld.UI;
using ZooWorld.World;

namespace ZooWorld.Composition
{
    public class ZooLifetimeScope : LifetimeScope
    {
        [SerializeField] private AnimalCatalog _catalog;
        [SerializeField] private AnimalSpawnSettings _spawnSettings;
        [SerializeField] private AnimalSpawnArea _spawnArea;
        [SerializeField] private GameplayAreaBounds _gameplayBounds;
        [SerializeField] private DeathCounterPresenter _deathCounterPresenter;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_catalog);
            builder.RegisterInstance(_spawnSettings);
            builder.RegisterComponent(_spawnArea);
            builder.RegisterComponent<IWorldBounds>(_gameplayBounds);
            builder.RegisterComponent(_deathCounterPresenter);

            builder.Register<IRandomSource, UnityRandomSource>(Lifetime.Singleton);
            builder.Register<AnimalDeathStatistics>(Lifetime.Singleton);
            builder.Register<PredatorConsumesPreyRule>(Lifetime.Singleton).As<IAnimalContactRule>();
            builder.Register<PredatorDuelRule>(Lifetime.Singleton).As<IAnimalContactRule>();
            builder.Register<AnimalEncounterResolver>(Lifetime.Singleton);
            builder.Register<AnimalSpawner>(Lifetime.Singleton);
            builder.RegisterEntryPoint<AnimalSpawnLoop>();
        }
    }
}
