using CodeBase.Data;
using CodeBase.GamePlay.Enemies;
using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        event UnityAction HeroCreated;

        Task<GameObject> CreateHeroAsync(Vector3 position, Quaternion rotation);
        Task<VirtualJoystick> CreateJoystickAsync();
        Task<FollowCamera> CreateFollowCameraAsync();
        Task<GameObject> CreateEnemyAsync(EnemyID id, Vector3 position);
        Task<GameObject> CreateLootItemFromPrefab(LootItemID id);
        Task WarmUp();

        GameObject HeroObject { get; }
        HeroHealth HeroHealth { get; }
        HeroInventory HeroInventory { get; }

        VirtualJoystick VirtualJoystick { get; }
        FollowCamera FollowCamera { get; }
        List<GameObject> EnemiesObject { get; }
        public GameObject LootObject { get; }

    }
}
