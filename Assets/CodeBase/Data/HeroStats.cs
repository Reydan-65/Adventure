using UnityEngine;

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

        public void SetDefaultStats()
        {
            Damage = 60;
            MaxHitPoints = 100;
            MovementSpeed = 5;
        }

        public void CopyFrom(HeroStats data)
        {
            Damage = data.Damage;
            MaxHitPoints= data.MaxHitPoints;
            MovementSpeed = data.MovementSpeed;
        }
    }
}
