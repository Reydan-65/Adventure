using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.LevelStates
{
    public class LevelBattleState : LevelBaseState, IEnterableState, IExitableState
    {
        private int activePersuersCount;

        public LevelBattleState(IGameFactory gameFactory, ILevelStateSwitcher levelStateSwitcher)
        : base(gameFactory, levelStateSwitcher) { }

        public void Enter()
        {
            activePersuersCount = 0;

            gameFactory.HeroHealth.Die += OnHeroDie;

            SubscribeToEnemyEvents(persuer =>
            {
                persuer.PersuitTarget += OnPersuitTarget;
                persuer.LostTarget += OnLostTarget;
            });

            UpdatePersuerCount();

            Debug.Log("LEVEL: Battle State");
        }

        public void Exit()
        {
            gameFactory.HeroHealth.Die -= OnHeroDie;

            SubscribeToEnemyEvents(persuer =>
            {
                persuer.PersuitTarget -= OnPersuitTarget;
                persuer.LostTarget -= OnLostTarget;
            });
        }

        private void OnHeroDie()
        {
            levelStateSwitcher.Enter<LevelLostState>();
        }

        private void OnPersuitTarget()
        {
            activePersuersCount++;

            Debug.Log("Persuer count: " + activePersuersCount);
        }

        private void OnLostTarget()
        {
            activePersuersCount--;

            Debug.Log("Persuer count: " + activePersuersCount);

            if (activePersuersCount == 0)
                levelStateSwitcher.Enter<LevelResearchState>();
        }

        private void UpdatePersuerCount()
        {
            SubscribeToEnemyEvents(persuer => 
            { 
                if (persuer.IsPursuing)
                    activePersuersCount++;
            });
        }
    }
}
