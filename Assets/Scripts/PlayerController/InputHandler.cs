using UnityEngine;

namespace PlayerController
{
    public class InputHandler : MonoBehaviour
    {
        #region Statements

        private InputReader _playerInputs;
        [SerializeField] private GameObject _menu;
        
        private void Awake()
        {
            _playerInputs = GetComponent<InputReader>();
        }

        #endregion
        
        #region Events

        private void OnEnable()
        {
            _playerInputs.MenuAction += OnMenuAction;
        }
        
        private void OnDisable()
        {
            _playerInputs.MenuAction -= OnMenuAction;
        }

        #endregion

        #region Functions

        private void OnMenuAction()
        {
            var menuValue = InputReader.MenuValue;
            
            _menu.SetActive(menuValue);
            Time.timeScale = menuValue ? 0 : 1;
        }

        #endregion
    }
}
