using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using ZooWorld.Animals;

namespace ZooWorld.Tests
{
    public class ZooWorldCompositionSmokeTests
    {
        private const float SpawnTimeoutSeconds = 3f;

        [UnityTest]
        public IEnumerator ConfiguredSceneSpawnsInjectedAnimal()
        {
            SceneManager.LoadScene("ZooWorld", LoadSceneMode.Single);

            var timeout = Time.realtimeSinceStartup + SpawnTimeoutSeconds;
            var animals = Object.FindObjectsByType<Animal>(FindObjectsSortMode.None);
            while (animals.Length == 0 && Time.realtimeSinceStartup < timeout)
            {
                yield return null;
                animals = Object.FindObjectsByType<Animal>(FindObjectsSortMode.None);
            }

            Assert.That(animals.Length, Is.GreaterThan(0));
            Assert.That(animals[0].IsAlive, Is.True);
        }
    }
}
