using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController
{
    public class InputReader : MonoBehaviour
    {
        public Vector2 MousePositionValue { get; private set; }
        
        public Action ClickAction { get; set; }
        public Action MouseLeftClickAction { get; set; }

        private void OnMousePosition(InputValue value)
        {
            MousePositionValue = value.Get<Vector2>();
        }
        
        private void OnClickAction() => ClickAction?.Invoke();
        private void OnMouseLeftClick() => MouseLeftClickAction?.Invoke();
    }
}
