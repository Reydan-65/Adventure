using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GamePlay.UI
{
    public class UIHealthText : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private TextMeshProUGUI text;

        private void Start ()
        {
            health.Changed += OnHitPointsChanged;

            OnHitPointsChanged();
        }

        private void OnDestroy()
        {
            health.Changed -= OnHitPointsChanged;
        }

        private void OnHitPointsChanged()
        {
            text.text = ((int) health.Current).ToString();
        }
    }
}
