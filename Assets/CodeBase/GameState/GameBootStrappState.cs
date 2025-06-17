using Assets.CodeBase.Infrastructure.ServiceLocator;
using Assets.CodeBase.Infrastructure.Services.GameStateMachine;
using Assets.CodeBase.Infrastructure.StateMachine;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.GameStates
{
    public class GameBootStrappState : IEnterableState, IService
    {
        private IGameStateSwitcher gameStateSwitcher;

        public GameBootStrappState(IGameStateSwitcher gameStateSwitcher)
        {
            this.gameStateSwitcher = gameStateSwitcher;
        }

        public void Enter()
        {
            // ����������� � �������
            // ��������� ��������

            Application.targetFrameRate = (int) Screen.currentResolution.refreshRateRatio.value;

            gameStateSwitcher.Enter<LoadNextLevelState>();
        }
    }
}