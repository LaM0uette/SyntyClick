using SaveData;
using UnityEngine;

namespace Menu
{
    public class IntroManager : MonoBehaviour
    {
        #region Statements

        public static IntroManager instance;
        
        [SerializeField] private GameObject[] _introObjects;
        [SerializeField] private GameObject _leftArrow;
        [SerializeField] private GameObject _rightArrow;
        
        public static int _currentIntroObject;
        
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
            
            _leftArrow.SetActive(false);
        }

        private void Start()
        {
            if (GamePreferences.Game == 1)
            {
                gameObject.SetActive(false);
            }
        }

        #endregion

        #region Functions

        public void IncrementIntroObject(int value)
        {
            _currentIntroObject += value;

            if (_currentIntroObject <= 0)
            {
                _leftArrow.SetActive(false);
                _currentIntroObject = 0;
            }
            else
            {
                _leftArrow.SetActive(true);
            }

            if (_currentIntroObject >= _introObjects.Length - 1)
            {
                _rightArrow.SetActive(false);
                _currentIntroObject = _introObjects.Length - 1;
            }
            else
            {
                _rightArrow.SetActive(true);
            }
        }

        public void ShowUi()
        {
            foreach (var introObject in _introObjects)
            {
                introObject.SetActive(false);
            }
            
            _introObjects[_currentIntroObject].SetActive(true);
        }

        #endregion
    }
}
