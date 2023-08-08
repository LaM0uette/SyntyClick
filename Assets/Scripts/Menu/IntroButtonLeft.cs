using UnityEngine;

namespace Menu
{
    public class IntroButtonLeft : MonoBehaviour
    {
        #region Events

        private void OnMouseDown()
        {
            IntroManager.instance.IncrementIntroObject(-1);
            IntroManager.instance.ShowUi();
        }

        #endregion
    }
}
