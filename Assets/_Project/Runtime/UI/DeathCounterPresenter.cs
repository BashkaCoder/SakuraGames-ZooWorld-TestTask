using TMPro;
using UnityEngine;
using VContainer;
using ZooWorld.Animals.Lifecycle;

namespace ZooWorld.UI
{
    public class DeathCounterPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _preyCount;
        [SerializeField] private TMP_Text _predatorCount;

        private AnimalDeathStatistics _statistics;

        [Inject]
        public void Construct(AnimalDeathStatistics statistics)
        {
            _statistics = statistics;
        }

        private void Start()
        {
            _statistics.Changed += Render;
            Render();
        }

        private void OnDestroy()
        {
            _statistics.Changed -= Render;
        }

        private void Render()
        {
            _preyCount.text = $"Dead prey: {_statistics.DeadPreyCount}";
            _predatorCount.text = $"Dead predators: {_statistics.DeadPredatorCount}";
        }
    }
}
