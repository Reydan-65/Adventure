using CodeBase.Infrastructure.Scene;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.GameStates;
using CodeBase.Infrastructure.Services.PlayerProgressProvider;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.GameStateMachine;
using UnityEngine;
using CodeBase.GamePlay.UI.Services;

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
            container.RegisterSingle<GameBootStrapState>();
            container.RegisterSingle<LoadNextLevelState>();
            container.RegisterSingle<LoadMainMenuState>();
        }

        private void RegisterGameServices()
        {
            container.RegisterSingle<IConfigsProvider, ConfigsProvider>();

            container.RegisterSingle<IProgressProvider, ProgressProvider>();
            container.RegisterSingle<IProgressSaver, ProgressSaver>();

            container.RegisterSingle<ICoroutineRunner, CoroutineRunner>();
            container.RegisterSingle<IAssetProvider, AssetProvider>();
            container.RegisterSingle<ISceneLoader, SceneLoader>();
            container.RegisterSingle<IInputService, InputService>();


            container.RegisterSingle<IGameFactory, GameFactory>();

            container.RegisterSingle<IUIFactory, UIFactory>();
            container.RegisterSingle<IWindowsProvider, WindowsProvider>();
        }
    }
}
