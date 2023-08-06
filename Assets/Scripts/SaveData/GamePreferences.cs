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
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-10460}", "0");
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-9664}", "0");
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-8888}", "0");
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-8092}", "0");
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-7322}", "0");
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-6556}", "0");
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-5760}", "0");
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-4954}", "0");
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-4158}", "0");
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-2436}", "0");
            PlayerPrefs.SetString($"{NewEmployeeDataKey}_{-1640}", "0");
        }
        
        private static void ResetEmployeeWorker()
        {
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-10522}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-10522}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-9726}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-9726}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-8950}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-8950}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-8154}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-8154}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-7384}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-7384}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-6618}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-6618}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-5822}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-5822}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-5016}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-5016}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-4220}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-4220}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-3288}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-3288}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-2498}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-2498}", "0");
            PlayerPrefs.SetString($"{EmployeeWorkerKey}_{-1702}", "0");
            PlayerPrefs.SetString($"{CurrentAssetsOnWorkedKey}_{-1702}", "0");
        }

        #endregion
    }
}