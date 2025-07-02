using CodeBase.Configs;
using CodeBase.GamePlay.Enemies;
using CodeBase.GamePlay.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.ConfigProvider
{
    public class ConfigsProvider : IConfigsProvider
    {
        private const string EnemiesConfigPath = "Configs/Enemies";
        private const string LevelsConfigPath = "Configs/Levels";
        private const string WindowsConfigPath = "Configs/Windows";

        private Dictionary<EnemyID, EnemyConfig> enemies;
        private Dictionary<string, LevelConfig> levels;
        private Dictionary<WindowID, WindowConfig> windows;
        private LevelConfig[] levelList;

        public int LevelAmount => levelList.Length;

        public void Load()
        {
            enemies = Resources.LoadAll<EnemyConfig>(EnemiesConfigPath).ToDictionary(x => x.EnemyID, x => x);
            windows = Resources.LoadAll<WindowConfig>(WindowsConfigPath).ToDictionary(x => x.WindowID, x => x);

            levelList = Resources.LoadAll<LevelConfig>(LevelsConfigPath);
            levels = levelList.ToDictionary(x => x.SceneName, x => x);
        }

        public EnemyConfig GetEnemyConfig(EnemyID enemyID)
        {
            return enemies[enemyID];
        }

        public LevelConfig GetLevelConfig(int index)
        {
            return levelList[index];
        }

        public LevelConfig GetLevelConfig(string name)
        {
            return levels[name];
        }

        public WindowConfig GetWindowConfig(WindowID id)
        {
            return windows[id];
        }
    }
}
