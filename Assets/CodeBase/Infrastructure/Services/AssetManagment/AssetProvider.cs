using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssetProvider
    {
        private Dictionary<string, AsyncOperationHandle> cacheHandle =
            new Dictionary<string, AsyncOperationHandle>();

        private Dictionary<string, List<AsyncOperationHandle>> allHandles =
            new Dictionary<string, List<AsyncOperationHandle>>();

        public T GetPrefab<T>(string prefabPath) where T : Object
        {
            return Resources.Load<T>(prefabPath);
        }

        public T Instantiate<T>(string prefabPath) where T : Object
        {
            T obj = Resources.Load<T>(prefabPath);
            return GameObject.Instantiate(obj);
        }

        public async Task<TType> Load<TType>(AssetReference assetReference) 
            where TType : class
        {
            if (cacheHandle.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle handle))
            {
                return handle.Result as TType;
            }

            AsyncOperationHandle<TType> loadOperationHandle = Addressables.LoadAssetAsync<TType>(assetReference.AssetGUID);

            loadOperationHandle.Completed += (h) =>
            {
                cacheHandle[assetReference.AssetGUID] = h;
            };

            AddHandle(assetReference.AssetGUID, loadOperationHandle);

            return await loadOperationHandle.Task;
        }

        private void AddHandle<TType>(string assetGUID, AsyncOperationHandle<TType> operationHandle) 
            where TType : class
        {
            if (allHandles.TryGetValue(assetGUID, out List<AsyncOperationHandle> handles) == false)
            {
                handles = new List<AsyncOperationHandle>();
                allHandles[assetGUID] = handles;
            }

            handles.Add(operationHandle);
        }

        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> operationHandles in allHandles.Values)
            {
                foreach (AsyncOperationHandle handle in operationHandles)
                {
                    Addressables.Release(handle);
                }
            }

            allHandles.Clear();
            cacheHandle.Clear();
        }
    }
}