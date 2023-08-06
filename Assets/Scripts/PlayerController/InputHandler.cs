using UnityEngine;

namespace PlayerController
{
    public class InputHandler : MonoBehaviour
    {
        #region Statements

        [SerializeField] private GameObject _menu;

        #endregion
        
        #region Events

        private void OnEnable()
        {
            GeneralInputReader.MenuAction += OnMenuAction;
        }
        
        private void OnDisable()
        {
            GeneralInputReader.MenuAction -= OnMenuAction;
        }

        #endregion

        #region Functions

        private void OnMenuAction()
        {
            var menuValue = GeneralInputReader.MenuValue;
            
            _menu.SetActive(menuValue);
            Time.timeScale = menuValue ? 0 : 1;
        }

        #endregion
    }
}
