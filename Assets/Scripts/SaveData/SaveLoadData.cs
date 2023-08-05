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
            SaveJsonData();
        }
        
        private static void SaveVariables()
        {
            GamePreferences.TotalAssets = _gameManager.TotalAssets;
            GamePreferences.CurrentAssets = _gameManager.CurrentAssets;
            GamePreferences.Fans = _gameManager.Fans;
            GamePreferences.Money = _gameManager.Money;
            GamePreferences.NewEmployeePrice = _gameManager.NewEmployeePrice;
            GamePreferences.CurrentObjectiveId = _objectiveManager.CurrentObjective.Id - 1;
        }

        private static void SaveJsonData()
        {
        }
        
        public static void Load()
        {
            GamePreferences.ResetAll(); //TODO: Remove this line
            
            LoadVariables();
            LoadJsonData();
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
        
        private static void LoadJsonData()
        {
        }

        #endregion
    }
}
