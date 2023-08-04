using UnityEngine;

namespace SaveData
{
    public static class GamePreferences
    {
        #region Statements

        private const string TotalAssetsKey = "TotalAssets";
        public static bool HasTotalAssets => PlayerPrefs.HasKey(TotalAssetsKey);
        public static int TotalAssets
        {
            get => PlayerPrefs.GetInt(TotalAssetsKey, 0);
            set => PlayerPrefs.SetInt(TotalAssetsKey, value);
        }
        
        private const string CurrentAssetsKey = "CurrentAssets";
        public static bool HasCurrentAssets => PlayerPrefs.HasKey(CurrentAssetsKey);
        public static int CurrentAssets
        {
            get => PlayerPrefs.GetInt(CurrentAssetsKey, 0);
            set => PlayerPrefs.SetInt(CurrentAssetsKey, value);
        }
        
        private const string FansKey = "Fans";
        public static bool HasFans => PlayerPrefs.HasKey(FansKey);
        public static int Fans
        {
            get => PlayerPrefs.GetInt(FansKey, 0);
            set => PlayerPrefs.SetInt(FansKey, value);
        }
        
        private const string MoneyKey = "Money";
        public static bool HasMoney => PlayerPrefs.HasKey(MoneyKey);
        public static int Money
        {
            get => PlayerPrefs.GetInt(MoneyKey, 0);
            set => PlayerPrefs.SetInt(MoneyKey, value);
        }

        #endregion

        #region Functions

        public static void ResetAll()
        {
            TotalAssets = 0;
            CurrentAssets = 0;
            Fans = 0;
            Money = 0;
        }

        #endregion
    }
}