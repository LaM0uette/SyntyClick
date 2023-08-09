using Audio;
using PlayerController;
using SaveData;
using UnityEngine;

namespace Menu
{
    public class SaveGame : MonoBehaviour
    {
        [SerializeField] private GameObject _parentMenu;
        
        private void OnMouseDown()
        {
            SaveLoadData.Save();
            
            _parentMenu.SetActive(false);
            Time.timeScale = 1f;
            
            MusicManager.instance.MmfClick.PlayFeedbacks();
            MusicManager.instance.AudioMixerMaster.SetFloat("Volume", GamePreferences.VolumeMusic);
            
            GeneralInputReader.OnSaticMenu();
            GeneralInputReader.MenuValue = false;
        }
    }
}
