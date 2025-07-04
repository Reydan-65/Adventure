using CodeBase.Data;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.GamePlay.Hero
{
    public class HeroInventory : MonoBehaviour, IProgressBeforeSaveHandler, IProgressLoadHandler
    {
        #region Coins
        [SerializeField] private HeroInventoryData inventoryData = new HeroInventoryData();

        public event UnityAction<int> OnCoinAmountChanged;

        public int CoinAmount
        {
            get => inventoryData.CoinAmount;
            private set
            {
                inventoryData.CoinAmount = value;
                OnCoinAmountChanged?.Invoke(inventoryData.CoinAmount);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress?.HeroInventoryData == null) return;

            inventoryData = progress.HeroInventoryData;
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
            inventory.inventoryData = HeroInventoryData.Default();
            return inventory;
        }

        #endregion
    }
}
