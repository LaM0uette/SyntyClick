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
        [SerializeField] private GameObject _startPostIt;
        
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
            _startPostIt.SetActive(false);
        }

        private void Start()
        {
            if (GamePreferences.Intro == 1)
            {
                gameObject.SetActive(false);
            }
            else
            {
                GamePreferences.Intro = 1;
            }
            
            // TODO Supprimer cette ligne
            GamePreferences.Intro = 0;
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
                _startPostIt.SetActive(true);
                _currentIntroObject = _introObjects.Length - 1;
            }
            else
            {
                _rightArrow.SetActive(true);
                _startPostIt.SetActive(false);
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
