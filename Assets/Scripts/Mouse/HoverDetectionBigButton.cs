using Audio;
using EPOOutline;
using UnityEngine;

namespace Mouse
{
    public class HoverDetectionBigButton : MonoBehaviour
    {
        #region Statements

        private Outlinable _outlinable;
        
        private void Awake()
        {
            _outlinable = GetComponent<Outlinable>();
        }

        #endregion

        #region Events

        public void OnMouseEnter()
        {
            CursorManager.SetHandCursor("hand");
            MusicManager.instance.MmfActionHover.PlayFeedbacks();
            _outlinable.enabled = true;
        }
        
        public void OnMouseExit()
        {
            CursorManager.ResetCursor();
            _outlinable.enabled = false;
        }

        #endregion
    }
}
