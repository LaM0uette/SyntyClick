using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController
{
    public class InputReader : MonoBehaviour
    {
        #region Statements

        public Vector2 MousePositionValue { get; private set; }
        
        public static Action ClickAction { get; set; }
        public static Action PdgClickAction { get; set; }
        
        public Action MouseLeftClickAction { get; set; }
        public Action MouseRightClickAction { get; set; }
        
        public static bool MenuValue { get; set; }
        public Action MenuAction { get; set; }
        
        public Action<GameObject> ClickGameObject { get; set; }

        #endregion

        #region Events

        private void OnMousePosition(InputValue value)
        {
            MousePositionValue = value.Get<Vector2>();
        }
        
        public static void OnClickAction() => ClickAction?.Invoke();
        public static void OnPdgClickAction() => PdgClickAction?.Invoke();

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

        private void OnMenu()
        {
            MenuValue = !MenuValue;
            MenuAction?.Invoke();
        }

        #endregion

        #region Functions

        private void OnClickGameObject()
        {
            if (Camera.main is null) return;
            
            var ray = Camera.main.ScreenPointToRay(MousePositionValue);
            if (Physics.Raycast(ray, out var hit))
            {
                ClickGameObject?.Invoke(hit.collider.gameObject);
            }
        }

        #endregion

        #region Dev
        
        public Action DevEarnMoneyAction { get; set; }
        private void OnDevEarnMoney() => DevEarnMoneyAction?.Invoke();

        #endregion
    }
}
