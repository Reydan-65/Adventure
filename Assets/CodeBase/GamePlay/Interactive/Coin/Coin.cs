using UnityEngine;

namespace CodeBase.GamePlay.Interactive
{
    public class Coin : Pickup
    {
        [SerializeField] private int coinCount = 1;

        protected override void OnPickup()
        {
            progressProvider.PlayerProgress.HeroInventoryData.CoinAmount += coinCount;
        }
    }
}
