using System;
using Audio;
using PlayerController;
using SaveData;
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
                GeneralInputReader.OnSaticMenu();
                GeneralInputReader.MenuValue = false;
            }
            catch (Exception)
            {
                // ignored
            }
            
            try
            {
                GameManager.Initialize();
            }
            catch (Exception)
            {
                // ignored
            }
            
            MusicManager.instance.MmfClick.PlayFeedbacks();
            
            Time.timeScale = 1f;
            
            SceneManager.LoadScene("DesktopScene");
            _parentMenu.SetActive(false);
        }
    }
}
