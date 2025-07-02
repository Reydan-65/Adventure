using CodeBase.GamePlay.Enemies;
using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.DependencyInjection;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 position, Quaternion rotation);
        VirtualJoystick CreateJoystick();
        FollowCamera CreateFollowCamera();
        GameObject CreateEnemy(EnemyID id, Vector3 position);

        GameObject HeroObject { get; }
        HeroHealth HeroHealth { get; }
        HeroInventory HeroInventory { get; }

        VirtualJoystick VirtualJoystick { get; }
        FollowCamera FollowCamera { get; }
        List<GameObject> EnemiesObject { get; }
    }
}
