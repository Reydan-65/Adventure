namespace CodeBase.Data
{
    [System.Serializable]
    public class HeroStats
    {
        public int Damage;
        public float MaxHitPoints;
        public float MovementSpeed;

        public static HeroStats GetDefaultStats()
        {
            HeroStats stats = new HeroStats();

            stats.Damage = 60;
            stats.MaxHitPoints = 100;
            stats.MovementSpeed = 5;

            return stats;
        }
    }
}
