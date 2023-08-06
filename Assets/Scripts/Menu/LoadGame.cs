using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class LoadGame : MonoBehaviour
    {
        [SerializeField] private GameObject _parentMenu;
        
        private void OnMouseDown()
        {
            _parentMenu.SetActive(false);
            Time.timeScale = 1f;
            
            SceneManager.LoadScene("DesktopScene");
        }
    }
}
