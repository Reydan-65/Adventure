using CodeBase.GamePlay.Hero;
using CodeBase.Infrastructure.DependencyInjection;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 position, Quaternion rotation);
        VirtualJoystick CreateJoystick();
        FollowCamera CreateFollowCamera();

        GameObject HeroObject { get; }
        HeroHealth HeroHealth { get; }
        VirtualJoystick VirtualJoystick { get; }
        FollowCamera FollowCamera { get; }
    }
}
