using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.GameStateMachine;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using CodeBase.GamePlay.UI.Services;

namespace CodeBase.Infrastructure.Services.GameStates
{
    public class GameBootStrapState : IEnterableState, IService
    {
        private IGameStateSwitcher gameStateSwitcher;
        private IProgressSaver progressSaver;
        private IConfigsProvider configProvider;
        private IUIFactory uiFactory;

        public GameBootStrapState(
            IGameStateSwitcher gameStateSwitcher,
            IProgressSaver progressSaver,
            IConfigsProvider configProvider,
            IUIFactory uiFactory)
        {
            this.gameStateSwitcher = gameStateSwitcher;
            this.progressSaver = progressSaver;
            this.configProvider = configProvider;
            this.uiFactory = uiFactory;
        }

        public void Enter()
        {
            uiFactory.WarmUp();

            configProvider.Load();

            progressSaver.LoadProgress();

            Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;

            Addressables.InitializeAsync();

            if (SceneManager.GetActiveScene().name == Constants.BootStrapSceneName ||
                SceneManager.GetActiveScene().name == Constants.MainMenuSceneName)
            {
                gameStateSwitcher.Enter<LoadMainMenuState>();
            }
        }
    }
}