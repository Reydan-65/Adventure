using UnityEngine;
using Assets.CodeBase.Infrastructure.ServiceLocator;

namespace CodeBase.Infrastructure.Services
{
    public interface IInputService : IService
    {
        bool Enable { get; set; }
        Vector2 MovementAxis { get; }
    }
}