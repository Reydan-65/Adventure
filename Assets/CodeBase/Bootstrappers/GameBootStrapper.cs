using CodeBase.Infrastructure.Services.GameStates;
using CodeBase.Infrastructure.DependencyInjection;
using Assets.CodeBase.Infrastructure.Services.GameStateMachine;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootStrapper : MonoBootStrapper
    {
        private IGameStateSwitcher gameStateSwitcher;
        private GameBootStrappState gameBootStrappState;
        private LoadNextLevelState loadNextLevelState;

        [Inject]
        public void Construct(IGameStateSwitcher gameStateSwitcher, 
                              GameBootStrappState gameBootStrappState, 
                              LoadNextLevelState loadNextLevelState)
        {
            this.gameStateSwitcher = gameStateSwitcher;
            this.gameBootStrappState = gameBootStrappState;
            this.loadNextLevelState = loadNextLevelState;
        }

        public override void OnBindResolved()
        {
            Debug.Log("GLOBAL: Init");

            InitGameStateMachine();
        }

        private void InitGameStateMachine()
        {
            gameStateSwitcher.AddState(gameBootStrappState);
            gameStateSwitcher.AddState(loadNextLevelState);

            gameStateSwitcher.Enter<GameBootStrappState>();
        }
    }
}
