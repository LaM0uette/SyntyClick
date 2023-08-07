using System;
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
            
            _parentMenu.SetActive(false);
            Time.timeScale = 1f;

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
                GameManager.instance.Initialize();
            }
            catch (Exception)
            {
                // ignored
            }
            
            SceneManager.LoadScene("DesktopScene");
        }
    }
}
