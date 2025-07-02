using CodeBase.GamePlay.UI;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "WindowConfig", menuName = "Configs/Window")]
    public class WindowConfig : ScriptableObject
    {
        public WindowID WindowID;
        public string Title;
        public GameObject Prefab;
    }
}