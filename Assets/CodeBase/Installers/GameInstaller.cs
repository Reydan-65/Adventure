using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Scene;
using CodeBase.Infrastructure.Services.GameStates;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.Factory;
using Assets.CodeBase.Infrastructure.Services.GameStateMachine;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("GLOBAL: Install");

            RegisterGameServices();

            RegisterGameStateMachine();
        }

        private void RegisterGameStateMachine()
        {
            container.RegisterSingle<IGameStateSwitcher, GameStateMachine>();
            container.RegisterSingle<GameBootStrappState>();
            container.RegisterSingle<LoadNextLevelState>();
        }

        private void RegisterGameServices()
        {
            container.RegisterSingle<ICoroutineRunner, CoroutineRunner>();
            container.RegisterSingle<IAssetProvider, AssetProvider>();
            container.RegisterSingle<ISceneLoader, SceneLoader>();
            container.RegisterSingle<IInputService, InputService>();
            container.RegisterSingle<IGameFactory, GameFactory>();
        }
    }
}
