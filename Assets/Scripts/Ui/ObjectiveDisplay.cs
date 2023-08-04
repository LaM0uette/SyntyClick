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
        
        private static GameManager _gameManager => GameManager.instance;
        private static ObjectiveManager _objectiveManager => ObjectiveManager.instance;
        
        private void FixedUpdate()
        {
            ImgObjectives.sprite = _objectiveManager.CurrentObjective.Image;
            TmpObjectivesName.text = _objectiveManager.CurrentObjective.Name;
            
            var total = _objectiveManager.CurrentObjective.isInfinite ? _gameManager.CurrentAssets.ToString() : $"{_gameManager.CurrentAssets} / {_objectiveManager.CurrentObjective.AssetCount}";
            TmpObjectivesTotal.text = $"Assets : {total}";
        }
    }
}
