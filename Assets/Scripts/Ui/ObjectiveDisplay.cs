using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class ObjectiveDisplay : MonoBehaviour
    {
        public Image ImgObjectives;
        public TextMeshProUGUI TmpObjectivesName;
        public TextMeshProUGUI TmpObjectivesTotal;
        [SerializeField] private Image _progressBar;
        
        private static GameManager _gameManager => GameManager.instance;
        private static ObjectiveManager _objectiveManager => ObjectiveManager.instance;
        
        private void Update()
        {
            try
            {
                ImgObjectives.sprite = _objectiveManager.CurrentObjective.Image;
                TmpObjectivesName.text = _objectiveManager.CurrentObjective.Name;
            
                var total = _objectiveManager.CurrentObjective.isInfinite ? _gameManager.CurrentAssets.ToString("N0") : $"{_gameManager.CurrentAssets:N0} / {_objectiveManager.CurrentObjective.AssetCount:N0}";
                TmpObjectivesTotal.text = $"Assets : {total}";
                
                _progressBar.fillAmount = _objectiveManager.CurrentObjective.isInfinite ? 1 : (float) _gameManager.CurrentAssets / _objectiveManager.CurrentObjective.AssetCount;
            }
            catch (Exception e)
            {
                // ignored
                Debug.LogError(e);
            }
        }
    }
}
