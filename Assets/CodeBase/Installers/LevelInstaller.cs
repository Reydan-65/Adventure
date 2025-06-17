using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.EntryPoint;
using Assets.CodeBase.Infrastructure.ServiceLocator;
using UnityEngine;
using CodeBase.Infrastructure.Services.LevelStates;

namespace CodeBase.Infrastructure
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private HeroSpawnPoint heroSpawnPoint;

        protected override void InstallBindings()
        {
            Debug.Log("LEVEL: Install");

            AllServices.Container.RegisterSingle<ILevelStateSwitcher>(new LevelStateMachine());
            AllServices.Container.RegisterSingle(heroSpawnPoint);
        }

        private void OnDestroy()
        {
            AllServices.Container.UnregisterSingle<HeroSpawnPoint>();
            AllServices.Container.UnregisterSingle<ILevelStateSwitcher>();
        }
    }
}
