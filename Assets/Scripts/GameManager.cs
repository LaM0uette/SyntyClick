using SaveData;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Statements

    public static GameManager instance;
    public const float SpeedBoost = 6f;
    
    [HideInInspector] public int TotalAssets;
    [HideInInspector] public int CurrentAssets;
    [HideInInspector] public int Fans;
    [HideInInspector] public int Money;
    
    [SerializeField] private TextMeshProUGUI[] _tmpPriceNewEmployee;
    
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

    private void Start()
    {
        GamePreferences.ResetAll(); //TODO: Remove this line
        SetValueFromPlayerPrefs();
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
    
    public void SetPlayerPrefs()
    {
        GamePreferences.TotalAssets = TotalAssets;
        GamePreferences.CurrentAssets = CurrentAssets;
        GamePreferences.Fans = Fans;
        GamePreferences.Money = Money;
    }
    
    private void SetValueFromPlayerPrefs()
    {
        TotalAssets = GamePreferences.TotalAssets;
        CurrentAssets = GamePreferences.CurrentAssets;
        Fans = GamePreferences.Fans;
        Money = GamePreferences.Money;
    }

    public void UpdateTextPriceNewEmployee(string newPrice)
    {
        foreach (var tmpPriceNewEmployee in _tmpPriceNewEmployee)
        {
            tmpPriceNewEmployee.text = newPrice;
        }
    }

    #endregion
}
