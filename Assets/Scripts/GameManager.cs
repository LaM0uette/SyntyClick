using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Statements

    public static GameManager instance;
    public const float SpeedNormal = 1f;
    public const float SpeedBoost = 6f;
    
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

    public void IncrementAssets(int amout)
    {
        TotalAssets += amout;
        CurrentAssets += amout;
    }
    
    public void IncrementFans(int amout)
    {
        Fans += amout;
    }
    
    public void IncrementMoney(int amout)
    {
        Money += amout;
    }

    #endregion
}
