using SaveData;
using UnityEngine;

namespace Menu
{
    public class MenuSetup : MonoBehaviour
    {
        [SerializeField] private GameObject _loadMenuPostIt;
        
        private void Start()
        {
            Debug.Log("Game: " + GamePreferences.Game);
            _loadMenuPostIt.SetActive(GamePreferences.Game == 1);
        }
    }
}
