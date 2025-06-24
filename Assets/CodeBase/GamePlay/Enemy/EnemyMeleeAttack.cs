using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine.AI;
using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{
    public class EnemyMeleeAttack : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private float cooldown;
        [SerializeField] private float radius;
        [SerializeField] private int damage;

        private IGameFactory gameFactory;
        private HeroHealth target;

        private float timer;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        private void Start()
        {
            target = gameFactory.HeroHealth;
        }

        private void Update()
        {
            if (target == null) return;

            timer += Time.deltaTime;

            if (CanAttack() == true && target.Current > 0)
            {
                StartAttack();
            }
        }

        private void AnimationEventOnHit()
        {
            if (target != null)
            {
                target.ApplyDamage(damage);
            }
        }

        private void StartAttack()
        {
            timer = 0;
            animator.PlayAttack();
        }

        private bool CanAttack()
        {
            return timer >= cooldown && agent.velocity.magnitude <= 0.1f &&
                Vector3.Distance(transform.position, target.transform.position) <= radius;
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

#endif

    }
}
