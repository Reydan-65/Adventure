﻿using CodeBase.Configs;
using CodeBase.GamePlay.Enemies;
using CodeBase.GamePlay.UI;
using CodeBase.Infrastructure.DependencyInjection;

namespace CodeBase.Infrastructure.Services.ConfigProvider
{
    public interface IConfigsProvider : IService
    {
        int LevelAmount { get; }

        void Load();

        EnemyConfig GetEnemyConfig(EnemyID enemyID);
        LevelConfig GetLevelConfig(int index);
        LevelConfig GetLevelConfig(string name);
        WindowConfig GetWindowConfig(WindowID id);
    }
}
