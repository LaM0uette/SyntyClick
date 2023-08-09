using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

namespace MMF
{
    public class MmfDashboard : MonoBehaviour
    {
        #region Statements

        public static MmfDashboard instance;
        
        public MMF_Player MmfTotalAssetInc;
        public MMF_Player MmfFansInc;
        public MMF_Player MmfFansDesc;
        public MMF_Player MmfMoneyInc;
        public MMF_Player MmfMoneyDesc;
        
        public TextMeshProUGUI TotalAssetsTextInc;
        public TextMeshProUGUI FansTextInc;
        public TextMeshProUGUI FansTextDesc;
        public TextMeshProUGUI MoneyTextInc;
        public TextMeshProUGUI MoneyTextDesc;
        
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

            //DontDestroyOnLoad(gameObject);
        }

        #endregion
    }
}
