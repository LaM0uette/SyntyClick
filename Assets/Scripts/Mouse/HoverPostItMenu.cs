using EPOOutline;
using UnityEngine;

namespace Mouse
{
    public class HoverPostItMenu : MonoBehaviour
    {
        #region Statements

        [SerializeField] private Material _materialHover;
        private Renderer _renderer;
        private Material _materialNormal;
        
        private Outlinable _outlinable;
        
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _materialNormal = _renderer.material;
            
            _outlinable = GetComponent<Outlinable>();
        }

        #endregion

        #region Events

        public void OnMouseEnter()
        {
            _renderer.material = _materialHover;
            _outlinable.enabled = true;
        }
        
        public void OnMouseExit()
        {
            _renderer.material = _materialNormal;
            _outlinable.enabled = false;
        }

        #endregion
    }
}
