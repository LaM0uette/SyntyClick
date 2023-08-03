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
        public Action MouseRightClickAction { get; set; }
        public Action<GameObject> OnClickGameObject { get; set; }

        private void OnMousePosition(InputValue value)
        {
            MousePositionValue = value.Get<Vector2>();
        }
        
        private void OnClickAction() => ClickAction?.Invoke();

        private void OnMouseLeftClick()
        {
            if (Camera.main is not null)
            {
                var ray = Camera.main.ScreenPointToRay(MousePositionValue);
                if (Physics.Raycast(ray, out var hit))
                {
                    OnClickGameObject?.Invoke(hit.collider.gameObject);
                }
            }

            MouseLeftClickAction?.Invoke();
        }
        private void OnMouseRightClick() => MouseRightClickAction?.Invoke();
    }
}
