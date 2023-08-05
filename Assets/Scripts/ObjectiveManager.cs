using SaveData;
using ScriptableOject.Objective;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    #region Statements

    public static ObjectiveManager instance;
    private static GameManager _gameManager => GameManager.instance;
    
    [SerializeField] private Objective[] _objectives;
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
        
        CurrentObjective = _objectives[0];
    }
    
    private void Start()
    {
        GamePreferences.ResetAll();
        SetValueFromPlayerPrefs();
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
        CurrentObjective = _objectives[CurrentObjective.Id];
        
        IncrementFansAndMoney(CurrentObjective.FansGainAmout, CurrentObjective.MoneyGainAmout);
    }
    
    private static void IncrementFansAndMoney(int amountFans, int amountMoney)
    {
        _gameManager.IncrementFans(amountFans);
        _gameManager.IncrementMoney(amountMoney);
    }
    
    public void SetPlayerPrefs()
    {
        GamePreferences.CurrentObjectiveId = CurrentObjective.Id - 1;
    }
    
    private void SetValueFromPlayerPrefs()
    {
        CurrentObjective = _objectives[GamePreferences.CurrentObjectiveId];
    }

    #endregion
}
