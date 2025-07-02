using CodeBase.Configs;
using CodeBase.Infrastructure.DependencyInjection;
using UnityEngine;

namespace CodeBase.GamePlay.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootGameObjectName = "UIRoot";

        private DIContainer container;
        public UIFactory(DIContainer container)
        {
            this.container = container;
        }

        public Transform UIRoot { get; set; }

        public LevelResultPresenter CreateLevelResultWindow(WindowConfig config)
        {
            return CreateWindow<LevelResultWindow, LevelResultPresenter>(config);
        }

        public MainMenuPresenter CreateMainMenuWindow(WindowConfig config)
        {
            return CreateWindow<MainMenuWindow, MainMenuPresenter>(config);
        }

        public void CreateUIRoot()
        {
            UIRoot = new GameObject(UIRootGameObjectName).transform;
        }

        private TPresenter CreateWindow<TWindow, TPresenter>(WindowConfig config)
            where TWindow : WindowBase
            where TPresenter : WindowPresenterBase<TWindow>
        {
            TWindow window = container.Instantiate(config.Prefab).GetComponent<TWindow>();
            window.transform.SetParent(UIRoot);
            window.SetTitle(config.Title);

            TPresenter presenter = container.CreateAndInject<TPresenter>();
            presenter.SetWindow(window);

            return presenter;
        }
    }
}
