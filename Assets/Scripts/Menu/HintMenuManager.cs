using Bug.MiniGame;
using PlayerController;
using UnityEngine;

namespace Menu
{
    public class HintMenuManager : MonoBehaviour
    {
        #region Statements
        
        [SerializeField] private GameObject[] _hintObjects;
        [SerializeField] private GameObject _leftArrow;
        [SerializeField] private GameObject _rightArrow;
        
        public int _currentHintObject;

        #endregion

        #region Events

        private void OnEnable()
        {
            GeneralInputReader.ExitAction += ExitAction;
            MiniGameManager.IsOnHint = true;
            
            SetArrowButton();
            
            _currentHintObject = 0;
            ShowUi();
        }

        private void OnDisable()
        {
            GeneralInputReader.ExitAction -= ExitAction;
            MiniGameManager.IsOnHint = false;
        }

        #endregion

        #region Functions

        private void SetArrowButton()
        {
            if (_hintObjects.Length <= 1)
            {
                _leftArrow.SetActive(false);
                _rightArrow.SetActive(false);
            }
            else
            {
                _leftArrow.SetActive(false);
                _rightArrow.SetActive(true);
            }
        }

        public void IncrementIntroObject(int value)
        {
            _currentHintObject += value;

            if (_currentHintObject <= 0)
            {
                _leftArrow.SetActive(false);
                _currentHintObject = 0;
            }
            else
            {
                _leftArrow.SetActive(true);
            }

            if (_currentHintObject >= _hintObjects.Length - 1)
            {
                _rightArrow.SetActive(false);
                _currentHintObject = _hintObjects.Length - 1;
            }
            else
            {
                _rightArrow.SetActive(true);
            }
        }

        public void ShowUi()
        {
            foreach (var introObject in _hintObjects)
            {
                introObject.SetActive(false);
            }
            
            _hintObjects[_currentHintObject].SetActive(true);
        }

        private void ExitAction()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
