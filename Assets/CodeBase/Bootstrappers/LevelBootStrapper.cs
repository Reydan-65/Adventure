using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.EntryPoint;
using Assets.CodeBase.Infrastructure.ServiceLocator;
using CodeBase.Infrastructure.Services.LevelStates;

namespace CodeBase.Infrastructure
{
    public class LevelBootStrapper : MonoBootStrapper
    {
        public override void BootStrapp()
        {
            ILevelStateSwitcher levelStateSwitcher = AllServices.Container.Single<ILevelStateSwitcher>();

            levelStateSwitcher.AddState(new LevelBootStrappState(
                AllServices.Container.Single<IAssetProvider>(),
                AllServices.Container.Single<HeroSpawnPoint>()
                ));

            levelStateSwitcher.Enter<LevelBootStrappState>();
        }
    }
}
