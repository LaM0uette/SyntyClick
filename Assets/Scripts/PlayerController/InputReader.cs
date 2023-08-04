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
        public Action<GameObject> ClickGameObject { get; set; }

        private void OnMousePosition(InputValue value)
        {
            MousePositionValue = value.Get<Vector2>();
        }
        
        private void OnClickAction() => ClickAction?.Invoke();

        private void OnMouseLeftClick()
        {
            OnClickGameObject();
            MouseLeftClickAction?.Invoke();
        }
        private void OnMouseRightClick()
        {
            OnClickGameObject();
            MouseRightClickAction?.Invoke();
        }
        private void OnClickGameObject()
        {
            if (Camera.main is null) return;
            
            var ray = Camera.main.ScreenPointToRay(MousePositionValue);
            if (Physics.Raycast(ray, out var hit))
            {
                ClickGameObject?.Invoke(hit.collider.gameObject);
            }
        }
    }
}
