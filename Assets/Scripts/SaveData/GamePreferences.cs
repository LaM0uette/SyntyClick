using Employee;
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
        
        private const string NewEmployeePriceKey = "NewEmployeePrice";
        public static int NewEmployeePrice
        {
            get => PlayerPrefs.GetInt(NewEmployeePriceKey, 0);
            set => PlayerPrefs.SetInt(NewEmployeePriceKey, value);
        }
        
        private const string NewEmployeeJsonDataKey = "NewEmployeeData";
        public static void SaveNewEmployeeJson(int id, bool isBought)
        {
            var isBoughtString = isBought ? "1" : "0";
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{id}", isBoughtString);
        }
        public static bool GetNewEmployeeJson(int id)
        {
            var newEmployeeData = PlayerPrefs.GetString($"{NewEmployeeJsonDataKey}_{id}", "0");
            return newEmployeeData == "1";
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
            NewEmployeePrice = 5000;
            CurrentObjectiveId = 0;

            ResetNewEmployee();
        }

        private static void ResetNewEmployee()
        {
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-10460}", "0");
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-9664}", "0");
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-8888}", "0");
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-8092}", "0");
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-7322}", "0");
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-6556}", "0");
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-5760}", "0");
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-4954}", "0");
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-4158}", "0");
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-2436}", "0");
            PlayerPrefs.SetString($"{NewEmployeeJsonDataKey}_{-1640}", "0");
        }

        #endregion
    }
}