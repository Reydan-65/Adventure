using CodeBase.Configs;
using CodeBase.GamePlay.Enemies;
using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
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

        public GameObject CreateHero(Vector3 position, Quaternion rotation)
        {
            HeroObject = CreateGameObjectFromPrefab(AssetPath.HeroPath);
            HeroObject.transform.position = position;
            HeroObject.transform.rotation = rotation;

            HeroHealth = HeroObject.GetComponent<HeroHealth>();
            HeroInventory = HeroInventory.Create(HeroObject.transform);

            progressSaver.AddObject(HeroObject);

            return HeroObject;
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

        public GameObject CreateEnemy(EnemyID id, Vector3 position)
        {
            EnemyConfig enemyConfig = configProvider.GetEnemyConfig(id);

            GameObject enemyPrefab = enemyConfig.Prefab;
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
