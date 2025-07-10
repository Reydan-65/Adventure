using CodeBase.Infrastructure.DependencyInjection;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public interface IInputService : IService
    {
        bool Enable { get; set; }
        Vector2 MovementAxis { get; }
    }
}