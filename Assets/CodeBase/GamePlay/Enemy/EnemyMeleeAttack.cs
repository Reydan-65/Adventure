using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Configs;
using UnityEngine.AI;
using UnityEngine;

namespace CodeBase.GamePlay.Enemies
{
    public class EnemyMeleeAttack : MonoBehaviour, IEnemyConfigInstaller
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private float cooldown;
        [SerializeField] private float radius;
        [SerializeField] private float damage;

        private IGameFactory gameFactory;
        private HeroHealth target;

        private float timer;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        public void InstallEnemyConfig(EnemyConfig config)
        {
            cooldown = config.AttackCooldown;
            radius = config.AttackRadius;
            damage = config.Damage;
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
            float offset = GetComponent<CapsuleCollider>().height / 2;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + offset, transform.position.z), radius);
        }

#endif

    }
}
