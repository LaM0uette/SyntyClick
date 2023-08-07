using System;
using SaveData;
using TMPro;
using Ui;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Statements

    public static GameManager instance;
    public const float SpeedBoost = 6f;
    
    [NonSerialized] public int TotalAssets;
    [NonSerialized] public int CurrentAssets;
    [NonSerialized] public int Fans;
    [NonSerialized] public int Money;
    [NonSerialized] public int NewEmployeePrice;
    
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
        Initialize();
    }
    
    public static void Initialize()
    {
        SaveLoadData.Load();
        UpdatePriceEmployee();
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

    private static void UpdatePriceEmployee()
    {
        LoadPriceEmployee.OnPriceEmployeeChanged?.Invoke();
    }

    #endregion
}
