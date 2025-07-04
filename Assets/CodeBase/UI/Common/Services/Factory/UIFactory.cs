using CodeBase.Configs;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.ConfigProvider;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GamePlay.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootGameObjectName = "UIRoot";

        private DIContainer container;
        private IAssetProvider assetProvider;
        private IConfigsProvider configProvider;

        public UIFactory(
            DIContainer container,
            IAssetProvider assetProvider,
            IConfigsProvider configProvider)
        {
            this.container = container;
            this.assetProvider = assetProvider;
            this.configProvider = configProvider;
        }

        public Transform UIRoot { get; set; }

        public async Task WarmUp()
        {
            await assetProvider.Load<GameObject>(configProvider.GetWindowConfig(WindowID.MainMenuWindow).PrefabReference);
            await assetProvider.Load<GameObject>(configProvider.GetWindowConfig(WindowID.VictoryWindow).PrefabReference);
            await assetProvider.Load<GameObject>(configProvider.GetWindowConfig(WindowID.LoseWindow).PrefabReference);
        }

        public async Task<LevelResultPresenter> CreateLevelResultWindow(WindowConfig config)
        {
            return await CreateWindowAsync<LevelResultWindow, LevelResultPresenter>(config);
        }

        public async Task<MainMenuPresenter> CreateMainMenuWindow(WindowConfig config)
        {
            return await CreateWindowAsync<MainMenuWindow, MainMenuPresenter>(config);
        }

        public void CreateUIRoot()
        {
            UIRoot = new GameObject(UIRootGameObjectName).transform;
        }

        private async Task<TPresenter> CreateWindowAsync<TWindow, TPresenter>(WindowConfig config)
            where TWindow : WindowBase
            where TPresenter : WindowPresenterBase<TWindow>
        {
            GameObject prefab = await assetProvider.Load<GameObject>(config.PrefabReference);

            TWindow window = container.Instantiate(prefab).GetComponent<TWindow>();
            window.transform.SetParent(UIRoot);
            window.SetTitle(config.Title);

            TPresenter presenter = container.CreateAndInject<TPresenter>();
            presenter.SetWindow(window);

            return presenter;
        }
    }
}
