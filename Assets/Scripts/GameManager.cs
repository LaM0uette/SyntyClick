using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Statements

    public static GameManager instance;
    public const float SpeedNormal = 1f;
    public const float SpeedBoost = 6f;
    
    private const float TimeToFan = 8f;
    private const int MoneyFan = 50;
    
    public int TotalAssets;
    public int CurrentAssets;
    public int Fans;
    public int Money;
    
    private float _fanInProgress;

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
    }

    #endregion

    #region Functions

    public void IncrementAssets()
    {
        TotalAssets++;
        CurrentAssets++;
    }

    #endregion
}
