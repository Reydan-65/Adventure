using CodeBase.Configs;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.GamePlay.Enemies
{
    public class EnemyFollowToHero : MonoBehaviour, IEnemyConfigInstaller
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float stopDistance;
        [SerializeField] private NavMeshAgent agent;

        private IGameFactory gameFactory;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        public void InstallEnemyConfig(EnemyConfig config)
        {
            movementSpeed = config.MovementSpeed;
            stopDistance = config.StopDistance;
        }

        private void Start()
        {
            agent.speed = movementSpeed;
            agent.stoppingDistance = stopDistance;
            agent.Warp(transform.position);
        }

        private void Update()
        {
            if (gameFactory.HeroObject == null) return;
            if (Vector3.Distance(agent.transform.position, gameFactory.HeroObject.transform.position) <= stopDistance) return;

            agent.destination = gameFactory.HeroObject.transform.position;
        }
    }
}
