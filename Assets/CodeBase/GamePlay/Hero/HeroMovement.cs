using CodeBase.Data;
using CodeBase.Infrastructure.Services.PlayerProgressSaver;
using UnityEngine;

namespace CodeBase.GamePlay.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMovement : MonoBehaviour, IProgressLoadHandler
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform viewTransform;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float gravity = 30f;
        [SerializeField] private float groundCheckDistance = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        private Vector3 directionControl;
        private Vector3 verticalVelocity;
        private bool isGrounded;
        public Vector3 DirectionControl => directionControl;

        private void Update()
        {
            CheckGround();
            ApplyGravity();
            MoveCharacter();
            RotateView();
        }

        private void CheckGround()
        {
            isGrounded = characterController.isGrounded;

            if (Physics.Raycast(
                transform.position + Vector3.up * 0.1f,
                Vector3.down,
                out RaycastHit hit,
                groundCheckDistance,
                groundLayer))
            {
                isGrounded = true;
                
                if (hit.distance < groundCheckDistance * 0.9f)
                {
                    characterController.Move(Vector3.down * (groundCheckDistance - hit.distance));
                }
            }
        }

        private void ApplyGravity()
        {
            if (isGrounded) verticalVelocity.y = -2f;
            else verticalVelocity.y -= gravity * Time.deltaTime;
        }

        private void MoveCharacter()
        {
            Vector3 moveDirection = directionControl * movementSpeed;
            moveDirection.y = verticalVelocity.y;

            characterController.Move(moveDirection * Time.deltaTime);
        }

        private void RotateView()
        {
            if (directionControl.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionControl);
                viewTransform.rotation = Quaternion.Slerp(
                    viewTransform.rotation,
                    targetRotation,
                    10f * Time.deltaTime
                );
            }
        }

        public void SetMovementDirection(Vector2 moveDirection)
        {
            directionControl.x = moveDirection.x;
            directionControl.z = moveDirection.y;
            directionControl.y = 0;

            directionControl.Normalize();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            movementSpeed = progress.HeroStats.MovementSpeed;
        }
    }
}