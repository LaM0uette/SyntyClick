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
            _outlinable.enabled = true;
        }
        
        public void OnMouseExit()
        {
            _outlinable.enabled = false;
        }

        #endregion
    }
}
