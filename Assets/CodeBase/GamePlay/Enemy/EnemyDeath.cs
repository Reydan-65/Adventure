using UnityEngine;

namespace CodeBase.GamePlay.Enemies
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
