using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.PlayerProgressProvider;
using TMPro;
using UnityEngine;

namespace CodeBase.GamePlay.UI
{
    public class HeroCoinText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private IProgressProvider progressProvider;

        [Inject]
        public void Construct(IProgressProvider progressProvider)
        {
            this.progressProvider = progressProvider;
        }

        private void Start()
        {
            progressProvider.PlayerProgress.HeroInventoryData.CoinValueChanged += OnCoinChanged;

            OnCoinChanged(progressProvider.PlayerProgress.HeroInventoryData.CoinAmount);
        }

        private void OnDestroy()
        {
            progressProvider.PlayerProgress.HeroInventoryData.CoinValueChanged -= OnCoinChanged;
        }

        private void OnCoinChanged(int coinsValue)
        {
            text.text = coinsValue.ToString();
        }
    }
}
