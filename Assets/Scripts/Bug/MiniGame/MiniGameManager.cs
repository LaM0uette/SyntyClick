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
            //_miniGameObjects[1].SetActive(true);
            _miniGameObjects[UnityEngine.Random.Range(0, _miniGameObjects.Length)].SetActive(true);
        }
        
        public static void AddFansAndMoney()
        {
            var _gameManager = GameManager.instance;
            var amoutFansGain = (int)(_gameManager.Fans * 0.01f);
            _gameManager.IncrementFans(amoutFansGain < 1 ? 1 : amoutFansGain);
        }

        #endregion
    }
}
