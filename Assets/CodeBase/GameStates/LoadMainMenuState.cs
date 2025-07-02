using CodeBase.GamePlay.UI;
using CodeBase.GamePlay.UI.Services;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Scene;

namespace CodeBase.Infrastructure.Services.GameStates
{
    public class LoadMainMenuState : IEnterableState, IService
    {
        private ISceneLoader sceneLoader;
        private IWindowsProvider windowsProvider;

        public LoadMainMenuState(ISceneLoader sceneLoader, IWindowsProvider windowsProvider)
        {
            this.sceneLoader = sceneLoader;
            this.windowsProvider = windowsProvider;
        }

        public void Enter()
        {
            sceneLoader.Load(Constants.MainMenuSceneName, 
                onLoaded: () => windowsProvider.Open(WindowID.MainMenuWindow));
        }
    }
}
