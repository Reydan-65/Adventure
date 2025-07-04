using CodeBase.Configs;
using CodeBase.Infrastructure.DependencyInjection;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GamePlay.UI.Services
{
    public interface IUIFactory : IService
    {
        Transform UIRoot { get; set; }
        Task WarmUp();
        Task<LevelResultPresenter> CreateLevelResultWindow(WindowConfig config);
        Task<MainMenuPresenter> CreateMainMenuWindow(WindowConfig config);
        void CreateUIRoot();
    }
}