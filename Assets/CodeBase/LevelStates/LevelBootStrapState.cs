using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.GamePlay.Enemies;
using CodeBase.Configs;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.LevelStates
{
    public class LevelBootStrapState : IEnterableState, IService
    {
        private IGameFactory gameFactory;
        private ILevelStateSwitcher levelStateSwitcher;
        private IInputService inputService;
        private IProgressSaver progressSaver;
        private IConfigsProvider configProvider;

        private int enemyAmount;
        public int EnemyAmount => enemyAmount;

        public LevelBootStrapState(IGameFactory gameFactory, ILevelStateSwitcher levelStateSwitcher,
            IInputService inputService, IProgressSaver progressSaver,
            IConfigsProvider configProvider)
        {
            this.gameFactory = gameFactory;
            this.levelStateSwitcher = levelStateSwitcher;
            this.inputService = inputService;
            this.progressSaver = progressSaver;
            this.configProvider = configProvider;
        }

        public void Enter()
        {
            Debug.Log("LEVEL: Init");

            string sceneName = SceneManager.GetActiveScene().name;
            LevelConfig levelConfig = configProvider.GetLevelConfig(sceneName);

            progressSaver.ClearObjects();

            inputService.Enable = true;

            gameFactory.EnemiesObject.Clear();

            // Временно
            EnemySpawner[] enemySpawners = GameObject.FindObjectsByType<EnemySpawner>(0);

            for (int i = 0; i < enemySpawners.Length; i++)
            {
                enemySpawners[i].Spawn();
                enemyAmount++;
            }

            gameFactory.CreateHero(levelConfig.HeroSpawnPoint, Quaternion.identity);
            gameFactory.CreateJoystick();
            gameFactory.CreateFollowCamera().SetTarget(gameFactory.HeroObject.transform);

            progressSaver.LoadProgress();

            levelStateSwitcher.Enter<LevelResearchState>();
        }
    }
}
