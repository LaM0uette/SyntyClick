using TMPro;
using UnityEngine;

namespace Employee
{
    public class NewEmployeeManager : MonoBehaviour
    {
        #region Statements

        private static GameManager _gameManager => GameManager.instance;
        
        private static int _newEmployeePrice = 5000;
        [SerializeField] private GameObject _buttonNewEmployee;
        [SerializeField] private GameObject _employee;

        private void Start()
        {
            _employee.SetActive(false);
        }

        #endregion

        #region Functions

        private void OnMouseDown()
        {
            CheckCostNewEmployee();
        }

        private void CheckCostNewEmployee()
        {
            if (_gameManager.Money < _newEmployeePrice) return;

            BuyNewEmployee();
        }
        
        private void BuyNewEmployee()
        {
            _gameManager.Money -= _newEmployeePrice;
            _buttonNewEmployee.SetActive(false);
            _employee.SetActive(true);

            IncrementNewEmployeePrice();
        }

        private static void IncrementNewEmployeePrice()
        {
            _newEmployeePrice += _newEmployeePrice;
            _gameManager.UpdateTextPriceNewEmployee(_newEmployeePrice.ToString());
        }

        #endregion
    }
}
