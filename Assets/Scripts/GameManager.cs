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
    [HideInInspector] public int NewEmployeePrice = 5000;
    
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
        SaveLoadData.Load();
        UpdateTextPriceNewEmployee();
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

    public void UpdateTextPriceNewEmployee()
    {
        if (NewEmployeePrice <= 0) NewEmployeePrice = 5000;
        
        foreach (var tmpPriceNewEmployee in _tmpPriceNewEmployee)
        {
            tmpPriceNewEmployee.text = NewEmployeePrice.ToString();
        }
    }

    #endregion
}
