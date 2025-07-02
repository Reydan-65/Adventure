using CodeBase.Data;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using UnityEngine;

namespace CodeBase.GamePlay.Hero
{
    public class HeroMovement : MonoBehaviour, IProgressLoadHandler
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform viewTransform;
        [SerializeField] private float movementSpeed;

        private Vector3 directionControl;
        public Vector3 DirectionControl => directionControl;

        private void Update()
        {
            if (directionControl.magnitude > 0)
            {
                characterController.Move(directionControl * movementSpeed * Time.deltaTime);
                viewTransform.rotation = Quaternion.LookRotation(directionControl);
            }
            else
            {
                characterController.Move(Vector3.zero);
            }
        }

        public void SetMevementDirection(Vector2 moveDirection)
        {
            directionControl.x = moveDirection.x;
            directionControl.z = moveDirection.y;

            directionControl.Normalize();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            movementSpeed = progress.HeroStats.MovementSpeed;
        }
    }
}
