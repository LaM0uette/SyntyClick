using SaveData;
using UnityEngine;

namespace Menu
{
    public class SaveGame : MonoBehaviour
    {
        private void OnMouseDown()
        {
            SaveLoadData.Save();
        }
    }
}
