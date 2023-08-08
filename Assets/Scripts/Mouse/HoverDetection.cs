using Audio;
using EPOOutline;
using UnityEngine;

namespace Mouse
{
    public class HoverDetection : MonoBehaviour
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
            MusicManager.instance.MmfActionHover.PlayFeedbacks();
            _outlinable.enabled = true;
        }
        
        public void OnMouseExit()
        {
            _outlinable.enabled = false;
        }

        #endregion
    }
}
