using NUnit.Framework;
using UnityEngine;
using ZooWorld.Animals;
using ZooWorld.Animals.Definitions;
using ZooWorld.Animals.Interaction;
using ZooWorld.Animals.Lifecycle;
using ZooWorld.World;

namespace ZooWorld.Tests
{
    public class AnimalEncounterTests
    {
        [Test]
        public void PreyMeetingPreyLeavesBothAlive()
        {
            var statistics = new AnimalDeathStatistics();
            var first = new FakeParticipant(AnimalRole.Prey, statistics);
            var second = new FakeParticipant(AnimalRole.Prey, statistics);
            var resolver = CreateResolver(true);

            var resolved = resolver.Resolve(first, second);

            Assert.That(resolved, Is.False);
            Assert.That(first.IsAlive && second.IsAlive, Is.True);
            Assert.That(statistics.DeadPreyCount, Is.EqualTo(0));
        }

        [Test]
        public void PredatorMeetingPreyKillsPreyAndReportsOneConsumption()
        {
            var statistics = new AnimalDeathStatistics();
            var predator = new FakeParticipant(AnimalRole.Predator, statistics);
            var prey = new FakeParticipant(AnimalRole.Prey, statistics);

            var resolved = CreateResolver(true).Resolve(predator, prey);

            Assert.That(resolved, Is.True);
            Assert.That(predator.IsAlive, Is.True);
            Assert.That(prey.IsAlive, Is.False);
            Assert.That(predator.ConsumptionCount, Is.EqualTo(1));
            Assert.That(statistics.DeadPreyCount, Is.EqualTo(1));
        }

        [Test]
        public void PredatorDuelLeavesExactlyOneSurvivorAndOneConsumption()
        {
            var statistics = new AnimalDeathStatistics();
            var first = new FakeParticipant(AnimalRole.Predator, statistics);
            var second = new FakeParticipant(AnimalRole.Predator, statistics);

            CreateResolver(true).Resolve(first, second);

            Assert.That(first.IsAlive ^ second.IsAlive, Is.True);
            Assert.That(first.ConsumptionCount + second.ConsumptionCount, Is.EqualTo(1));
            Assert.That(statistics.DeadPredatorCount, Is.EqualTo(1));
        }

        [Test]
        public void RepeatedEncounterCannotKillOrCountTwice()
        {
            var statistics = new AnimalDeathStatistics();
            var predator = new FakeParticipant(AnimalRole.Predator, statistics);
            var prey = new FakeParticipant(AnimalRole.Prey, statistics);
            var resolver = CreateResolver(true);

            resolver.Resolve(predator, prey);
            var resolvedAgain = resolver.Resolve(predator, prey);

            Assert.That(resolvedAgain, Is.False);
            Assert.That(predator.ConsumptionCount, Is.EqualTo(1));
            Assert.That(statistics.DeadPreyCount, Is.EqualTo(1));
        }

        [Test]
        public void StatisticsTracksRolesIndependently()
        {
            var statistics = new AnimalDeathStatistics();
            statistics.Record(AnimalRole.Prey);
            statistics.Record(AnimalRole.Predator);
            statistics.Record(AnimalRole.Predator);

            Assert.That(statistics.DeadPreyCount, Is.EqualTo(1));
            Assert.That(statistics.DeadPredatorCount, Is.EqualTo(2));
        }

        [TestCase(-2f, 0f, 1f, 0f)]
        [TestCase(2f, 0f, -1f, 0f)]
        [TestCase(0f, -2f, 0f, 1f)]
        [TestCase(0f, 2f, 0f, -1f)]
        public void BoundarySteeringReturnsDirectionTowardPlayableArea(float x, float z, float expectedX, float expectedZ)
        {
            var bounds = new WorldRect(Vector2.zero, new Vector2(2f, 2f));
            var result = ScreenReturnSteering.Redirect(new Vector3(x, 0f, z), new Vector3(-expectedX, 0f, -expectedZ), bounds);

            Assert.That(result.x * expectedX + result.z * expectedZ, Is.GreaterThan(0f));
        }

        private static AnimalEncounterResolver CreateResolver(bool nextBool)
        {
            IAnimalContactRule[] rules =
            {
                new PredatorConsumesPreyRule(),
                new PredatorDuelRule(new FixedRandomSource(nextBool))
            };

            return new AnimalEncounterResolver(rules);
        }

        private class FixedRandomSource : IRandomSource
        {
            private readonly bool _nextBool;

            public FixedRandomSource(bool nextBool)
            {
                _nextBool = nextBool;
            }

            public float Value01() => 0.5f;
            public bool NextBool() => _nextBool;
        }

        private class FakeParticipant : IAnimalEncounterParticipant
        {
            private readonly AnimalLifetime _lifetime;

            public FakeParticipant(AnimalRole role, AnimalDeathStatistics statistics)
            {
                Role = role;
                _lifetime = new AnimalLifetime(role, statistics);
            }

            public AnimalRole Role { get; }
            public bool IsAlive => _lifetime.IsAlive;
            public int ConsumptionCount { get; private set; }

            public bool TryDie() => _lifetime.TryDie();
            public void ReportConsumption() => ConsumptionCount++;
        }
    }
}
