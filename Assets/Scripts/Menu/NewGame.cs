using SaveData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class NewGame : MonoBehaviour
    {
        private void OnMouseDown()
        {
            GamePreferences.ResetAll();
            SceneManager.LoadScene("DesktopScene");
        }
    }
}
