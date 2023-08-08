using UnityEngine;

namespace Menu
{
    public class PostItActiveGo : MonoBehaviour
    {
        #region Statements

        [SerializeField] private GameObject _gameObject;

        #endregion

        #region Events

        private void OnMouseDown()
        {
            _gameObject.SetActive(true);
        }

        #endregion
    }
}
