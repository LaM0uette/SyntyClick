using System;
using MMF;
using MoreMountains.Feedbacks;
using SaveData;
using TMPro;
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
    
    [SerializeField] private MMF_Player _mmfCurrentAssetsInc;
    [SerializeField] private TextMeshProUGUI _totalCurrentAssetsInc;
    
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
        
        if (amout <= 0) return;

        try
        {
            MmfDashboard.instance.TotalAssetsTextInc.text = $"+{amout:N0}";
            MmfDashboard.instance.MmfTotalAssetInc.PlayFeedbacks();
        
            _totalCurrentAssetsInc.text = $"+{amout:N0}";
            _mmfCurrentAssetsInc.PlayFeedbacks();
        }
        catch (Exception)
        {
            // ignored
        }
    }
    
    public void IncrementFans(int amout)
    {
        Fans += amout;
        
        UpdateDashboard();

        try
        {
            switch (amout)
            {
                case 0:
                    return;
                case > 0:
                    MmfDashboard.instance.FansTextInc.text = $"+{amout:N0}";
                    MmfDashboard.instance.MmfFansInc.PlayFeedbacks();
                    break;
                default:
                    MmfDashboard.instance.FansTextDesc.text = $"{amout:N0}";
                    MmfDashboard.instance.MmfFansDesc.PlayFeedbacks();
                    break;
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
    
    public void IncrementMoney(int amout)
    {
        Money += amout;
        
        UpdateDashboard();

        try
        {
            switch (amout)
            {
                case 0:
                    return;
                case > 0:
                    MmfDashboard.instance.MoneyTextInc.text = $"+{amout:N0}";
                    MmfDashboard.instance.MmfMoneyInc.PlayFeedbacks();
                    break;
                default:
                    MmfDashboard.instance.MoneyTextDesc.text = $"{amout:N0}";
                    MmfDashboard.instance.MmfMoneyDesc.PlayFeedbacks();
                    break;
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public static void UpdateDashboard()
    {
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
