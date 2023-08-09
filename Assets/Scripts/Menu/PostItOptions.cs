using Audio;
using UnityEngine;

namespace Menu
{
    public class PostItOptions : MonoBehaviour
    {
        #region Statements

        [SerializeField] private GameObject _menuOption;

        #endregion

        #region Events

        private void OnMouseDown()
        {
            MusicManager.instance.MmfClick.PlayFeedbacks();
            _menuOption.SetActive(true);
        }

        #endregion
    }
}
