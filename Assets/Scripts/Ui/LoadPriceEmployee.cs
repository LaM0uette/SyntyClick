using TMPro;
using UnityEngine;

namespace Ui
{
    public class LoadPriceEmployee : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmpPriceNewEmployee;
        
        private void OnEnable()
        {
            GameManager.OnPriceEmployeeChanged += UpdatePriceEmployee;
        }
        
        private void OnDisable()
        {
            GameManager.OnPriceEmployeeChanged -= UpdatePriceEmployee;
        }
    
        private void Start()
        {
            UpdatePriceEmployee();
        }
    
        private void UpdatePriceEmployee()
        {
            var newEmployeePrice = GameManager.instance.NewEmployeePrice.ToString("N0");
            _tmpPriceNewEmployee.text = newEmployeePrice;
        }
    }
}
