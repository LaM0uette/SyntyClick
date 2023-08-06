using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class LoadGame : MonoBehaviour
    {
        private void OnMouseDown()
        {
            SceneManager.LoadScene("DesktopScene");
        }
    }
}
