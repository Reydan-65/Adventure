using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace CodeBase.GamePlay.UI
{
    public class MainMenuWindow : WindowBase
    {
        public event UnityAction PlayButtonClicked;

        [SerializeField] private string buttonLablePrefix = "Start Level ";
        [SerializeField] private TextMeshProUGUI levelIndex;
        [SerializeField] private Button playButton;

        private void Start()
        {
            playButton.onClick.AddListener(() => PlayButtonClicked?.Invoke());
        }

        public void SetLevelIndex(int index)
        {
            levelIndex.text = buttonLablePrefix + (index + 1).ToString();
        }

        public void HideLevelButton()
        {
            playButton.gameObject.SetActive(false);
        }
    }
}
