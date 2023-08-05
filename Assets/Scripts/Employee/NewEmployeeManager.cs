using UnityEngine;

namespace Employee
{
    public class NewEmployeeManager : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonNewEmployee;
        [SerializeField] private GameObject _employee;
        
        void Start()
        {
            _employee.SetActive(false);
        }
    }
}
