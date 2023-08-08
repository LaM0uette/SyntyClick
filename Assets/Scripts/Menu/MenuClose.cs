using System;
using Audio;
using PlayerController;
using UnityEngine;

namespace Menu
{
    public class MenuClose : MonoBehaviour
    {
        [SerializeField] private GameObject _parentMenu;
        
        private void OnMouseDown()
        {
            _parentMenu.SetActive(false);
            Time.timeScale = 1f;
            
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
        }
    }
}
