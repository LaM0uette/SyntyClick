using SaveData;
using ScriptableOject.Objective;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    #region Statements

    public static ObjectiveManager instance;
    private static GameManager _gameManager => GameManager.instance;
    
    public Objective[] Objectives;
    [HideInInspector] public Objective CurrentObjective;

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

        DontDestroyOnLoad(gameObject);
        
        CurrentObjective = Objectives[0];
    }
    
    private void Start()
    {
        SaveLoadData.Load();
    }

    #endregion

    #region Events

    private void Update()
    {
        CheckObjectiveAvancement();
    }

    #endregion

    #region Functions

    private void CheckObjectiveAvancement()
    {
        if (CurrentObjective.isInfinite) return;
        
        TryIncrementAssets();
    }
    
    private void TryIncrementAssets()
    {
        if (_gameManager.CurrentAssets < CurrentObjective.AssetCount) return;
        
        _gameManager.CurrentAssets = 0;
        CurrentObjective = Objectives[CurrentObjective.Id];
        
        IncrementFansAndMoney(CurrentObjective.FansGainAmout, CurrentObjective.MoneyGainAmout);
    }
    
    private static void IncrementFansAndMoney(int amountFans, int amountMoney)
    {
        _gameManager.IncrementFans(amountFans);
        _gameManager.IncrementMoney(amountMoney);
    }

    #endregion
}
