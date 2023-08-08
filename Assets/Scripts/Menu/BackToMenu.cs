using Audio;
using Ui;
using UnityEngine;

namespace Menu
{
    public class BackToMenu : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Time.timeScale = 1f;
            //SceneManager.LoadScene("MenuScene");
            LoadingScreen.instance.LaodScene(0);
            
            MusicManager.instance.MmfClick.PlayFeedbacks();
        }
    }
}
