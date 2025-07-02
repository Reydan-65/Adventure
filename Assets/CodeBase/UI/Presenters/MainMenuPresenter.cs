using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.GameStateMachine;
using CodeBase.Infrastructure.Services.GameStates;
using CodeBase.Infrastructure.Services.PlayerProgressProvider;
using System;

namespace CodeBase.GamePlay.UI
{
    public class MainMenuPresenter : WindowPresenterBase<MainMenuWindow>
    {
        private IGameStateSwitcher gameStateSwitcher;
        private IProgressProvider progressProvider;
        private IConfigsProvider configProvider;
        private MainMenuWindow window;

        public MainMenuPresenter(
            IGameStateSwitcher gameStateSwitcher,
            IProgressProvider progressProvider,
            IConfigsProvider configProvider)
        {
            this.gameStateSwitcher = gameStateSwitcher;
            this.progressProvider = progressProvider;
            this.configProvider = configProvider;
        }

        public override void SetWindow(MainMenuWindow window)
        {
            this.window = window;

            int currentLevelIndex = progressProvider.PlayerProgress.CurrentLevelIndex;

            if (currentLevelIndex >= configProvider.LevelAmount)
                window.HideLevelButton();
            else
                window.SetLevelIndex(currentLevelIndex);

            window.PlayButtonClicked += OnPlayButtonClicked;
            window.CleanUped += OnCleanUped;
        }

        private void OnCleanUped()
        {
            window.PlayButtonClicked -= OnPlayButtonClicked;
            window.CleanUped -= OnCleanUped;
        }

        private void OnPlayButtonClicked()
        {
            gameStateSwitcher.Enter<LoadNextLevelState>();
        }
    }
}
