using System;
using Employee;
using UnityEngine;

namespace Bug.MiniGame
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Statements

        public static Action<EmployeeWorker> BugAction { get; private set; }
        public static Action<EmployeeWorker> BugCorrectedAction { get; set; }
        
        public static EmployeeWorker CurrentEmployeeWorker { get; set; }
        
        [SerializeField] private GameObject[] _miniGameObjects;

        #endregion

        #region Events

        private void OnEnable()
        {
            BugAction += OnBugAction;
        }
        
        private void OnDisable()
        {
            BugAction -= OnBugAction;
        }

        #endregion

        #region Functions

        private void OnBugAction(EmployeeWorker employeeWorker)
        {
            CurrentEmployeeWorker = employeeWorker;
            RandomMiniGame();
        }

        private void RandomMiniGame()
        {
            _miniGameObjects[UnityEngine.Random.Range(0, _miniGameObjects.Length)].SetActive(true);
        }

        #endregion
    }
}
