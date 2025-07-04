using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace CodeBase.GamePlay.UI
{
    public class MainMenuWindow : WindowBase
    {
        public event UnityAction PlayButtonClicked;
        public event UnityAction ResetButtonClicked;

        [SerializeField] private TextMeshProUGUI levelIndex;
        [SerializeField] private Button playButton;
        [SerializeField] private Button resetButton;

        private void Start()
        {
            playButton.onClick.AddListener(() => PlayButtonClicked?.Invoke());
            resetButton.onClick.AddListener(() => ResetButtonClicked?.Invoke());
        }

        public void SetLevelIndex(int index)
        {
            levelIndex.text = Constants.PlayButtonPrefix + (index + 1).ToString();
        }

        public void HideLevelButton()
        {
            playButton.gameObject.SetActive(false);
        }
    }
}
