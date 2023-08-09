using Audio;
using SaveData;
using Ui;
using UnityEngine;

namespace Menu
{
    public class BackToMenu : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Time.timeScale = 1f;
            MusicManager.instance.MmfClick.PlayFeedbacks();
            MusicManager.instance.AudioMixerMaster.SetFloat("Volume", GamePreferences.VolumeMusic);
            
            LoadingScreen.instance.LaodScene(0);
            
        }
    }
}
