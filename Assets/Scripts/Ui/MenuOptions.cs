using System;
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
            _audioMixerMaster.GetFloat("Volume", out var volumeMaster);
            _audioMixerSfx.GetFloat("Volume", out var volumeSfx);
            
            _sliderMaster.value = volumeMaster;
            _sliderSfx.value = volumeSfx;
        }

        #endregion
        
        #region Events
        
        public void SetVolumeMaster(float volume)
        {
            _audioMixerMaster.SetFloat("Volume", volume);
        }
        
        public void SetVolumeSfx(float volume)
        {
            _audioMixerSfx.SetFloat("Volume", volume);
        }
        
        #endregion
    }
}
