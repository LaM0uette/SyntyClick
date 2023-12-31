using Employee;
using UnityEngine;

namespace SaveData
{
    public static class SaveLoadData
    {
        #region Statements

        private static GameManager _gameManager => GameManager.instance;
        private static ObjectiveManager _objectiveManager => ObjectiveManager.instance;

        #endregion

        #region Functions

        public static void Save()
        {
            SaveVariables();
        }
        
        private static void SaveVariables()
        {
            GamePreferences.Game = 1;
            GamePreferences.TotalAssets = _gameManager.TotalAssets;
            GamePreferences.CurrentAssets = _gameManager.CurrentAssets;
            GamePreferences.Fans = _gameManager.Fans;
            GamePreferences.Money = _gameManager.Money;
            GamePreferences.NewEmployeePrice = _gameManager.NewEmployeePrice;
            GamePreferences.CurrentObjectiveId = _objectiveManager.CurrentObjective.Id - 1;
        }

        public static void SaveNewEmployeeData(int id, bool isBought)
        {
            GamePreferences.SaveNewEmployeeData(id, isBought);
        }
        
        public static void SaveEmployeeWorker(int id, int level)
        {
            GamePreferences.SaveEmployeeWorker(id, level);
        }
        
        public static void SaveCurrentAssetsOnWorkedKey(int id, int currentAssetsOnWorkedKey)
        {
            GamePreferences.SaveCurrentAssetsOnWorked(id, currentAssetsOnWorkedKey);
        }
        
        public static void SaveCurrentIsBug(int id, bool isBug)
        {
            GamePreferences.SaveCurrentIsBug(id, isBug);
        }
        
        public static void Load()
        {
            LoadVariables();
        }
        
        private static void LoadVariables()
        {
            _gameManager.TotalAssets = GamePreferences.TotalAssets;
            _gameManager.CurrentAssets = GamePreferences.CurrentAssets;
            _gameManager.Fans = GamePreferences.Fans;
            _gameManager.Money = GamePreferences.Money;
            _gameManager.NewEmployeePrice = GamePreferences.NewEmployeePrice;
            _objectiveManager.CurrentObjective = _objectiveManager.Objectives[GamePreferences.CurrentObjectiveId];
        }
        
        public static bool LoadNewEmployeeData(int id)
        {
            return GamePreferences.GetNewEmployeeData(id);
        }
        
        public static int LoadEmployeeWorker(int id)
        {
            return GamePreferences.GetEmployeeWorker(id);
        }
        
        public static int LoadCurrentAssetsOnWorkedKey(int id)
        {
            return GamePreferences.GetCurrentAssetsOnWorked(id);
        }
        
        public static bool LoadCurrentIsBug(int id)
        {
            return GamePreferences.GetCurrentIsBug(id);
        }

        #endregion
    }
}
