using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ZooWorld.Animals
{
    public class TastyFeedback : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private float _visibleSeconds;

        private Animal _animal;
        private CancellationToken _destructionToken;
        private int _displayVersion;

        private void Awake()
        {
            _destructionToken = this.GetCancellationTokenOnDestroy();
            _label.gameObject.SetActive(false);
        }

        public void Bind(Animal owner)
        {
            _animal = owner;
            _animal.ConsumedAnotherAnimal += Show;
        }

        private void Show()
        {
            _displayVersion++;
            ShowAsync(_displayVersion, _destructionToken).Forget(Debug.LogException);
        }

        private async UniTask ShowAsync(int version, CancellationToken cancellationToken)
        {
            _label.gameObject.SetActive(true);
            var cancelled = await UniTask.Delay(TimeSpan.FromSeconds(_visibleSeconds), cancellationToken: cancellationToken).SuppressCancellationThrow();
            if (cancelled)
            {
                return;
            }

            if (version == _displayVersion)
            {
                _label.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            _animal.ConsumedAnotherAnimal -= Show;
        }
    }
}
