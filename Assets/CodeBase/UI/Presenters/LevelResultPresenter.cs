using CodeBase.Infrastructure.Services.GameStateMachine;
using CodeBase.Infrastructure.Services.GameStates;
using System;

namespace CodeBase.GamePlay.UI
{
    public class LevelResultPresenter : WindowPresenterBase<LevelResultWindow>
    {
        private IGameStateSwitcher gameStateSwitcher;
        private LevelResultWindow window;

        public LevelResultPresenter(IGameStateSwitcher gameStateSwitcher)
        {
            this.gameStateSwitcher = gameStateSwitcher;
        }

        public override void SetWindow(LevelResultWindow window)
        {
            window.MenuButtonClicked += OnMenuButtonClicked;
            window.CleanUped += OnCleanUped;

            this.window = window;
        }

        private void OnCleanUped()
        {
            window.MenuButtonClicked -= OnMenuButtonClicked;
            window.CleanUped -= OnCleanUped;
        }

        private void OnMenuButtonClicked()
        {
            gameStateSwitcher.Enter<LoadMainMenuState>();
        }
    }
}
