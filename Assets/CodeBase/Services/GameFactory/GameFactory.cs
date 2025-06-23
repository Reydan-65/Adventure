using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.DependencyInjection;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider assetProvider;
        private DIContainer container;

        public GameObject Hero { get; private set; }
        public VirtualJoystick VirtualJoystick { get; private set; }
        public FollowCamera FollowCamera { get; private set; }

        public GameFactory(IAssetProvider assetProvider, DIContainer container)
        {
            this.assetProvider = assetProvider;
            this.container = container;
        }

        public GameObject CreateHero(Vector3 position, Quaternion rotation)
        {
            Hero = CreateGameObjectFromPrefab(AssetPath.HeroPath);
            Hero.transform.position = position;
            Hero.transform.rotation = rotation;
            return Hero;
        }

        public VirtualJoystick CreateJoystick()
        {
            VirtualJoystick = CreateComponentFromPrefab<VirtualJoystick>(AssetPath.VirtualJoystickPath);
            return VirtualJoystick;
        }

        public FollowCamera CreateFollowCamera()
        {
            FollowCamera = CreateComponentFromPrefab<FollowCamera>(AssetPath.FollowCameraPath);
            return FollowCamera;
        }

        private GameObject CreateGameObjectFromPrefab(string prefabPath)
        {
            GameObject prefab = assetProvider.GetPrefab<GameObject>(prefabPath);
            return container.Instantiate(prefab);
        }

        private T CreateComponentFromPrefab<T>(string prefabPath) where T : Component
        {
            GameObject prefab = assetProvider.GetPrefab<GameObject>(prefabPath);
            GameObject obj = container.Instantiate(prefab);
            return obj.GetComponent<T>();
        }
    }
}
