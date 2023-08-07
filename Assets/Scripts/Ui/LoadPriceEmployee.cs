using System;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class LoadPriceEmployee : MonoBehaviour
    {
        private TextMeshProUGUI _tmpPriceNewEmployee;
        public static Action OnPriceEmployeeChanged;
        
        private void OnEnable()
        {
            OnPriceEmployeeChanged += UpdatePriceEmployee;
        }
        
        private void OnDisable()
        {
            OnPriceEmployeeChanged -= UpdatePriceEmployee;
        }
    
        private void Start()
        {
            _tmpPriceNewEmployee = GetComponent<TextMeshProUGUI>();
            UpdatePriceEmployee();
        }
    
        private void UpdatePriceEmployee()
        {
            var newEmployeePrice = GameManager.instance.NewEmployeePrice.ToString();
            _tmpPriceNewEmployee.text = newEmployeePrice;
        }
    }
}
