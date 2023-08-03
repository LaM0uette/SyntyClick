using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int IncrementDelay = 1;
    public const float SpeedNormal = 1f;
    public const float SpeedBoost = 6f;
    
    public int TotalAssets;
    public int CurrentAssets;
    public int Fans;
    public int Money;

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

    public void IncrementAssets()
    {
        TotalAssets++;
        CurrentAssets++;
    }
}
