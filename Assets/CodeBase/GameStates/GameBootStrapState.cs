using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.GameStateMachine;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.GameStates
{
    public class GameBootStrapState : IEnterableState, IService
    {
        private IGameStateSwitcher gameStateSwitcher;
        private IProgressSaver progressSaver;
        private IConfigsProvider configProvider;

        public GameBootStrapState(IGameStateSwitcher gameStateSwitcher, IProgressSaver progressSaver, IConfigsProvider configProvider)
        {
            this.gameStateSwitcher = gameStateSwitcher;
            this.progressSaver = progressSaver;
            this.configProvider = configProvider;
        }

        public void Enter()
        {
            // Подключение к серверу
            // Подгрузка конфигов

            configProvider.Load();

            progressSaver.LoadProgress();

            Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;

            if (SceneManager.GetActiveScene().name == Constants.BootStrapSceneName ||
                SceneManager.GetActiveScene().name == Constants.MainMenuSceneName)
            {
                gameStateSwitcher.Enter<LoadMainMenuState>();
            }
        }
    }
}