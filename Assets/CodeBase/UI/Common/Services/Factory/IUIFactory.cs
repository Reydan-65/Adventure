using CodeBase.Configs;
using CodeBase.Infrastructure.DependencyInjection;
using UnityEngine;

namespace CodeBase.GamePlay.UI.Services
{
    public interface IUIFactory : IService
    {
        Transform UIRoot { get; set; }

        LevelResultPresenter CreateLevelResultWindow(WindowConfig config);
        MainMenuPresenter CreateMainMenuWindow(WindowConfig config);
        void CreateUIRoot();
    }
}