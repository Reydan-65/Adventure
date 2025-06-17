using UnityEngine;
using System.Collections;
using Assets.CodeBase.Infrastructure.ServiceLocator;

namespace CodeBase.Infrastructure.Services
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
