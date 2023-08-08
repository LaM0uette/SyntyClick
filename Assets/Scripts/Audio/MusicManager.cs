using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        #region Statements

        public static MusicManager instance;
        
        public List<AudioClip> tracks = new();
    
        private AudioSource audioSource;
        private int currentTrackIndex;
        
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
        
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            StartCoroutine(PlayTracksInLoop());
        }

        #endregion

        #region Functions

        private IEnumerator PlayTracksInLoop()
        {
            while (true)
            {
                audioSource.clip = tracks[currentTrackIndex];
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length);

                currentTrackIndex = (currentTrackIndex + 1) % tracks.Count;
            }
        }

        #endregion
    }
}
