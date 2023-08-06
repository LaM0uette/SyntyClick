using UnityEngine;

namespace Menu
{
    public class PostItQuit : MonoBehaviour
    {
        private void OnMouseDown()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
