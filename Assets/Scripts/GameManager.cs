using System;
using Audio;
using SaveData;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Statements

    public static GameManager instance;
    public const float SpeedBoost = 6f;
    
    public static Action DashboardChanged;
    public static Action OnPriceEmployeeChanged;
    
    [NonSerialized] public int TotalAssets;
    [NonSerialized] public int CurrentAssets;
    [NonSerialized] public int Fans;
    [NonSerialized] public int Money;
    [NonSerialized] public int NewEmployeePrice;
    
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
        UpdateDashboard();
    }

    #endregion

    #region Functions

    public void IncrementAssets(int amout)
    {
        TotalAssets += amout;
        CurrentAssets += amout;

        UpdateDashboard();
    }
    
    public void IncrementFans(int amout)
    {
        Fans += amout;
        
        UpdateDashboard();
    }
    
    public void IncrementMoney(int amout)
    {
        Money += amout;
        
        UpdateDashboard();
    }

    public static void UpdateDashboard()
    {
        // TODO: BUG chargement des données sur le whiteboard au second load/new game
        
        try
        {
            DashboardChanged?.Invoke();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private static void UpdatePriceEmployee()
    {
        try
        {
            OnPriceEmployeeChanged?.Invoke();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    #endregion
}
