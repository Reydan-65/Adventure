using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Scene;
using UnityEditor.SceneManagement;

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
            string currentSceneName = EditorSceneManager.GetActiveScene().name;

            if (currentSceneName == "BootStrap")
                sceneLoader.Load("Level_1");
            else
                sceneLoader.Load(currentSceneName);
        }
    }
}
