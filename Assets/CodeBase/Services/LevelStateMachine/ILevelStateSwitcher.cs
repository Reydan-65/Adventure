using Assets.CodeBase.Infrastructure.StateMachine;
using Assets.CodeBase.Infrastructure.ServiceLocator;

namespace CodeBase.Infrastructure.Services.LevelStates
{
    public interface ILevelStateSwitcher : IService
    {
        object CurrentState { get; }

        void AddState<TState>(TState state) where TState : class, IState;
        void RemoveState<TState>() where TState : class, IState;
        void Enter<TState>() where TState : class, IState;
        void Exit<TState>() where TState : class, IState;
        void UpdateTick();
    }
}
