using CodeBase.Infrastructure.DependencyInjection;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagment
{
    public interface IAssetProvider : IService
    {
        T GetPrefab<T>(string prefabPath) where T : Object;
        T Instantiate<T>(string prefabPath) where T : Object;
    }
}