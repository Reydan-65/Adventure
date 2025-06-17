using Assets.CodeBase.Infrastructure.ServiceLocator;
using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.AssetManagment;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.LevelStates
{
    public class LevelBootStrappState : IEnterableState, IService
    {
        private IAssetProvider assetProvider;
        private HeroSpawnPoint heroSpawnPoint;

        public LevelBootStrappState(IAssetProvider assetProvider, HeroSpawnPoint heroSpawnPoint)
        {
            this.assetProvider = assetProvider;
            this.heroSpawnPoint = heroSpawnPoint;
        }

        public void Enter()
        {
            Debug.Log("LEVEL: Init");

            GameObject hero = assetProvider.Instantiate<GameObject>(AssetPath.HeroPath);
            hero.transform.position = heroSpawnPoint.transform.position;
            hero.transform.rotation = heroSpawnPoint.transform.rotation;

            FollowCamera followCamera = assetProvider.Instantiate<FollowCamera>(AssetPath.FollowCameraPath);
            followCamera.SetTarget(hero.transform);

            assetProvider.Instantiate<GameObject>(AssetPath.VirtualJoystickPath);
        }
    }
}
