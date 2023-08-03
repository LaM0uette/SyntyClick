using TMPro;
using UnityEngine;

namespace Ui
{
    public class ScoreDisplay : MonoBehaviour
    {
        public TextMeshProUGUI TmpTotalAssets;
        public TextMeshProUGUI TmpCurrentAssets;
        public TextMeshProUGUI TmpFans;
        public TextMeshProUGUI TmpMoney;

        private void LateUpdate()
        {
            var gameManager = GameManager.instance;
            TmpTotalAssets.text = gameManager.TotalAssets.ToString();
            TmpCurrentAssets.text = gameManager.CurrentAssets.ToString();
            TmpFans.text = gameManager.Fans.ToString();
            TmpMoney.text = gameManager.Money.ToString();
        }
    }
}
