using System.Collections.Generic;
using SaveData;
using TMPro;
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
        
        [SerializeField] private TMP_Dropdown _qualityDropdown;

        private void Start()
        {
            _sliderMaster.value = GamePreferences.VolumeMusic;
            _sliderSfx.value = GamePreferences.VolumeSfx;

            SetQualities();
            SetInitialQuality();
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

        #region Functions

        private void SetQualities()
        {
            var qualities = QualitySettings.names;
            _qualityDropdown.ClearOptions();
            _qualityDropdown.AddOptions(new List<string>(qualities));
        }

        private void SetInitialQuality()
        {
            var qualityIndex = GamePreferences.QualityIndex;
            _qualityDropdown.value = qualityIndex == 0 ? QualitySettings.GetQualityLevel() : qualityIndex;
        }
        
        public void SetQualityIndex(int index)
        {
            QualitySettings.SetQualityLevel(index);
            GamePreferences.QualityIndex = index;
        }

        #endregion
    }
}
