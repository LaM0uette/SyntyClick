using TMPro;
using UnityEngine;

namespace Ui
{
    public class ScoreDisplay : MonoBehaviour
    {
        private static GameManager _gameManager => GameManager.instance;
        
        public TextMeshProUGUI TmpTotalAssets;
        public TextMeshProUGUI TmpCurrentAssets;
        public TextMeshProUGUI TmpFans;
        public TextMeshProUGUI TmpMoney;

        private void Update()
        {
            TmpTotalAssets.text = _gameManager.TotalAssets.ToString();
            TmpCurrentAssets.text = _gameManager.CurrentAssets.ToString();
            TmpFans.text = _gameManager.Fans.ToString();
            TmpMoney.text = _gameManager.Money.ToString();
        }
    }
}
