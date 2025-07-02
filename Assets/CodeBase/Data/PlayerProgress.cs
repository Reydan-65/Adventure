using CodeBase.GamePlay.Hero;

namespace CodeBase.Data
{
    [System.Serializable]
    public class PlayerProgress
    {
        public HeroStats HeroStats;
        public HeroInventoryData HeroInventoryData;
        public int CurrentLevelIndex;

        public static PlayerProgress GetDefaultProgress()
        {
            PlayerProgress progress = new PlayerProgress();

            progress.CurrentLevelIndex = 0;
            progress.HeroStats = HeroStats.GetDefaultStats();
            progress.HeroInventoryData = HeroInventoryData.Default();

            return progress;
        }
    }
}
