using CodeBase.Data;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.GamePlay.Hero
{
    public class HeroInventory : MonoBehaviour, IProgressBeforeSaveHandler, IProgressLoadHandler
    {
        #region Coins
        [SerializeField] private HeroInventoryData _inventoryData = new HeroInventoryData();

        public event UnityAction<int> OnCoinAmountChanged;

        public int CoinAmount
        {
            get => _inventoryData.CoinAmount;
            private set
            {
                _inventoryData.CoinAmount = value;
                OnCoinAmountChanged?.Invoke(_inventoryData.CoinAmount);
            }
        }

        public void AddCoins(int amount)
        {
            if (amount < 0) return;
            CoinAmount += amount;
        }

        public bool SpendCoins(int amount)
        {
            if (amount < 0 || CoinAmount < amount)
                return false;

            CoinAmount -= amount;
            return true;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress?.HeroInventoryData == null) return;

            _inventoryData = progress.HeroInventoryData;
            OnCoinAmountChanged?.Invoke(CoinAmount);
        }

        public void UpdateProgressBeforeSave(PlayerProgress progress)
        {
            if (progress?.HeroInventoryData == null) return;

            progress.HeroInventoryData.CoinAmount = CoinAmount;
        }

        public static HeroInventory Create(Transform parent = null)
        {
            GameObject go = new GameObject("HeroInventory");
            if (parent != null)
                go.transform.SetParent(parent);

            var inventory = go.AddComponent<HeroInventory>();
            inventory._inventoryData = HeroInventoryData.Default();
            return inventory;
        }

        #endregion
    }
}
