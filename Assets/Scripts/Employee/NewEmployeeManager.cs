using SaveData;
using TMPro;
using UnityEngine;

namespace Employee
{
    public class NewEmployeeManager : MonoBehaviour
    {
        #region Statements

        private static GameManager _gameManager => GameManager.instance;
        
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
            if (_gameManager.Money < _gameManager.NewEmployeePrice) return;

            BuyNewEmployee();
        }
        
        private void BuyNewEmployee()
        {
            _gameManager.Money -= _gameManager.NewEmployeePrice;
            _buttonNewEmployee.SetActive(false);
            _employee.SetActive(true);

            IncrementNewEmployeePrice();
            SaveLoadData.Save();
        }

        private static void IncrementNewEmployeePrice()
        {
            _gameManager.NewEmployeePrice *= 2;
            _gameManager.UpdateTextPriceNewEmployee();
        }

        #endregion
    }
}
