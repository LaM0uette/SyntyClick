using Audio;
using UnityEngine;

namespace Mouse
{
    public class HoverCursor : MonoBehaviour
    {
        #region Events

        public void OnMouseEnter()
        {
            CursorManager.SetHandCursor("clic");
            MusicManager.instance.MmfActionHover.PlayFeedbacks();
        }
        
        public void OnMouseExit()
        {
            CursorManager.ResetCursor();
        }

        #endregion
    }
}
