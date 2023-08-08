using SaveData;
using UnityEngine;

namespace Menu
{
    public class MenuSetup : MonoBehaviour
    {
        [SerializeField] private GameObject _loadMenuPostIt;
        
        private void Start()
        {
            _loadMenuPostIt.SetActive(GamePreferences.Game == 1);
            
            if (GamePreferences.Game != 1)
            {
                GamePreferences.NewEmployeePrice = 1000;
            }
        }
    }
}
