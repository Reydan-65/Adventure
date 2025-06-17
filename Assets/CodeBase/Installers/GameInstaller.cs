using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.EntryPoint;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Scene;
using CodeBase.Infrastructure.Services.GameStates;
using Assets.CodeBase.Infrastructure.Services.GameStateMachine;
using Assets.CodeBase.Infrastructure.ServiceLocator;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameInstaller : MonoInstaller, ICoroutineRunner
    {
        protected override void InstallBindings()
        {
            Debug.Log("GLOBAL: Install");

            RegisterInputServices();
        }

        private void RegisterInputServices()
        {
            AllServices.Container.RegisterSingle<ISceneLoader>(new SceneLoader(this));
            AllServices.Container.RegisterSingle<IGameStateSwitcher>(new GameStateMachine());
            AllServices.Container.RegisterSingle<IAssetProvider>(new AssetProvider());
            AllServices.Container.RegisterSingle<IInputService>(new InputService());
        }
    }
}
