using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.GamePlay.Hero
{
    public class HeroInput : MonoBehaviour
    {
        [SerializeField] private HeroMovement heroMovement;

        private IInputService inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            this.inputService = inputService;
        }

        private void Update()
        {
            heroMovement.SetMevementDirection(inputService.MovementAxis);
        }
    }
}
