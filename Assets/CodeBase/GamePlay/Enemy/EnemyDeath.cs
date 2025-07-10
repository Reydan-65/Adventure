using CodeBase.Data;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GamePlay.Enemies
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private EnemyMeleeAttack meleeAttack;
        [SerializeField] private EnemyFollowToHero followToHero;
        [SerializeField] private GameObject visualModel;
        [SerializeField] private Collider enemyCollider;

        private EnemyInventory inventory;
        private ILootService lootService;

        [Inject]
        public void Construct(ILootService lootService)
        {
            this.lootService = lootService;

            inventory = GetComponent<EnemyInventory>();
            enemyCollider = GetComponent<Collider>();
            health.Die += OnDie;
        }

        private void OnDestroy()
        {
            health.Die -= OnDie;
        }

        private async void OnDie()
        {
            SetEnabled(false);

            if (inventory != null)
            {
                await DropAllLoot();
            }

            Destroy(gameObject);
        }

        private async Task DropAllLoot()
        {
            var lootTasks = new List<Task>();

            int coinsAmount = inventory.GetCoinsAmount();
            if (coinsAmount > 0)
                lootTasks.Add(lootService.DropLoot(transform.position, LootItemID.Coin, coinsAmount));

            int potionsAmount = inventory.GetHealingPotionsAmount();
            if (potionsAmount > 0)
                lootTasks.Add(lootService.DropLoot(transform.position, LootItemID.HealingPotion, potionsAmount));

            if (inventory.HasKey())
                lootTasks.Add(lootService.DropLoot(transform.position, LootItemID.Key));

            await Task.WhenAll(lootTasks);
        }

        private void SetEnabled(bool enabled)
        {
            followToHero.enabled = enabled;
            meleeAttack.enabled = enabled;
            visualModel.SetActive(enabled);
            health.enabled = enabled;
            enemyCollider.enabled = enabled;
        }
    }
}
