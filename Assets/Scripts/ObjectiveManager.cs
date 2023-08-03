using ScriptableOject.Objective;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    #region Statements

    public static ObjectiveManager instance;
    private static GameManager _gameManager => GameManager.instance;
    
    [SerializeField] private Objective[] _objectives;
    private Objective _currentObjectives;

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
        
        _currentObjectives = _objectives[0];
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
        if (_currentObjectives.isInfinite) return;
        
        TryIncrementAssets();
    }
    
    private void TryIncrementAssets()
    {
        if (_gameManager.CurrentAssets < _currentObjectives.AssetCount) return;
        
        _gameManager.CurrentAssets = 0;
        _currentObjectives = _objectives[_currentObjectives.Id];
        Debug.Log($"New Objective : {_currentObjectives.Name}");
    }

    #endregion
}
