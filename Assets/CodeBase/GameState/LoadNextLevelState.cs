using Assets.CodeBase.Infrastructure.ServiceLocator;
using Assets.CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.Scene;

namespace CodeBase.Infrastructure.Services.GameStates
{
    public class LoadNextLevelState : IEnterableState, IService
    {
        private ISceneLoader sceneLoader;

        public LoadNextLevelState(ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            sceneLoader.Load("Level_1");
        }
    }
}
