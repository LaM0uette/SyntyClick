using SaveData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Menu
{
    public class HintMenuManager : MonoBehaviour
    {
        #region Statements

        public static HintMenuManager instance;
        
        [SerializeField] private GameObject[] _hintObjects;
        [SerializeField] private GameObject _leftArrow;
        [SerializeField] private GameObject _rightArrow;
        
        public static int _currentHintObject;
        
        private void Awake()
        {
            if (instance is null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            SetArrowButton();
            
            _currentHintObject = 0;
            ShowUi();
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

        #endregion
    }
}
