using PlayerController;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class LoadGame : MonoBehaviour
    {
        [SerializeField] private GameObject _parentMenu;
        
        private void OnMouseDown()
        {
            SceneManager.LoadScene("DesktopScene");
            _parentMenu.SetActive(false);
            GeneralInputReader.OnStaticMenu();
        }
    }
}
