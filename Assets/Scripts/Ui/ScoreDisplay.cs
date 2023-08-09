using TMPro;
using UnityEngine;

namespace Ui
{
    public class ScoreDisplay : MonoBehaviour
    {
        #region Statements

        private static GameManager _gameManager => GameManager.instance;
        
        public TextMeshProUGUI TmpTotalAssets;
        public TextMeshProUGUI TmpFans;
        public TextMeshProUGUI TmpMoney;

        #endregion

        #region Events

        private void OnEnable()
        {
            TmpTotalAssets.text = "0 Assets";
            TmpFans.text = "0 Fans";
            TmpMoney.text = "0$";
            
            GameManager.DashboardChanged += UpdateDashboard;
        }
        
        private void OnDisable()
        {
            GameManager.DashboardChanged -= UpdateDashboard;
        }

        #endregion

        #region Functions

        private void UpdateDashboard()
        {
            TmpTotalAssets.text = $"{_gameManager.TotalAssets:N0} Assets";
            TmpFans.text = $"{_gameManager.Fans:N0} Fans";
            TmpMoney.text = $"{_gameManager.Money:N0}$";
        }

        #endregion
    }
}
