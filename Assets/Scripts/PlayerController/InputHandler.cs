using System;
using Audio;
using SaveData;
using UnityEngine;

namespace PlayerController
{
    public class InputHandler : MonoBehaviour
    {
        #region Statements

        [SerializeField] private GameObject _menu;
        [SerializeField] private GameObject _menuOption;
        
        private void Start()
        {
            try
            {
                GameManager.Initialize();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #endregion
        
        #region Events

        private void OnEnable()
        {
            GeneralInputReader.MenuAction += OnMenuAction;
        }
        
        private void OnDisable()
        {
            GeneralInputReader.MenuAction -= OnMenuAction;
        }

        #endregion

        #region Functions

        private void OnMenuAction()
        {
            var menuValue = GeneralInputReader.MenuValue;
            _menu.SetActive(menuValue);
            _menuOption.SetActive(false);

            if (menuValue)
            {
                MusicManager.instance.AudioMixerMaster.SetFloat("Volume", -80);
                Time.timeScale = 0;
            }
            else
            {
                MusicManager.instance.AudioMixerMaster.SetFloat("Volume", GamePreferences.VolumeMusic);
                Time.timeScale = 1;
            }
        }

        #endregion
    }
}
