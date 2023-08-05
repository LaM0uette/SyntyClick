using ScriptableOject.EmployeeLevel;
using UnityEngine;

namespace Employee
{
    public class LevelButtonManager : MonoBehaviour
    {
        #region Statements
        
        [SerializeField] private GameObject[] _prefabButtonsLvl;
        
        #endregion

        #region Functions

        public void SetButtonLvl(EmployeeLevel employeeLevel)
        {
            DisableAllButton();
            
            for (var i = 0; i < employeeLevel.Level; i++)
            {
                _prefabButtonsLvl[i].SetActive(true);
            }
        }
        
        private void DisableAllButton()
        {
            foreach (var button in _prefabButtonsLvl)
            {
                button.SetActive(false);
            }
        }

        #endregion
    }
}
