using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;
using ZooWorld.Animals.Definitions;

namespace ZooWorld.Animals.Spawning
{
    public class AnimalSpawnLoop : IAsyncStartable
    {
        private readonly AnimalCatalog _catalog;
        private readonly AnimalSpawner _spawner;
        private readonly AnimalSpawnArea _spawnArea;
        private readonly IRandomSource _random;
        private readonly AnimalSpawnSettings _settings;

        public AnimalSpawnLoop(AnimalCatalog catalog, AnimalSpawner spawner, AnimalSpawnArea spawnArea, IRandomSource random, AnimalSpawnSettings settings)
        {
            _catalog = catalog;
            _spawner = spawner;
            _spawnArea = spawnArea;
            _random = random;
            _settings = settings;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var delay = Mathf.Lerp(_settings.MinDelay, _settings.MaxDelay, _random.Value01());
                var cancelled = await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cancellationToken).SuppressCancellationThrow();
                if (cancelled)
                {
                    break;
                }

                var definition = _catalog.Pick(_random);
                _spawner.Spawn(definition, _spawnArea.PickPosition(_random));
            }
        }
    }
}
