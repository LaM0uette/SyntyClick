using System;
using SaveData;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Ui
{
    public class MenuOptions : MonoBehaviour
    {
        #region Statements

        [SerializeField] private Slider _sliderMaster;
        [SerializeField] private Slider _sliderSfx;
        [SerializeField] private AudioMixer _audioMixerMaster;
        [SerializeField] private AudioMixer _audioMixerSfx;

        private void Start()
        {
            _sliderMaster.value = GamePreferences.VolumeMusic;
            _sliderSfx.value = GamePreferences.VolumeSfx;
        }

        #endregion
        
        #region Events
        
        public void SetVolumeMaster(float volume)
        {
            _audioMixerMaster.SetFloat("Volume", volume);
            GamePreferences.VolumeMusic = volume;
        }
        
        public void SetVolumeSfx(float volume)
        {
            _audioMixerSfx.SetFloat("Volume", volume);
            GamePreferences.VolumeSfx = volume;
        }
        
        #endregion
    }
}
