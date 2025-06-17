using CodeBase.Infrastructure.EntryPoint;
using CodeBase.Infrastructure.Scene;
using CodeBase.Infrastructure.Services.GameStates;
using Assets.CodeBase.Infrastructure.ServiceLocator;
using Assets.CodeBase.Infrastructure.Services.GameStateMachine;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootStrapper : MonoBootStrapper
    {
        public override void BootStrapp()
        {
            Debug.Log("GLOBAL: Init");

            IGameStateSwitcher gameStateSwitcher = AllServices.Container.Single<IGameStateSwitcher>();

            gameStateSwitcher.AddState(new GameBootStrappState(gameStateSwitcher));
            gameStateSwitcher.AddState(new LoadNextLevelState(AllServices.Container.Single<ISceneLoader>()));

            gameStateSwitcher.Enter<GameBootStrappState>();
        }
    }
}
