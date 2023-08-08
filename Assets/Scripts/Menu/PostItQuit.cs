using Audio;
using UnityEngine;

namespace Menu
{
    public class PostItQuit : MonoBehaviour
    {
        private void OnMouseDown()
        {
            MusicManager.instance.MmfClick.PlayFeedbacks();
            
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
