using PlayerController;
using SaveData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class NewGame : MonoBehaviour
    {
        private static GameManager _gameManager => GameManager.instance;
        private static ObjectiveManager _objectiveManager => ObjectiveManager.instance;
        
        [SerializeField] private GameObject _parentMenu;
        
        private void OnMouseDown()
        {
            GamePreferences.ResetAll();
            
            SceneManager.LoadScene("DesktopScene");
            _parentMenu.SetActive(false);
            GeneralInputReader.OnStaticMenu();

            _gameManager.NewEmployeePrice = 1000;
            GamePreferences.NewEmployeePrice = 1000;
            
            _gameManager.Initialize();
            _objectiveManager.Initialize();
        }
    }
}
