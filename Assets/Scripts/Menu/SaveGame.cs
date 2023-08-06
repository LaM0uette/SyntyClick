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
            GeneralInputReader.OnStaticMenu();
        }
    }
}
