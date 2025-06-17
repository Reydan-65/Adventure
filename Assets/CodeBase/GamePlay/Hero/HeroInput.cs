using Assets.CodeBase.Infrastructure.ServiceLocator;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.GamePlay.Hero
{
    public class HeroInput : MonoBehaviour
    {
        [SerializeField] private HeroMovement heroMovement;

        private IInputService inputService;

        private void Start()
        {
            inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            heroMovement.SetMevementDirection(inputService.MovementAxis);
        }
    }
}
