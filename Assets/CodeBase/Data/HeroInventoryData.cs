using System;
using UnityEngine;

namespace CodeBase.Data
{
    [System.Serializable]
    public class HeroInventoryData
    {
        public event Action<int> CoinValueChanged;

        // DEBUG
        [SerializeField] private int coinAmount;

        public int CoinAmount
        {
            get => coinAmount;
            set
            {
                coinAmount = value;
                CoinValueChanged?.Invoke(coinAmount);
            }
        }
        public static HeroInventoryData Default()
        {
            return new HeroInventoryData { CoinAmount = 0 };
        }
    }
}
