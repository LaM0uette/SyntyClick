using System;
using Audio;
using PlayerController;
using SaveData;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class NewGame : MonoBehaviour
    {
        [SerializeField] private GameObject _parentMenu;
        
        private void OnMouseDown()
        {
            GamePreferences.ResetAll();
            GamePreferences.NewEmployeePrice = 1000;
            
            try
            {
                GameManager.Initialize();
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                GeneralInputReader.OnSaticMenu();
                GeneralInputReader.MenuValue = false;
            }
            catch (Exception)
            {
                // ignored
            }
            
            MusicManager.instance.MmfClick.PlayFeedbacks();
            MusicManager.instance.AudioMixerMaster.SetFloat("Volume", GamePreferences.VolumeMusic);
            
            Time.timeScale = 1f;
            
            LoadingScreen.instance.LaodScene(1);
            _parentMenu.SetActive(false);
        }
    }
}
