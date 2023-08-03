using ScriptableOject.Objective;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    #region Statements

    public static ObjectiveManager instance;
    private static GameManager _gameManager => GameManager.instance;
    
    [SerializeField] private Objective[] _objectives;
    [HideInInspector] public Objective CurrentObjectives;

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
        
        CurrentObjectives = _objectives[0];
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
        if (CurrentObjectives.isInfinite) return;
        
        TryIncrementAssets();
    }
    
    private void TryIncrementAssets()
    {
        if (_gameManager.CurrentAssets < CurrentObjectives.AssetCount) return;
        
        _gameManager.CurrentAssets = 0;
        CurrentObjectives = _objectives[CurrentObjectives.Id];
        Debug.Log($"New Objective : {CurrentObjectives.Name}");
    }

    #endregion
}
