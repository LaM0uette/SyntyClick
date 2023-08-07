using System.Collections;
using System.Collections.Generic;
using SaveData;
using TMPro;
using UnityEngine;

public class LoadPriceEmployee : MonoBehaviour
{
    private TextMeshProUGUI _tmpPriceNewEmployee;
    
    private void Start()
    {
        _tmpPriceNewEmployee = GetComponent<TextMeshProUGUI>();
    }
    
    private void Update()
    {
        _tmpPriceNewEmployee.text = GamePreferences.NewEmployeePrice.ToString();
    }
}
