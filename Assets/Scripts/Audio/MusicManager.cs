using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        #region Statements

        public MMF_Player MmfButtonHover;
        public MMF_Player MmfActionHover;
        public MMF_Player MmfClick;
        public MMF_Player MmfPop;
        public MMF_Player MmfPopAction;
        public MMF_Player MmfValidation;
        public MMF_Player MmfError;
        public MMF_Player MmfBug;
        public MMF_Player MmfCash;
        public MMF_Player MmfComplete;

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
            
            //_audioMixerMaster.SetFloat("Volume", -10f);
            //_audioMixerSfx.SetFloat("Volume", -20f);
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
