using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.GamePlay.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        private IGameFactory gameFactory;

        [SerializeField] private EnemyID enemyID;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        public void Spawn()
        {
            gameFactory.CreateEnemy(enemyID, transform.position);
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }

#endif
    }
}
