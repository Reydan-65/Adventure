using CodeBase.GamePlay.Enemies;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [Header("Prefab")]
        public AssetReferenceGameObject PrefabReference;

        [Header("Identificator")]
        public EnemyID EnemyID;

        [Header("Max Health")]
        public float MaxHitPoints;

        [Header("Movement")]
        public float MovementSpeed;
        public float StopDistance;

        [Header("Offensive")]
        public float AttackCooldown;
        public float AttackRadius;
        public float Damage;

        [Header("Defensive")]
        public string todo;
    }
}
