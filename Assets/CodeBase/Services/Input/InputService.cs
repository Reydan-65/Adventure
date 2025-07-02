using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class InputService : IInputService
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        private bool enable = true;

        private Vector2 GetMovementAxis()
        {
            if (enable == false) return Vector2.zero;

            if (VirtualJoystick.Value != Vector2.zero)
                return VirtualJoystick.Value;

            return new Vector2(Input.GetAxis(HorizontalAxisName), Input.GetAxis(VerticalAxisName));
        }

        public Vector2 MovementAxis
        {
            get
            {
                return GetMovementAxis();
            }
        }

        public bool Enable { get => enable; set => enable = value; }
    }
}
