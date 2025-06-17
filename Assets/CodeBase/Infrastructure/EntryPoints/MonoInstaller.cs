using UnityEngine;

namespace CodeBase.Infrastructure.EntryPoint
{
    public class MonoInstaller : MonoBehaviour
    {
        [SerializeField] private MonoBootStrapper monoBootStrapper;

        private void Awake()
        {
            InstallBindings();

            monoBootStrapper?.BootStrapp();
        }

        protected virtual void InstallBindings() { }
    }
}
