using SaveData;
using UnityEngine;

namespace Employee
{
    public class NewEmployeeManager : MonoBehaviour
    {
        #region Statements

        private static GameManager _gameManager => GameManager.instance;
        
        [SerializeField] private GameObject _buttonNewEmployee;
        [SerializeField] private GameObject _employee;
        [SerializeField] private int _id;

        private bool _isBought;

        private void Start()
        {
            LoadNewEmployee();
        }

        #endregion

        #region Functions

        private void OnMouseDown()
        {
            CheckCostNewEmployee();
        }

        private void CheckCostNewEmployee()
        {
            if (_gameManager.Money < _gameManager.NewEmployeePrice || _isBought) return;

            BuyNewEmployee();
        }
        
        private void BuyNewEmployee()
        {
            _gameManager.Money -= _gameManager.NewEmployeePrice;
            _buttonNewEmployee.SetActive(false);
            _employee.SetActive(true);
            _isBought = true;

            IncrementNewEmployeePrice();
            Save();
        }

        private static void IncrementNewEmployeePrice()
        {
            _gameManager.NewEmployeePrice *= 2;
            _gameManager.UpdateTextPriceNewEmployee();
        }

        private void Save()
        {
            SaveLoadData.SaveNewEmployeeData(_id, _isBought);
            SaveLoadData.Save();
        }
        
        private void LoadNewEmployee()
        {
            SaveLoadData.Load();
            
            var isBought = SaveLoadData.LoadNewEmployeeData(_id);
            _isBought = isBought;
            
            if (!_isBought) _employee.SetActive(false);
            else _buttonNewEmployee.SetActive(false);
        }

        #endregion
    }
}
