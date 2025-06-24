using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{
    public class EnemyHeroPersuer : MonoBehaviour
    {
        [SerializeField] private EnemyFollowToHero followToHero;
        [SerializeField] private float viewDistance;

        private IGameFactory gameFactory;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        private void Start()
        {
            followToHero.enabled = false;
        }

        private void Update()
        {
            if (gameFactory.HeroObject == null) return;

            if (Vector3.Distance(followToHero.transform.position, gameFactory.HeroObject.transform.position) <= viewDistance)
            {
                StartPersuit();
            }
            else
            {
                StopPersuit();
            }
        }

        private void StartPersuit()
        {
            followToHero.enabled = true;
        }

        private void StopPersuit()
        {
            followToHero.enabled = false;
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, viewDistance);
        }
#endif
    }
}
