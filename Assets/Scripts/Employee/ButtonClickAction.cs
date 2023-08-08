using Audio;
using PlayerController;
using UnityEngine;

namespace Employee
{
    public class ButtonClickAction : MonoBehaviour
    {
        #region Events

        private void OnMouseDown()
        {
            MusicManager.instance.MmfPopAction.PlayFeedbacks();
            GeneralInputReader.OnStaticClickAction();
        }

        #endregion
    }
}
