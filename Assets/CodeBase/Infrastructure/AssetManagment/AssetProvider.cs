using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssetProvider
    {
        public T GetPrefab<T>(string prefabPath) where T : Object
        {
            return Resources.Load<T>(prefabPath);
        }

        public T Instantiate<T>(string prefabPath) where T : Object
        {
            T obj = Resources.Load<T>(prefabPath);
            return GameObject.Instantiate(obj);
        }
    }
}