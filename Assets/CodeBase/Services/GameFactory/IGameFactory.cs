using CodeBase.Infrastructure.DependencyInjection;
using UnityEngine;

public interface IGameFactory : IService
{
    GameObject CreateHero(Vector3 position, Quaternion rotation);
    VirtualJoystick CreateJoystick();
    FollowCamera CreateFollowCamera();

    GameObject Hero {  get; }
    VirtualJoystick VirtualJoystick { get; }
    FollowCamera FollowCamera { get; }
}
