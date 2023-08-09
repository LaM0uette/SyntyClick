using Audio;
using MoreMountains.Feedbacks;
using PlayerController;
using UnityEngine;

namespace Employee
{
    public class ButtonClickAction : MonoBehaviour
    {
        [SerializeField] private MMF_Player _mmfPlayer;
        
        #region Events

        private void OnMouseDown()
        {
            _mmfPlayer.PlayFeedbacks();
            MusicManager.instance.MmfPopAction.PlayFeedbacks();
            GeneralInputReader.OnStaticClickAction();
        }

        #endregion
    }
}
