using CodeBase.Data;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using UnityEngine;

namespace CodeBase.GamePlay.Hero
{
    public class HeroHealth : Health, IProgressLoadHandler
    {
        [SerializeField] private float restoredHealthAmount;
        [SerializeField] private float restoreDelay;

        private float restoreTimer;

        private void Update()
        {
            restoreTimer += Time.deltaTime;

            if (restoreTimer >= restoredHealthAmount)
            {
                RestoreHealth(restoredHealthAmount);
                restoreTimer = 0;
            }
        }

        private void RestoreHealth(float value)
        {
            if (current == max || value == 0) return;

            current += value;

            if (current > max)
            {
                current = max;
            }

            InvokeChangedEvent();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            current = progress.HeroStats.MaxHitPoints;
            max = progress.HeroStats.MaxHitPoints;
        }
    }
}
