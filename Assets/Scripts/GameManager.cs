using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int TotalAssets;
    public int ActualAssets;
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
        ActualAssets++;
    }
}
