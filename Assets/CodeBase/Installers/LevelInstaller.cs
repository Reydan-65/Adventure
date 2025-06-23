using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.LevelStates;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private HeroSpawnPoint heroSpawnPoint;

        public override void InstallBindings()
        {
            Debug.Log("LEVEL: Install");

            container.RegisterSingle(heroSpawnPoint);

            RegisterLevelStateMachine();

        }

        private void OnDestroy()
        {
            UnregisterLevelStateMachine();

            container.UnregisterSingle<HeroSpawnPoint>();
        }

        private void RegisterLevelStateMachine()
        {
            container.RegisterSingle<ILevelStateSwitcher, LevelStateMachine>();
            container.RegisterSingle<LevelBootStrappState>();
        }

        private void UnregisterLevelStateMachine()
        {
            container.UnregisterSingle<ILevelStateSwitcher>();
            container.UnregisterSingle<LevelBootStrappState>();
        }
    }
}
