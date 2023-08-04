using UnityEngine;

namespace SaveData
{
    public static class GamePreferences
    {
        #region Statements

        private const string TotalAssetsKey = "TotalAssets";
        public static int TotalAssets
        {
            get => PlayerPrefs.GetInt(TotalAssetsKey, 0);
            set => PlayerPrefs.SetInt(TotalAssetsKey, value);
        }
        
        private const string CurrentAssetsKey = "CurrentAssets";
        public static int CurrentAssets
        {
            get => PlayerPrefs.GetInt(CurrentAssetsKey, 0);
            set => PlayerPrefs.SetInt(CurrentAssetsKey, value);
        }
        
        private const string FansKey = "Fans";
        public static int Fans
        {
            get => PlayerPrefs.GetInt(FansKey, 0);
            set => PlayerPrefs.SetInt(FansKey, value);
        }
        
        private const string MoneyKey = "Money";
        public static int Money
        {
            get => PlayerPrefs.GetInt(MoneyKey, 0);
            set => PlayerPrefs.SetInt(MoneyKey, value);
        }
        
        private const string CurrentObjectiveIdKey = "CurrentObjectiveId";
        public static int CurrentObjectiveId
        {
            get => PlayerPrefs.GetInt(CurrentObjectiveIdKey, 0);
            set => PlayerPrefs.SetInt(CurrentObjectiveIdKey, value);
        }

        #endregion

        #region Functions

        public static void ResetAll()
        {
            TotalAssets = 0;
            CurrentAssets = 0;
            Fans = 0;
            Money = 0;
            CurrentObjectiveId = 0;
        }

        #endregion
    }
}