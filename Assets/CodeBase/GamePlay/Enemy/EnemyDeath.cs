using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private Health health;

        private void Start()
        {
            health.Die += OnDie;
        }

        private void OnDestroy()
        {
            health.Die -= OnDie;
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }
    }
}
