using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class BackToMenu : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MenuScene");
            
            MusicManager.instance.MmfClick.PlayFeedbacks();
        }
    }
}
