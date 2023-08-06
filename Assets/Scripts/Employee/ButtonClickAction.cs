using PlayerController;
using UnityEngine;

namespace Employee
{
    public class ButtonClickAction : MonoBehaviour
    {
        #region Events

        private void OnMouseDown()
        {
            GeneralInputReader.OnStaticClickAction();
        }

        #endregion
    }
}
