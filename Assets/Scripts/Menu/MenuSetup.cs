using System;
using SaveData;
using UnityEngine;

namespace Menu
{
    public class MenuSetup : MonoBehaviour
    {
        [SerializeField] private GameObject _loadMenuPostIt;

        private void Start()
        {
            try
            {
                _loadMenuPostIt.SetActive(GamePreferences.Game == 1);
            }
            catch (Exception)
            {
                // ignored
            }
            
            if (GamePreferences.Game != 1)
            {
                GamePreferences.NewEmployeePrice = 1000;
            }
        }
    }
}
