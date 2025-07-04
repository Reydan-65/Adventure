using CodeBase.Configs;
using CodeBase.GamePlay.Enemies;
using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        public event UnityAction HeroCreated;

        private IAssetProvider assetProvider;
        private DIContainer container;
        private IProgressSaver progressSaver;
        private IConfigsProvider configProvider;

        public GameFactory(IAssetProvider assetProvider, DIContainer container, IProgressSaver progressSaver, IConfigsProvider configProvider)
        {
            this.assetProvider = assetProvider;
            this.container = container;
            this.progressSaver = progressSaver;
            this.configProvider = configProvider;
        }

        public GameObject HeroObject { get; private set; }
        public VirtualJoystick VirtualJoystick { get; private set; }
        public FollowCamera FollowCamera { get; private set; }

        public HeroHealth HeroHealth { get; private set; }
        public HeroInventory HeroInventory { get; private set; }
        public List<GameObject> EnemiesObject { get; private set; } = new List<GameObject>();

        public async Task WarmUp()
        {
            EnemyConfig[] enemyConfigs = configProvider.GetAllEnemiesConfigs();

            for (int i = 0; i < enemyConfigs.Length; i++)
                await assetProvider.Load<GameObject>(enemyConfigs[i].PrefabReference);
        }

        public async Task<GameObject> CreateHeroAsync(Vector3 position, Quaternion rotation)
        {
            HeroObject = await InstantiateAndInject(AssetAddress.HeroPath);

            HeroObject.transform.position = position;
            HeroObject.transform.rotation = rotation;

            HeroHealth = HeroObject.GetComponent<HeroHealth>();
            HeroInventory = HeroInventory.Create(HeroObject.transform);

            progressSaver.AddObject(HeroObject);

            HeroCreated?.Invoke();

            return HeroObject;
        }

        public async Task<VirtualJoystick> CreateJoystickAsync()
        {
            GameObject joystickObject = await InstantiateAndInject(AssetAddress.VirtualJoystickPath);
            VirtualJoystick = joystickObject.GetComponent<VirtualJoystick>();
            return VirtualJoystick;
        }

        public async Task<FollowCamera> CreateFollowCameraAsync()
        {
            GameObject followCameraObject = await InstantiateAndInject(AssetAddress.FollowCameraPath);
            FollowCamera = followCameraObject.GetComponent<FollowCamera>();
            return FollowCamera;
        }

        public async Task<GameObject> InstantiateAndInject(string address)
        {
            GameObject newGameObject = await Addressables.InstantiateAsync(address).Task;
            container.InjectToGameObject(newGameObject);
            return newGameObject;
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

        public async Task<GameObject> CreateEnemyAsync(EnemyID id, Vector3 position)
        {
            EnemyConfig enemyConfig = configProvider.GetEnemyConfig(id);

            GameObject enemyPrefab = await assetProvider.Load<GameObject>(enemyConfig.PrefabReference);
            GameObject enemy = container.Instantiate(enemyPrefab);

            enemy.transform.position = position;

            IEnemyConfigInstaller[] enemyConfigInstallers = enemy.GetComponentsInChildren<IEnemyConfigInstaller>();

            for (int i = 0; i < enemyConfigInstallers.Length; i++)
            {
                enemyConfigInstallers[i].InstallEnemyConfig(enemyConfig);
            }

            EnemiesObject.Add(enemy);

            return enemy;
        }
    }
}
