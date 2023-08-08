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
        
        private const string CurrentObjectiveIdKey = "CurrentObjectiveId";
        public static int CurrentObjectiveId
        {
            get => PlayerPrefs.GetInt(CurrentObjectiveIdKey, 0);
            set => PlayerPrefs.SetInt(CurrentObjectiveIdKey, value);
        }
        
        private const string NewEmployeeDataKey = "NewEmployeeData";
        public static void SaveNewEmployeeData(int id, bool isBought)
        {
            var isBoughtString = isBought ? "1" : "0";
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{id}", isBoughtString);
        }
        public static bool GetNewEmployeeData(int id)
        {
            var newEmployeeData = PlayerPrefs.GetString($"{NewEmployeeDataKey}_{id}", "0");
            return newEmployeeData == "1";
        }
        
        private const string EmployeeWorkerKey = "EmployeeWorker";
        public static void SaveEmployeeWorker(int id, int level)
        {
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{id}", (level - 1).ToString());
        }
        public static int GetEmployeeWorker(int id)
        {
            var employeeWorker = PlayerPrefs.GetString($"{EmployeeWorkerKey}_{id}", "0");
            return employeeWorker == "0" ? 0 : int.Parse(employeeWorker);
        }

        private const string CurrentAssetsOnWorkedKey = "CurrentAssetsOnWorked";
        public static void SaveCurrentAssetsOnWorked(int id, int currentAssetsOnWorked)
        {
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{id}", currentAssetsOnWorked.ToString());
        }
        public static int GetCurrentAssetsOnWorked(int id)
        {
            var currentAssetsOnWorked = PlayerPrefs.GetString($"{CurrentAssetsOnWorkedKey}_{id}", "0");
            return int.Parse(currentAssetsOnWorked);
        }
        
        private const string CurrentIsBugKey = "CurrentIsBug";
        public static void SaveCurrentIsBug(int id, bool isBug)
        {
            var isBugString = isBug ? "1" : "0";
            PlayerPrefs.SetString($"{CurrentIsBugKey}_{id}", isBugString);
        }
        public static bool GetCurrentIsBug(int id)
        {
            var currentIsBug = PlayerPrefs.GetString($"{CurrentIsBugKey}_{id}", "0");
            return currentIsBug == "1";
        }
        
        private const string VolumeMusicKey = "VolumeMusic";
        public static float VolumeMusic
        {
            get => PlayerPrefs.GetFloat(VolumeMusicKey, 0);
            set => PlayerPrefs.SetFloat(VolumeMusicKey, value);
        }
        
        private const string VolumeSfxKey = "VolumeSfx";
        public static float VolumeSfx
        {
            get => PlayerPrefs.GetFloat(VolumeSfxKey, 0);
            set => PlayerPrefs.SetFloat(VolumeSfxKey, value);
        }

        #endregion

        #region Functions

        public static void ResetAll()
        {
            TotalAssets = 0;
            CurrentAssets = 0;
            Fans = 0;
            Money = 0;
            NewEmployeePrice = 1000;
            CurrentObjectiveId = 0;

            ResetNewEmployee();
            ResetEmployeeWorker();
        }

        private static void ResetNewEmployee()
        {
            for (var i = 0; i <= 11; i++)
            {
                PlayerPrefs.DeleteKey($"{NewEmployeeDataKey}_{i}");
            }
        }
        
        private static void ResetEmployeeWorker()
        {
            for (var i = 0; i <= 12; i++)
            {
                PlayerPrefs.DeleteKey($"{EmployeeWorkerKey}_{i}");
                PlayerPrefs.DeleteKey($"{CurrentAssetsOnWorkedKey}_{i}");
                PlayerPrefs.DeleteKey($"{CurrentIsBugKey}_{i}");
            }
        }

        #endregion
    }
}