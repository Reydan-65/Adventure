using CodeBase.Configs;
using CodeBase.GamePlay;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.LevelStates
{
    public class LevelResearchState : LevelBaseState, IEnterableState, ITickableState, IExitableState
    {
        private int activePersuersCount;
        private IConfigsProvider configProvider;

        private LevelConfig levelConfig;

        public LevelResearchState(IGameFactory gameFactory, ILevelStateSwitcher levelStateSwitcher,
            IConfigsProvider configProvider)
        : base(gameFactory, levelStateSwitcher)
        {
            this.configProvider = configProvider;
        }

        public void Enter()
        {
            activePersuersCount = 0;

            levelConfig = configProvider.GetLevelConfig(SceneManager.GetActiveScene().name);

            gameFactory.HeroHealth.Die += OnHeroDie;

            SubscribeToEnemyEvents(persuer =>
            {
                persuer.PersuitTarget += OnPersuitTarget;
            });

            Debug.Log("LEVEL: Research state");
        }

        public void Tick()
        {
            CheckVictory();
        }

        public void Exit()
        {
            gameFactory.HeroHealth.Die -= OnHeroDie;

            SubscribeToEnemyEvents(persuer =>
            {
                persuer.PersuitTarget -= OnPersuitTarget;
            });
        }

        private void OnHeroDie()
        {
            levelStateSwitcher.Enter<LevelLostState>();
        }

        private void CheckVictory()
        {
            if (Vector3.Distance(gameFactory.HeroObject.transform.position, levelConfig.FinishPoint) < FinishPoint.Radius)
            {
                levelStateSwitcher.Enter<LevelVictoryState>();
            }
        }

        private void OnPersuitTarget()
        {
            activePersuersCount++;

            Debug.Log("Persuer count: " + activePersuersCount);

            levelStateSwitcher.Enter<LevelBattleState>();
        }
    }
}
