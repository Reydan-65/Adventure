using CodeBase.GamePlay.UI.Services;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.LevelStates
{
    public class LevelLostState : LevelBaseState, IEnterableState
    {
        private IInputService inputService;
        private IWindowsProvider windowsProvider;

        public LevelLostState(
            IGameFactory gameFactory,
            ILevelStateSwitcher levelStateSwitcher,
            IInputService inputService,
            IWindowsProvider windowsProvider)
            : base(gameFactory, levelStateSwitcher)
        {
            this.inputService = inputService;
            this.windowsProvider = windowsProvider;
        }

        public void Enter()
        {
            inputService.Enable = false;
            gameFactory.VirtualJoystick.gameObject.SetActive(false);

            windowsProvider.Open(GamePlay.UI.WindowID.LoseWindow);
        }
    }
}
