using Assets.CodeBase.Infrastructure.ServiceLocator;
using System;

namespace CodeBase.Infrastructure.Scene
{
    public interface ISceneLoader : IService
    {
        void Load(string name, Action onLoaded = null);
    }
}