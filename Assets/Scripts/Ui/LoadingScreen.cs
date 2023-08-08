using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui
{
    public class LoadingScreen : MonoBehaviour
    {
        public static LoadingScreen instance;
        public GameObject Screen;
        public Image LoadingBar;
        
        private void Awake()
        {
            if (instance is null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void LaodScene(int sceneId)
        {
            StartCoroutine(LoadSceneAsync(sceneId));
        }

        private IEnumerator LoadSceneAsync(int sceneId)
        {
            var operation = SceneManager.LoadSceneAsync(sceneId);
            Screen.SetActive(true);
            
            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / .9f);
                LoadingBar.fillAmount = progress;
                yield return null;
            }
            
            Screen.SetActive(false);
        }
    }
}
